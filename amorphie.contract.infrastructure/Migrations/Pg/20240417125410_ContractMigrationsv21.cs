using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.contract.infrastructure.Migrations.Pg
{
    /// <inheritdoc />
    public partial class ContractMigrationsv21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE \"DocTp\".\"DocumentOnlineSing\" RENAME TO \"DocumentOnlineSign\"; ");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentAllowedClientDetail_DocumentOnlineSing_DocumentOnli~",
                schema: "Doc",
                table: "DocumentAllowedClientDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentDefinition_DocumentOnlineSing_DocumentOnlineSingId",
                schema: "Doc",
                table: "DocumentDefinition");

            migrationBuilder.DropTable(
                name: "ContractInstanceDetail",
                schema: "Cont");

            // migrationBuilder.DropTable(
            //     name: "DocumentOnlineSing",
            //     schema: "DocTp");

            migrationBuilder.DropTable(
                name: "ContractInstance",
                schema: "Cont");

            migrationBuilder.RenameColumn(
                name: "DocumentOnlineSingId",
                schema: "Doc",
                table: "DocumentDefinition",
                newName: "DocumentOnlineSignId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentDefinition_DocumentOnlineSingId",
                schema: "Doc",
                table: "DocumentDefinition",
                newName: "IX_DocumentDefinition_DocumentOnlineSignId");

            migrationBuilder.RenameColumn(
                name: "DocumentOnlineSingId",
                schema: "Doc",
                table: "DocumentAllowedClientDetail",
                newName: "DocumentOnlineSignId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentAllowedClientDetail_DocumentOnlineSingId",
                schema: "Doc",
                table: "DocumentAllowedClientDetail",
                newName: "IX_DocumentAllowedClientDetail_DocumentOnlineSignId");

            // migrationBuilder.CreateTable(
            //     name: "DocumentOnlineSign",
            //     schema: "DocTp",
            //     columns: table => new
            //     {
            //         Id = table.Column<Guid>(type: "uuid", nullable: false),
            //         Required = table.Column<bool>(type: "boolean", nullable: false),
            //         CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
            //         CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
            //         CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
            //         ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
            //         ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
            //         ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
            //         IsActive = table.Column<bool>(type: "boolean", nullable: false),
            //         IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
            //         Templates = table.Column<string>(type: "jsonb", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_DocumentOnlineSign", x => x.Id);
            //     });

            migrationBuilder.CreateTable(
                name: "UserSignedContract",
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
                    table.PrimaryKey("PK_UserSignedContract", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSignedContractDetail",
                schema: "Cont",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentInstanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserSignedContractId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_UserSignedContractDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSignedContractDetail_UserSignedContract_UserSignedContr~",
                        column: x => x.UserSignedContractId,
                        principalSchema: "Cont",
                        principalTable: "UserSignedContract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSignedContract_CustomerId",
                schema: "Cont",
                table: "UserSignedContract",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSignedContractDetail_DocumentInstanceId",
                schema: "Cont",
                table: "UserSignedContractDetail",
                column: "DocumentInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSignedContractDetail_UserSignedContractId",
                schema: "Cont",
                table: "UserSignedContractDetail",
                column: "UserSignedContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentAllowedClientDetail_DocumentOnlineSign_DocumentOnli~",
                schema: "Doc",
                table: "DocumentAllowedClientDetail",
                column: "DocumentOnlineSignId",
                principalSchema: "DocTp",
                principalTable: "DocumentOnlineSign",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentDefinition_DocumentOnlineSign_DocumentOnlineSignId",
                schema: "Doc",
                table: "DocumentDefinition",
                column: "DocumentOnlineSignId",
                principalSchema: "DocTp",
                principalTable: "DocumentOnlineSign",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentAllowedClientDetail_DocumentOnlineSign_DocumentOnli~",
                schema: "Doc",
                table: "DocumentAllowedClientDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentDefinition_DocumentOnlineSign_DocumentOnlineSignId",
                schema: "Doc",
                table: "DocumentDefinition");

            migrationBuilder.DropTable(
                name: "DocumentOnlineSign",
                schema: "DocTp");

            migrationBuilder.DropTable(
                name: "UserSignedContractDetail",
                schema: "Cont");

            migrationBuilder.DropTable(
                name: "UserSignedContract",
                schema: "Cont");

            migrationBuilder.RenameColumn(
                name: "DocumentOnlineSignId",
                schema: "Doc",
                table: "DocumentDefinition",
                newName: "DocumentOnlineSingId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentDefinition_DocumentOnlineSignId",
                schema: "Doc",
                table: "DocumentDefinition",
                newName: "IX_DocumentDefinition_DocumentOnlineSingId");

            migrationBuilder.RenameColumn(
                name: "DocumentOnlineSignId",
                schema: "Doc",
                table: "DocumentAllowedClientDetail",
                newName: "DocumentOnlineSingId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentAllowedClientDetail_DocumentOnlineSignId",
                schema: "Doc",
                table: "DocumentAllowedClientDetail",
                newName: "IX_DocumentAllowedClientDetail_DocumentOnlineSingId");

            migrationBuilder.CreateTable(
                name: "ContractInstance",
                schema: "Cont",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContractCode = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractInstance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentOnlineSing",
                schema: "DocTp",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    Required = table.Column<bool>(type: "boolean", nullable: false),
                    Templates = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentOnlineSing", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractInstanceDetail",
                schema: "Cont",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContractInstanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    DocumentInstanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
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

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentAllowedClientDetail_DocumentOnlineSing_DocumentOnli~",
                schema: "Doc",
                table: "DocumentAllowedClientDetail",
                column: "DocumentOnlineSingId",
                principalSchema: "DocTp",
                principalTable: "DocumentOnlineSing",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentDefinition_DocumentOnlineSing_DocumentOnlineSingId",
                schema: "Doc",
                table: "DocumentDefinition",
                column: "DocumentOnlineSingId",
                principalSchema: "DocTp",
                principalTable: "DocumentOnlineSing",
                principalColumn: "Id");
        }
    }
}
