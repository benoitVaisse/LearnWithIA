using System.ComponentModel.DataAnnotations;

namespace LearnWithIA.Application.ChatBot;

public class ChatBotRequest
{
    [Required]
    public required string Request { get; set; }
}

