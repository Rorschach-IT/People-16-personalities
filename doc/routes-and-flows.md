# Routes and User Flows

## Route Summary

| Method | URL | Controller | Action | Purpose |
| --- | --- | --- | --- | --- |
| GET | `/` | `Home` | `Index` | Main people list |
| GET | `/Home/Index` | `Home` | `Index` | Main people list |
| GET | `/Home/Index?type=INTJ` | `Home` | `Index` | Filter list by type |
| GET | `/AddPerson` | `AddPerson` | `Index` | Add-person form |
| POST | `/AddPerson` | `AddPerson` | `Index` | Create person |
| GET | `/Documentation` | `Documentation` | `Index` | Static documentation page |
| GET | `/Home/Error` | `Home` | `Error` | Error view |

## Flow: Browse People

1. User opens `/`.
2. `HomeController.Index` receives optional `type` query parameter.
3. `PersonPersonalityService.GetAllAsync` reads all people or filtered people.
4. `AggregateDataService.GetDistinctTypesAsync` reads distinct type values for the dropdown.
5. `HomeIndexViewModel` is passed to `Views/Home/Index.cshtml`.
6. Razor renders the filter form and result table.

## Flow: Filter People By Type

1. User selects a personality type from the dropdown.
2. User clicks `Filtruj`.
3. Browser submits a GET request with `type={selectedType}`.
4. Backend applies a MongoDB equality filter on `Type`.
5. Results and selected filter are rendered back to the home page.

## Flow: Add Person

1. User opens `/AddPerson`.
2. `AddPersonController.Index` renders the add-person form.
3. User enters first name, last name, and MBTI type.
4. Browser submits POST request with anti-forgery token.
5. MVC validates `PersonPersonality`.
6. `AddPersonService` normalizes values.
7. Service checks whether the same normalized first name and last name already exists.
8. If unique, MongoDB insert is executed.
9. User is redirected to `/`.

## Flow: Duplicate Person

1. User submits a person with an existing normalized first name and last name.
2. `AddPersonService` detects the duplicate through `ExistsAsync`, or MongoDB rejects insert through the unique index.
3. The service returns a failure result.
4. Controller adds a model-level validation error.
5. The add-person view displays the duplicate error message.

## Flow: Documentation Page

1. User opens `/Documentation`.
2. `DocumentationController.Index` returns `Views/Documentation/Index.cshtml`.
3. The page displays static project and external reference links.
