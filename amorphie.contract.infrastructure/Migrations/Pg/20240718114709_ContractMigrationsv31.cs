using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.contract.infrastructure.Migrations.Pg
{
    /// <inheritdoc />
    public partial class ContractMigrationsv31 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TagValuesOrg",
                schema: "Doc",
                table: "DocumentMigrationDysDocumentTags",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagValuesOrg",
                schema: "Doc",
                table: "DocumentMigrationDysDocumentTags");
        }
    }
}
