using LearnWithIA.Domain.ChatBot;
using Microsoft.Extensions.Options;
using Shared.Mistral;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace LearnWithIA.Infrastucture.Data.ChatBot;

public class MistralEmbeddingsAdapter(
        IHttpClientFactory httpClientFactory,
        IOptions<MistralAIOptions> mistralAiOptions
    ) : IMistralEmbeddingsAdapter
{
    public async Task<float[]> Get(string text)
    {
        MistralAIOptions options = mistralAiOptions.Value;
        HttpClient client = httpClientFactory.CreateClient(MistralAIOptions.Position);
        var request = new
        {
            model = options.Embeddings!.Model,
            input = new[] { text }
        };
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(options.Embeddings!.Url, content);
        response.EnsureSuccessStatusCode();

        EmbeddingResponseClient responseJson = await response.Content.ReadFromJsonAsync<EmbeddingResponseClient>() ?? new();
        return responseJson.Data[0].Embedding;
    }
}