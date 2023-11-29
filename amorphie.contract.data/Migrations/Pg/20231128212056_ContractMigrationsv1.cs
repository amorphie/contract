using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.contract.data.Migrations.Pg
{
    /// <inheritdoc />
    public partial class ContractMigrationsv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Cont");

            migrationBuilder.EnsureSchema(
                name: "Doc");

            migrationBuilder.EnsureSchema(
                name: "DocGroup");

            migrationBuilder.EnsureSchema(
                name: "DocTp");

            migrationBuilder.EnsureSchema(
                name: "EAV");

            migrationBuilder.EnsureSchema(
                name: "Common");

            migrationBuilder.CreateTable(
                name: "DocumentAllowedClient",
                schema: "Doc",
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
                    table.PrimaryKey("PK_DocumentAllowedClient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentContent",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContentData = table.Column<string>(type: "text", nullable: false),
                    KiloBytesSize = table.Column<string>(type: "text", nullable: true),
                    ContentType = table.Column<string>(type: "text", nullable: true),
                    ContentTransferEncoding = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
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
                });

            migrationBuilder.CreateTable(
                name: "DocumentFormatType",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
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
                    table.PrimaryKey("PK_DocumentFormatType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentOnlineSing",
                schema: "DocTp",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Required = table.Column<bool>(type: "boolean", nullable: false),
                    Semver = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentOnlineSing", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentOperations",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentManuelControl = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentOperations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentOptimizeType",
                schema: "Doc",
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
                    table.PrimaryKey("PK_DocumentOptimizeType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentRender",
                schema: "DocTp",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Semver = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentRender", x => x.Id);
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
                name: "DocumentUpload",
                schema: "DocTp",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Required = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentUpload", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityPropertyValue",
                schema: "EAV",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_EntityPropertyValue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LanguageType",
                schema: "Common",
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
                    table.PrimaryKey("PK_LanguageType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                schema: "Common",
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
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                schema: "Common",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Contact = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ValidationDecision",
                schema: "Common",
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
                    table.PrimaryKey("PK_ValidationDecision", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentOptimize",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Size = table.Column<bool>(type: "boolean", nullable: false),
                    DocumentOptimizeTypeId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_DocumentOptimize_DocumentOptimizeType_DocumentOptimizeTypeId",
                        column: x => x.DocumentOptimizeTypeId,
                        principalSchema: "Doc",
                        principalTable: "DocumentOptimizeType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentFormat",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentFormatTypeId = table.Column<Guid>(type: "uuid", nullable: false),
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
                        name: "FK_DocumentFormat_DocumentFormatType_DocumentFormatTypeId",
                        column: x => x.DocumentFormatTypeId,
                        principalSchema: "Doc",
                        principalTable: "DocumentFormatType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentFormat_DocumentSize_DocumentSizeId",
                        column: x => x.DocumentSizeId,
                        principalSchema: "Doc",
                        principalTable: "DocumentSize",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentAllowedClientDetail",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentAllowedClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentOnlineSingId = table.Column<Guid>(type: "uuid", nullable: true),
                    DocumentRenderId = table.Column<Guid>(type: "uuid", nullable: true),
                    DocumentUploadId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentAllowedClientDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentAllowedClientDetail_DocumentAllowedClient_DocumentA~",
                        column: x => x.DocumentAllowedClientId,
                        principalSchema: "Doc",
                        principalTable: "DocumentAllowedClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentAllowedClientDetail_DocumentOnlineSing_DocumentOnli~",
                        column: x => x.DocumentOnlineSingId,
                        principalSchema: "DocTp",
                        principalTable: "DocumentOnlineSing",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumentAllowedClientDetail_DocumentRender_DocumentRenderId",
                        column: x => x.DocumentRenderId,
                        principalSchema: "DocTp",
                        principalTable: "DocumentRender",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumentAllowedClientDetail_DocumentUpload_DocumentUploadId",
                        column: x => x.DocumentUploadId,
                        principalSchema: "DocTp",
                        principalTable: "DocumentUpload",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EntityProperty",
                schema: "EAV",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    EEntityPropertyType = table.Column<int>(type: "integer", nullable: false),
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
                        name: "FK_EntityProperty_EntityPropertyValue_EntityPropertyValueId",
                        column: x => x.EntityPropertyValueId,
                        principalSchema: "EAV",
                        principalTable: "EntityPropertyValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTemplate",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_DocumentTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentTemplate_LanguageType_LanguageTypeId",
                        column: x => x.LanguageTypeId,
                        principalSchema: "Common",
                        principalTable: "LanguageType",
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
                name: "ContractDefinition",
                schema: "Cont",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    StatusId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_ContractDefinition_Status_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "Common",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentGroup",
                schema: "DocGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    StatusId = table.Column<Guid>(type: "uuid", nullable: false),
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
                        name: "FK_DocumentGroup_Status_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "Common",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentOperationsTagsDetail",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentOperationsId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentOperationsTagsDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentOperationsTagsDetail_DocumentOperations_DocumentOpe~",
                        column: x => x.DocumentOperationsId,
                        principalSchema: "Doc",
                        principalTable: "DocumentOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentOperationsTagsDetail_Tag_TagId",
                        column: x => x.TagId,
                        principalSchema: "Common",
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Validation",
                schema: "Common",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EValidationType = table.Column<int>(type: "integer", nullable: false),
                    ValidationDecisionId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Validation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Validation_ValidationDecision_ValidationDecisionId",
                        column: x => x.ValidationDecisionId,
                        principalSchema: "Common",
                        principalTable: "ValidationDecision",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DocumentDefinition",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    StatusId = table.Column<Guid>(type: "uuid", nullable: false),
                    BaseStatusId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentUploadId = table.Column<Guid>(type: "uuid", nullable: true),
                    DocumentOnlineSingId = table.Column<Guid>(type: "uuid", nullable: true),
                    DocumentOptimizeId = table.Column<Guid>(type: "uuid", nullable: true),
                    DocumentOptimizeId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    DocumentOperationsId = table.Column<Guid>(type: "uuid", nullable: true),
                    DocumentOperationsId1 = table.Column<Guid>(type: "uuid", nullable: true),
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
                        name: "FK_DocumentDefinition_DocumentOnlineSing_DocumentOnlineSingId",
                        column: x => x.DocumentOnlineSingId,
                        principalSchema: "DocTp",
                        principalTable: "DocumentOnlineSing",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumentDefinition_DocumentOperations_DocumentOperationsId1",
                        column: x => x.DocumentOperationsId1,
                        principalSchema: "Doc",
                        principalTable: "DocumentOperations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumentDefinition_DocumentOptimize_DocumentOptimizeId1",
                        column: x => x.DocumentOptimizeId1,
                        principalSchema: "Doc",
                        principalTable: "DocumentOptimize",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumentDefinition_DocumentUpload_DocumentUploadId",
                        column: x => x.DocumentUploadId,
                        principalSchema: "DocTp",
                        principalTable: "DocumentUpload",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumentDefinition_Status_BaseStatusId",
                        column: x => x.BaseStatusId,
                        principalSchema: "Common",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentDefinition_Status_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "Common",
                        principalTable: "Status",
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
                    DocumentUploadId = table.Column<Guid>(type: "uuid", nullable: true),
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
                        name: "FK_DocumentFormatDetail_DocumentFormat_DocumentFormatId",
                        column: x => x.DocumentFormatId,
                        principalSchema: "Doc",
                        principalTable: "DocumentFormat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentFormatDetail_DocumentUpload_DocumentUploadId",
                        column: x => x.DocumentUploadId,
                        principalSchema: "DocTp",
                        principalTable: "DocumentUpload",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DocumentTemplateDetail",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentOnlineSingId = table.Column<Guid>(type: "uuid", nullable: true),
                    DocumentRenderId = table.Column<Guid>(type: "uuid", nullable: true),
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
                        name: "FK_DocumentTemplateDetail_DocumentOnlineSing_DocumentOnlineSin~",
                        column: x => x.DocumentOnlineSingId,
                        principalSchema: "DocTp",
                        principalTable: "DocumentOnlineSing",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumentTemplateDetail_DocumentRender_DocumentRenderId",
                        column: x => x.DocumentRenderId,
                        principalSchema: "DocTp",
                        principalTable: "DocumentRender",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumentTemplateDetail_DocumentTemplate_DocumentTemplateId",
                        column: x => x.DocumentTemplateId,
                        principalSchema: "Doc",
                        principalTable: "DocumentTemplate",
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
                name: "ContractTag",
                schema: "Cont",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContractDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractTag_ContractDefinition_ContractDefinitionId",
                        column: x => x.ContractDefinitionId,
                        principalSchema: "Cont",
                        principalTable: "ContractDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractTag_Tag_TagId",
                        column: x => x.TagId,
                        principalSchema: "Common",
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractDocumentGroupDetail",
                schema: "Cont",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContractDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    AtLeastRequiredDocument = table.Column<long>(type: "bigint", nullable: false),
                    Required = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractDocumentGroupDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractDocumentGroupDetail_ContractDefinition_ContractDefi~",
                        column: x => x.ContractDefinitionId,
                        principalSchema: "Cont",
                        principalTable: "ContractDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractDocumentGroupDetail_DocumentGroup_DocumentGroupId",
                        column: x => x.DocumentGroupId,
                        principalSchema: "DocGroup",
                        principalTable: "DocumentGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentGroupLanguageDetail",
                schema: "DocGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MultiLanguageId = table.Column<Guid>(type: "uuid", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "ContractValidation",
                schema: "Cont",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContractDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ValidationId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractValidation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractValidation_ContractDefinition_ContractDefinitionId",
                        column: x => x.ContractDefinitionId,
                        principalSchema: "Cont",
                        principalTable: "ContractDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractValidation_Validation_ValidationId",
                        column: x => x.ValidationId,
                        principalSchema: "Common",
                        principalTable: "Validation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractDocumentDetail",
                schema: "Cont",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContractDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    UseExisting = table.Column<int>(type: "integer", nullable: false),
                    Semver = table.Column<string>(type: "text", nullable: false),
                    Required = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractDocumentDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractDocumentDetail_ContractDefinition_ContractDefinitio~",
                        column: x => x.ContractDefinitionId,
                        principalSchema: "Cont",
                        principalTable: "ContractDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractDocumentDetail_DocumentDefinition_DocumentDefinitio~",
                        column: x => x.DocumentDefinitionId,
                        principalSchema: "Doc",
                        principalTable: "DocumentDefinition",
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
                    StatusId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Document_Status_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "Common",
                        principalTable: "Status",
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
                name: "DocumentGroupDetail",
                schema: "DocGroup",
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
                        principalSchema: "Doc",
                        principalTable: "DocumentDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentGroupDetail_DocumentGroup_DocumentGroupId",
                        column: x => x.DocumentGroupId,
                        principalSchema: "DocGroup",
                        principalTable: "DocumentGroup",
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
                    TagId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_DocumentTag_Tag_TagId",
                        column: x => x.TagId,
                        principalSchema: "Common",
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractDefinition_Code",
                schema: "Cont",
                table: "ContractDefinition",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractDefinition_StatusId",
                schema: "Cont",
                table: "ContractDefinition",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDocumentDetail_ContractDefinitionId",
                schema: "Cont",
                table: "ContractDocumentDetail",
                column: "ContractDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDocumentDetail_DocumentDefinitionId",
                schema: "Cont",
                table: "ContractDocumentDetail",
                column: "DocumentDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDocumentGroupDetail_ContractDefinitionId",
                schema: "Cont",
                table: "ContractDocumentGroupDetail",
                column: "ContractDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDocumentGroupDetail_DocumentGroupId",
                schema: "Cont",
                table: "ContractDocumentGroupDetail",
                column: "DocumentGroupId");

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
                name: "IX_ContractTag_ContractDefinitionId",
                schema: "Cont",
                table: "ContractTag",
                column: "ContractDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTag_TagId",
                schema: "Cont",
                table: "ContractTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractValidation_ContractDefinitionId",
                schema: "Cont",
                table: "ContractValidation",
                column: "ContractDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractValidation_ValidationId",
                schema: "Cont",
                table: "ContractValidation",
                column: "ValidationId");

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
                name: "IX_Document_StatusId",
                schema: "Doc",
                table: "Document",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAllowedClient_Code",
                schema: "Doc",
                table: "DocumentAllowedClient",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAllowedClientDetail_DocumentAllowedClientId",
                schema: "Doc",
                table: "DocumentAllowedClientDetail",
                column: "DocumentAllowedClientId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAllowedClientDetail_DocumentOnlineSingId",
                schema: "Doc",
                table: "DocumentAllowedClientDetail",
                column: "DocumentOnlineSingId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAllowedClientDetail_DocumentRenderId",
                schema: "Doc",
                table: "DocumentAllowedClientDetail",
                column: "DocumentRenderId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAllowedClientDetail_DocumentUploadId",
                schema: "Doc",
                table: "DocumentAllowedClientDetail",
                column: "DocumentUploadId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDefinition_BaseStatusId",
                schema: "Doc",
                table: "DocumentDefinition",
                column: "BaseStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDefinition_Code",
                schema: "Doc",
                table: "DocumentDefinition",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDefinition_DocumentOnlineSingId",
                schema: "Doc",
                table: "DocumentDefinition",
                column: "DocumentOnlineSingId");

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
                name: "IX_DocumentDefinition_DocumentUploadId",
                schema: "Doc",
                table: "DocumentDefinition",
                column: "DocumentUploadId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDefinition_StatusId",
                schema: "Doc",
                table: "DocumentDefinition",
                column: "StatusId");

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
                name: "IX_DocumentFormat_DocumentFormatTypeId",
                schema: "Doc",
                table: "DocumentFormat",
                column: "DocumentFormatTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFormat_DocumentSizeId",
                schema: "Doc",
                table: "DocumentFormat",
                column: "DocumentSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFormatDetail_DocumentFormatId",
                schema: "Doc",
                table: "DocumentFormatDetail",
                column: "DocumentFormatId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFormatDetail_DocumentUploadId",
                schema: "Doc",
                table: "DocumentFormatDetail",
                column: "DocumentUploadId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFormatType_Code",
                schema: "Doc",
                table: "DocumentFormatType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentGroup_Code",
                schema: "DocGroup",
                table: "DocumentGroup",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentGroup_StatusId",
                schema: "DocGroup",
                table: "DocumentGroup",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentGroupDetail_DocumentDefinitionId",
                schema: "DocGroup",
                table: "DocumentGroupDetail",
                column: "DocumentDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentGroupDetail_DocumentGroupId",
                schema: "DocGroup",
                table: "DocumentGroupDetail",
                column: "DocumentGroupId");

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

            migrationBuilder.CreateIndex(
                name: "IX_DocumentOperationsTagsDetail_DocumentOperationsId",
                schema: "Doc",
                table: "DocumentOperationsTagsDetail",
                column: "DocumentOperationsId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentOperationsTagsDetail_TagId",
                schema: "Doc",
                table: "DocumentOperationsTagsDetail",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentOptimize_DocumentOptimizeTypeId",
                schema: "Doc",
                table: "DocumentOptimize",
                column: "DocumentOptimizeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentOptimizeType_Code",
                schema: "Doc",
                table: "DocumentOptimizeType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTag_DocumentDefinitionId",
                schema: "Doc",
                table: "DocumentTag",
                column: "DocumentDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTag_TagId",
                schema: "Doc",
                table: "DocumentTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTemplate_Code",
                schema: "Doc",
                table: "DocumentTemplate",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTemplate_LanguageTypeId",
                schema: "Doc",
                table: "DocumentTemplate",
                column: "LanguageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTemplateDetail_DocumentOnlineSingId",
                schema: "Doc",
                table: "DocumentTemplateDetail",
                column: "DocumentOnlineSingId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTemplateDetail_DocumentRenderId",
                schema: "Doc",
                table: "DocumentTemplateDetail",
                column: "DocumentRenderId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTemplateDetail_DocumentTemplateId",
                schema: "Doc",
                table: "DocumentTemplateDetail",
                column: "DocumentTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityProperty_Code",
                schema: "EAV",
                table: "EntityProperty",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntityProperty_EntityPropertyValueId",
                schema: "EAV",
                table: "EntityProperty",
                column: "EntityPropertyValueId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageType_Code",
                schema: "Common",
                table: "LanguageType",
                column: "Code",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Status_Code",
                schema: "Common",
                table: "Status",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tag_Code",
                schema: "Common",
                table: "Tag",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Validation_EValidationType",
                schema: "Common",
                table: "Validation",
                column: "EValidationType",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Validation_ValidationDecisionId",
                schema: "Common",
                table: "Validation",
                column: "ValidationDecisionId");

            migrationBuilder.CreateIndex(
                name: "IX_ValidationDecision_Code",
                schema: "Common",
                table: "ValidationDecision",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractDocumentDetail",
                schema: "Cont");

            migrationBuilder.DropTable(
                name: "ContractDocumentGroupDetail",
                schema: "Cont");

            migrationBuilder.DropTable(
                name: "ContractEntityProperty",
                schema: "Cont");

            migrationBuilder.DropTable(
                name: "ContractTag",
                schema: "Cont");

            migrationBuilder.DropTable(
                name: "ContractValidation",
                schema: "Cont");

            migrationBuilder.DropTable(
                name: "Document",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentAllowedClientDetail",
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
                name: "DocumentGroupDetail",
                schema: "DocGroup");

            migrationBuilder.DropTable(
                name: "DocumentGroupLanguageDetail",
                schema: "DocGroup");

            migrationBuilder.DropTable(
                name: "DocumentOperationsTagsDetail",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentTag",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentTemplateDetail",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "ContractDefinition",
                schema: "Cont");

            migrationBuilder.DropTable(
                name: "Validation",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "DocumentContent",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentAllowedClient",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "EntityProperty",
                schema: "EAV");

            migrationBuilder.DropTable(
                name: "DocumentFormat",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentGroup",
                schema: "DocGroup");

            migrationBuilder.DropTable(
                name: "MultiLanguage",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "DocumentDefinition",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "Tag",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "DocumentRender",
                schema: "DocTp");

            migrationBuilder.DropTable(
                name: "DocumentTemplate",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "ValidationDecision",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "EntityPropertyValue",
                schema: "EAV");

            migrationBuilder.DropTable(
                name: "DocumentFormatType",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentSize",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentOnlineSing",
                schema: "DocTp");

            migrationBuilder.DropTable(
                name: "DocumentOperations",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentOptimize",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentUpload",
                schema: "DocTp");

            migrationBuilder.DropTable(
                name: "Status",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "LanguageType",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "DocumentOptimizeType",
                schema: "Doc");
        }
    }
}
