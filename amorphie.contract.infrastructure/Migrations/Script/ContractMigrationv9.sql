ALTER TABLE "Doc"."DocumentDys"
ADD COLUMN "TitleFields" TEXT NOT NULL DEFAULT '';

CREATE TABLE "Doc"."DocumentInstanceNote" (
    "Id" UUID PRIMARY KEY,
    "DocumentId" UUID NOT NULL,
    "Note" TEXT NOT NULL,
    "CreatedAt" TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    "CreatedBy" UUID NOT NULL,
    "CreatedByBehalfOf" UUID NULL,
    "ModifiedAt" TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    "ModifiedBy" UUID NOT NULL,
    "ModifiedByBehalfOf" UUID NULL,
    "IsActive" BOOLEAN NOT NULL,
    "IsDeleted" BOOLEAN NOT NULL,
    CONSTRAINT "FK_DocumentInstanceNote_Document_DocumentId" FOREIGN KEY ("DocumentId") REFERENCES "Doc"."Document" ("Id") ON DELETE CASCADE
);
CREATE INDEX "IX_DocumentInstanceNote_DocumentId" ON "Doc"."DocumentInstanceNote" ("DocumentId");