using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.contract.infrastructure.Migrations.Pg
{
    /// <inheritdoc />
    public partial class ContractMigrationsv4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractDefinitionLanguageDetail",
                schema: "Cont",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MultiLanguageId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContractDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractDefinitionLanguageDetail",
                schema: "Cont");
        }
    }
}
