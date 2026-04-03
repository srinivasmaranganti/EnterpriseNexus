namespace EnterpriseNexus.Api.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try { await next(context); }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unhandled exception occurred during an AI operation.");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new
                {
                    Error = "Internal Server Error",
                    Message = "The AI service is currently unavailable. please try again later."
                });
            }
        }
    }

}
