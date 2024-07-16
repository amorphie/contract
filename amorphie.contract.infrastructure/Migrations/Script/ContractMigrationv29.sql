-- Alter Column
ALTER TABLE "Cus"."Customer" 
ALTER COLUMN "Reference" 
SET NOT NULL,
ALTER COLUMN "Reference" 
SET DEFAULT '';

-- Add Column
ALTER TABLE "Cus"."Customer"
ADD COLUMN "TaxNo" VARCHAR(10);

-- Create Table DocumentMigrationAggregations
CREATE TABLE "Doc"."DocumentMigrationAggregations" (
    "Id" UUID NOT NULL PRIMARY KEY,
    "DocumentCode" VARCHAR(250) NOT NULL,
    "IsDeleted" BOOLEAN NOT NULL,
    "CreatedAt" TIMESTAMPTZ NOT NULL,
    "CreatedBy" UUID NOT NULL,
    "CreatedByBehalfOf" UUID,
    "ModifiedAt" TIMESTAMPTZ NOT NULL,
    "ModifiedBy" UUID NOT NULL,
    "ModifiedByBehalfOf" UUID,
    "ContractCodes" JSONB
);

-- Create Index on DocumentCode
CREATE UNIQUE INDEX "IX_DocumentMigrationAggregations_DocumentCode" 
ON "Doc"."DocumentMigrationAggregations" ("DocumentCode");

-- Create Table DocumentMigrationDysDocuments
CREATE TABLE "Doc"."DocumentMigrationDysDocuments" (
    "Id" UUID NOT NULL PRIMARY KEY,
    "DocId" BIGINT NOT NULL,
    "Title" VARCHAR(350) NOT NULL,
    "Notes" VARCHAR(500) NOT NULL,
    "DocCreatedAt" TIMESTAMPTZ NOT NULL,
    "OwnerId" VARCHAR(250) NOT NULL,
    "Channel" VARCHAR(250) NOT NULL,
    "IsDeleted" BOOLEAN NOT NULL,
    "CreatedAt" TIMESTAMPTZ NOT NULL,
    "CreatedBy" UUID NOT NULL,
    "CreatedByBehalfOf" UUID,
    "ModifiedAt" TIMESTAMPTZ NOT NULL,
    "ModifiedBy" UUID NOT NULL,
    "ModifiedByBehalfOf" UUID
);

-- Create Index on DocId
CREATE UNIQUE INDEX "IX_DocumentMigrationDysDocuments_DocId"
ON "Doc"."DocumentMigrationDysDocuments" ("DocId");

-- Create Table DocumentMigrationDysDocumentTags
CREATE TABLE "Doc"."DocumentMigrationDysDocumentTags" (
    "Id" UUID NOT NULL PRIMARY KEY,
    "DocId" BIGINT NOT NULL,
    "TagId" VARCHAR(350) NOT NULL,
    "TagValues" JSONB NOT NULL,
    "IsDeleted" BOOLEAN NOT NULL,
    "CreatedAt" TIMESTAMPTZ NOT NULL,
    "CreatedBy" UUID NOT NULL,
    "CreatedByBehalfOf" UUID,
    "ModifiedAt" TIMESTAMPTZ NOT NULL,
    "ModifiedBy" UUID NOT NULL,
    "ModifiedByBehalfOf" UUID
);

-- Create Index on DocId
CREATE UNIQUE INDEX "IX_DocumentMigrationDysDocumentTags_DocId"
ON "Doc"."DocumentMigrationDysDocumentTags" ("DocId");

-- Create Index on TagId
CREATE INDEX "IX_DocumentMigrationDysDocumentTags_TagId"
ON "Doc"."DocumentMigrationDysDocumentTags" ("TagId");

-- Create Table DocumentMigrationProcessing
CREATE TABLE "Doc"."DocumentMigrationProcessing" (
    "Id" UUID NOT NULL PRIMARY KEY,
    "DocId" BIGINT NOT NULL,
    "TagId" VARCHAR(350) NOT NULL,
    "Status" VARCHAR(50) NOT NULL,
    "TryCount" INTEGER NOT NULL,
    "LastTryTime" TIMESTAMPTZ,
    "ErrorMessage" VARCHAR(500),
    "IsDeleted" BOOLEAN NOT NULL,
    "CreatedAt" TIMESTAMPTZ NOT NULL,
    "CreatedBy" UUID NOT NULL,
    "CreatedByBehalfOf" UUID,
    "ModifiedAt" TIMESTAMPTZ NOT NULL,
    "ModifiedBy" UUID NOT NULL,
    "ModifiedByBehalfOf" UUID
);

-- Create Index on DocId
CREATE UNIQUE INDEX "IX_DocumentMigrationProcessing_DocId"
ON "Doc"."DocumentMigrationProcessing" ("DocId");

-- Create Index on TagId
CREATE INDEX "IX_DocumentMigrationProcessing_TagId"
ON "Doc"."DocumentMigrationProcessing" ("TagId");
