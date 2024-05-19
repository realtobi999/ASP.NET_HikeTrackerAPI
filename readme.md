# **HikingTracks Api Project** - NOT FINISHED, IN PROGRESS

## **Summary**

## **Endpoints Overview**

Overview of all endpoints using Swagger. In total there is **19** endpoints.

![swagger](https://cdn.discordapp.com/attachments/697505581375946833/1241820690450087966/image.png?ex=664b9724&is=664a45a4&hm=e6e8244381a49175abba5308c8f7abac9fd196596ead3f1fa4184208866451ad&)

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