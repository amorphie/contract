using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.contract.infrastructure.Migrations.Pg
{
    /// <inheritdoc />
    public partial class ContractMigrationsv13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractDefinitionLanguageDetail",
                schema: "Cont");

            migrationBuilder.DropTable(
                name: "DocumentDefinitionLanguageDetail",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentGroupLanguageDetail",
                schema: "DocGroup");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractDefinitionLanguageDetail",
                schema: "Cont",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContractDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    MultiLanguageId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractDefinitionLanguageDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractDefinitionLanguageDetail_ContractDefinition_Contrac~",
                        column: x => x.ContractDefinitionId,
                        principalSchema: "Cont",
                        principalTable: "ContractDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractDefinitionLanguageDetail_MultiLanguage_MultiLanguag~",
                        column: x => x.MultiLanguageId,
                        principalSchema: "Common",
                        principalTable: "MultiLanguage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentDefinitionLanguageDetail",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    MultiLanguageId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentDefinitionLanguageDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentDefinitionLanguageDetail_DocumentDefinition_Documen~",
                        column: x => x.DocumentDefinitionId,
                        principalSchema: "Doc",
                        principalTable: "DocumentDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentDefinitionLanguageDetail_MultiLanguage_MultiLanguag~",
                        column: x => x.MultiLanguageId,
                        principalSchema: "Common",
                        principalTable: "MultiLanguage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentGroupLanguageDetail",
                schema: "DocGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    MultiLanguageId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentGroupLanguageDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentGroupLanguageDetail_DocumentGroup_DocumentGroupId",
                        column: x => x.DocumentGroupId,
                        principalSchema: "DocGroup",
                        principalTable: "DocumentGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentGroupLanguageDetail_MultiLanguage_MultiLanguageId",
                        column: x => x.MultiLanguageId,
                        principalSchema: "Common",
                        principalTable: "MultiLanguage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractDefinitionLanguageDetail_ContractDefinitionId",
                schema: "Cont",
                table: "ContractDefinitionLanguageDetail",
                column: "ContractDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDefinitionLanguageDetail_MultiLanguageId",
                schema: "Cont",
                table: "ContractDefinitionLanguageDetail",
                column: "MultiLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDefinitionLanguageDetail_DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentDefinitionLanguageDetail",
                column: "DocumentDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDefinitionLanguageDetail_MultiLanguageId",
                schema: "Doc",
                table: "DocumentDefinitionLanguageDetail",
                column: "MultiLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentGroupLanguageDetail_DocumentGroupId",
                schema: "DocGroup",
                table: "DocumentGroupLanguageDetail",
                column: "DocumentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentGroupLanguageDetail_MultiLanguageId",
                schema: "DocGroup",
                table: "DocumentGroupLanguageDetail",
                column: "MultiLanguageId");
        }
    }
}
