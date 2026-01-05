namespace LearnWithIA.Domain.ChatBot;

public interface IMistralEmbeddingsAdapter
{
    Task<float[]> Get(string text);
}

