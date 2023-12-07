using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.contract.data.Migrations.Pg
{
    /// <inheritdoc />
    public partial class ContractMigrationsv7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractDocumentDetail_ContractDefinition_ContractDefinitio~",
                schema: "Cont",
                table: "ContractDocumentDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractDocumentGroupDetail_ContractDefinition_ContractDefi~",
                schema: "Cont",
                table: "ContractDocumentGroupDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractEntityProperty_ContractDefinition_ContractDefinitio~",
                schema: "Cont",
                table: "ContractEntityProperty");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractTag_ContractDefinition_ContractDefinitionId",
                schema: "Cont",
                table: "ContractTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractValidation_ContractDefinition_ContractDefinitionId",
                schema: "Cont",
                table: "ContractValidation");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentAllowedClientDetail_DocumentRender_DocumentRenderId",
                schema: "Doc",
                table: "DocumentAllowedClientDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentDefinition_DocumentOperations_DocumentOperationsId1",
                schema: "Doc",
                table: "DocumentDefinition");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentDefinition_DocumentOptimize_DocumentOptimizeId1",
                schema: "Doc",
                table: "DocumentDefinition");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentDefinitionLanguageDetail_DocumentDefinition_Documen~",
                schema: "Doc",
                table: "DocumentDefinitionLanguageDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentEntityProperty_DocumentDefinition_DocumentDefinitio~",
                schema: "Doc",
                table: "DocumentEntityProperty");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentGroupDetail_DocumentGroup_DocumentGroupId",
                schema: "DocGroup",
                table: "DocumentGroupDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentGroupLanguageDetail_DocumentGroup_DocumentGroupId",
                schema: "DocGroup",
                table: "DocumentGroupLanguageDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTag_DocumentDefinition_DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentTag");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTemplateDetail_DocumentRender_DocumentRenderId",
                schema: "Doc",
                table: "DocumentTemplateDetail");

            migrationBuilder.DropTable(
                name: "DocumentRender",
                schema: "DocTp");

            migrationBuilder.DropIndex(
                name: "IX_DocumentTemplateDetail_DocumentRenderId",
                schema: "Doc",
                table: "DocumentTemplateDetail");

            migrationBuilder.DropIndex(
                name: "IX_DocumentDefinition_DocumentOperationsId1",
                schema: "Doc",
                table: "DocumentDefinition");

            migrationBuilder.DropIndex(
                name: "IX_DocumentDefinition_DocumentOptimizeId1",
                schema: "Doc",
                table: "DocumentDefinition");

            migrationBuilder.DropIndex(
                name: "IX_DocumentAllowedClientDetail_DocumentRenderId",
                schema: "Doc",
                table: "DocumentAllowedClientDetail");

            migrationBuilder.DropColumn(
                name: "DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentTemplateDetail");

            migrationBuilder.DropColumn(
                name: "DocumentRenderId",
                schema: "Doc",
                table: "DocumentTemplateDetail");

            migrationBuilder.DropColumn(
                name: "DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentOptimize");

            migrationBuilder.DropColumn(
                name: "DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentOperations");

            migrationBuilder.DropColumn(
                name: "Semver",
                schema: "DocTp",
                table: "DocumentOnlineSing");

            migrationBuilder.DropColumn(
                name: "DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentFormatDetail");

            migrationBuilder.DropColumn(
                name: "DocumentOperationsId1",
                schema: "Doc",
                table: "DocumentDefinition");

            migrationBuilder.DropColumn(
                name: "DocumentOptimizeId1",
                schema: "Doc",
                table: "DocumentDefinition");

            migrationBuilder.DropColumn(
                name: "DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentAllowedClientDetail");

            migrationBuilder.DropColumn(
                name: "DocumentRenderId",
                schema: "Doc",
                table: "DocumentAllowedClientDetail");

            migrationBuilder.AddColumn<string>(
                name: "DocumentDefinitionCode",
                schema: "Doc",
                table: "DocumentTemplateDetail",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentTag",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "DocumentDefinitionCode",
                schema: "Doc",
                table: "DocumentTag",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DocumentDefinitionCode",
                schema: "Doc",
                table: "DocumentOptimize",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DocumentDefinitionCode",
                schema: "Doc",
                table: "DocumentOperations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "DocumentGroupId",
                schema: "DocGroup",
                table: "DocumentGroupLanguageDetail",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "DocumentGroupCode",
                schema: "DocGroup",
                table: "DocumentGroupLanguageDetail",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "DocumentGroupId",
                schema: "DocGroup",
                table: "DocumentGroupDetail",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "DocumentDefinitionCode",
                schema: "DocGroup",
                table: "DocumentGroupDetail",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DocumentGroupCode",
                schema: "DocGroup",
                table: "DocumentGroupDetail",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DocumentDefinitionCode",
                schema: "Doc",
                table: "DocumentFormatDetail",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentEntityProperty",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "DocumentDefinitionCode",
                schema: "Doc",
                table: "DocumentEntityProperty",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentDefinitionLanguageDetail",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "DocumentDefinitionCode",
                schema: "Doc",
                table: "DocumentDefinitionLanguageDetail",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Semver",
                schema: "Doc",
                table: "DocumentDefinition",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DocumentDefinitionCode",
                schema: "Doc",
                table: "DocumentAllowedClientDetail",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContractDefinitionId",
                schema: "Cont",
                table: "ContractValidation",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "ContractDefinitionCode",
                schema: "Cont",
                table: "ContractValidation",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContractDefinitionId",
                schema: "Cont",
                table: "ContractTag",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "ContractDefinitionCode",
                schema: "Cont",
                table: "ContractTag",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContractDefinitionId",
                schema: "Cont",
                table: "ContractEntityProperty",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "ContractDefinitionCode",
                schema: "Cont",
                table: "ContractEntityProperty",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContractDefinitionId",
                schema: "Cont",
                table: "ContractDocumentGroupDetail",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "ContractDefinitionCode",
                schema: "Cont",
                table: "ContractDocumentGroupDetail",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DocumentGroupCode",
                schema: "Cont",
                table: "ContractDocumentGroupDetail",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContractDefinitionId",
                schema: "Cont",
                table: "ContractDocumentDetail",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "ContractDefinitionCode",
                schema: "Cont",
                table: "ContractDocumentDetail",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DocumentDefinitionCode",
                schema: "Cont",
                table: "ContractDocumentDetail",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDefinition_DocumentOperationsId",
                schema: "Doc",
                table: "DocumentDefinition",
                column: "DocumentOperationsId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDefinition_DocumentOptimizeId",
                schema: "Doc",
                table: "DocumentDefinition",
                column: "DocumentOptimizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractDocumentDetail_ContractDefinition_ContractDefinitio~",
                schema: "Cont",
                table: "ContractDocumentDetail",
                column: "ContractDefinitionId",
                principalSchema: "Cont",
                principalTable: "ContractDefinition",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractDocumentGroupDetail_ContractDefinition_ContractDefi~",
                schema: "Cont",
                table: "ContractDocumentGroupDetail",
                column: "ContractDefinitionId",
                principalSchema: "Cont",
                principalTable: "ContractDefinition",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractEntityProperty_ContractDefinition_ContractDefinitio~",
                schema: "Cont",
                table: "ContractEntityProperty",
                column: "ContractDefinitionId",
                principalSchema: "Cont",
                principalTable: "ContractDefinition",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractTag_ContractDefinition_ContractDefinitionId",
                schema: "Cont",
                table: "ContractTag",
                column: "ContractDefinitionId",
                principalSchema: "Cont",
                principalTable: "ContractDefinition",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractValidation_ContractDefinition_ContractDefinitionId",
                schema: "Cont",
                table: "ContractValidation",
                column: "ContractDefinitionId",
                principalSchema: "Cont",
                principalTable: "ContractDefinition",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentDefinition_DocumentOperations_DocumentOperationsId",
                schema: "Doc",
                table: "DocumentDefinition",
                column: "DocumentOperationsId",
                principalSchema: "Doc",
                principalTable: "DocumentOperations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentDefinition_DocumentOptimize_DocumentOptimizeId",
                schema: "Doc",
                table: "DocumentDefinition",
                column: "DocumentOptimizeId",
                principalSchema: "Doc",
                principalTable: "DocumentOptimize",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentDefinitionLanguageDetail_DocumentDefinition_Documen~",
                schema: "Doc",
                table: "DocumentDefinitionLanguageDetail",
                column: "DocumentDefinitionId",
                principalSchema: "Doc",
                principalTable: "DocumentDefinition",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentEntityProperty_DocumentDefinition_DocumentDefinitio~",
                schema: "Doc",
                table: "DocumentEntityProperty",
                column: "DocumentDefinitionId",
                principalSchema: "Doc",
                principalTable: "DocumentDefinition",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentGroupDetail_DocumentGroup_DocumentGroupId",
                schema: "DocGroup",
                table: "DocumentGroupDetail",
                column: "DocumentGroupId",
                principalSchema: "DocGroup",
                principalTable: "DocumentGroup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentGroupLanguageDetail_DocumentGroup_DocumentGroupId",
                schema: "DocGroup",
                table: "DocumentGroupLanguageDetail",
                column: "DocumentGroupId",
                principalSchema: "DocGroup",
                principalTable: "DocumentGroup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTag_DocumentDefinition_DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentTag",
                column: "DocumentDefinitionId",
                principalSchema: "Doc",
                principalTable: "DocumentDefinition",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractDocumentDetail_ContractDefinition_ContractDefinitio~",
                schema: "Cont",
                table: "ContractDocumentDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractDocumentGroupDetail_ContractDefinition_ContractDefi~",
                schema: "Cont",
                table: "ContractDocumentGroupDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractEntityProperty_ContractDefinition_ContractDefinitio~",
                schema: "Cont",
                table: "ContractEntityProperty");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractTag_ContractDefinition_ContractDefinitionId",
                schema: "Cont",
                table: "ContractTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractValidation_ContractDefinition_ContractDefinitionId",
                schema: "Cont",
                table: "ContractValidation");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentDefinition_DocumentOperations_DocumentOperationsId",
                schema: "Doc",
                table: "DocumentDefinition");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentDefinition_DocumentOptimize_DocumentOptimizeId",
                schema: "Doc",
                table: "DocumentDefinition");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentDefinitionLanguageDetail_DocumentDefinition_Documen~",
                schema: "Doc",
                table: "DocumentDefinitionLanguageDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentEntityProperty_DocumentDefinition_DocumentDefinitio~",
                schema: "Doc",
                table: "DocumentEntityProperty");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentGroupDetail_DocumentGroup_DocumentGroupId",
                schema: "DocGroup",
                table: "DocumentGroupDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentGroupLanguageDetail_DocumentGroup_DocumentGroupId",
                schema: "DocGroup",
                table: "DocumentGroupLanguageDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTag_DocumentDefinition_DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentTag");

            migrationBuilder.DropIndex(
                name: "IX_DocumentDefinition_DocumentOperationsId",
                schema: "Doc",
                table: "DocumentDefinition");

            migrationBuilder.DropIndex(
                name: "IX_DocumentDefinition_DocumentOptimizeId",
                schema: "Doc",
                table: "DocumentDefinition");

            migrationBuilder.DropColumn(
                name: "DocumentDefinitionCode",
                schema: "Doc",
                table: "DocumentTemplateDetail");

            migrationBuilder.DropColumn(
                name: "DocumentDefinitionCode",
                schema: "Doc",
                table: "DocumentTag");

            migrationBuilder.DropColumn(
                name: "DocumentDefinitionCode",
                schema: "Doc",
                table: "DocumentOptimize");

            migrationBuilder.DropColumn(
                name: "DocumentDefinitionCode",
                schema: "Doc",
                table: "DocumentOperations");

            migrationBuilder.DropColumn(
                name: "DocumentGroupCode",
                schema: "DocGroup",
                table: "DocumentGroupLanguageDetail");

            migrationBuilder.DropColumn(
                name: "DocumentDefinitionCode",
                schema: "DocGroup",
                table: "DocumentGroupDetail");

            migrationBuilder.DropColumn(
                name: "DocumentGroupCode",
                schema: "DocGroup",
                table: "DocumentGroupDetail");

            migrationBuilder.DropColumn(
                name: "DocumentDefinitionCode",
                schema: "Doc",
                table: "DocumentFormatDetail");

            migrationBuilder.DropColumn(
                name: "DocumentDefinitionCode",
                schema: "Doc",
                table: "DocumentEntityProperty");

            migrationBuilder.DropColumn(
                name: "DocumentDefinitionCode",
                schema: "Doc",
                table: "DocumentDefinitionLanguageDetail");

            migrationBuilder.DropColumn(
                name: "Semver",
                schema: "Doc",
                table: "DocumentDefinition");

            migrationBuilder.DropColumn(
                name: "DocumentDefinitionCode",
                schema: "Doc",
                table: "DocumentAllowedClientDetail");

            migrationBuilder.DropColumn(
                name: "ContractDefinitionCode",
                schema: "Cont",
                table: "ContractValidation");

            migrationBuilder.DropColumn(
                name: "ContractDefinitionCode",
                schema: "Cont",
                table: "ContractTag");

            migrationBuilder.DropColumn(
                name: "ContractDefinitionCode",
                schema: "Cont",
                table: "ContractEntityProperty");

            migrationBuilder.DropColumn(
                name: "ContractDefinitionCode",
                schema: "Cont",
                table: "ContractDocumentGroupDetail");

            migrationBuilder.DropColumn(
                name: "DocumentGroupCode",
                schema: "Cont",
                table: "ContractDocumentGroupDetail");

            migrationBuilder.DropColumn(
                name: "ContractDefinitionCode",
                schema: "Cont",
                table: "ContractDocumentDetail");

            migrationBuilder.DropColumn(
                name: "DocumentDefinitionCode",
                schema: "Cont",
                table: "ContractDocumentDetail");

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentTemplateDetail",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentRenderId",
                schema: "Doc",
                table: "DocumentTemplateDetail",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentTag",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentOptimize",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentOperations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Semver",
                schema: "DocTp",
                table: "DocumentOnlineSing",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "DocumentGroupId",
                schema: "DocGroup",
                table: "DocumentGroupLanguageDetail",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DocumentGroupId",
                schema: "DocGroup",
                table: "DocumentGroupDetail",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentFormatDetail",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentEntityProperty",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentDefinitionLanguageDetail",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentOperationsId1",
                schema: "Doc",
                table: "DocumentDefinition",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentOptimizeId1",
                schema: "Doc",
                table: "DocumentDefinition",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentAllowedClientDetail",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentRenderId",
                schema: "Doc",
                table: "DocumentAllowedClientDetail",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ContractDefinitionId",
                schema: "Cont",
                table: "ContractValidation",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ContractDefinitionId",
                schema: "Cont",
                table: "ContractTag",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ContractDefinitionId",
                schema: "Cont",
                table: "ContractEntityProperty",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ContractDefinitionId",
                schema: "Cont",
                table: "ContractDocumentGroupDetail",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ContractDefinitionId",
                schema: "Cont",
                table: "ContractDocumentDetail",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DocumentRender",
                schema: "DocTp",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    Semver = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentRender", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTemplateDetail_DocumentRenderId",
                schema: "Doc",
                table: "DocumentTemplateDetail",
                column: "DocumentRenderId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDefinition_DocumentOperationsId1",
                schema: "Doc",
                table: "DocumentDefinition",
                column: "DocumentOperationsId1");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDefinition_DocumentOptimizeId1",
                schema: "Doc",
                table: "DocumentDefinition",
                column: "DocumentOptimizeId1");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAllowedClientDetail_DocumentRenderId",
                schema: "Doc",
                table: "DocumentAllowedClientDetail",
                column: "DocumentRenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractDocumentDetail_ContractDefinition_ContractDefinitio~",
                schema: "Cont",
                table: "ContractDocumentDetail",
                column: "ContractDefinitionId",
                principalSchema: "Cont",
                principalTable: "ContractDefinition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractDocumentGroupDetail_ContractDefinition_ContractDefi~",
                schema: "Cont",
                table: "ContractDocumentGroupDetail",
                column: "ContractDefinitionId",
                principalSchema: "Cont",
                principalTable: "ContractDefinition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractEntityProperty_ContractDefinition_ContractDefinitio~",
                schema: "Cont",
                table: "ContractEntityProperty",
                column: "ContractDefinitionId",
                principalSchema: "Cont",
                principalTable: "ContractDefinition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractTag_ContractDefinition_ContractDefinitionId",
                schema: "Cont",
                table: "ContractTag",
                column: "ContractDefinitionId",
                principalSchema: "Cont",
                principalTable: "ContractDefinition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractValidation_ContractDefinition_ContractDefinitionId",
                schema: "Cont",
                table: "ContractValidation",
                column: "ContractDefinitionId",
                principalSchema: "Cont",
                principalTable: "ContractDefinition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentAllowedClientDetail_DocumentRender_DocumentRenderId",
                schema: "Doc",
                table: "DocumentAllowedClientDetail",
                column: "DocumentRenderId",
                principalSchema: "DocTp",
                principalTable: "DocumentRender",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentDefinition_DocumentOperations_DocumentOperationsId1",
                schema: "Doc",
                table: "DocumentDefinition",
                column: "DocumentOperationsId1",
                principalSchema: "Doc",
                principalTable: "DocumentOperations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentDefinition_DocumentOptimize_DocumentOptimizeId1",
                schema: "Doc",
                table: "DocumentDefinition",
                column: "DocumentOptimizeId1",
                principalSchema: "Doc",
                principalTable: "DocumentOptimize",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentDefinitionLanguageDetail_DocumentDefinition_Documen~",
                schema: "Doc",
                table: "DocumentDefinitionLanguageDetail",
                column: "DocumentDefinitionId",
                principalSchema: "Doc",
                principalTable: "DocumentDefinition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentEntityProperty_DocumentDefinition_DocumentDefinitio~",
                schema: "Doc",
                table: "DocumentEntityProperty",
                column: "DocumentDefinitionId",
                principalSchema: "Doc",
                principalTable: "DocumentDefinition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentGroupDetail_DocumentGroup_DocumentGroupId",
                schema: "DocGroup",
                table: "DocumentGroupDetail",
                column: "DocumentGroupId",
                principalSchema: "DocGroup",
                principalTable: "DocumentGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentGroupLanguageDetail_DocumentGroup_DocumentGroupId",
                schema: "DocGroup",
                table: "DocumentGroupLanguageDetail",
                column: "DocumentGroupId",
                principalSchema: "DocGroup",
                principalTable: "DocumentGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTag_DocumentDefinition_DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentTag",
                column: "DocumentDefinitionId",
                principalSchema: "Doc",
                principalTable: "DocumentDefinition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTemplateDetail_DocumentRender_DocumentRenderId",
                schema: "Doc",
                table: "DocumentTemplateDetail",
                column: "DocumentRenderId",
                principalSchema: "DocTp",
                principalTable: "DocumentRender",
                principalColumn: "Id");
        }
    }
}
