using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiGrafWeb.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginEnabled",
                table: "InsuranceCompanies");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "InsuranceCompanies",
                newName: "Value");

            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "InsuranceCompanies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Label",
                table: "InsuranceCompanies");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "InsuranceCompanies",
                newName: "Name");

            migrationBuilder.AddColumn<bool>(
                name: "OriginEnabled",
                table: "InsuranceCompanies",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
