using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.contract.data.Migrations.Pg
{
    /// <inheritdoc />
    public partial class MyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Definition");

            migrationBuilder.EnsureSchema(
                name: "Common");

            migrationBuilder.CreateTable(
                name: "Language",
                schema: "Common",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                schema: "Definition",
                columns: table => new
                {
                    LanguageId = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    Code = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    LanguageId1 = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.LanguageId);
                    table.ForeignKey(
                        name: "FK_Document_Language_LanguageId1",
                        column: x => x.LanguageId1,
                        principalSchema: "Common",
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTemplate",
                schema: "Definition",
                columns: table => new
                {
                    DocumentDefinitionId = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    LanguageId = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTemplate", x => new { x.LanguageId, x.DocumentDefinitionId });
                    table.ForeignKey(
                        name: "FK_DocumentTemplate_Document_DocumentDefinitionId",
                        column: x => x.DocumentDefinitionId,
                        principalSchema: "Definition",
                        principalTable: "Document",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentType",
                schema: "Definition",
                columns: table => new
                {
                    DocumentDefinitionId = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: false),
                    DocumentDefinitionLanguageId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.DocumentDefinitionId);
                    table.ForeignKey(
                        name: "FK_DocumentType_Document_DocumentDefinitionLanguageId",
                        column: x => x.DocumentDefinitionLanguageId,
                        principalSchema: "Definition",
                        principalTable: "Document",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Document_Code",
                schema: "Definition",
                table: "Document",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Document_LanguageId1",
                schema: "Definition",
                table: "Document",
                column: "LanguageId1");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTemplate_DocumentDefinitionId",
                schema: "Definition",
                table: "DocumentTemplate",
                column: "DocumentDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentType_DocumentDefinitionLanguageId",
                schema: "Definition",
                table: "DocumentType",
                column: "DocumentDefinitionLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Language_Code",
                schema: "Common",
                table: "Language",
                column: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentTemplate",
                schema: "Definition");

            migrationBuilder.DropTable(
                name: "DocumentType",
                schema: "Definition");

            migrationBuilder.DropTable(
                name: "Document",
                schema: "Definition");

            migrationBuilder.DropTable(
                name: "Language",
                schema: "Common");
        }
    }
}
