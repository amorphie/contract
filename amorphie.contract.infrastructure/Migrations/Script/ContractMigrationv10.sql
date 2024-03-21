CREATE TABLE IF NOT EXISTS "Cont"."ContractDefinitionHistory" (
    "Id" UUID PRIMARY KEY,
    "ContractDefinitionHistoryModel" JSONB NOT NULL,
    "ContractDefinitionId" UUID NOT NULL,
    "CreatedAt" TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    "CreatedBy" UUID NOT NULL,
    "CreatedByBehalfOf" UUID,
    "ModifiedAt" TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    "ModifiedBy" UUID NOT NULL,
    "ModifiedByBehalfOf" UUID,
    "IsActive" BOOLEAN NOT NULL,
    "IsDeleted" BOOLEAN NOT NULL
);

CREATE TABLE IF NOT EXISTS "DocGroup"."DocumentGroupHistory" (
    "Id" UUID PRIMARY KEY,
    "DocumentGroupHistoryModel" JSONB NOT NULL,
    "DocumentGroupId" UUID NOT NULL,
    "CreatedAt" TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    "CreatedBy" UUID NOT NULL,
    "CreatedByBehalfOf" UUID,
    "ModifiedAt" TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    "ModifiedBy" UUID NOT NULL,
    "ModifiedByBehalfOf" UUID,
    "IsActive" BOOLEAN NOT NULL,
    "IsDeleted" BOOLEAN NOT NULL
);