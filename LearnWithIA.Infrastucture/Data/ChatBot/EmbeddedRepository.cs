using LearnWithIA.Domain.ChatBot;
using Qdrant.Client.Grpc;

namespace LearnWithIA.Infrastucture.Data.ChatBot;

public class EmbeddedRepository(QDRandDatabase qDRandDatabase) : IEmbeddedRepository
{
    public async Task CreateEmbedded(string text, float[] vector)
    {
        await qDRandDatabase.Client.UpsertAsync(QDRandDatabase.CollectionName, new[]
        {
            new PointStruct
            {
                Id = Guid.NewGuid(),
                Vectors = vector,
                Payload = {
                    ["text"] = text,
                  }
            }
        });
    }

    public async Task<string?> SearchSimilarAsync(string queryText, float[] queryVector)
    {
        var result = await qDRandDatabase.Client.SearchAsync(QDRandDatabase.CollectionName, queryVector, limit: 1);
        return result.FirstOrDefault()?.Payload["text"].StringValue;
    }
}
