ALTER TABLE "Doc"."DocumentMigrationDysDocumentTags"
ADD COLUMN "TagValuesOrg" TEXT NOT NULL DEFAULT '';

-- Insert new migration record into EF Migration History table
INSERT INTO "public"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240718114709_ContractMigrationsv31', '8.0.4');