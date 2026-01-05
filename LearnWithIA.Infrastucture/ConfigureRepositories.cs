using LearnWithIA.Domain;
using LearnWithIA.Domain.ChatBot;
using LearnWithIA.Infrastucture.Data.ChatBot;
using Microsoft.Extensions.DependencyInjection;

namespace LearnWithIA.Infrastucture;

public static class ConfigureRepositories
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        var repos = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
            .Where(t => !t.IsAbstract
                            && !t.IsInterface
            &&
                t.GetInterfaces().Any(
                    i => i.IsInterface && i.Name == nameof(IRepositoryBase).ToString()
                )
            );

        foreach (Type classType in repos)
        {
            services.AddScoped(classType.GetInterfaces()[0], classType);
        }

        services.AddAdapters();
        return services;
    }

    public static IServiceCollection AddAdapters(this IServiceCollection services)
    {
        services.AddScoped<IMistralEmbeddingsAdapter, MistralEmbeddingsAdapter>();
        return services;
    }
}
