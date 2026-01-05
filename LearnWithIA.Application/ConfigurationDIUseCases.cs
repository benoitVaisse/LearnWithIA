using Microsoft.Extensions.DependencyInjection;

namespace LearnWithIA.Application;

public static class ConfigurationDIUseCases
{
    public static IServiceCollection AddUseCase(this IServiceCollection services)
    {
        var useCases = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
            .Where(t => !t.IsAbstract
                            && !t.IsInterface
                            &&
                            (
                                t.GetInterfaces().Any(
                                    i => i.IsInterface && i.Name == nameof(IUseCase).ToString()
                            )
                        )
            );

        foreach (Type classType in useCases)
        {
            services.AddScoped(classType.GetInterfaces()[0], classType);
        }
        return services;
    }
}
