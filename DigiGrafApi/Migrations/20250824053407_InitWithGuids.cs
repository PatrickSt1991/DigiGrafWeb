using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiGrafWeb.Migrations
{
    /// <inheritdoc />
    public partial class InitWithGuids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dossiers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FuneralLeader = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FuneralNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FuneralType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Voorregeling = table.Column<bool>(type: "bit", nullable: false),
                    DossierCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dossiers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeathInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfDeath = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeOfDeath = table.Column<TimeSpan>(type: "time", nullable: true),
                    LocationOfDeath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCodeOfDeath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseNumberOfDeath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseNumberAdditionOfDeath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetOfDeath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityOfDeath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountyOfDeath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BodyFinding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DossierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeathInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeathInfos_Dossiers_DossierId",
                        column: x => x.DossierId,
                        principalTable: "Dossiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Deceaseds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SocialSecurity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salutation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlaceOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseNumberAddition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeDeceased = table.Column<bool>(type: "bit", nullable: false),
                    Gp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GpPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Me = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DossierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deceaseds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deceaseds_Dossiers_DossierId",
                        column: x => x.DossierId,
                        principalTable: "Dossiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeathInfos_DossierId",
                table: "DeathInfos",
                column: "DossierId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deceaseds_DossierId",
                table: "Deceaseds",
                column: "DossierId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeathInfos");

            migrationBuilder.DropTable(
                name: "Deceaseds");

            migrationBuilder.DropTable(
                name: "Dossiers");
        }
    }
}
