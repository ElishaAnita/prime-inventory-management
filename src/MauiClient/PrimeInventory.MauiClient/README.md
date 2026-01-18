# Prime Inventory MAUI Client

This project contains a .NET MAUI client for the Prime Inventory Management System.

## Important Note

This project is structured as a **MAUI-ready application** but is currently configured as a class library to allow building on systems without the MAUI workload installed.

## Converting to Full MAUI Application

To convert this project to a fully functional MAUI application on a system with MAUI workload installed:

1. Replace the `PrimeInventory.MauiClient.csproj` content with the MAUI-specific project file:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFrameworks>net8.0-android;net8.0-ios;net8.0-maccatalyst</TargetFrameworks>
    <TargetFrameworks Condition="$([MSBuild]::IsOSPlatform('windows'))">$(TargetFrameworks);net8.0-windows10.0.19041.0</TargetFrameworks>
    <OutputType>Exe</OutputType>
    <UseMaui>true</UseMaui>
    <SingleProject>true</SingleProject>
    <!-- ... other MAUI properties ... -->
  </PropertyGroup>
</Project>
```

2. Install the MAUI workload:
```bash
dotnet workload install maui
```

3. Restore and build the project:
```bash
dotnet restore
dotnet build
```

## Project Structure

- **Services/**: API client services for communicating with the REST API
- **Views/**: XAML-based UI pages
- **ViewModels/**: (To be added) View models for MVVM pattern
- **Resources/**: Application resources (styles, images, icons)

## Features

The MAUI client includes:
- Cross-platform support (Android, iOS, macOS, Windows)
- API integration with the Prime Inventory REST API
- XAML-based UI with Material Design principles
- Service-oriented architecture

## Building and Running

### With MAUI Workload

```bash
# Android
dotnet build -f net8.0-android
dotnet run -f net8.0-android

# iOS (macOS only)
dotnet build -f net8.0-ios
dotnet run -f net8.0-ios

# Windows
dotnet build -f net8.0-windows10.0.19041.0
dotnet run -f net8.0-windows10.0.19041.0
```

### Without MAUI Workload

The project will build as a class library demonstrating the architecture and code structure.

## Configuration

Update the API base URL in `Services/InventoryApiService.cs`:

```csharp
_baseUrl = "https://your-api-url/api";
```

## Architecture

The MAUI client follows clean architecture principles:
- Depends only on ApplicationCore (DTOs and interfaces)
- Uses HttpClient for API communication
- MVVM pattern for UI separation
- Dependency injection for services
