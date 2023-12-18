using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.contract.data.Migrations.Pg
{
    /// <inheritdoc />
    public partial class ContractMigrationsV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Version",
                schema: "Doc",
                table: "DocumentTemplate",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                schema: "Doc",
                table: "DocumentTemplate");
        }
    }
}
