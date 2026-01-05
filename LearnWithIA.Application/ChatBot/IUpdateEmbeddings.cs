namespace LearnWithIA.Application.ChatBot;

public interface IUpdateEmbeddings : IUseCase
{
    Task Handle(string request);
}