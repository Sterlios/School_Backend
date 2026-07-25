# School_Backend# School Api
School API is the backend service for the School educational platform.
    
The platform is designed to provide a scalable and maintainable environment for creating and delivering online courses. It is intended to be used as the primary platform for teaching students and managing their learning process.

The project is built using Clean Architecture and Domain-Driven Design (DDD) principles, making it easy to extend and maintain as new features are introduced.

---

## Features

Currently implemented:

- User registration
- JWT-based authentication and authorization
- Swagger (OpenAPI) documentation
- PostgreSQL integration
- Docker and Docker Compose support
- Unit testing for Domain and Application layers

Planned features:

- Roles and permissions
- Courses management
- Modules and lessons
- Student progress tracking
- Refresh tokens
- Integration tests
- CI/CD pipeline
- Docker deployment improvements

and other

---

## Architecture

The solution follows the principles of Clean Architecture and is divided into four layers:

```text
src
│
├── Api
├── Application
├── Domain
└── Infrastructure
```

### Domain

Contains the core business logic of the application:

- Aggregates
- Entities
- Value Objects
- Domain exceptions
- Domain rules

The Domain layer has no dependencies on external frameworks or infrastructure components.

### Application

Contains application use cases and abstractions:

- DTOs (Commands, Queries)
- Services
- Interfaces

Responsible for orchestrating business scenarios.

### Infrastructure

Contains implementations of infrastructure concerns:

- Entity Framework Core
- PostgreSQL
- Repository implementations
- JWT token generation
- Password hashing

### API

Provides access to the application through HTTP endpoints:

- Controllers
- Authentication configuration
- Authorization configuration
- Swagger/OpenAPI documentation

---

## Technology Stack

- ASP.NET Core (.NET 10)
- Entity Framework Core
- PostgreSQL
- JWT Authentication
- Docker
- Docker Compose
- Swagger (OpenAPI)
- xUnit
- FluentAssertions
- Moq

---

## Running the Application

### Prerequisites

#### Local development

- .NET SDK 10
- PostgreSQL

#### Docker

- Docker
- Docker Compose

---

## Running Locally

### Create a PostgreSQL database

```sql
CREATE DATABASE School_db;
```

### Configure User Secrets

Navigate to the API project:

```bash
cd src/Api
```

Add the PostgreSQL connection string:

```bash
dotnet user-secrets set "ConnectionStrings:Postgres" "Host=localhost;Port=5432;Database=School_db;Username=postgres;Password=YOUR_PASSWORD"
```

Configure JWT settings:

```bash
dotnet user-secrets set "Jwt:SecretKey" "YOUR_SECRET_KEY"
```
Optional JWT settings:

```bash
dotnet user-secrets set "Jwt:Issuer" "School.Api"
dotnet user-secrets set "Jwt:Audience" "School.Client"
dotnet user-secrets set "Jwt:ExpirationMinutes" "60"
```


### Apply migrations

```bash
dotnet ef database update --project src/Infrastructure --startup-project src/Api
```

### Run the application

```bash
dotnet run --project src/Api
```

Swagger UI will be available at:

```text
http://localhost:<PORT>/swagger
```

---

## Running with Docker

Build and run all services:

```bash
docker compose up --build
```

Docker Compose is responsible for:

- Starting PostgreSQL
- Creating the database
- Building the API image
- Running the backend service

---