using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.contract.data.Migrations.Pg
{
    /// <inheritdoc />
    public partial class MyMigration6 : Migration
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
                name: "DocumentDefinition",
                schema: "Definition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    LanguageId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentDefinition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentDefinition_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "Common",
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentGroup",
                schema: "Definition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LanguageId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentGroupId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentGroup_DocumentGroup_DocumentGroupId",
                        column: x => x.DocumentGroupId,
                        principalSchema: "Definition",
                        principalTable: "DocumentGroup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumentGroup_Language_LanguageId",
                        column: x => x.LanguageId,
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DocumentDefinitionId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentTemplate_DocumentDefinition_DocumentDefinitionId",
                        column: x => x.DocumentDefinitionId,
                        principalSchema: "Definition",
                        principalTable: "DocumentDefinition",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DocumentType",
                schema: "Definition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: false),
                    DocumentDefinitionId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentType_DocumentDefinition_DocumentDefinitionId",
                        column: x => x.DocumentDefinitionId,
                        principalSchema: "Definition",
                        principalTable: "DocumentDefinition",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DocumentVersions",
                schema: "Definition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DocumentDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentVersions_DocumentDefinition_DocumentDefinitionId",
                        column: x => x.DocumentDefinitionId,
                        principalSchema: "Definition",
                        principalTable: "DocumentDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentDefinitionDocumentGroup",
                schema: "Definition",
                columns: table => new
                {
                    DocumentDefinitionsId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentGroupsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentDefinitionDocumentGroup", x => new { x.DocumentDefinitionsId, x.DocumentGroupsId });
                    table.ForeignKey(
                        name: "FK_DocumentDefinitionDocumentGroup_DocumentDefinition_Document~",
                        column: x => x.DocumentDefinitionsId,
                        principalSchema: "Definition",
                        principalTable: "DocumentDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentDefinitionDocumentGroup_DocumentGroup_DocumentGroup~",
                        column: x => x.DocumentGroupsId,
                        principalSchema: "Definition",
                        principalTable: "DocumentGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentGroupDetail",
                schema: "Definition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentGroupDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentGroupDetail_DocumentDefinition_DocumentDefinitionId",
                        column: x => x.DocumentDefinitionId,
                        principalSchema: "Definition",
                        principalTable: "DocumentDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentGroupDetail_DocumentGroup_DocumentGroupId",
                        column: x => x.DocumentGroupId,
                        principalSchema: "Definition",
                        principalTable: "DocumentGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentContent",
                schema: "Definition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContentData = table.Column<string>(type: "text", nullable: false),
                    DocumentVersionsId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentContent_DocumentVersions_DocumentVersionsId",
                        column: x => x.DocumentVersionsId,
                        principalSchema: "Definition",
                        principalTable: "DocumentVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                schema: "Definition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentContentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Document_DocumentContent_DocumentContentId",
                        column: x => x.DocumentContentId,
                        principalSchema: "Definition",
                        principalTable: "DocumentContent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Document_DocumentDefinition_DocumentDefinitionId",
                        column: x => x.DocumentDefinitionId,
                        principalSchema: "Definition",
                        principalTable: "DocumentDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Document_DocumentContentId",
                schema: "Definition",
                table: "Document",
                column: "DocumentContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_DocumentDefinitionId",
                schema: "Definition",
                table: "Document",
                column: "DocumentDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentContent_DocumentVersionsId",
                schema: "Definition",
                table: "DocumentContent",
                column: "DocumentVersionsId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDefinition_Code",
                schema: "Definition",
                table: "DocumentDefinition",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDefinition_LanguageId",
                schema: "Definition",
                table: "DocumentDefinition",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDefinitionDocumentGroup_DocumentGroupsId",
                schema: "Definition",
                table: "DocumentDefinitionDocumentGroup",
                column: "DocumentGroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentGroup_Code",
                schema: "Definition",
                table: "DocumentGroup",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentGroup_DocumentGroupId",
                schema: "Definition",
                table: "DocumentGroup",
                column: "DocumentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentGroup_LanguageId",
                schema: "Definition",
                table: "DocumentGroup",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentGroupDetail_DocumentDefinitionId",
                schema: "Definition",
                table: "DocumentGroupDetail",
                column: "DocumentDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentGroupDetail_DocumentGroupId",
                schema: "Definition",
                table: "DocumentGroupDetail",
                column: "DocumentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTemplate_DocumentDefinitionId",
                schema: "Definition",
                table: "DocumentTemplate",
                column: "DocumentDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentType_DocumentDefinitionId",
                schema: "Definition",
                table: "DocumentType",
                column: "DocumentDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentVersions_DocumentDefinitionId",
                schema: "Definition",
                table: "DocumentVersions",
                column: "DocumentDefinitionId");

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
                name: "Document",
                schema: "Definition");

            migrationBuilder.DropTable(
                name: "DocumentDefinitionDocumentGroup",
                schema: "Definition");

            migrationBuilder.DropTable(
                name: "DocumentGroupDetail",
                schema: "Definition");

            migrationBuilder.DropTable(
                name: "DocumentTemplate",
                schema: "Definition");

            migrationBuilder.DropTable(
                name: "DocumentType",
                schema: "Definition");

            migrationBuilder.DropTable(
                name: "DocumentContent",
                schema: "Definition");

            migrationBuilder.DropTable(
                name: "DocumentGroup",
                schema: "Definition");

            migrationBuilder.DropTable(
                name: "DocumentVersions",
                schema: "Definition");

            migrationBuilder.DropTable(
                name: "DocumentDefinition",
                schema: "Definition");

            migrationBuilder.DropTable(
                name: "Language",
                schema: "Common");
        }
    }
}
