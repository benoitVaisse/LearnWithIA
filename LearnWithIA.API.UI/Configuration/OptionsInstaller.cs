using Shared.Mistral;

namespace LearnWithIA.API.UI.Configuration;

public static class OptionsInstaller
{
    public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MistralAIOptions>(configuration.GetSection(MistralAIOptions.Position));
        return services;
    }
}