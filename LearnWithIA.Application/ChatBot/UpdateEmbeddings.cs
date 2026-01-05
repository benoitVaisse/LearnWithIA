using LearnWithIA.Domain.ChatBot;

namespace LearnWithIA.Application.ChatBot;

public class UpdateEmbeddings(
    IMistralEmbeddingsAdapter mistralEmbeddingsAdapter,
    IEmbeddedRepository embeddedRepository
    ) : IUpdateEmbeddings
{
    public async Task Handle(string request)
    {
        float[] vectors = await mistralEmbeddingsAdapter.Get(request);
        if (vectors.Count() == 0)
        {
            return;
        }

        var result = embeddedRepository.CreateEmbedded(request, vectors);

        return;
    }
}
