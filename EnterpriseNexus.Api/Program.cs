using EnterpriseNexus.Infrastructure;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("Logs/nexus-ai-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Enable Controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddNexusAI(
    builder.Configuration["Azure:OpenAI:DeploymentName"]!,
    builder.Configuration["Azure:OpenAI:Endpoint"]!,
    builder.Configuration["Azure:OpenAI:ApiKey"]!
);

builder.Host.UseSerilog();

var app = builder.Build();

app.UseMiddleware<EnterpriseNexus.Api.Middleware.ExceptionHandlingMiddleware>();

// Enable Swagger UI in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers(); // This makes your ChatController visible
app.Run();
