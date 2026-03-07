using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiGrafWeb.Migrations
{
    /// <inheritdoc />
    public partial class PriceComponentsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsurancePriceComponents_InsuranceParties_InsurancePartyId",
                table: "InsurancePriceComponents");

            migrationBuilder.DropIndex(
                name: "IX_InsurancePriceComponents_InsurancePartyId_IsActive",
                table: "InsurancePriceComponents");

            migrationBuilder.DropColumn(
                name: "InsurancePartyId",
                table: "InsurancePriceComponents");

            migrationBuilder.RenameColumn(
                name: "Aantal",
                table: "InsurancePriceComponents",
                newName: "VerzekerdAantal");

            migrationBuilder.AddColumn<decimal>(
                name: "FactuurBedrag",
                table: "InsurancePriceComponents",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "StandaardPM",
                table: "InsurancePriceComponents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "InsurancePriceComponentInsuranceParties",
                columns: table => new
                {
                    InsurancePriceComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsurancePartyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurancePriceComponentInsuranceParties", x => new { x.InsurancePriceComponentId, x.InsurancePartyId });
                    table.ForeignKey(
                        name: "FK_InsurancePriceComponentInsuranceParties_InsuranceParties_InsurancePartyId",
                        column: x => x.InsurancePartyId,
                        principalTable: "InsuranceParties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InsurancePriceComponentInsuranceParties_InsurancePriceComponents_InsurancePriceComponentId",
                        column: x => x.InsurancePriceComponentId,
                        principalTable: "InsurancePriceComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePriceComponents_IsActive_SortOrder",
                table: "InsurancePriceComponents",
                columns: new[] { "IsActive", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePriceComponentInsuranceParties_InsurancePartyId",
                table: "InsurancePriceComponentInsuranceParties",
                column: "InsurancePartyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InsurancePriceComponentInsuranceParties");

            migrationBuilder.DropIndex(
                name: "IX_InsurancePriceComponents_IsActive_SortOrder",
                table: "InsurancePriceComponents");

            migrationBuilder.DropColumn(
                name: "FactuurBedrag",
                table: "InsurancePriceComponents");

            migrationBuilder.DropColumn(
                name: "StandaardPM",
                table: "InsurancePriceComponents");

            migrationBuilder.RenameColumn(
                name: "VerzekerdAantal",
                table: "InsurancePriceComponents",
                newName: "Aantal");

            migrationBuilder.AddColumn<Guid>(
                name: "InsurancePartyId",
                table: "InsurancePriceComponents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePriceComponents_InsurancePartyId_IsActive",
                table: "InsurancePriceComponents",
                columns: new[] { "InsurancePartyId", "IsActive" });

            migrationBuilder.AddForeignKey(
                name: "FK_InsurancePriceComponents_InsuranceParties_InsurancePartyId",
                table: "InsurancePriceComponents",
                column: "InsurancePartyId",
                principalTable: "InsuranceParties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
