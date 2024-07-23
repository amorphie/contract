-- Add SendMail column to DocumentGroupDetail table
ALTER TABLE "DocGroup"."DocumentGroupDetail"
ADD COLUMN "SendMail" BOOLEAN NOT NULL DEFAULT FALSE;

-- Add SendMail column to ContractDocumentDetail table
ALTER TABLE "Cont"."ContractDocumentDetail"
ADD COLUMN "SendMail" BOOLEAN NOT NULL DEFAULT FALSE;

-- Create CustomerCommunication table
CREATE TABLE "Cus"."CustomerCommunication" (
    "Id" UUID NOT NULL PRIMARY KEY,
    "CustomerId" UUID NOT NULL,
    "EmailAddress" TEXT NOT NULL,
    "DocumentList" JSONB NOT NULL,
    "IsSuccess" BOOLEAN NOT NULL,
    "CreatedAt" TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    "CreatedBy" UUID NOT NULL,
    "CreatedByBehalfOf" UUID,
    "ModifiedAt" TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    "ModifiedBy" UUID NOT NULL,
    "ModifiedByBehalfOf" UUID,
    "IsActive" BOOLEAN NOT NULL,
    "IsDeleted" BOOLEAN NOT NULL,
    CONSTRAINT "FK_CustomerCommunication_Customer_CustomerId" FOREIGN KEY ("CustomerId")
        REFERENCES "Cus"."Customer" ("Id") ON DELETE CASCADE
);

-- Create index on CustomerId in CustomerCommunication table
CREATE INDEX "IX_CustomerCommunication_CustomerId"
ON "Cus"."CustomerCommunication" ("CustomerId");

-- Insert new migration record into EF Migration History table
INSERT INTO "public"."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240708141708_ContractMigrationsv28', '8.0.4');
