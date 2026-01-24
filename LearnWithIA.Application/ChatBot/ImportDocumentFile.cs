using LearnWithIA.Domain.ChatBot;

namespace LearnWithIA.Application.ChatBot;

public class ImportDocumentFile(
    IFileParserService fileParserService,
    IMistralEmbeddingsAdapter mistralEmbeddingsAdapter,
    IEmbeddedRepository embeddedRepository
    ) : IImportDocumentFile
{
    public async Task<ImportDocumentFileResult> Handle(Stream fileStream, string fileName)
    {
        var chunks = fileParserService.ParseToChunks(fileStream, fileName).ToList();

        if (chunks.Count == 0)
        {
            return new ImportDocumentFileResult(0, 0);
        }

        int importedCount = 0;

        foreach (var chunk in chunks)
        {
            float[] vectors = await mistralEmbeddingsAdapter.Get(chunk);

            if (vectors.Length == 0)
            {
                continue;
            }

            await embeddedRepository.CreateEmbedded(chunk, vectors);
            importedCount++;
        }

        return new ImportDocumentFileResult(importedCount, chunks.Count);
    }
}
