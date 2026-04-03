using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Azure.Identity;
using EnterpriseNexus.Core.Plugins;

namespace EnterpriseNexus.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddNexusAI(this IServiceCollection services, string modelId, string endpoint)
    {
        services.AddScoped(sp =>
        {
            var builder = Kernel.CreateBuilder();

            // Native MS Connection (Keyless/Entra ID)
            builder.AddAzureOpenAIChatCompletion(
                deploymentName: modelId,
                endpoint: endpoint,
                credentials: new DefaultAzureCredential());

            // Register your "Bridge" plugin
            builder.Plugins.AddFromType<EnterpriseDataPlugin>();

            return builder.Build();
        });

        return services;
    }
}
