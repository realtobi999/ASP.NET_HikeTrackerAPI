# HikingTracks Api Project - NOT FINISHED, IN PROGRESS

## Summary

## Account Endpoints

### `GET /api/account`

Retrieve a list of all accounts.

### Parameters

- `limit` & `offset` : Pagination.

### Response

``` json
[
  {
    "id": "1c62211f-0451-4e13-8ddf-6a6de9284325",
    "username": "tobinek",
    "email": "tobiasfilgas@gmai.com",
    "totalHikes": 14,
    "totalDistance": 15.25,
    "totalMovingTime": "00:00:00",
    "createdAt": "2024-04-29T13:02:17.007955+00:00"
  }
]
```

`200` - Status OK

---

### `GET /api/account/{account_id}`

Retrieve a account by it's ID.

### Parameters

- `account_id` : The id of the account.

### Response

``` json
[
  {
    "id": "1c62211f-0451-4e13-8ddf-6a6de9284325",
    "username": "tobinek",
    "email": "tobiasfilgas@gmai.com",
    "totalHikes": 14,
    "totalDistance": 15.25,
    "totalMovingTime": "00:00:00",
    "createdAt": "2024-04-29T13:02:17.007955+00:00"
  }
]
```

`200` - Status OK

---

### `POST /api/account`

Create a new account with the provided details.

### Request Body

``` json
{
  "Username": "string",
  "Email": "string",
  "Password": "string"
}
```

### Response

``` json
{
  "token": "1c62211f-0451-4e13-8ddf-6a6de9284325"
}
```

`201` - Status Created

---

### `PUT /api/account/{account_id}`

Update an existing account's information.

### Parameters

- `account_id` : The id of the account.

### Request Body

```json
{
  "Username": "string",
  "Email": "string",
  "TotalHikes": "int",
  "TotalDistance": "int",
  "TotalMovingTime": "string (time format)"
}
```

### Response

`200` - Status OK

---

### `DELETE /api/account/{account_id}`

Delete an existing account.

### Parameters

- `account_id` : The id of the account.

### Response

`200` - Status OK

## Auth Endpoints

### `POST /api/login`

Creates a new JWT token for authentication.

### Request Body

```json
{
  "Email": "string",
  "Password": "string"
}
```

### Response

```json
{
  "token": "very long token"
}
```

`200` - Status OK

## Hike Endpoints

### `GET /api/hike`

Retrieve a list of all hikes.

### Parameters

- `account_id` : The id of the account to filter by.
- `limit` & `offset` : Pagination.

### Response

```json
[
  {
    "id": "5819c4d2-7535-4df8-a17b-51d7cd294731",
    "accountId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "distance": 10.5,
    "elevationGain": 200,
    "elevationLoss": 150,
    "averageSpeed": 0.0019444444444444444,
    "maxSpeed": 15.5,
    "movingTime": "01:30:00",
    "coordinates": [
      {
        "latitude": 37.7749,
        "longitude": -122.4194,
        "elevation": 0
      },
      {
        "latitude": 40.7128,
        "longitude": -74.006,
        "elevation": 0
      },
      {
        "latitude": 34.0522,
        "longitude": -118.2437,
        "elevation": 0
      },
      {
        "latitude": 51.5074,
        "longitude": -0.1278,
        "elevation": 0
      }
    ],
    "createdAt": "2024-05-13T20:56:50.529272+00:00",
    "photos": [],
    "segments": []
  },
]
```

`200` - Status OK

---

### `GET /api/hike/{hike_id}`

Retrieve a hike by it's ID.

### Parameters

- `hike_id` : The id of the hike.

### Response

```json
{
  "id": "5819c4d2-7535-4df8-a17b-51d7cd294731",
  "accountId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "distance": 10.5,
  "elevationGain": 200,
  "elevationLoss": 150,
  "averageSpeed": 0.0019444444444444444,
  "maxSpeed": 15.5,
  "movingTime": "01:30:00",
  "coordinates": [
    {
      "latitude": 37.7749,
      "longitude": -122.4194,
      "elevation": 0
    },
    {
      "latitude": 40.7128,
      "longitude": -74.006,
      "elevation": 0
    },
    {
      "latitude": 34.0522,
      "longitude": -118.2437,
      "elevation": 0
    },
    {
      "latitude": 51.5074,
      "longitude": -0.1278,
      "elevation": 0
    }
  ],
  "createdAt": "2024-05-13T20:56:50.529272+00:00",
  "photos": [],
  "segments": []
}
```

`200` - Status OK

---

### `POST /api/hike`

Create a new hike with the provided details.

### Parameters

- `account_id` : The id of the account.

### Request Body

``` json
{
  "Id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "AccountId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "Title": "string",
  "Description": "string",
  "Distance": 0,
  "ElevationGain": 0,
  "ElevationLoss": 0,
  "MaxSpeed": 0,
  "MovingTime": "string", 
  "Coordinates": [
    { "Latitude": 0, "Longitude": 0, "Elevation": 0 }
    { "Latitude": 0, "Longitude": 0, "Elevation": 0 }
    { "Latitude": 0, "Longitude": 0, "Elevation": 0 }
  ]
}
```

### Response

`201` - Status Created

---

### `POST /api/hike/{hike_id}/photo/upload`

Uploads photo provided in the body to the specified hike.

### Parameters

- `hike_id` : The id of the hike.

### Request Body

Provide body as a form with the files in column 'files'

### Response 

`200` - Status Ok

---

### `POST /api/hike/{hike_id}/segment/upload`

Assigns segments to a specified hikes that the hike went trough.

### Parameters

- `hike_id` : The id of the hike.

### Response

`200` - Status OK

---

### `DELETE /api/hike/{hike_id}`

Delete an existing hike.

### Parameters

- `hike_id` : The id of the hike.

### Response

`200` - Status OK

## Segment Endpoints

### `GET /api/segment`

Retrieve a list of all segments.

### Parameters

- `limit` & `offset` : Pagination.

### Response

``` json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "name": "string",
    "distance": 0,
    "elevationGain": 0,
    "elevationLoss": 0,
    "coordinates": [
      {
        "latitude": 0,
        "longitude": 0,
        "elevation": 0
      }
    ],
    "createdAt": "2024-05-18T17:03:10.091087+00:00"
  }
]
```

`200` - Status OK

---

### `GET /api/segment/{segment_id}`

Retrieve a segment by it's ID.

### Parameters

- `segment_id` : The id of the segment.

### Response

``` json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "string",
  "distance": 0,
  "elevationGain": 0,
  "elevationLoss": 0,
  "coordinates": [
    {
      "latitude": 0,
      "longitude": 0,
      "elevation": 0
    }
  ],
  "createdAt": "2024-05-18T17:03:10.091087+00:00"
}
```

`200` - Status OK

---

### `POST /api/segment`

Create a new segment with the provided details.

### Request Body

``` json
{
  "Id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "Name": "string",
  "Distance": 0,
  "ElevationGain": 0,
  "ElevationLoss": 0,
  "Coordinates": [
    { "Latitude": 0, "Longitude": 0, "Elevation": 0 }
  ]
}
```

### Response

`201` - Status Created

---

### `PUT /api/segment/{segment_id}`

Update an existing segment's information.

### Parameters

- `segment` : The id of the segment.

### Request Body

```json
{
  "Id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "Name": "string",
  "Distance": 0,
  "ElevationGain": 0,
  "ElevationLoss": 0,
  "Coordinates": [
    { "Latitude": 0, "Longitude": 0, "Elevation": 0 }
  ]
}
```

### Response

`200` - Status OK

---

### `DELETE /api/segment/{segment_id}`

Delete an existing segment.

### Parameters

- `segment_id` : The id of the segment.

### Response

`200` - Status OK