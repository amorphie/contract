DROP INDEX IF EXISTS "Cont"."IX_ContractDefinition_Code";
CREATE UNIQUE INDEX "IX_DocumentDefinition_Code_Semver" ON "Doc"."DocumentDefinition" ("Code", "Semver");
CREATE UNIQUE INDEX "IX_ContractDefinition_Code_BankEntity" ON "Cont"."ContractDefinition" ("Code", "BankEntity");