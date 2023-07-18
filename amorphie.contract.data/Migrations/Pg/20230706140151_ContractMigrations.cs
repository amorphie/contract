using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.contract.data.Migrations.Pg
{
    /// <inheritdoc />
    public partial class ContractMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Cont");

            migrationBuilder.EnsureSchema(
                name: "Doc");

            migrationBuilder.EnsureSchema(
                name: "EAV");

            migrationBuilder.EnsureSchema(
                name: "Common");

            migrationBuilder.CreateTable(
                name: "ContractDefinition",
                schema: "Cont",
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
                    table.PrimaryKey("PK_ContractDefinition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentAllowedType",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentAllowedType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentFormIO",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentFormIO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentOptimize",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Size = table.Column<bool>(type: "boolean", nullable: false),
                    Transform = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentOptimize", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentSize",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    KiloBytes = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentSize", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTemplate",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    URL = table.Column<string>(type: "text", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "DocumentType",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "EntityPropertyType",
                schema: "EAV",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_EntityPropertyType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityPropertyValue",
                schema: "EAV",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityPropertyValue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LanguageType",
                schema: "Common",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentDefinition",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ContractDefinitionId = table.Column<Guid>(type: "uuid", nullable: true),
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
                        name: "FK_DocumentDefinition_ContractDefinition_ContractDefinitionId",
                        column: x => x.ContractDefinitionId,
                        principalSchema: "Cont",
                        principalTable: "ContractDefinition",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DocumentAllowed",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DocumentAllowedTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentAllowed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentAllowed_DocumentAllowedType_DocumentAllowedTypeId",
                        column: x => x.DocumentAllowedTypeId,
                        principalSchema: "Doc",
                        principalTable: "DocumentAllowedType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentFormat",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentSizeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentFormat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentFormat_DocumentSize_DocumentSizeId",
                        column: x => x.DocumentSizeId,
                        principalSchema: "Doc",
                        principalTable: "DocumentSize",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentFormat_DocumentType_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalSchema: "Doc",
                        principalTable: "DocumentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntityProperty",
                schema: "EAV",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    EntityPropertyTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityPropertyValueId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityProperty_EntityPropertyType_EntityPropertyTypeId",
                        column: x => x.EntityPropertyTypeId,
                        principalSchema: "EAV",
                        principalTable: "EntityPropertyType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntityProperty_EntityPropertyValue_EntityPropertyValueId",
                        column: x => x.EntityPropertyValueId,
                        principalSchema: "EAV",
                        principalTable: "EntityPropertyValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MultiLanguage",
                schema: "Common",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    LanguageTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiLanguage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultiLanguage_LanguageType_LanguageTypeId",
                        column: x => x.LanguageTypeId,
                        principalSchema: "Common",
                        principalTable: "LanguageType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentFormIODetail",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentFormIOId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentFormIODetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentFormIODetail_DocumentDefinition_DocumentDefinitionId",
                        column: x => x.DocumentDefinitionId,
                        principalSchema: "Doc",
                        principalTable: "DocumentDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentFormIODetail_DocumentFormIO_DocumentFormIOId",
                        column: x => x.DocumentFormIOId,
                        principalSchema: "Doc",
                        principalTable: "DocumentFormIO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTag",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Contact = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentTag_DocumentDefinition_DocumentDefinitionId",
                        column: x => x.DocumentDefinitionId,
                        principalSchema: "Doc",
                        principalTable: "DocumentDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTemplateDetail",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTemplateDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentTemplateDetail_DocumentDefinition_DocumentDefinitio~",
                        column: x => x.DocumentDefinitionId,
                        principalSchema: "Doc",
                        principalTable: "DocumentDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentTemplateDetail_DocumentTemplate_DocumentTemplateId",
                        column: x => x.DocumentTemplateId,
                        principalSchema: "Doc",
                        principalTable: "DocumentTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentVersions",
                schema: "Doc",
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
                        principalSchema: "Doc",
                        principalTable: "DocumentDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentAllowedDetail",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentAllowedId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentAllowedDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentAllowedDetail_DocumentAllowed_DocumentAllowedId",
                        column: x => x.DocumentAllowedId,
                        principalSchema: "Doc",
                        principalTable: "DocumentAllowed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentAllowedDetail_DocumentDefinition_DocumentDefinition~",
                        column: x => x.DocumentDefinitionId,
                        principalSchema: "Doc",
                        principalTable: "DocumentDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentFormatDetail",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentFormatId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentFormatDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentFormatDetail_DocumentDefinition_DocumentDefinitionId",
                        column: x => x.DocumentDefinitionId,
                        principalSchema: "Doc",
                        principalTable: "DocumentDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentFormatDetail_DocumentFormat_DocumentFormatId",
                        column: x => x.DocumentFormatId,
                        principalSchema: "Doc",
                        principalTable: "DocumentFormat",
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
                name: "DocumentDefinitionLanguageDetail",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MultiLanguageId = table.Column<Guid>(type: "uuid", nullable: false),
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
                name: "DocumentGroup",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LanguageId = table.Column<Guid>(type: "uuid", nullable: false),
                    MultiLanguageId = table.Column<Guid>(type: "uuid", nullable: false),
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
                        name: "FK_DocumentGroup_MultiLanguage_MultiLanguageId",
                        column: x => x.MultiLanguageId,
                        principalSchema: "Common",
                        principalTable: "MultiLanguage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentContent",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContentData = table.Column<string>(type: "text", nullable: false),
                    KiloBytesSize = table.Column<string>(type: "text", nullable: false),
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
                        principalSchema: "Doc",
                        principalTable: "DocumentVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractDefinitionDocumentDetail",
                schema: "Cont",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContractDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_ContractDefinitionDocumentDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractDefinitionDocumentDetail_DocumentDefinition_Contrac~",
                        column: x => x.ContractDefinitionId,
                        principalSchema: "Doc",
                        principalTable: "DocumentDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractDefinitionDocumentDetail_DocumentGroup_DocumentDefi~",
                        column: x => x.DocumentDefinitionId,
                        principalSchema: "Doc",
                        principalTable: "DocumentGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentDefinitionGroupDetail",
                schema: "Doc",
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
                    table.PrimaryKey("PK_DocumentDefinitionGroupDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentDefinitionGroupDetail_DocumentDefinition_DocumentDe~",
                        column: x => x.DocumentDefinitionId,
                        principalSchema: "Doc",
                        principalTable: "DocumentDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentDefinitionGroupDetail_DocumentGroup_DocumentGroupId",
                        column: x => x.DocumentGroupId,
                        principalSchema: "Doc",
                        principalTable: "DocumentGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                schema: "Doc",
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
                        principalSchema: "Doc",
                        principalTable: "DocumentContent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Document_DocumentDefinition_DocumentDefinitionId",
                        column: x => x.DocumentDefinitionId,
                        principalSchema: "Doc",
                        principalTable: "DocumentDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractDefinitionDocumentDetail_ContractDefinitionId",
                schema: "Cont",
                table: "ContractDefinitionDocumentDetail",
                column: "ContractDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDefinitionDocumentDetail_DocumentDefinitionId",
                schema: "Cont",
                table: "ContractDefinitionDocumentDetail",
                column: "DocumentDefinitionId");

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
                name: "IX_Document_DocumentContentId",
                schema: "Doc",
                table: "Document",
                column: "DocumentContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_DocumentDefinitionId",
                schema: "Doc",
                table: "Document",
                column: "DocumentDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAllowed_DocumentAllowedTypeId",
                schema: "Doc",
                table: "DocumentAllowed",
                column: "DocumentAllowedTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAllowedDetail_DocumentAllowedId",
                schema: "Doc",
                table: "DocumentAllowedDetail",
                column: "DocumentAllowedId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAllowedDetail_DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentAllowedDetail",
                column: "DocumentDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentContent_DocumentVersionsId",
                schema: "Doc",
                table: "DocumentContent",
                column: "DocumentVersionsId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDefinition_Code",
                schema: "Doc",
                table: "DocumentDefinition",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDefinition_ContractDefinitionId",
                schema: "Doc",
                table: "DocumentDefinition",
                column: "ContractDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDefinitionGroupDetail_DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentDefinitionGroupDetail",
                column: "DocumentDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDefinitionGroupDetail_DocumentGroupId",
                schema: "Doc",
                table: "DocumentDefinitionGroupDetail",
                column: "DocumentGroupId");

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
                name: "IX_DocumentFormat_DocumentSizeId",
                schema: "Doc",
                table: "DocumentFormat",
                column: "DocumentSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFormat_DocumentTypeId",
                schema: "Doc",
                table: "DocumentFormat",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFormatDetail_DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentFormatDetail",
                column: "DocumentDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFormatDetail_DocumentFormatId",
                schema: "Doc",
                table: "DocumentFormatDetail",
                column: "DocumentFormatId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFormIODetail_DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentFormIODetail",
                column: "DocumentDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFormIODetail_DocumentFormIOId",
                schema: "Doc",
                table: "DocumentFormIODetail",
                column: "DocumentFormIOId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentGroup_Code",
                schema: "Doc",
                table: "DocumentGroup",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentGroup_MultiLanguageId",
                schema: "Doc",
                table: "DocumentGroup",
                column: "MultiLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTag_DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentTag",
                column: "DocumentDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTemplateDetail_DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentTemplateDetail",
                column: "DocumentDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTemplateDetail_DocumentTemplateId",
                schema: "Doc",
                table: "DocumentTemplateDetail",
                column: "DocumentTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentVersions_DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentVersions",
                column: "DocumentDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityProperty_EntityPropertyTypeId",
                schema: "EAV",
                table: "EntityProperty",
                column: "EntityPropertyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityProperty_EntityPropertyValueId",
                schema: "EAV",
                table: "EntityProperty",
                column: "EntityPropertyValueId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiLanguage_Code",
                schema: "Common",
                table: "MultiLanguage",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_MultiLanguage_LanguageTypeId",
                schema: "Common",
                table: "MultiLanguage",
                column: "LanguageTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractDefinitionDocumentDetail",
                schema: "Cont");

            migrationBuilder.DropTable(
                name: "ContractEntityProperty",
                schema: "Cont");

            migrationBuilder.DropTable(
                name: "Document",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentAllowedDetail",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentDefinitionGroupDetail",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentDefinitionLanguageDetail",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentEntityProperty",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentFormatDetail",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentFormIODetail",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentOptimize",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentTag",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentTemplateDetail",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentContent",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentAllowed",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentGroup",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "EntityProperty",
                schema: "EAV");

            migrationBuilder.DropTable(
                name: "DocumentFormat",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentFormIO",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentTemplate",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentVersions",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentAllowedType",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "MultiLanguage",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "EntityPropertyType",
                schema: "EAV");

            migrationBuilder.DropTable(
                name: "EntityPropertyValue",
                schema: "EAV");

            migrationBuilder.DropTable(
                name: "DocumentSize",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentType",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentDefinition",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "LanguageType",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "ContractDefinition",
                schema: "Cont");
        }
    }
}
