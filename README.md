# Movie Collection REST Web API
Welcome to the Movie Collection REST Web API! This API allows users to manage their movie collections, including adding, removing, and sharing movies with other users.

## Features
- Add Movies: Users can add new movies to their collection.
- Remove Movies: Users can remove movies from their collection.
- Share Movies: Users can view movies from other users collection .
## Technologies Used
- ASP.NET Core 8
- SQL Server
- Entity Framework Core with Fluent Migrator
## Installation
To install and run the project locally, follow these steps:

- Clone the repository to your local machine:
``` bash
git clone https://github.com/pushprajvitekar/movie-collection-api.git
```
- Navigate to the project directory:

``` bash
cd Presentation\MoviesCollectionWebApi
```
-- Install dependencies:

``` bash
dotnet restore
```
-- Set up your SQL Server database connection in the appsettings.json file.

-- Apply migrations to create the database schema:

```sql
cd Infrastructure\Persistence\FluentMigrator
dotnet fm migrate -p "DatabaseMigrations"
```
-- Run the application:
``` arduino
dotnet run
```
The API will start running locally on http://localhost:5000.

## Usage
### Authentication
Describe any authentication mechanisms used by the API, such as JWT tokens or OAuth.

## Endpoints
- GET /api/movies: Retrieve all movies in the collection.
- POST /api/movies: Add a new movie to the collection.
- DELETE /api/movies/{id}: Remove a movie from the collection by its ID.
- POST /api/movies/share: Share a movie with another user.
  Provide detailed explanations of each endpoint, including request parameters, request bodies, and response formats.

## Documentation
Include any additional documentation for the API, such as API reference guides or Swagger documentation.

## License
This project is licensed under the MIT License. See the LICENSE file for details.

## Contact
If you have any questions, feedback, or issues, please feel free to contact us at your-email@example.com.
