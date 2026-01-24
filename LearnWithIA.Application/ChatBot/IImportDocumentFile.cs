namespace LearnWithIA.Application.ChatBot;

public interface IImportDocumentFile : IUseCase
{
    Task<ImportDocumentFileResult> Handle(Stream fileStream, string fileName);
}

public record ImportDocumentFileResult(int ChunksImported, int TotalChunks);
