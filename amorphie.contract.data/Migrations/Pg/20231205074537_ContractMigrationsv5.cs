using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.contract.data.Migrations.Pg
{
    /// <inheritdoc />
    public partial class ContractMigrationsv5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                schema: "Doc",
                table: "Document",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "DocumentDefinitionCode",
                schema: "Doc",
                table: "Document",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Owner = table.Column<string>(type: "text", nullable: false),
                    Reference = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Document_CustomerId",
                schema: "Doc",
                table: "Document",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Customer_CustomerId",
                schema: "Doc",
                table: "Document",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_Customer_CustomerId",
                schema: "Doc",
                table: "Document");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Document_CustomerId",
                schema: "Doc",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "Doc",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "DocumentDefinitionCode",
                schema: "Doc",
                table: "Document");
        }
    }
}
