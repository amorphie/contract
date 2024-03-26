ALTER TABLE "Cont"."ContractDefinition"
ADD COLUMN "Titles" jsonb NOT NULL DEFAULT '{}'::jsonb;