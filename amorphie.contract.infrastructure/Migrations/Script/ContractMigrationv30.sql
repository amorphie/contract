DROP INDEX IF EXISTS "Doc"."IX_DocumentMigrationProcessing_DocId";

CREATE INDEX "IX_DocumentMigrationProcessing_DocId"
ON "Doc"."DocumentMigrationProcessing" ("DocId");

-- Insert new migration record into EF Migration History table
INSERT INTO "public"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240711121307_ContractMigrationsv30', '8.0.4');