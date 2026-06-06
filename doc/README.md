# PeoplePersonalities Documentation
## Purpose

PeoplePersonalities is an ASP.NET Core MVC web application for maintaining a local register of people and their MBTI personality types. The application provides a server-rendered user interface for:

- Listing all stored people.
- Filtering people by personality type.
- Adding a new person with validation.
- Preventing duplicate people by first name and last name.
- Displaying a basic in-application documentation page.

## Technology Stack

| Area               | Technology                           |
|--------------------|--------------------------------------|
| Runtime            | .NET 10                              |
| Web framework      | ASP.NET Core MVC                     |
| View engine        | Razor Views                          |
| Database           | MongoDB                              |
| MongoDB driver     | MongoDB.Driver 3.7.1                 |
| Frontend libraries | Bootstrap, jQuery, jQuery Validation |
| Styling            | CSS files under `wwwroot/css`        |
| Rendering          | Server-Side                          |

## Documentation Index

- [Architecture](architecture.md)
- [Backend Documentation](backend.md)
- [Frontend Documentation](frontend.md)
- [Data Model and Validation](data-model.md)
- [Setup and Operations](setup-and-operations.md)
- [Routes and User Flows](routes-and-flows.md)
- [Known Limitations and Recommendations](known-limitations.md)

## Source Layout

```text
PeoplePersonalities/
|-- Configuration/          MongoDB settings model
|-- Controllers/            MVC controllers
|-- Models/                 View models and domain model
|-- Services/               Business logic and data access services
|-- Views/                  Razor views and shared layout
|-- wwwroot/                Static CSS, JS, and frontend libraries
|-- Program.cs              Application bootstrap and dependency injection
|-- appsettings.json        Runtime configuration
|-- Properties/             Launch settings
```

## High-Level Behavior

The application starts by loading MongoDB configuration, registering MVC and application services, creating a MongoDB client, checking database connectivity with a `ping` command, and ensuring a unique MongoDB index for person identity. If MongoDB is unavailable at startup, the application logs an error and stops.

The default route opens the home page, which queries MongoDB for all people or a filtered subset by MBTI type. The add-person page validates input with ASP.NET Core model validation and stores normalized values in MongoDB.
