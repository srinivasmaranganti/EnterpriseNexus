using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;


namespace EnterpriseNexus.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly Kernel _kernel;

    // The Kernel is injected here from our Infrastructure layer
    public ChatController(Kernel kernel)
    {
        _kernel = kernel;
    }

    [HttpPost("ask")]
    public async Task<IActionResult> Ask([FromBody] string userPrompt)
    {
        // ARCHITECT'S MOVE: Enable "Auto Function Calling"
        // This tells the AI: "Look at my EnterpriseDataPlugin and call it if needed."
        OpenAIPromptExecutionSettings settings = new()
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
        };

        // Invoke the AI with your prompt and the "Auto-Call" settings
        var result = await _kernel.InvokePromptAsync(userPrompt, new(settings));

        return Ok(new
        {
            Answer = result.ToString(),
            Timestamp = DateTime.UtcNow
        });
    }
}
