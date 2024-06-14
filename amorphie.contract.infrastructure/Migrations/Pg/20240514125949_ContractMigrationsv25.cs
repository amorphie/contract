using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.contract.infrastructure.Migrations.Pg
{
    /// <inheritdoc />
    public partial class ContractMigrationsv25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "DocGroup",
                table: "DocumentGroup");

            migrationBuilder.DropColumn(
                name: "BaseStatus",
                schema: "Doc",
                table: "DocumentDefinition");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "Doc",
                table: "DocumentDefinition");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "Cont",
                table: "ContractDefinition");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "DocGroup",
                table: "DocumentGroup",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BaseStatus",
                schema: "Doc",
                table: "DocumentDefinition",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "Doc",
                table: "DocumentDefinition",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "Cont",
                table: "ContractDefinition",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
