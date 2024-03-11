using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.contract.infrastructure.Migrations.Pg
{
    /// <inheritdoc />
    public partial class ContractMigrationsv7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Required",
                schema: "EAV",
                table: "EntityProperty",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "DocumentInstanceEntityProperty",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityPropertyId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_DocumentInstanceEntityProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentInstanceEntityProperty_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "Doc",
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentInstanceEntityProperty_EntityProperty_EntityPropert~",
                        column: x => x.EntityPropertyId,
                        principalSchema: "EAV",
                        principalTable: "EntityProperty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentInstanceEntityProperty_DocumentId",
                schema: "Doc",
                table: "DocumentInstanceEntityProperty",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentInstanceEntityProperty_EntityPropertyId",
                schema: "Doc",
                table: "DocumentInstanceEntityProperty",
                column: "EntityPropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentInstanceEntityProperty",
                schema: "Doc");

            migrationBuilder.DropColumn(
                name: "Required",
                schema: "EAV",
                table: "EntityProperty");
        }
    }
}
