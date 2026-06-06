# PeoplePersonalities

## Purpose

PeoplePersonalities is an ASP.NET Core MVC web application for maintaining a local register of people and their MBTI personality types. The application provides a server-rendered user interface for:

- Listing all stored people.
- Filtering people by personality type.
- Adding a new person with validation.
- Preventing duplicate people by first name and last name.
- Displaying a basic in-application documentation page.

## Technology Stack

| Area               | Technology                           |
| ------------------ | ------------------------------------ |
| Runtime            | .NET 10                              |
| Web framework      | ASP.NET Core MVC                     |
| View engine        | Razor Views                          |
| Database           | MongoDB                              |
| MongoDB driver     | MongoDB.Driver 3.7.1                 |
| Frontend libraries | Bootstrap, jQuery, jQuery Validation |
| Styling            | CSS files under `wwwroot/css`        |
| Rendering          | Server-Side                          |

## High-Level Behavior

The application starts by loading MongoDB configuration, registering MVC and application services, creating a MongoDB client, checking database connectivity with a `ping` command, and ensuring a unique MongoDB index for person identity. If MongoDB is unavailable at startup, the application logs an error and stops.

The default route opens the home page, which queries MongoDB for all people or a filtered subset by MBTI type. The add-person page validates input with ASP.NET Core model validation and stores normalized values in MongoDB.

## Requirements

- Operating system, that supports docker containers.
- .NET SDK.

## Instalation and run

1. Run `docker compose up -d --build` after cloning the repository. This creates mongodb container and then activates .sh script located in `\init`. The script creates a new database and the collection, then it imports data into that collection via mockData.json, located in `\seed`.
2. Run `dotnet run` in `\src\PeoplePersonalities\PeoplePersonalities` directory.
3. Open browser and type: `http://localhost:5188/`.

> App can be accessed via different LAN devices, website exported on `0.0.0.0`.

## Production

For production purposes, on backend change environment variable from: `ASPNETCORE_ENVIRONMENT=Development` to `ASPNETCORE_ENVIRONMENT=Production`. Also comment and uncomment section for this in .sh file.
There is also a `\production` folder, but there will be published **only minor updates**.
