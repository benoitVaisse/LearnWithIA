using Qdrant.Client;
using Qdrant.Client.Grpc;

namespace LearnWithIA.Infrastucture.Data;

public class QDRandDatabase
{
    public readonly QdrantClient Client;
    public const string CollectionName = "chat_bot";
    public QDRandDatabase(string host = "localhost", int port = 6334)
    {
        var channel = QdrantChannel.ForAddress("http://localhost:6334", new ClientConfiguration
        {
        });
        QdrantGrpcClient grpcClient = new(channel);
        Client = new QdrantClient(grpcClient);
    }

    public async Task EnsureCollectionExistsAsync()
    {
        bool exist = await Client!.CollectionExistsAsync(CollectionName);
        if (!exist)
        {
            await Client!.CreateCollectionAsync(CollectionName, new VectorParams
            {
                Size = 1024, // Taille des embeddings Mistral
                Distance = Distance.Cosine
            });
        }
    }
}
