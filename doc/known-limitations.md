# Known Limitations and Recommendations

## Current Limitations

- No authentication or authorization is implemented.
- Anyone with access to the application can submit the add-person form.
- No delete, update, or detail page exists for stored people.
- No automated tests are present in the inspected project.
- No chart analysis implementation is present; navigation item `Analiza wykresowa` is disabled.
- The JavaScript file currently contains only commented code.
- Validation and UI text are primarily Polish, while the document language and HTML `lang` value are not fully aligned.
- The uniqueness rule is based only on first name and last name.
- The application fails startup if MongoDB is unavailable.
- There is no explicit health check endpoint.
- There is no documented production secrets strategy.

## Technical Recommendations

- Add authentication before exposing the application outside a trusted environment.
- Add integration tests for MongoDB-backed services and controller flows.
- Add unit tests for validation and normalization rules.
- Consider a case-insensitive or normalized unique key if MongoDB collation or mixed historical data becomes relevant.
- Add health checks for MongoDB readiness.
- Move production secrets to environment variables, user secrets, or a secret manager.
- Add a stable DTO or view model for add-person input if the domain model grows.
- Add paging or sorting if the people collection grows significantly.
- Decide whether `FirstName + LastName` is a sufficient identity rule for the real use case.
- Implement the disabled chart analysis feature or remove it from navigation until available.

## Documentation Maintenance

Update this documentation whenever one of the following changes:

- MVC routes or controller actions.
- MongoDB collection names or indexes.
- Validation rules.
- Startup dependencies.
- Deployment configuration.
- Frontend navigation or user flows.
