namespace LearnWithIA.Domain.ChatBot;

public interface IEmbeddedRepository : IRepositoryBase
{
    Task CreateEmbedded(string text, float[] vector);

    Task<string?> SearchSimilarAsync(string queryText, float[] queryVector);
}

