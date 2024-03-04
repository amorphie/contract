using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.contract.infrastructure.Migrations.Pg
{
    /// <inheritdoc />
    public partial class ContractMigrationsv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BehalfOfUser",
                schema: "Cont",
                table: "ContractProcess");

            migrationBuilder.AddColumn<int>(
                name: "BankEntity",
                schema: "Cont",
                table: "ContractDefinition",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankEntity",
                schema: "Cont",
                table: "ContractDefinition");

            migrationBuilder.AddColumn<string>(
                name: "BehalfOfUser",
                schema: "Cont",
                table: "ContractProcess",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
