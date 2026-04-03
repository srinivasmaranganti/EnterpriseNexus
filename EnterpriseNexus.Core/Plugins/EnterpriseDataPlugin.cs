using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace EnterpriseNexus.Core.Plugins;

public class EnterpriseDataPlugin
{
    [KernelFunction]
    [Description("Fetches real-time inventory levels from the legacy warehouse system.")]
    public string GetWarehouseStock([Description("The product SKU or name")] string productName)
    {
        // Architect's Note: This is a placeholder for a SQL or REST call
        // Demonstrates integration with "On-Premise" data
        return productName.ToLower() switch
        {
            "laptops" => "Warehouse A: 45 units | Warehouse B: 12 units",
            "monitors" => "Warehouse A: Out of Stock | Warehouse B: 150 units",
            _ => $"No stock records found for {productName}."
        };
    }
}
