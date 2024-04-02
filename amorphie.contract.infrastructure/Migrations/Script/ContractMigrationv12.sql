ALTER TABLE "Doc"."DocumentDefinition"
ADD COLUMN "Titles" jsonb NOT NULL DEFAULT '{}'::jsonb;

ALTER TABLE "DocGroup"."DocumentGroup"
ADD COLUMN "Titles" jsonb NOT NULL DEFAULT '{}'::jsonb;