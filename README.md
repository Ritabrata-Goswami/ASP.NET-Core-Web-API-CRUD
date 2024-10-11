## Description
That CRUD application is developed using EF Core ORM framework.
Insted using LINQ, We have used Raw SQL Queries and SP.
For example incase of DML query:-
```
var parameters = new[]
{
    new SqlParameter("@StudentName", student.StudentName),
    new SqlParameter("@Class", student.Class),
    new SqlParameter("@DoB", student.DoB)
};

_context.Database.ExecuteSqlRawAsync("EXEC spInsertStudent @StudentName, @Class, @DoB", parameters);
```
Or,
```
var Id = new SqlParameter("@id", id);
_context.Database.ExecuteSqlRawAsync("DELETE FROM Student WHERE Id=@id", Id);
```

Incase of DQL operation:-
```
_context.Students.FromSqlRaw("SELECT * FROM Student").ToListAsync();
```

Inside the StudentController.cs all api's are written.


### Database
For database i'm using SQL Server with windows authentication.
The connection string use inside appsettings.json is:- 
```
"ConnectionStrings": {
    "Demo": "Server=DESKTOP-7H6L3KN;Database=Demo;Integrated Security=True;TrustServerCertificate=True;"
  }
```

After running the application Swagger will open with all listed API endpoints.
To exchange data you have to insert POST JSON or GET Id parameter inside every endpoint to check response.
