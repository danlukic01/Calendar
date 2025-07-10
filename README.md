# Calendar API

This project implements a simple multi‑calendar date calculation service based on the BRD requirements. It allows posting a start date and interval specification and returns the corresponding dates across several calendar systems.

Date conversions for the Mayan Long Count, Tzolkin and Haab calendars use
algorithms derived from the open source calendar utilities published by
Fourmilab.

## Prerequisites
- .NET SDK 7.0 or later
 - SQL Server (default connection string uses a standard `Server=...;Database=...` format)

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

### Get current date conversion
```
curl http://localhost:5000/api/date/current
```
Returns the current date expressed in Gregorian, Julian, Hebrew, Mayan (Long Count),
Tzolkin and Haab forms.

Migrations are not included because `dotnet ef` tools were unavailable in this
environment. When running against a new SQL Server instance, create the initial
schema with EF Core migrations:

```bash
dotnet ef migrations add InitialCreate -p Calendar.Api -s Calendar.Api
dotnet ef database update -p Calendar.Api -s Calendar.Api
```

### Entering lotto numbers
Navigate to `/lotto-entry.html` after the API starts to record Powerball numbers.
Entries are saved via the `/api/lottoentries` endpoint. If running with a fresh
database, create a migration for the `LottoEntries` table and apply it:

```bash
dotnet ef migrations add AddLottoEntries -p Calendar.Api -s Calendar.Api
dotnet ef database update -p Calendar.Api -s Calendar.Api
```

Viewing predictions on `/index.html` now records every calculated lotto number.
Both matched and unmatched numbers are stored via the `/api/lottomatches`
endpoint and can be queried by lotto name, draw date and match status.
