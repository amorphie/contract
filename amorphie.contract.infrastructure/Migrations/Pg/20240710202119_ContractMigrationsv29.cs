using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.contract.infrastructure.Migrations.Pg
{
    /// <inheritdoc />
    public partial class ContractMigrationsv29 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Reference",
                schema: "Cus",
                table: "Customer",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxNo",
                schema: "Cus",
                table: "Customer",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DocumentMigrationAggregations",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentCode = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ContractCodes = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentMigrationAggregations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentMigrationDysDocuments",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "character varying(350)", maxLength: 350, nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DocCreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    OwnerId = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Channel = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentMigrationDysDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentMigrationDysDocumentTags",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocId = table.Column<long>(type: "bigint", nullable: false),
                    TagId = table.Column<string>(type: "character varying(350)", maxLength: 350, nullable: false),
                    TagValues = table.Column<string>(type: "jsonb", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentMigrationDysDocumentTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentMigrationProcessing",
                schema: "Doc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocId = table.Column<long>(type: "bigint", nullable: false),
                    TagId = table.Column<string>(type: "character varying(350)", maxLength: 350, nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TryCount = table.Column<int>(type: "integer", nullable: false),
                    LastTryTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ErrorMessage = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentMigrationProcessing", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentMigrationAggregations_DocumentCode",
                schema: "Doc",
                table: "DocumentMigrationAggregations",
                column: "DocumentCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentMigrationDysDocuments_DocId",
                schema: "Doc",
                table: "DocumentMigrationDysDocuments",
                column: "DocId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentMigrationDysDocumentTags_DocId",
                schema: "Doc",
                table: "DocumentMigrationDysDocumentTags",
                column: "DocId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentMigrationDysDocumentTags_TagId",
                schema: "Doc",
                table: "DocumentMigrationDysDocumentTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentMigrationProcessing_DocId",
                schema: "Doc",
                table: "DocumentMigrationProcessing",
                column: "DocId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentMigrationProcessing_TagId",
                schema: "Doc",
                table: "DocumentMigrationProcessing",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentMigrationAggregations",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentMigrationDysDocuments",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentMigrationDysDocumentTags",
                schema: "Doc");

            migrationBuilder.DropTable(
                name: "DocumentMigrationProcessing",
                schema: "Doc");

            migrationBuilder.DropColumn(
                name: "TaxNo",
                schema: "Cus",
                table: "Customer");

            migrationBuilder.AlterColumn<string>(
                name: "Reference",
                schema: "Cus",
                table: "Customer",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
