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
