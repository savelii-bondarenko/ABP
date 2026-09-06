# Conference Room Booking API

Hey! This is my solution for the Conference Room Booking API test task.

The main goal of this project is to provide a simple and scalable API for a company that rents out conference rooms to businesses. The API allows clients to search for available rooms, manage bookings, and automatically calculate the final rental cost based on the time of day and selected extra services.

I built this using **.NET 10** with a strict **N-Tier architecture** to keep the code clean, testable, and easy to scale in the future.

## Tech Stack

* **Framework:** .NET 10 (ASP.NET Core Minimal APIs)
* **Database:** PostgreSQL
* **ORM:** Entity Framework Core
* **Architecture:** N-Tier (Controllers, Business Logic, Data Access), Repository Pattern
* **Mapping:** AutoMapper
* **Testing:** xUnit, Moq

## Features

* **Rooms Management:** Full CRUD for conference rooms (name, capacity, base price per hour)
* **Additional Services:** CRUD for extra stuff like Projectors, Wi-Fi, etc
* **Smart Booking System:** Checks for time overlaps so you can't book a room that's already taken
* **Dynamic Price Calculator:** Automatically calculates the total price based on business rules:
  * Standard hours (09:00 - 18:00): Base price
  * Morning hours (06:00 - 09:00): 10% discount
  * Peak hours (12:00 - 14:00): 15% markup
  * Evening hours (18:00 - 23:00): 20% discount
* **Business Analytics:** A reporting endpoint that calculates total revenue and shows the most popular rooms for a given period
* **Robust Error Handling (RFC 7807):** Implemented a Global Exception Handler pipeline. Instead of raw stack traces, the API catches exceptions globally and returns standardized, predictable `ProblemDetails` JSON responses (following the RFC 7807 / 9457 standards)

## Project Structure

* `Controllers` — The API entry point. Contains Minimal API endpoints, Global Exception Handler middleware, and configuration.
* `BusinessLogic` — The core of the app. Services, DTOs, AutoMapper profiles, and the custom Price Calculator.
* `DataAccess` — Everything related to the database. Entities, EF Core DbContext, Configurations, and Repositories.
* `BusinessLogic.Tests` — Unit tests for the services (especially the booking overlap logic and price calculations).

---

## 🛠️ How to Run (Step-by-Step Guide)

If you just want to run this locally and test it out, follow these steps:

### 1. Prerequisites

Make sure you have installed:

* [.NET 10 SDK](https://dotnet.microsoft.com/download)
* [PostgreSQL](https://www.postgresql.org/download/) (running on your machine)

### 2. Configure the Database

1. Open the project in your IDE (Visual Studio, Rider, or VS Code).
2. Go to the `Controllers` folder and find `appsettings.json` (or check `Program.cs` where the connection string is built).
3. Make sure the PostgreSQL credentials match your local database setup (Host, Port, Database name, Username, and Password).

### 3. Apply Migrations

Open your terminal in the root folder of the solution and run the following command to create the database tables:

```bash
dotnet ef database update --project DataAccess --startup-project Controllers
```

### 4. Run the Application

Start the API by running the following command from the root folder:

```bash
dotnet run --project Controllers
```

### 5. Test it out!

Once the app is running, open your web browser and go to:

`http://localhost:<your-port>/swagger`

You will see the Swagger UI where you can easily test all the endpoints.

**Pro tip for testing:** Create an `Additional Service` first, then create a `Room`, and finally create a `Booking` using their IDs to see the price calculator in action!

---

## Running Tests

The core business logic is heavily covered by unit tests. This includes the price calculator, booking overlap prevention, and fully isolated CRUD testing for components like `AdditionalServiceService` using `Moq`.

To run the tests, use this command in the root folder:

```bash
dotnet test
```
