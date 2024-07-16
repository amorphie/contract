using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.contract.infrastructure.Migrations.Pg
{
    /// <inheritdoc />
    public partial class ContractMigrationsv30 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DocumentMigrationProcessing_DocId",
                schema: "Doc",
                table: "DocumentMigrationProcessing");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentMigrationProcessing_DocId",
                schema: "Doc",
                table: "DocumentMigrationProcessing",
                column: "DocId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DocumentMigrationProcessing_DocId",
                schema: "Doc",
                table: "DocumentMigrationProcessing");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentMigrationProcessing_DocId",
                schema: "Doc",
                table: "DocumentMigrationProcessing",
                column: "DocId",
                unique: true);
        }
    }
}
