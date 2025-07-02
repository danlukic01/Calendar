
# Calendar

This project will contain a multi-calendar date calculation library and an ASP.NET Core application for storing calculated intervals. It aims to unify date operations across calendars such as Gregorian and Julian while persisting interval results in a database.

## Prerequisites

- .NET SDK 8.0 or newer
- Access to a relational database (e.g., SQL Server or PostgreSQL) for interval storage

## Building and running

Use the .NET CLI to build and run the application once the ASP.NET Core project is in place:

```bash
# Restore dependencies and compile the project
 dotnet build

# Run the web application
 dotnet run
```

The app will start locally (by default on `http://localhost:5000` or similar). API endpoints will provide calendar calculations and persist intervals in the configured database.

## Example date calculation

After running the application, you can trigger a sample calculation with a command such as:

```bash
curl -X POST \
  http://localhost:5000/api/date/calculate \
  -H "Content-Type: application/json" \
  -d '{"from":"2025-01-01","to":"2025-02-01","calendar":"Gregorian"}'
```

The service will return the number of days between the dates and store the interval record.
=======
# Calendar API

This repository contains a basic ASP.NET Core Web API project with placeholder controllers and data models. The project demonstrates an EF Core setup using SQLite for persistence. Migrations were not generated because .NET tooling isn't available in this environment.

