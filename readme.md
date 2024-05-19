# **HikingTracks Api Project** - NOT FINISHED, IN PROGRESS

## **Summary**

## **Endpoints Overview**

Overview of all endpoints using Swagger. In total there is **18** endpoints.

![swagger](https://cdn.discordapp.com/attachments/697505581375946833/1241736205809488022/image.png?ex=664b4875&is=6649f6f5&hm=4bec5cbc586dfdadc252b67329f0051c4bc3c0cbecb9ae32d01bb011b8876572&)

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