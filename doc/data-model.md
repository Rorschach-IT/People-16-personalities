# Data Model and Validation

## MongoDB Settings

Configuration section:

```json
{
  "MongoDb": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "PeoplePersonalities",
    "PersonPersonalitiesCollectionName": "PeoplePersonalities"
  }
}
```

Mapped class: `MongoDbSettings`

| Property | Description |
| --- | --- |
| `ConnectionString` | MongoDB connection URI |
| `DatabaseName` | Database used by the application |
| `PersonPersonalitiesCollectionName` | Collection used for people/personality documents |

## Person Document

Class: `PersonPersonality`

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| `FirstName` | string | Yes | Person first name |
| `LastName` | string | Yes | Person last name |
| `Type` | string | Yes | MBTI personality type |

The model uses `[BsonIgnoreExtraElements]`, so additional MongoDB fields are ignored during deserialization.

## Validation Rules

### First Name

Rules:

- Required.
- Allows letters, Polish characters, and a single hyphen between name parts.
- Does not allow spaces, digits, punctuation, or leading/trailing hyphens.

### Last Name

Rules:

- Required.
- Allows letters, Polish characters, and a single hyphen between surname parts.
- Does not allow spaces, digits, punctuation, or leading/trailing hyphens.

### Personality Type

Rules:

- Required.
- Must be one of the following MBTI values:

```text
INTJ, INTP, ENTJ, ENTP,
INFJ, INFP, ENFJ, ENFP,
ISTJ, ISFJ, ESTJ, ESFJ,
ISTP, ISFP, ESTP, ESFP
```

## Normalization

Before insert, `AddPersonService` normalizes input:

| Field | Normalization |
| --- | --- |
| `FirstName` | Trim, uppercase first character, lowercase remaining characters |
| `LastName` | Trim, uppercase first character, lowercase remaining characters |
| `Type` | Trim and uppercase invariant |

## Uniqueness

The application treats the combination of `FirstName` and `LastName` as unique.

Enforcement:

- Pre-insert existence query.
- Unique MongoDB compound index `UX_FirstName_LastName`.

Practical implication:

- Two people cannot share the same normalized first name and last name, even if their personality types differ.
