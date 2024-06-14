using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.contract.infrastructure.Migrations.Pg
{
    /// <inheritdoc />
    public partial class ContractMigrationsv23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserSignedContractDetail_DocumentInstanceId_UserSignedContr~",
                schema: "Cont",
                table: "UserSignedContractDetail",
                columns: new[] { "DocumentInstanceId", "UserSignedContractId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserSignedContractDetail_DocumentInstanceId_UserSignedContr~",
                schema: "Cont",
                table: "UserSignedContractDetail");
        }
    }
}
