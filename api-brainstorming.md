# Analytics API

## **POST** /api/v1/analytics

### REQUEST
```json
{
	"page": "home",
	"browser": "Firefox",
	"data": {
		"test": 1
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
	"message": "Could not complete request. User was not authenticated."
}
```

### RESPONSE **500**
```json

{
	"status": true,
	"message": "Analytic data saved successfully."
}
```

## **GET** /api/v1/analytics?ip=127.0.0.0

### RESPONSE **200**
```json
{
	"status": true,
	"message": "Analytic data saved successfully.",
	"data": [
		{
			"ip": "127.0.0.0",
			"vendor": {
				"name": "Firefox",
				"version": "73"
			},
			"parameters": {
				"foo": [
					1,
					2
				],
				"bar": false
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

## **GET** /api/v1/analytics?page=home

### RESPONSE **200**
```json
{
	"status": true,
	"message": "Analytic data saved successfully.",
	"data": [
		{
			"ip": "127.0.0.0",
			"vendor": {
				"name": "Firefox",
				"version": "73"
			},
			"parameters": {
				"foo": [
					1,
					2
				],
				"bar": false
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
