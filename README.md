Enterprise Nexus: AI Integration Gateway
Enterprise Nexus is a cloud-native integration middleware built with .NET 8 and Microsoft Semantic Kernel. It serves as an architectural bridge between modern Large Language Models (LLMs) and internal enterprise data systems.
🏗️ Architectural Overview
The solution is built using Clean Architecture principles to ensure a clear separation of concerns and high testability.
EnterpriseNexus.Api: ASP.NET Core 8 Web API managing the HTTP request pipeline and middleware.
EnterpriseNexus.Infrastructure: Encapsulates Semantic Kernel orchestration and Azure OpenAI connectors.
EnterpriseNexus.Core: Contains "Native Functions" (Plugins) that allow the AI to interact with internal business logic and legacy data safely.
🌟 Key Technical Features
AI Orchestration: Utilizes Semantic Kernel to dynamically plan and invoke C# functions based on natural language intent.
Plugin-Based Integration: Implements a Native Plugin Architecture to query internal systems (simulated via C# services) without exposing raw database schemas to the LLM.
Keyless Security: Leverages DefaultAzureCredential for Azure OpenAI connections, utilizing Managed Identities to avoid hardcoded secrets.
Global Resiliency: Includes a custom Global Exception Handling Middleware that provides a centralized safety net for the AI orchestration layer.
Decoupled Configuration: Uses the Options Pattern and Service Collection Extensions to keep the API layer agnostic of the underlying AI provider.
🛠️ Tech Stack
Runtime: .NET 8 (LTS)
AI Framework: Microsoft Semantic Kernel
LLM Provider: Azure OpenAI (Native MS Integration)
Security: Microsoft Entra ID (Managed Identities)
Observability: Structured Logging via ILogger
🚀 Getting Started
Prerequisites
.NET 8 SDK
Azure OpenAI Resource (or an OpenAI-compatible endpoint)
Configuration
Update the appsettings.json in the EnterpriseNexus.Api project:
json
{
  "Azure": {
    "OpenAI": {
      "Endpoint": "https://azure.com",
      "DeploymentName": "gpt-4o"
    }
  }
}
Use code with caution.

Installation & Run
Open your terminal.
Navigate to the solution root.
Execute the following command:
powershell
dotnet run --project EnterpriseNexus.Api
Use code with caution.

📊 Process Flow
Request: The API receives a natural language query via the ChatController.
Analysis: The Semantic Kernel evaluates the prompt and identifies required tools.
Execution: The EnterpriseDataPlugin is automatically invoked to retrieve real-time data from the legacy C# service.
Synthesis: The LLM processes the internal data and generates a human-readable response.
Response: The system returns a structured JSON object containing the answer and operational metadata.
📝 Project Scope
This repository demonstrates secure and resilient integration patterns for modernizing legacy enterprise workflows through AI-augmented orchestration.