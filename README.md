# Calendar API

This project implements a simple multi‑calendar date calculation service based on the BRD requirements. It allows posting a start date and interval specification and returns the corresponding dates across several calendar systems.

## Prerequisites
- .NET SDK 7.0 or later
- SQLite (default connection string is `Data Source=calendar.db`)

## Build and run
```
dotnet build

dotnet run --project Calendar.Api
```
The API will start listening on the default ASP.NET Core port.

## Example usage
Submit a calculation request using `curl`:
```bash
curl -X POST http://localhost:5000/api/calculations \
  -H "Content-Type: application/json" \
  -d '{"startDate":"2023-01-01","intervalType":0,"intervalValue":3,"direction":0}'
```
The response will include the generated dates for the next three days in all supported calendars.

Migrations are not included because `dotnet ef` tools were unavailable in this environment.
