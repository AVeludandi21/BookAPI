# BookApi

This is a simple web API built with ASP.NET Core. It lets you manage a list of books and get a weather forecast. All data is stored in memory (no database).

## Features

- Add, view, update, and delete books (CRUD)
- Get a weather forecast
- No database needed
- Checks that book data is valid
- Has unit tests
- API is documented and testable with Swagger UI

## About the Book API

- The Book API is a web service built with ASP.NET Core that lets you manage a list of books.
- It supports basic CRUD operations: you can add, view, update, and delete books using HTTP requests.
- Each book has an ID, title, publisher, and subject. The API checks that the data you send is valid.
- All book data is stored in memory, so it resets when the application restarts (no database is used).
- The API also includes a weather forecast endpoint that returns random weather data for the next 5 days.
- There are unit tests included to make sure the API works as expected.
- You interact with the API using endpoints like `/api/book` for books and `/weatherforecast` for weather data.

### Used

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Link

https://github.com/AVeludandi/BookAPI-AVeludandi.git  

## API Endpoints

- `GET /api/book` - List all books
- `GET /api/book/{id}` - Get a book by ID
- `POST /api/book` - Add a new book
- `PUT /api/book/{id}` - Update a book
- `DELETE /api/book/{id}` - Delete a book
- `GET /weatherforecast` - Get weather forecast

