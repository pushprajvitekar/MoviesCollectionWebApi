# Movie Collection REST Web API
  Welcome to the Movie Collection REST Web API! 
  This API allows users to manage their movie collections, including adding, removing, and sharing movies with other users.

## Features
- Add Movies: Users can add new movies to their collection.
- Remove Movies: Users can remove movies from their collection.
- Share Movies: Users can view movies from other users collection .
## Technologies Used
- ASP.NET Core 8 Web Api
- SQL Server 2022
- Entity Framework Core
- Fluent Migrator Migrations Console
## Installation
To install and run the project locally, follow these steps:

- Clone the repository to your local machine:
``` bash
git clone https://github.com/pushprajvitekar/moviesmcollectionwebapi.git
```
- Navigate to the project directory:

``` bash
cd Presentation\MoviesCollectionWebApi
```
- Install dependencies:

``` bash
dotnet restore
```
- Set up your SQL Server database connection in the appsettings.json file.

- Apply migrations to create the database schema:
- Set up your SQL Server database connection in the appsettings.json file.
```sql
cd Infrastructure\Persistence\FluentMigrator
dotnet fm migrate -p "DatabaseMigrations"
```
- Run the application:
``` arduino
dotnet run
```
The API will start running locally on http://localhost:5054.

## Usage
### Authentication
  Uses JWT claims based authentication to login user and restrict access to endpoints 
### Authorization
  - Roles
    - Admin
      - can register another admin ,
      - add /update /remove movie from master movie collection
      - add /remove movie from a user collection
    - User
      - can view all users
      - can view all movies
      - can view other user movies
      - can add / remove movies from own collection   

## Endpoints
- POST /api/authenticate/register-admin register admin user
- POST /api/authenticate/register register user to enable login
- POST /api/authenticate/login login to gain access with token
- GET /api/movies: Retrieve all movies in the collection.
- POST /api/movies: Add a new movie to the collection.
- DELETE /api/movies/{id}: Remove a movie from the collection by its ID.
- GET /api/users/: View all users registered.
- GET /api/users/{username} View all movies added by the user
- POST /api/users/{username} add movie to your collection
- DELETE /api/users/{username} remove movie from your collection
  

## Documentation
Once the application is running, navigate to the Swagger UI in your web browser:
``` bash
http://localhost:5054/swagger
```
You should now see the Swagger UI interface, which lists all available endpoints, request parameters, request bodies, and response formats. 
Use the Swagger UI to explore and interact with the API.

## License
This project is licensed under the MIT License. See the LICENSE file for details.

## Contact
If you have any questions, feedback, or issues, please feel free to contact us at pushpraj.vitekar@gmail.com.
