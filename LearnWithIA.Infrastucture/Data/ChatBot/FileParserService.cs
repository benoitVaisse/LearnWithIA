using System.Text;
using System.Text.RegularExpressions;
using LearnWithIA.Domain.ChatBot;

namespace LearnWithIA.Infrastucture.Data.ChatBot;

public class FileParserService : IFileParserService
{
    private const int MinChunkLength = 50;
    private const int MaxChunkLength = 1000;

    public IEnumerable<string> ParseToChunks(Stream fileStream, string fileName)
    {
        string extension = Path.GetExtension(fileName).ToLowerInvariant();
        string content = ReadFileContent(fileStream, extension);

        if (string.IsNullOrWhiteSpace(content))
        {
            return Enumerable.Empty<string>();
        }

        return SplitIntoChunks(content);
    }

    private static string ReadFileContent(Stream fileStream, string extension)
    {
        using var reader = new StreamReader(fileStream, Encoding.UTF8);
        return reader.ReadToEnd();
    }

    private static IEnumerable<string> SplitIntoChunks(string content)
    {
        var chunks = new List<string>();

        var paragraphs = Regex.Split(content, @"(?:\r?\n){2,}");

        foreach (var paragraph in paragraphs)
        {
            string trimmedParagraph = paragraph.Trim();

            if (string.IsNullOrWhiteSpace(trimmedParagraph))
            {
                continue;
            }

            if (trimmedParagraph.Length <= MaxChunkLength)
            {
                if (trimmedParagraph.Length >= MinChunkLength)
                {
                    chunks.Add(trimmedParagraph);
                }
            }
            else
            {
                var sentences = SplitIntoSentences(trimmedParagraph);
                chunks.AddRange(CombineSentencesIntoChunks(sentences));
            }
        }

        return chunks;
    }

    private static IEnumerable<string> SplitIntoSentences(string text)
    {
        var sentencePattern = @"(?<=[.!?])\s+";
        var sentences = Regex.Split(text, sentencePattern);
        return sentences.Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim());
    }

    private static IEnumerable<string> CombineSentencesIntoChunks(IEnumerable<string> sentences)
    {
        var chunks = new List<string>();
        var currentChunk = new StringBuilder();

        foreach (var sentence in sentences)
        {
            if (currentChunk.Length + sentence.Length + 1 <= MaxChunkLength)
            {
                if (currentChunk.Length > 0)
                {
                    currentChunk.Append(' ');
                }
                currentChunk.Append(sentence);
            }
            else
            {
                if (currentChunk.Length >= MinChunkLength)
                {
                    chunks.Add(currentChunk.ToString());
                }
                currentChunk.Clear();
                currentChunk.Append(sentence);
            }
        }

        if (currentChunk.Length >= MinChunkLength)
        {
            chunks.Add(currentChunk.ToString());
        }

        return chunks;
    }
}
