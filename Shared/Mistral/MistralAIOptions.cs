namespace Shared.Mistral;

public class MistralAIOptions
{
    public const string Position = "Mistral";
    public string? BaseUrl { get; set; }
    public string? ApiKey { get; set; }
    public ConfigApi? Embeddings { get; set; }
    public ConfigApi? Completions { get; set; }
}

public class ConfigApi
{
    public string? Url { get; set; }
    public string? Model { get; set; }
}
