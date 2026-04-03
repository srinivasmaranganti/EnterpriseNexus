Enterprise Nexus: ACEP AI Integration Gateway
Enterprise Nexus is a cloud‑native integration middleware built with .NET 8 and Microsoft Semantic Kernel. It serves as an intelligent architectural bridge between Large Language Models (LLMs) and ACEP’s core business systems, including NetForum CRM, CME Tracking, and Event Registration databases.
🏗️ Architectural Overview
The solution follows Clean Architecture principles to ensure a decoupled, testable, and scalable foundation for enterprise AI operations.
Project Structure
EnterpriseNexus.Api
ASP.NET Core 8 Web API managing the HTTP pipeline and the Global Exception Middleware for resilient JSON responses.
EnterpriseNexus.Infrastructure
The orchestration engine housing Semantic Kernel setup, Azure OpenAI connectors, and service registrations.
EnterpriseNexus.Core
Contains the domain logic and Native Functions (Plugins) that allow the AI to safely interact with ACEP business data.
🌟 Key Technical Features
Multi-Domain AI Orchestration
Uses Semantic Kernel to dynamically route natural language prompts to specific business domains (e.g., Membership vs. Logistics).
ACEP Business Plugins
Implements a Native Plugin Architecture to query NetForum CRM for member status and the CME Tracker for credit verification without exposing sensitive schemas.
Global Exception Middleware
A custom resiliency layer that catches orchestration failures and ensures the API always returns structured, "AI-ready" JSON.
Secure Configuration
Uses the Options Pattern to manage Azure OpenAI endpoints and API keys securely across different environments.
🛠️ Tech Stack
Category	Technology
Runtime	.NET 8 (LTS)
AI Framework	Microsoft Semantic Kernel
LLM Provider	Azure OpenAI (GPT-4o)
Security	Managed Identities / API Key Authentication
Observability	Structured Logging via Serilog & ILogger
🚀 Getting Started
⚙️ Configuration
Update appsettings.json in EnterpriseNexus.Api:
json
{
  "Azure": {
    "OpenAI": {
      "Endpoint": "https://azure.com",
      "DeploymentName": "gpt-4o",
      "ApiKey": "YOUR_SECURE_KEY"
    }
  }
}
Use code with caution.

▶️ Installation & Run
powershell
dotnet run --project EnterpriseNexus.Api
Use code with caution.

📊 Process Flow
Request: API receives a query (e.g., "Check my ACEP26 registration status").
Analysis: Semantic Kernel evaluates the prompt and identifies the ACEPMemberServicesPlugin.
Execution: The plugin retrieves real-time data from the simulated CRM/Meeting service.
Synthesis: The LLM processes the raw data into a human-readable, professional response.
Response: API returns structured JSON containing the answer and metadata.
📝 Project Scope
This repository demonstrates how professional associations like ACEP can modernize legacy workflows (CRM, CME, and Payments) using AI‑augmented orchestration, ensuring secure data access and a resilient user experience through .NET cloud-native design.