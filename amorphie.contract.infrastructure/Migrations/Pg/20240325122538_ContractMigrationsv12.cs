using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.contract.infrastructure.Migrations.Pg
{
    /// <inheritdoc />
    public partial class ContractMigrationsv12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Titles",
                schema: "DocGroup",
                table: "DocumentGroup",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Titles",
                schema: "Doc",
                table: "DocumentDefinition",
                type: "jsonb",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Titles",
                schema: "DocGroup",
                table: "DocumentGroup");

            migrationBuilder.DropColumn(
                name: "Titles",
                schema: "Doc",
                table: "DocumentDefinition");
        }
    }
}
