CREATE TABLE "Doc"."DocumentDys" (
    "Id" UUID PRIMARY KEY,
    "ReferenceId" INTEGER NOT NULL,
    "ReferenceKey" INTEGER NOT NULL,
    "ReferenceName" TEXT NOT NULL,
    "Fields" TEXT NOT NULL,
    "DocumentDefinitionId" UUID NOT NULL,
    "CreatedAt" TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    "CreatedBy" UUID NOT NULL,
    "CreatedByBehalfOf" UUID NULL,
    "ModifiedAt" TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    "ModifiedBy" UUID NOT NULL,
    "ModifiedByBehalfOf" UUID NULL,
    "IsActive" BOOLEAN NOT NULL,
    "IsDeleted" BOOLEAN NOT NULL
);
CREATE TABLE "Doc"."DocumentTsizl" (
    "Id" UUID PRIMARY KEY,
    "EngagementKind" TEXT NOT NULL,
    "DocumentDefinitionId" UUID NOT NULL,
    "CreatedAt" TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    "CreatedBy" UUID NOT NULL,
    "CreatedByBehalfOf" UUID NULL,
    "ModifiedAt" TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    "ModifiedBy" UUID NOT NULL,
    "ModifiedByBehalfOf" UUID NULL,
    "IsActive" BOOLEAN NOT NULL,
    "IsDeleted" BOOLEAN NOT NULL
);
CREATE UNIQUE INDEX "IX_DocumentDys_DocumentDefinitionId" ON "Doc"."DocumentDys" ("DocumentDefinitionId");
CREATE UNIQUE INDEX "IX_DocumentTsizl_DocumentDefinitionId" ON "Doc"."DocumentTsizl" ("DocumentDefinitionId");
