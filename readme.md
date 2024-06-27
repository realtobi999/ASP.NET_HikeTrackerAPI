# **HikingTracks Api Project** 

## **Summary**

- This is a **demo API** for a hike-tracking web application.
- **19** endpoints in total, with over **45 unit and integration tests.**
- Utilizes **PostgreSQL** database with EF.
- Project is written in C# utilizing **Clean Architecture**.
- Most endpoints are authenticated and authorized with JWT token.
- Hike can have **multiple photos** associated with it. Also **automatic segment assignation** to each hike can be done.


## **Endpoints Overview**

Overview of all endpoints using Swagger. In total there is **19** endpoints.

![swagger](https://imgur.com/a/oEGsQFK)

**To check out all of the endpoints in greater detail click [here.](endpoints.md)**

## **Tests Overview**

In this project there is total of **45** tests. About 1/3 of those tests are **integration** tests and the rest are **unit tests**. I've chosen mainly the happy path for the integration tests and for the unit tests i tried to cover all scenarios.

```
├───Integration
│   │   WebAppFactory.cs // Creates a mock Program class
│   │
│   ├───AccountEndpointTests
│   │       AccountControllerTests.cs
│   │       AccountTestExtensions.cs
│   │
│   ├───HikeEndpointTests
│   │       HikeControllerTests.cs
│   │       HikeTestExtensions.cs
│   │
│   ├───Middleware
│   │       MiddlewareTests.cs
│   │
│   └───SegmentEndpointTests
│           SegmentControllerTests.cs
│           SegmentTestExtensions.cs
└───Unit
        AccountServiceTests.cs
        CoordinateTests.cs
        FormFileServiceTests.cs
        HikeServiceTests.cs
        SegmentServiceTests.cs
        TokenServiceTests.cs
```

## **Architecture Overview**

This WEB-API project utilizes the well known clean architecture type.

### Tree Overview

```
.
├── endpoints.md
├── readme.md
└── src
    ├── HikingTracks.Application // Application Layer
    │   ├── Attributes
    │   ├── Factories
    │   ├── Interfaces
    │   └── Service
    ├── HikingTracks.Domain // Domain Layer
    │   ├── DTOs
    │   ├── Entities
    │   ├── Exceptions
    │   └── Interfaces
    ├── HikingTracks.Infrastructure // Infrastructure Layer
    │   ├── Factories
    │   ├── HikingTracksContext.cs
    │   ├── Migrations
    │   └── Repositories
    ├── HikingTracks.LoggerService
    │   └── LoggerManager.cs
    ├── HikingTracks.Presentation // Presentation Layer
    │   ├── Controllers
    │   ├── Extensions
    │   ├── Middleware
    │   └── Program.cs
    └── HikingTracks.Tests
        ├── Integration
        └── Unit
```

### **Layer Descriptions**

***Application Layer (HikingTracks.Application):***

This layer contains the business logic and application-specific code.

- Attributes: Custom attributes used for validation, middleware or other purposes.
- Factories: Factories to create instances of services.
- Interfaces: Contracts for the services that the application layer depends on.
- Service: Implementation of the application services interfaces.

***Domain Layer (HikingTracks.Domain):***

This layer represents the core of the business logic.

- DTOs: Data Transfer Objects used for communication between layers.
- Entities: Core business entities that represent the data.
- Exceptions: Custom exceptions used within the domain layer.
- Interfaces: Contracts for repositories and other.

***Infrastructure Layer (HikingTracks.Infrastructure):***

This layer handles communication with external services - database, migrations.

- Factories: Factories to create instances of repositories.
- HikingTracksContext.cs: The database context for Entity Framework Core.
- Migrations: Database migration files.
- Repositories: Implementation of the repository interfaces in the domain layer.

***Presentation Layer (HikingTracks.Presentation):***

This layer handles the API endpoints and HTTP requests.

- Controllers: API controllers that handle incoming HTTP requests and return responses.
- Extensions: Extension methods for configuring services and middleware.
- Middleware: Custom middleware components for request processing.
- Program.cs: Entry point of the application.
