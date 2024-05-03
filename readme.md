# HikingTracks Api Project

## Summary

## Account Endpoints

### `GET /api/account`

Retrieve a list of all accounts.

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

## Hike Endpoints

### `GET /api/hike`

Retrieve a list of all hikes.

### Response

```json
[
  {
    "id": "ad31e37d-cfb2-4b6d-bfc8-e577849f6265",
    "accountID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "distance": 10.5,
    "elevationGain": 200,
    "elevationLoss": 150,
    "averageSpeed": 10.5,
    "maxSpeed": 15.5,
    "movingTime": "01:30:00",
    "coordinates": [
      {
        "latitude": 37.7749,
        "longitude": -122.4194
      },
      {
        "latitude": 40.7128,
        "longitude": -74.006
      },
      {
        "latitude": 34.0522,
        "longitude": -118.2437
      },
      {
        "latitude": 51.5074,
        "longitude": -0.1278
      }
    ]
  }
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
[
  {
    "id": "ad31e37d-cfb2-4b6d-bfc8-e577849f6265",
    "accountID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "distance": 10.5,
    "elevationGain": 200,
    "elevationLoss": 150,
    "averageSpeed": 10.5,
    "maxSpeed": 15.5,
    "movingTime": "01:30:00",
    "coordinates": [
      {
        "latitude": 37.7749,
        "longitude": -122.4194
      },
      {
        "latitude": 40.7128,
        "longitude": -74.006
      },
      {
        "latitude": 34.0522,
        "longitude": -118.2437
      },
      {
        "latitude": 51.5074,
        "longitude": -0.1278
      }
    ]
  }
]
```

`200` - Status OK

---

### `POST /api/account/{account_id}/hike`

Create a new hike with the provided details.

### Parameters

- `account_id` : The id of the account.

### Request Body

``` json

{
    "Distance": "int",
    "ElevationGain": "int",
    "ElevationLoss": "int",
    "MaxSpeed": "int",
    "MovingTime": "string (format: 01:30:05)",
    "Coordinates": [
        { "latitude": "int", "longitude": "int" },
        { "latitude": "int", "longitude": "int" },
        { "latitude": "int", "longitude": "int" },
        { "latitude": "int", "longitude": "int" }
    ]
}

```

### Response

`201` - Status Created

---

### `DELETE /api/hike/{hike_id}`

Delete an existing hike.

### Parameters

- `hike_id` : The id of the hike.

### Response

`200` - Status OK