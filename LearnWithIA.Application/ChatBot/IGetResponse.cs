namespace LearnWithIA.Application.ChatBot;

public interface IGetResponse : IUseCase
{
    Task<string?> Handle(string request);
}
