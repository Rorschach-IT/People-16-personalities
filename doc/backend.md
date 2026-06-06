# Backend Documentation

## Runtime

The backend is an ASP.NET Core MVC application targeting `net10.0`.

Primary backend packages:

- `MongoDB.Driver` 3.7.1
- `MongoDB.Bson` 3.7.1
- `Microsoft.VisualStudio.Web.CodeGeneration.Design` 10.0.2

## Controllers

### `HomeController`

Responsible for the main listing page and error page.

Endpoints:

| Method | Route | Action | Description |
| --- | --- | --- | --- |
| GET | `/` | `Index` | Lists all people. |
| GET | `/Home/Index?type={type}` | `Index` | Lists people filtered by MBTI type. |
| GET | `/Home/Error` | `Error` | Displays non-cacheable error view. |

`Index` builds `HomeIndexViewModel` with:

- `Items`: people returned from MongoDB.
- `Types`: distinct personality types available in the collection.
- `SelectedType`: current query filter.

### `AddPersonController`

Responsible for displaying and processing the add-person form.

Endpoints:

| Method | Route | Action | Description |
| --- | --- | --- | --- |
| GET | `/AddPerson` or `/AddPerson/Index` | `Index` | Displays add-person form. |
| POST | `/AddPerson` or `/AddPerson/Index` | `Index` | Validates and inserts a person. |

The POST action uses `[ValidateAntiForgeryToken]`. Invalid model state returns the form with validation messages. Successful insertion redirects to the home page.

### `DocumentationController`

Responsible for the in-application documentation page.

Endpoints:

| Method | Route | Action | Description |
| --- | --- | --- | --- |
| GET | `/Documentation` or `/Documentation/Index` | `Index` | Displays static documentation content. |

## Services

### `PersonPersonalityService`

Reads people from MongoDB.

Methods:

- `GetAllAsync(string? type = null)`

Behavior:

- If `type` is empty or whitespace, returns all documents.
- If `type` is provided, applies an equality filter on `Type`.

### `AggregateDataService`

Provides aggregate read data for the home filter.

Methods:

- `GetDistinctTypesAsync()`

Behavior:

- Reads distinct values from the MongoDB `Type` field.
- Removes empty values.
- Sorts values ascending.

### `AddPersonService`

Handles person insertion.

Methods:

- `AddPersonAsync(PersonPersonality person)`
- `EnsureIndexesAsync()`

Insertion behavior:

1. Trims and capitalizes `FirstName`.
2. Trims and capitalizes `LastName`.
3. Trims and uppercases `Type`.
4. Checks whether a person with the same first name and last name exists.
5. Inserts the document if it is unique.
6. Returns a localized error message if the person already exists.

Index behavior:

- Creates a unique compound index on `FirstName` and `LastName`.
- Handles duplicate key exceptions defensively during insert.

## Error Handling

MongoDB connectivity is checked during startup. If the ping fails, the application logs the failure and throws, preventing startup with a broken database dependency.

Duplicate person insertion is handled in two layers:

- Application-level pre-check with `ExistsAsync`.
- Database-level unique index and `MongoWriteException` handling.

## Security Controls

Current controls:

- Anti-forgery token on the add-person POST form.
- Server-side model validation.
- HTTPS redirection.
- HSTS outside development.

No authentication or authorization policy is currently implemented.
