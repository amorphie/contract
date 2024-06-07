using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.contract.infrastructure.Migrations.Pg
{
    /// <inheritdoc />
    public partial class ContractMigrationsv27 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DecisionTableId",
                schema: "Cont",
                table: "ContractDefinition",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DecisionTableMetadata",
                schema: "Cont",
                table: "ContractDefinition",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DecisionTableId",
                schema: "Cont",
                table: "ContractDefinition");

            migrationBuilder.DropColumn(
                name: "DecisionTableMetadata",
                schema: "Cont",
                table: "ContractDefinition");
        }
    }
}
