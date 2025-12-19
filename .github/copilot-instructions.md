# Online Courses Platform - AI Coding Guidelines

## Project Overview
ASP.NET Core Web API for managing online courses, users, roles, and enrollments. Built with .NET 9, Entity Framework Core 9, and SQL Server.

## Architecture
- **Layered Structure**: Controllers → Data (DbContext) → Models
- **No Service Layer**: Controllers directly inject and use `AppDbContext` for data operations
- **Minimal API Setup**: Uses `WebApplication.CreateBuilder()` with OpenAPI support

## Key Patterns
- **Entity Relationships**: Many-to-many via junction entities (e.g., `UserRole`, `CourseCategory`, `Enrolment`)
- **Composite Keys**: Defined in `AppDbContext.OnModelCreating()` for junction tables
- **Navigation Properties**: Models include `ICollection<T>` for related entities (e.g., `Course.Lessons`, `Course.Assignments`)
- **Nullable Types**: Enabled globally; use `?` for optional properties (e.g., `string? Description`)

## Development Workflow
- **Build**: `dotnet build`
- **Run**: `dotnet run` (launches on http://localhost:5170)
- **Database Migrations**:
  - Add: `dotnet ef migrations add <Name>`
  - Update: `dotnet ef database update`
  - Remove: `dotnet ef migrations remove`
- **API Testing**: Use `OnlineCourses.http` file in VS Code REST Client extension

## Database
- **Provider**: SQL Server with Trusted Connection
- **Connection String**: `"Server=.;Database=OnlineCoursesDb;Trusted_connection=True;TrustServerCertificate=True;"`
- **Schema**: Auto-generated via EF migrations; inspect `Migrations/` folder for current state

## API Conventions
- **Routing**: `api/[controller]` (e.g., `/api/Course`)
- **Controllers**: Inherit `ControllerBase`; inject `AppDbContext` in constructor
- **HTTP Methods**: Standard REST (GET, POST, PUT, DELETE) - implement as needed in controllers
- **OpenAPI**: Enabled in Development; access via `/openapi/v1.json`

## Dependencies
- `Microsoft.EntityFrameworkCore` 9.0.0
- `Microsoft.EntityFrameworkCore.SqlServer` 9.0.0
- `Microsoft.AspNetCore.OpenApi` 9.0.0

## Code Style
- **Naming**: PascalCase for classes/properties; camelCase for local variables
- **Implicit Usings**: Enabled; no explicit `using` for common namespaces
- **File Structure**: Models in `Models/`, Controllers in `Controllers/`, Data in `Data/`

Reference: `Program.cs` for app setup, `AppDbContext.cs` for data model, `Course.cs` for entity example.</content>
<parameter name="filePath">.github/copilot-instructions.md