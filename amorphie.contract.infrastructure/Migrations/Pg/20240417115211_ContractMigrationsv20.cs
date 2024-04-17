using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.contract.infrastructure.Migrations.Pg
{
    /// <inheritdoc />
    public partial class ContractMigrationsv20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractEntityProperty",
                schema: "Cont");

            migrationBuilder.DropTable(
                name: "DocumentEntityProperty",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentInstanceEntityProperty",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "EntityProperty",
                schema: "EAV");

            migrationBuilder.DropTable(
                name: "EntityPropertyValue",
                schema: "EAV");

            migrationBuilder.AddColumn<string>(
                name: "DefinitionMetadata",
                schema: "Doc",
                table: "DocumentDefinition",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefinitionMetadata",
                schema: "Cont",
                table: "ContractDefinition",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefinitionMetadata",
                schema: "Doc",
                table: "DocumentDefinition");

            migrationBuilder.DropColumn(
                name: "DefinitionMetadata",
                schema: "Cont",
                table: "ContractDefinition");

            migrationBuilder.EnsureSchema(
                name: "EAV");

            migrationBuilder.CreateTable(
                name: "EntityPropertyValue",
                schema: "EAV",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    Data = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityPropertyValue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityProperty",
                schema: "EAV",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityPropertyValueId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    EEntityPropertyType = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    Required = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityProperty_EntityPropertyValue_EntityPropertyValueId",
                        column: x => x.EntityPropertyValueId,
                        principalSchema: "EAV",
                        principalTable: "EntityPropertyValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractEntityProperty",
                schema: "Cont",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContractDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityPropertyId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_ContractEntityProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractEntityProperty_ContractDefinition_ContractDefinitio~",
                        column: x => x.ContractDefinitionId,
                        principalSchema: "Cont",
                        principalTable: "ContractDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractEntityProperty_EntityProperty_EntityPropertyId",
                        column: x => x.EntityPropertyId,
                        principalSchema: "EAV",
                        principalTable: "EntityProperty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentEntityProperty",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityPropertyId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_DocumentEntityProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentEntityProperty_DocumentDefinition_DocumentDefinitio~",
                        column: x => x.DocumentDefinitionId,
                        principalSchema: "Doc",
                        principalTable: "DocumentDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentEntityProperty_EntityProperty_EntityPropertyId",
                        column: x => x.EntityPropertyId,
                        principalSchema: "EAV",
                        principalTable: "EntityProperty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
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
                name: "IX_ContractEntityProperty_ContractDefinitionId",
                schema: "Cont",
                table: "ContractEntityProperty",
                column: "ContractDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractEntityProperty_EntityPropertyId",
                schema: "Cont",
                table: "ContractEntityProperty",
                column: "EntityPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentEntityProperty_DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentEntityProperty",
                column: "DocumentDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentEntityProperty_EntityPropertyId",
                schema: "Doc",
                table: "DocumentEntityProperty",
                column: "EntityPropertyId");

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

            migrationBuilder.CreateIndex(
                name: "IX_EntityProperty_EntityPropertyValueId",
                schema: "EAV",
                table: "EntityProperty",
                column: "EntityPropertyValueId");
        }
    }
}
