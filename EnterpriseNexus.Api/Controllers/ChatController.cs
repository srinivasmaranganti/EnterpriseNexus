using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Serilog; // Add this for direct logging or use ILogger

namespace EnterpriseNexus.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly Kernel _kernel;
    private readonly ILogger<ChatController> _logger;

    // We inject both the Kernel and the Logger here
    public ChatController(Kernel kernel, ILogger<ChatController> logger)
    {
        _kernel = kernel;
        _logger = logger;
    }

    [HttpPost("ask")]
    public async Task<IActionResult> Ask([FromBody] string userPrompt)
    {
        // 1. Audit Trail: Log the incoming request
        _logger.LogInformation("AI Request Received: {Prompt}", userPrompt);

        try
        {
            OpenAIPromptExecutionSettings settings = new()
            {
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
            };

            // 2. Execution: Invoke the AI Orchestrator
            var result = await _kernel.InvokePromptAsync(userPrompt, new(settings));

            // 3. Success Log
            _logger.LogInformation("AI Response generated successfully for: {Prompt}", userPrompt);

            return Ok(new
            {
                Answer = result.ToString(),
                Timestamp = DateTime.UtcNow
            });
        }
        catch (Exception ex)
        {
            // 4. Error Log: Capture the failure in your log file
            _logger.LogError(ex, "AI Orchestration failed for prompt: {Prompt}", userPrompt);

            // Re-throw so the Global Exception Middleware handles the JSON response
            throw;
        }
    }
}
