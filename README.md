# ECommerceAPI

ECommerceAPI is a modular and scalable e-commerce API project built with C#. It follows a layered architecture and Domain-Driven Design (DDD) principles to ensure maintainability, testability, and separation of concerns.

## Project Structure

The solution is organized into the following main layers:

- **Core**: Contains business logic, domain models, and interfaces.
- **Infrastructure**: Contains data access implementations and external service integrations.
- **Presentation/ECommerceAPI.API**: Exposes API endpoints and handles user interaction.

## Getting Started

### Prerequisites

- .NET 6 SDK or higher
- A database server (e.g., SQL Server or PostgreSQL)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/sdkmen/ECommerceAPI.git
   cd ECommerceAPI
   ```

2. Restore NuGet packages:
   ```bash
   dotnet restore
   ```

3. Configure the database connection in `appsettings.json`.

4. Apply database migrations:
   ```bash
   dotnet ef database update
   ```

5. Run the application:
   ```bash
   dotnet run --project Presentation/ECommerceAPI.API
   ```

## API Documentation

Once the application is running, you can access the Swagger UI at [http://localhost:5000/swagger](http://localhost:5000/swagger) to explore and test available endpoints.
