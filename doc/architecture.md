# Architecture

## Architectural Style

The project follows a standard ASP.NET Core MVC structure:

- Controllers receive HTTP requests and select views.
- Services contain application logic and MongoDB operations.
- Models define input, output, and view data contracts.
- Razor views render HTML on the server.
- Static frontend assets are served from `wwwroot`.

## Component Overview

```text
Browser
  |
  | HTTP GET/POST
  v
ASP.NET Core MVC Controllers
  |
  | service interfaces
  v
Application Services
  |
  | MongoDB.Driver
  v
MongoDB Database
```

## Startup Flow

`Program.cs` is responsible for the application bootstrap:

1. Registers MVC with `AddControllersWithViews`.
2. Binds `MongoDb` configuration to `MongoDbSettings`.
3. Registers `IMongoClient` as a singleton.
4. Registers `IMongoDatabase` as scoped.
5. Registers `IMongoCollection<PersonPersonality>` as scoped.
6. Registers application services:
   - `IPersonPersonalityService`
   - `IAggregateDataService`
   - `IAddPersonService`
7. Builds the app.
8. Verifies MongoDB connectivity with `db.RunCommandAsync({"ping": 1})`.
9. Creates or verifies the unique index on first name and last name.
10. Configures middleware and the default MVC route.

## Dependency Injection

| Service | Lifetime | Implementation | Responsibility |
| --- | --- | --- | --- |
| `IMongoClient` | Singleton | `MongoClient` | MongoDB connection client |
| `IMongoDatabase` | Scoped | Configured database | Request-scoped database access |
| `IMongoCollection<PersonPersonality>` | Scoped | Configured collection | Request-scoped collection access |
| `IPersonPersonalityService` | Scoped | `PersonPersonalityService` | Read people and optional type filter |
| `IAggregateDataService` | Scoped | `AggregateDataService` | Read distinct personality types |
| `IAddPersonService` | Scoped | `AddPersonService` | Normalize, validate duplicate state, insert person, create indexes |

## Middleware Pipeline

The configured middleware pipeline is:

```text
Exception handler and HSTS in non-development
HTTPS redirection
Routing
Authorization
Static assets
Default MVC controller route
```

The default MVC route is:

```text
{controller=Home}/{action=Index}/{id?}
```

## Persistence Design

The database stores `PersonPersonality` documents. The model intentionally ignores extra MongoDB fields through `[BsonIgnoreExtraElements]`, which allows documents to contain fields not represented by the current C# model.

The application creates a unique compound index:

```text
UX_FirstName_LastName
FirstName ascending
LastName ascending
Unique: true
```

This index enforces uniqueness for a normalized first-name and last-name pair.
