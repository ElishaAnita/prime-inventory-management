# Prime Inventory Management System

A cross-platform .NET 8 inventory management system with a MAUI client and REST API, following clean architecture principles.

## Architecture

The solution follows a layered architecture pattern with clear separation of concerns:

### Domain Layer (`PrimeInventory.Domain`)
- **Entities**: Core business entities (Product, Category, Supplier)
- **Interfaces**: Repository and service interfaces
- **No external dependencies** - pure business logic

### Application Core Layer (`PrimeInventory.ApplicationCore`)
- **DTOs**: Data Transfer Objects for API communication
- **Services**: Business logic implementation
- **Interfaces**: Service contracts
- Depends only on Domain layer

### Infrastructure Layer (`PrimeInventory.Infrastructure`)
- **Data**: Entity Framework Core DbContext
- **Repositories**: Data access implementation
- **Database**: SQLite for local storage
- Implements interfaces from Domain layer

### API Layer (`PrimeInventory.API`)
- **Controllers**: REST API endpoints
- **ASP.NET Core Web API** with Swagger/OpenAPI
- Dependency injection configuration
- CORS support for MAUI client

### MAUI Client (`PrimeInventory.MauiClient`)
- **Cross-platform UI**: Runs on iOS, Android, macOS, and Windows
- **Services**: API client services
- **Views**: XAML-based user interface
- **ViewModels**: MVVM pattern support

## Features

- 🎯 Product management (CRUD operations)
- 📦 Category organization
- 🏢 Supplier tracking
- 📊 Inventory levels monitoring
- 🔍 Low stock alerts
- 🌐 RESTful API with Swagger documentation
- 📱 Cross-platform mobile and desktop support

## Prerequisites

- .NET 8 SDK
- Visual Studio 2022 (for MAUI development) or VS Code
- MAUI workload (for building the client app)

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/ElishaAnita/prime-inventory-management.git
cd prime-inventory-management
```

### 2. Build the Solution

```bash
dotnet build PrimeInventory.sln
```

### 3. Run the API

```bash
cd src/API/PrimeInventory.API
dotnet run
```

The API will be available at:
- HTTPS: `https://localhost:7001`
- HTTP: `http://localhost:5000`
- Swagger UI: `https://localhost:7001/swagger`

### 4. Run the MAUI Client (Optional)

**Note**: The MAUI client requires the MAUI workload to be installed.

```bash
# Install MAUI workload (if not already installed)
dotnet workload install maui

# Run on Android
cd src/MauiClient/PrimeInventory.MauiClient
dotnet build -f net8.0-android
dotnet run -f net8.0-android

# Run on Windows
dotnet build -f net8.0-windows10.0.19041.0
dotnet run -f net8.0-windows10.0.19041.0
```

## API Endpoints

### Products
- `GET /api/products` - Get all products
- `GET /api/products/{id}` - Get product by ID
- `GET /api/products/category/{categoryId}` - Get products by category
- `GET /api/products/supplier/{supplierId}` - Get products by supplier
- `GET /api/products/low-stock/{threshold}` - Get low stock products
- `POST /api/products` - Create a new product
- `PUT /api/products/{id}` - Update a product
- `DELETE /api/products/{id}` - Delete a product

### Categories
- `GET /api/categories` - Get all categories
- `GET /api/categories/{id}` - Get category by ID
- `POST /api/categories` - Create a new category
- `PUT /api/categories/{id}` - Update a category
- `DELETE /api/categories/{id}` - Delete a category

### Suppliers
- `GET /api/suppliers` - Get all suppliers
- `GET /api/suppliers/{id}` - Get supplier by ID
- `POST /api/suppliers` - Create a new supplier
- `PUT /api/suppliers/{id}` - Update a supplier
- `DELETE /api/suppliers/{id}` - Delete a supplier

## Project Structure

```
prime-inventory-management/
├── src/
│   ├── Domain/
│   │   └── PrimeInventory.Domain/
│   │       ├── Entities/
│   │       └── Interfaces/
│   ├── ApplicationCore/
│   │   └── PrimeInventory.ApplicationCore/
│   │       ├── DTOs/
│   │       ├── Services/
│   │       └── Interfaces/
│   ├── Infrastructure/
│   │   └── PrimeInventory.Infrastructure/
│   │       ├── Data/
│   │       └── Repositories/
│   ├── API/
│   │   └── PrimeInventory.API/
│   │       ├── Controllers/
│   │       └── Program.cs
│   └── MauiClient/
│       └── PrimeInventory.MauiClient/
│           ├── Services/
│           ├── Views/
│           └── Resources/
└── PrimeInventory.sln
```

## Technologies

- **.NET 8**: Latest .NET framework
- **ASP.NET Core**: Web API framework
- **Entity Framework Core**: ORM for data access
- **SQLite**: Lightweight database
- **.NET MAUI**: Cross-platform UI framework
- **Swagger/OpenAPI**: API documentation

## Design Patterns

- **Repository Pattern**: Data access abstraction
- **Dependency Injection**: Loose coupling between components
- **Clean Architecture**: Separation of concerns
- **MVVM**: Model-View-ViewModel for MAUI client
- **DTO Pattern**: Data transfer between layers

## Database

The application uses SQLite for local data storage. The database file (`inventory.db`) is created automatically on first run in the API project directory.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License.
