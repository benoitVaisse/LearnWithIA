using Shared.Mistral;
using System.Net.Http.Headers;

namespace LearnIAEmbeddings.Configuration;

public static class HttpClientInstaller
{
    public static IServiceCollection AddCustomHttpClient(this IServiceCollection services, IConfiguration configuration)
    {
        MistralAIOptions mistralOptions = new MistralAIOptions();
        configuration.GetSection(MistralAIOptions.Position).Bind(mistralOptions);

        #region Mistral
        if (mistralOptions == null || string.IsNullOrEmpty(mistralOptions.BaseUrl))
        {
            throw new Exception("Mistal config required");
        }
        UriBuilder mistralUri = new(mistralOptions.BaseUrl);
        services.AddHttpClient(
            MistralAIOptions.Position,
            client =>
            {
                client.BaseAddress = mistralUri.Uri;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", mistralOptions.ApiKey);
            }
        );
        #endregion
        return services;
    }
}
