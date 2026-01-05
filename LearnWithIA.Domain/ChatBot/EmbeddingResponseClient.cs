namespace LearnWithIA.Domain.ChatBot;

public class EmbeddingResponseClient
{
    public EmbeddingDataClient[] Data { get; set; } = [];
}

public class EmbeddingDataClient
{
    public float[] Embedding { get; set; } = [];
}