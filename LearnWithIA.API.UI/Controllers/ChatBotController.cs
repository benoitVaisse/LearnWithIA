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

    [HttpPost("import-file")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ImportFile(
        IFormFile file,
        [FromServices] IImportDocumentFile importDocumentFile)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Aucun fichier fourni");
        }

        using var stream = file.OpenReadStream();
        var result = await importDocumentFile.Handle(stream, file.FileName);

        return Ok(new
        {
            message = $"Import termine: {result.ChunksImported}/{result.TotalChunks} chunks importes",
            chunksImported = result.ChunksImported,
            totalChunks = result.TotalChunks
        });
    }

}
