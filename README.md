# ASP.NET Core Web API: Books and JWT-Secured Products

Two small REST APIs in one project:

1. A **Book API** with full CRUD, strong validation and filters
2. A **Product API** protected with **JWT authentication** and role-based authorization

Swagger is included for testing the endpoints in the browser.

## Book API

Base route: `api/Books`

| Method | Endpoint | What it does |
|---|---|---|
| GET | `/RetrievalOfAllBooks` | Get all books |
| GET | `/BookById/{id}` | Get one book |
| GET | `/BookByGenre/{genre}` | Filter by genre |
| GET | `/BooksByPriceRange/{minPrice}/{maxPrice}` | Filter by price range |
| POST | `/AddBook` | Add a book |
| PUT | `/UpdateBook/{id}` | Update a book |
| DELETE | `/{id}` | Delete a book |

**Validation rules**

- Title: required, 1 to 200 characters
- Author: letters and spaces only, at least 2 characters
- ISBN: exactly 13 digits, and it must be unique (a duplicate returns `409 Conflict`)
- Genre: must be one of Fiction, Non-Fiction, Science, Technology, History, Biography, Fantasy, Mystery, Romance, Thriller
- Price: between 0.01 and 10,000, with at most 2 decimal places
- Publication year: between 1800 and 2024
- Quantity: not negative

## Product API (JWT secured)

Base route: `api/Products`. All endpoints need a valid token.

| Method | Endpoint | Access |
|---|---|---|
| POST | `/api/Auth/login` | Public. Returns a JWT token |
| GET | `/GetById/{id}` | Any logged-in user |
| POST | `/Create` | Any logged-in user |
| DELETE | `/Delete/{id}` | **Admin role only** |

## How JWT Authentication Works

1. The client sends a username and password to `/api/Auth/login`.
2. If they are correct, the server creates a signed token that holds the user's role and an expiry time.
3. The client sends this token in the `Authorization: Bearer <token>` header with each request.
4. The server checks the signature, issuer, audience and expiry before allowing access.
5. Endpoints marked `[Authorize(Roles = "Admin")]` also check the role inside the token.

## Concepts Used

- RESTful API design with the right HTTP status codes (200, 201, 204, 400, 404, 409, 500)
- Model validation with data annotations and a custom `GenreValidation` attribute
- A service class (`BookService`) that separates business logic from the controller
- JWT bearer authentication and role-based authorization
- Swagger / OpenAPI documentation

## Notes

- Data is stored **in memory**, so there is no database and data is lost when the app restarts.
- The login uses a fixed demo account for learning purposes only. Do not use this approach in a real application.
- Replace the JWT key in `appsettings.json` with your own secret before running.

## Built With

- C#
- ASP.NET Core Web API
- JWT Bearer Authentication
- Swagger

## About

Made for the Web Technologies / SCD coursework at the University of the Punjab.
