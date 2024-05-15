dotnet ef --startup-project "../amorphie.contract/amorphie.contract.csproj" migrations add ContractMigrationsv25 --context ProjectDbContext --output-dir Migrations/Pg

dotnet ef database update --startup-project "../amorphie.contract/amorphie.contract.csproj"  --context ProjectDbContext

dotnet ef database update ContractMigrationsv24 --startup-project "../amorphie.contract/amorphie.contract.csproj"  --context ProjectDbContext
 