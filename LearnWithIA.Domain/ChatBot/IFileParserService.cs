namespace LearnWithIA.Domain.ChatBot;

public interface IFileParserService
{
    IEnumerable<string> ParseToChunks(Stream fileStream, string fileName);
}
