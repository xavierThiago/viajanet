# Analytics API

## **POST** /api/v1/analytics

### REQUEST
```json
{
	"pageName": "home",
	"vendor": {
		"name": "Firefox",
		"version": "71.0"
	},
	"parameters": {
		"test": [ 1 ]
	}
}
```

### RESPONSE **201**
```json
{
	"status": true,
	"message": "Analytic data saved successfully."
}
```

### RESPONSE **401**
```json
{
	"status": false,
	"message": "Authentication is required."
}
```

### RESPONSE **403**
```json
{
	"status": false,
	"message": "Could not complete request. User was rejected."
}
```

### RESPONSE **500**
```json

{
	"status": false,
	"message": "Oops! An unexpected error occurred. Analytics hit was not saved. Please, try again."
}
```

## **GET** /api/v1/analytics?ip=127.0.0.1

### RESPONSE **200**
```json
{
	"status": true,
	"message": "Analytic data found.",
	"result": [
		{
			"ip": "127.0.0.1",
			"vendor": {
				"name": "Firefox",
				"version": "73"
			},
			"parameters": {
				"foo": [
					1,
					2
				],
				"bar": [
					false
				]
			}
		}
	]
}
```

### RESPONSE **401**
```json
{
	"status": false,
	"message": "Authentication is required."
}
```

### RESPONSE **404**
```json
{
	"status": false,
	"message": "Not data found for given search criteria."
}
```

### RESPONSE **500**
```json

{
	"status": false,
	"message": "Oops! An unexpected error occurred. Analytics hit was not saved. Please, try again."
}
```

## **GET** /api/v1/analytics?pagename=home

### RESPONSE **200**
```json
{
	"status": true,
	"message": "Analytic data found.",
	"result": [
		{
			"ip": "127.0.0.1",
			"vendor": {
				"name": "Firefox",
				"version": "73"
			},
			"parameters": {
				"foo": [
					1,
					2
				],
				"bar": [
					true
				]
			}
		}
	]
}
```

### RESPONSE **401**
```json
{
	"status": false,
	"message": "Authentication is required."
}
```

### RESPONSE **404**
```json
{
	"status": false,
	"message": "Not data found for given search criteria."
}
```

### RESPONSE **500**
```json

{
	"status": false,
	"message": "Oops! An unexpected error occurred. Analytics hit was not saved. Please, try again."
}
```

## **GET** /api/v1/analytics?ip=127.0.0.1&pagename=home

### RESPONSE **200**
```json
{
	"status": true,
	"message": "Analytic data found.",
	"result": [
		{
			"ip": "127.0.0.1",
			"vendor": {
				"name": "Firefox",
				"version": "73"
			},
			"parameters": {
				"foo": [
					1,
					2
				],
				"bar": [
					true
				]
			}
		}
	]
}
```

### RESPONSE **401**
```json
{
	"status": false,
	"message": "Authentication is required."
}
```

### RESPONSE **404**
```json
{
	"status": false,
	"message": "Not data found for given search criteria."
}
```

### RESPONSE **500**
```json

{
	"status": false,
	"message": "Oops! An unexpected error occurred. Analytics hit was not saved. Please, try again."
}
```
