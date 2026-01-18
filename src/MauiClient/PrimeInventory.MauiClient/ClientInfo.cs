namespace PrimeInventory.MauiClient;

/// <summary>
/// This project demonstrates the MAUI client architecture for the Prime Inventory Management System.
/// The full MAUI implementation requires the MAUI workload to be installed.
/// 
/// To enable full MAUI support:
/// 1. Install MAUI workload: dotnet workload install maui
/// 2. Update the project file to use MAUI SDK
/// 3. Add MAUI-specific views and application startup
/// 
/// The InventoryApiService in the Services folder demonstrates how the client
/// communicates with the REST API.
/// </summary>
public class ClientInfo
{
    public static string Name => "Prime Inventory MAUI Client";
    public static string Version => "1.0.0";
    public static string Description => "Cross-platform inventory management client";
}
