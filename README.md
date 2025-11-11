Simple .NET Core Web API using PostgreSQL + EF Core to manage students, groups and subjects.

What it does
- Basic CRUD for students
- Management of groups, subjects, and their relations
- Filtering + pagination
- DNI/NIE validation
- Data seeding with Bogus (tens of thousands of test records)

Tech stack
- ASP.NET Core Web API
- Entity Framework Core (Npgsql)
- PostgreSQL
- Layered architecture (Controllers → Services → Interfaces)
- User Secrets for hiding DB credentials

How to run
- Configure User Secrets: dotnet user-secrets set "ConnectionStrings:EscuelaDb" "Host=...;Database=...;Username=...;Password=..."
- Run the API
- Seeder automatically creates sample data (20k+ students and relations)
