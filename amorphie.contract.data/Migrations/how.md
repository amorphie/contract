

 dotnet ef --startup-project "../amorphie.contract/amorphie.contract.csproj" migrations add ContractMigrationsV --context ProjectDbContext --output-dir Migrations/Pg

 
dotnet ef database update --startup-project "../amorphie.contract/amorphie.contract.csproj"  --context ProjectDbContext

 