using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.contract.infrastructure.Migrations.Pg
{
    /// <inheritdoc />
    public partial class ContractMigrationsv15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractInstance",
                schema: "Cont",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContractCode = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractInstance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractInstanceDetail",
                schema: "Cont",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentInstanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContractInstanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractInstanceDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractInstanceDetail_ContractInstance_ContractInstanceId",
                        column: x => x.ContractInstanceId,
                        principalSchema: "Cont",
                        principalTable: "ContractInstance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractInstance_CustomerId",
                schema: "Cont",
                table: "ContractInstance",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractInstanceDetail_ContractInstanceId",
                schema: "Cont",
                table: "ContractInstanceDetail",
                column: "ContractInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractInstanceDetail_DocumentInstanceId",
                schema: "Cont",
                table: "ContractInstanceDetail",
                column: "DocumentInstanceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractInstanceDetail",
                schema: "Cont");

            migrationBuilder.DropTable(
                name: "ContractInstance",
                schema: "Cont");
        }
    }
}
