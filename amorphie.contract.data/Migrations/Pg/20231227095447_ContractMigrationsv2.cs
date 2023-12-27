using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.contract.data.Migrations.Pg
{
    /// <inheritdoc />
    public partial class ContractMigrationsv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Validation_ValidationDecision_ValidationDecisionId",
                schema: "Common",
                table: "Validation");

            migrationBuilder.DropIndex(
                name: "IX_ValidationDecision_Code",
                schema: "Common",
                table: "ValidationDecision");

            migrationBuilder.DropIndex(
                name: "IX_Tag_Code",
                schema: "Common",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_EntityProperty_Code",
                schema: "EAV",
                table: "EntityProperty");

            migrationBuilder.DropIndex(
                name: "IX_DocumentOptimizeType_Code",
                schema: "Doc",
                table: "DocumentOptimizeType");

            migrationBuilder.DropIndex(
                name: "IX_DocumentGroup_Code",
                schema: "DocGroup",
                table: "DocumentGroup");

            migrationBuilder.DropIndex(
                name: "IX_DocumentFormatType_Code",
                schema: "Doc",
                table: "DocumentFormatType");

            migrationBuilder.DropIndex(
                name: "IX_DocumentDefinition_Code",
                schema: "Doc",
                table: "DocumentDefinition");

            migrationBuilder.DropIndex(
                name: "IX_DocumentAllowedClient_Code",
                schema: "Doc",
                table: "DocumentAllowedClient");

            migrationBuilder.AlterColumn<Guid>(
                name: "ValidationDecisionId",
                schema: "Common",
                table: "Validation",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateTable(
                name: "ContractProcess",
                schema: "Cont",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Client = table.Column<string>(type: "text", nullable: false),
                    User = table.Column<string>(type: "text", nullable: false),
                    BehalfOfUser = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false),
                    Action = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractProcess", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                schema: "Cont",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContractName = table.Column<string>(type: "text", nullable: false),
                    Reference = table.Column<string>(type: "text", nullable: false),
                    Owner = table.Column<string>(type: "text", nullable: false),
                    CallbackName = table.Column<string>(type: "text", nullable: false),
                    ProcessId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contract_ContractProcess_ProcessId",
                        column: x => x.ProcessId,
                        principalSchema: "Cont",
                        principalTable: "ContractProcess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ProcessId",
                schema: "Cont",
                table: "Contract",
                column: "ProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_Validation_ValidationDecision_ValidationDecisionId",
                schema: "Common",
                table: "Validation",
                column: "ValidationDecisionId",
                principalSchema: "Common",
                principalTable: "ValidationDecision",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Validation_ValidationDecision_ValidationDecisionId",
                schema: "Common",
                table: "Validation");

            migrationBuilder.DropTable(
                name: "Contract",
                schema: "Cont");

            migrationBuilder.DropTable(
                name: "ContractProcess",
                schema: "Cont");

            migrationBuilder.AlterColumn<Guid>(
                name: "ValidationDecisionId",
                schema: "Common",
                table: "Validation",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ValidationDecision_Code",
                schema: "Common",
                table: "ValidationDecision",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tag_Code",
                schema: "Common",
                table: "Tag",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntityProperty_Code",
                schema: "EAV",
                table: "EntityProperty",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentOptimizeType_Code",
                schema: "Doc",
                table: "DocumentOptimizeType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentGroup_Code",
                schema: "DocGroup",
                table: "DocumentGroup",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFormatType_Code",
                schema: "Doc",
                table: "DocumentFormatType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDefinition_Code",
                schema: "Doc",
                table: "DocumentDefinition",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAllowedClient_Code",
                schema: "Doc",
                table: "DocumentAllowedClient",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Validation_ValidationDecision_ValidationDecisionId",
                schema: "Common",
                table: "Validation",
                column: "ValidationDecisionId",
                principalSchema: "Common",
                principalTable: "ValidationDecision",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
