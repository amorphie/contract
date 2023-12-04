using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.contract.data.Migrations.Pg
{
    /// <inheritdoc />
    public partial class ContractMigrationsv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Semver",
                schema: "Cont",
                table: "ContractDocumentDetail",
                newName: "MinVersion");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Common",
                table: "ValidationDecision",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Common",
                table: "ValidationDecision",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Common",
                table: "ValidationDecision",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "Common",
                table: "Validation",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Common",
                table: "Validation",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Common",
                table: "Validation",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Common",
                table: "Tag",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Common",
                table: "Tag",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Common",
                table: "Tag",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Common",
                table: "Status",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Common",
                table: "Status",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Common",
                table: "Status",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Common",
                table: "MultiLanguage",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Common",
                table: "MultiLanguage",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Common",
                table: "LanguageType",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Common",
                table: "LanguageType",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Common",
                table: "LanguageType",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "EAV",
                table: "EntityPropertyValue",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "EAV",
                table: "EntityPropertyValue",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "EAV",
                table: "EntityProperty",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "EAV",
                table: "EntityProperty",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "EAV",
                table: "EntityProperty",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "DocTp",
                table: "DocumentUpload",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "DocTp",
                table: "DocumentUpload",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentTemplateDetail",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentTemplateDetail",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Doc",
                table: "DocumentTemplate",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentTemplate",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentTemplate",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentTag",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentTag",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentSize",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentSize",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "DocTp",
                table: "DocumentRender",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "DocTp",
                table: "DocumentRender",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Doc",
                table: "DocumentOptimizeType",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentOptimizeType",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentOptimizeType",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentOptimize",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentOptimize",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentOperationsTagsDetail",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentOperationsTagsDetail",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentOperations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentOperations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "DocTp",
                table: "DocumentOnlineSing",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "DocTp",
                table: "DocumentOnlineSing",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "DocGroup",
                table: "DocumentGroupLanguageDetail",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "DocGroup",
                table: "DocumentGroupLanguageDetail",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "DocGroup",
                table: "DocumentGroupDetail",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "DocGroup",
                table: "DocumentGroupDetail",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "DocGroup",
                table: "DocumentGroup",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "DocGroup",
                table: "DocumentGroup",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "DocGroup",
                table: "DocumentGroup",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Doc",
                table: "DocumentFormatType",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentFormatType",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentFormatType",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentFormatDetail",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentFormatDetail",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentFormat",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentFormat",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentEntityProperty",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentEntityProperty",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentDefinitionLanguageDetail",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentDefinitionLanguageDetail",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Doc",
                table: "DocumentDefinition",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentDefinition",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentDefinition",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentContent",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentContent",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentAllowedClientDetail",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentAllowedClientDetail",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Doc",
                table: "DocumentAllowedClient",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentAllowedClient",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentAllowedClient",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Doc",
                table: "Document",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Doc",
                table: "Document",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Cont",
                table: "ContractValidation",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Cont",
                table: "ContractValidation",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Cont",
                table: "ContractTag",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Cont",
                table: "ContractTag",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Cont",
                table: "ContractEntityProperty",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Cont",
                table: "ContractEntityProperty",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Cont",
                table: "ContractDocumentGroupDetail",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Cont",
                table: "ContractDocumentGroupDetail",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Cont",
                table: "ContractDocumentDetail",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Cont",
                table: "ContractDocumentDetail",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Cont",
                table: "ContractDefinition",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Cont",
                table: "ContractDefinition",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Cont",
                table: "ContractDefinition",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Validation_Code",
                schema: "Common",
                table: "Validation",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Validation_Code",
                schema: "Common",
                table: "Validation");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Common",
                table: "ValidationDecision");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Common",
                table: "ValidationDecision");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "Common",
                table: "Validation");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Common",
                table: "Validation");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Common",
                table: "Validation");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Common",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Common",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Common",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Common",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Common",
                table: "MultiLanguage");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Common",
                table: "MultiLanguage");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Common",
                table: "LanguageType");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Common",
                table: "LanguageType");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "EAV",
                table: "EntityPropertyValue");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "EAV",
                table: "EntityPropertyValue");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "EAV",
                table: "EntityProperty");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "EAV",
                table: "EntityProperty");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "DocTp",
                table: "DocumentUpload");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "DocTp",
                table: "DocumentUpload");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentTemplateDetail");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentTemplateDetail");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentTemplate");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentTemplate");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentTag");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentTag");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentSize");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentSize");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "DocTp",
                table: "DocumentRender");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "DocTp",
                table: "DocumentRender");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentOptimizeType");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentOptimizeType");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentOptimize");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentOptimize");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentOperationsTagsDetail");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentOperationsTagsDetail");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentOperations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentOperations");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "DocTp",
                table: "DocumentOnlineSing");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "DocTp",
                table: "DocumentOnlineSing");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "DocGroup",
                table: "DocumentGroupLanguageDetail");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "DocGroup",
                table: "DocumentGroupLanguageDetail");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "DocGroup",
                table: "DocumentGroupDetail");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "DocGroup",
                table: "DocumentGroupDetail");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "DocGroup",
                table: "DocumentGroup");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "DocGroup",
                table: "DocumentGroup");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentFormatType");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentFormatType");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentFormatDetail");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentFormatDetail");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentFormat");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentFormat");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentEntityProperty");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentEntityProperty");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentDefinitionLanguageDetail");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentDefinitionLanguageDetail");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentDefinition");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentDefinition");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentContent");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentContent");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentAllowedClientDetail");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentAllowedClientDetail");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Doc",
                table: "DocumentAllowedClient");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Doc",
                table: "DocumentAllowedClient");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Doc",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Doc",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Cont",
                table: "ContractValidation");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Cont",
                table: "ContractValidation");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Cont",
                table: "ContractTag");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Cont",
                table: "ContractTag");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Cont",
                table: "ContractEntityProperty");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Cont",
                table: "ContractEntityProperty");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Cont",
                table: "ContractDocumentGroupDetail");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Cont",
                table: "ContractDocumentGroupDetail");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Cont",
                table: "ContractDocumentDetail");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Cont",
                table: "ContractDocumentDetail");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Cont",
                table: "ContractDefinition");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Cont",
                table: "ContractDefinition");

            migrationBuilder.RenameColumn(
                name: "MinVersion",
                schema: "Cont",
                table: "ContractDocumentDetail",
                newName: "Semver");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Common",
                table: "ValidationDecision",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Common",
                table: "Tag",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Common",
                table: "Status",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Common",
                table: "LanguageType",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "EAV",
                table: "EntityProperty",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Doc",
                table: "DocumentTemplate",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Doc",
                table: "DocumentOptimizeType",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "DocGroup",
                table: "DocumentGroup",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Doc",
                table: "DocumentFormatType",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Doc",
                table: "DocumentDefinition",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Doc",
                table: "DocumentAllowedClient",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Cont",
                table: "ContractDefinition",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);
        }
    }
}
