

 dotnet ef --startup-project "../amorphie.contract.data/amorphie.contract.data.csproj" migrations add MyMigration --context ProjectDbContext --output-dir Migrations/Pg

 
dotnet ef database update --startup-project "../amorphie.contract.data/amorphie.contract.data.csproj"  --context ProjectDbContext

dotnet ef --startup-project "../WebAPI/WebAPI.csproj" migrations add MyMigration --context ProjectDbContext --output-dir Migrations
dotnet ef database update --startup-project "../WebAPI/WebAPI.csproj" --context ProjectDbContext


dotnet ef migrations add InitDatabase --project DataAccess -s WebAPI -c ProjectDbContext  --output-dir Migrations/Pg --verbose 
dotnet ef --startup-project "../WebAPI/WebAPI.csproj" migrations add MyMigration --context ProjectDbContext --output-dir Migrations --project "../DataAccess/DataAccess.csproj.csproj"

// PostgreSQL

$env:ASPNETCORE_ENVIRONMENT='Staging'
Add-Migration InitialCreate --Context ProjectDbContext -OutputDir Migrations/Pg
dotnet ef migrations add InitialCreate --context DataAccess.Concrete.EntityFramework.Contexts.ProjectDbContext -output-dir Migrations/Pg

$env:ASPNETCORE_ENVIRONMENT='Staging'
Update-Database -context ProjectDbContext