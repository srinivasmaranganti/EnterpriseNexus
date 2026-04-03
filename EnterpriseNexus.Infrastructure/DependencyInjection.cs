using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Azure.Identity;
using EnterpriseNexus.Core.Plugins;

namespace EnterpriseNexus.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddNexusAI(this IServiceCollection services, string modelId, string endpoint, string apiKey)
    {
        services.AddScoped(sp =>
        {
            var builder = Kernel.CreateBuilder();

            // Native MS Connection (Keyless/Entra ID)
            builder.AddAzureOpenAIChatCompletion(
                deploymentName: modelId,
                endpoint: endpoint, apiKey
                //credentials: new DefaultAzureCredential()
                );

            // Register your "Bridge" plugin
            // --- Multi-Domain Plugin Orchestration ---

            // 1. Logistics Domain (Warehouse)
            builder.Plugins.AddFromType<EnterpriseDataPlugin>();

            // 2. ACEP Business Domain (CRM, CME, Registration)
            builder.Plugins.AddFromType<ACEPMemberServicesPlugin>();

            return builder.Build();
        });

        return services;
    }
}
