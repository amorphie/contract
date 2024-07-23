DROP INDEX IF EXISTS "Doc"."IX_DocumentMigrationProcessing_DocId";

CREATE INDEX "IX_DocumentMigrationProcessing_DocId"
ON "Doc"."DocumentMigrationProcessing" ("DocId");
