

 dotnet ef --startup-project "../amorphie.contract/amorphie.contract.csproj" migrations add ContractMigrationsv4 --context ProjectDbContext --output-dir Migrations/Pg

 
dotnet ef database update --startup-project "../amorphie.contract/amorphie.contract.csproj"  --context ProjectDbContext

dotnet ef database update ContractMigrationsv4 --startup-project "../amorphie.contract/amorphie.contract.csproj"  --context ProjectDbContext
 