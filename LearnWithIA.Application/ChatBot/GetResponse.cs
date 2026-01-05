using LearnWithIA.Domain.ChatBot;

namespace LearnWithIA.Application.ChatBot;

public class GetResponse(
        IMistralEmbeddingsAdapter mistralEmbeddingsAdapter,
        IEmbeddedRepository embeddedRepository
    ) : IGetResponse
{
    public async Task<string?> Handle(string request)
    {
        float[] vectors = await mistralEmbeddingsAdapter.Get(request);
        if (vectors.Count() == 0)
        {
            return "on ne sia spas ";
        }
        string? response = await embeddedRepository.SearchSimilarAsync(request, vectors);

        return response;

    }
}