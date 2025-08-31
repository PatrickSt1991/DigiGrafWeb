using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiGrafWeb.Migrations
{
    /// <inheritdoc />
    public partial class DocumentsImprovementsv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "DocumentTemplates",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "DocumentTemplates");
        }
    }
}
