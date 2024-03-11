using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.contract.infrastructure.Migrations.Pg
{
    /// <inheritdoc />
    public partial class ContractMigrationsv5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContractDefinition_Code",
                schema: "Cont",
                table: "ContractDefinition");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDefinition_Code_Semver",
                schema: "Doc",
                table: "DocumentDefinition",
                columns: new[] { "Code", "Semver" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractDefinition_Code_BankEntity",
                schema: "Cont",
                table: "ContractDefinition",
                columns: new[] { "Code", "BankEntity" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DocumentDefinition_Code_Semver",
                schema: "Doc",
                table: "DocumentDefinition");

            migrationBuilder.DropIndex(
                name: "IX_ContractDefinition_Code_BankEntity",
                schema: "Cont",
                table: "ContractDefinition");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDefinition_Code",
                schema: "Cont",
                table: "ContractDefinition",
                column: "Code",
                unique: true);
        }
    }
}
