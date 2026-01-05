using LearnWithIA.Application.ChatBot;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnWithIA.API.UI.Controllers;

[ApiController]
[Route("chat-bot")]
[AllowAnonymous]
public class ChatBotController : ControllerBase
{

    [HttpPost("set")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> PostRequest(
        [FromBody] ChatBotRequest chatBotRequest,
        [FromServices] IUpdateEmbeddings updateEmbeddings)
    {
        await updateEmbeddings.Handle(chatBotRequest.Request);
        return Ok();
    }

    [HttpPost("get")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRequest(
        [FromBody] ChatBotRequest chatBotRequest,
        [FromServices] IGetResponse getResponse)
    {
        string? response = await getResponse.Handle(chatBotRequest.Request);
        return Ok(response ?? "^pas de reponse");
    }

}
