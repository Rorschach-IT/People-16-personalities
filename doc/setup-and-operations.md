# Setup and Operations

## Prerequisites

- .NET 10 SDK/runtime.
- MongoDB running locally or reachable through a configured connection string.
- Access to the application source directory.

## Configuration

Default configuration is stored in `appsettings.json`:

```json
"MongoDb": {
  "ConnectionString": "mongodb://localhost:27017",
  "DatabaseName": "PeoplePersonalities",
  "PersonPersonalitiesCollectionName": "PeoplePersonalities"
}
```

For local development, `appsettings.Development.json` may override these values.

## Local Run

From the project directory:

```powershell
dotnet restore
dotnet run
```

The configured development launch profile uses:

```text
http://0.0.0.0:5188
ASPNETCORE_ENVIRONMENT=Development
```

## Startup Requirements

MongoDB must be reachable during startup. The application performs a MongoDB `ping` command before serving requests. If the database is unavailable, startup fails.

At startup, the application also creates or verifies the unique index:

```text
UX_FirstName_LastName
```

## Operational Checks

Recommended checks after startup:

1. Open `/` and verify that the home page loads.
2. Open `/AddPerson` and submit a valid MBTI record.
3. Verify the new record appears on `/`.
4. Try adding the same first name and last name again and verify the duplicate error is displayed.
5. Use the type dropdown on `/` and confirm filtering works.

## Logging

The default log levels are:

| Scope | Level |
| --- | --- |
| Default | Information |
| Microsoft.AspNetCore | Warning |

MongoDB connection success is logged as an information message. MongoDB connection failure is logged as an error.

## Deployment Notes

- Configure MongoDB connection settings per environment.
- Ensure the hosting environment supports .NET 10.
- Ensure static assets from `wwwroot` are deployed.
- If deployed behind a reverse proxy, review HTTPS redirection and forwarded headers configuration.
- Authentication is not currently implemented; do not expose write operations publicly without access controls.
