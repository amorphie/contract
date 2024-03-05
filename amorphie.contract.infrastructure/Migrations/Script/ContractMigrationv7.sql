ALTER TABLE "EAV"."EntityProperty"
ADD COLUMN "Required" BOOLEAN NOT NULL DEFAULT FALSE;

CREATE TABLE "Doc"."DocumentInstanceEntityProperty" (
    "Id" UUID PRIMARY KEY,
    "DocumentId" UUID NOT NULL,
    "EntityPropertyId" UUID NOT NULL,
    "CreatedAt" TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    "CreatedBy" UUID NOT NULL,
    "CreatedByBehalfOf" UUID NULL,
    "ModifiedAt" TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    "ModifiedBy" UUID NOT NULL,
    "ModifiedByBehalfOf" UUID NULL,
    "IsActive" BOOLEAN NOT NULL,
    "IsDeleted" BOOLEAN NOT NULL,
    CONSTRAINT "FK_DocumentInstanceEntityProperty_Document_DocumentId" FOREIGN KEY ("DocumentId") REFERENCES "Doc"."Document" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_DocumentInstanceEntityProperty_EntityProperty_EntityPropertyId" FOREIGN KEY ("EntityPropertyId") REFERENCES "EAV"."EntityProperty" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_DocumentInstanceEntityProperty_DocumentId" ON "Doc"."DocumentInstanceEntityProperty" ("DocumentId");
CREATE INDEX "IX_DocumentInstanceEntityProperty_EntityPropertyId" ON "Doc"."DocumentInstanceEntityProperty" ("EntityPropertyId");
