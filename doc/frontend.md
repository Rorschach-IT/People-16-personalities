# Frontend Documentation

## Rendering Model

The frontend is server-rendered with Razor views. Pages are composed from:

- Shared layout: `Views/Shared/_Layout.cshtml`
- Page views:
  - `Views/Home/Index.cshtml`
  - `Views/AddPerson/Index.cshtml`
  - `Views/Documentation/Index.cshtml`
- CSS under `wwwroot/css`
- Bootstrap and jQuery libraries under `wwwroot/lib`

## Shared Layout

The shared layout defines:

- HTML metadata and page title.
- Bootstrap CSS.
- Application CSS files.
- Navigation bar.
- Main content container.
- Footer with version label `Alpha-0.1.1`.
- jQuery and Bootstrap scripts.

Navigation items:

| Label | Target | State |
| --- | --- | --- |
| `Strona glowna` | `Home/Index` | Active when current route matches |
| `Dodaj osobe` | `AddPerson/Index` | Active when current route matches |
| `Analiza wykresowa` | `Home/Index` | Disabled |
| `Dokumentacja` | `Documentation/Index` | Active when current route matches |

The active navigation class is provided by the `IsActive` HTML helper extension in `Services/ReplaceNavStyle.cs`.

## Home Page

View: `Views/Home/Index.cshtml`

Features:

- GET filter form with `type` query parameter.
- Type dropdown populated from distinct MongoDB `Type` values.
- Buttons:
  - `Filtruj`: submits the selected type.
  - `Reset`: clears the filter by returning to `Home/Index`.
  - `Dodaj osobe`: navigates to add-person page.
- Results table with first name, last name, and type.
- Empty state row: `No data`.

## Add Person Page

View: `Views/AddPerson/Index.cshtml`

Features:

- POST form with anti-forgery token.
- Inputs:
  - `FirstName`
  - `LastName`
  - `Type`
- Client-side validation scripts are rendered through `_ValidationScriptsPartial`.
- Server-side validation messages are rendered using tag helpers.
- Duplicate-person errors are displayed from `ModelState`.

## Documentation Page

View: `Views/Documentation/Index.cshtml`

Features:

- Static informational content.
- Link to 16personalities type descriptions.
- Link to the GitHub repository and alpha branch.

## CSS Assets

| File | Responsibility |
| --- | --- |
| `wwwroot/css/site.css` | Global font sizing, layout, focus styles, footer/body spacing |
| `wwwroot/css/main-view.css` | Home table, filter form, select, and aggregate button styling |
| `wwwroot/css/add-person-view.css` | Add-person form sizing |
| `wwwroot/css/documentation.css` | Documentation page typography |

## JavaScript

`wwwroot/js/site.js` currently contains only commented-out code for disabling the type select when no filter is selected. Runtime behavior is currently handled primarily by Razor, Bootstrap, and jQuery validation.
