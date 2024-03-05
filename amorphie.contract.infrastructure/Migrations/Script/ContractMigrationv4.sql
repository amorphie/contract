CREATE TABLE "Cont"."ContractDefinitionLanguageDetail" (
    "Id" UUID PRIMARY KEY,
    "MultiLanguageId" UUID NOT NULL,
    "ContractDefinitionId" UUID NOT NULL,
    "CreatedAt" TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    "CreatedBy" UUID NOT NULL,
    "CreatedByBehalfOf" UUID NULL,
    "ModifiedAt" TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    "ModifiedBy" UUID NOT NULL,
    "ModifiedByBehalfOf" UUID NULL,
    "IsActive" BOOLEAN NOT NULL,
    "IsDeleted" BOOLEAN NOT NULL,
    CONSTRAINT "FK_ContractDefinitionLanguageDetail_ContractDefinition_ContractDefinitionId" FOREIGN KEY ("ContractDefinitionId") REFERENCES "Cont"."ContractDefinition" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_ContractDefinitionLanguageDetail_MultiLanguage_MultiLanguageId" FOREIGN KEY ("MultiLanguageId") REFERENCES "Common"."MultiLanguage" ("Id") ON DELETE CASCADE
);
CREATE INDEX "IX_ContractDefinitionLanguageDetail_ContractDefinitionId" ON "Cont"."ContractDefinitionLanguageDetail" ("ContractDefinitionId");
CREATE INDEX "IX_ContractDefinitionLanguageDetail_MultiLanguageId" ON "Cont"."ContractDefinitionLanguageDetail" ("MultiLanguageId");
