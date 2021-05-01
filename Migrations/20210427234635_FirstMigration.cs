using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "DigSites",
                columns: table => new
                {
                    DigSiteID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SiteName = table.Column<string>(nullable: false),
                    SiteLatitude = table.Column<float>(nullable: false),
                    SiteLongitude = table.Column<float>(nullable: false),
                    ImageSrc = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    FossilID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigSites", x => x.DigSiteID);
                    table.ForeignKey(
                        name: "FK_DigSites_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Museums",
                columns: table => new
                {
                    MuseumID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MuseumName = table.Column<string>(nullable: false),
                    MuseumLatitude = table.Column<float>(nullable: false),
                    MuseumLongitude = table.Column<float>(nullable: false),
                    ImageSrc = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    FossilID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Museums", x => x.MuseumID);
                    table.ForeignKey(
                        name: "FK_Museums_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fossils",
                columns: table => new
                {
                    FossilID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FossilName = table.Column<string>(nullable: false),
                    FossilSpecies = table.Column<string>(nullable: false),
                    ImageSrc = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    MuseumID = table.Column<int>(nullable: false),
                    DigSiteID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fossils", x => x.FossilID);
                    table.ForeignKey(
                        name: "FK_Fossils_DigSites_DigSiteID",
                        column: x => x.DigSiteID,
                        principalTable: "DigSites",
                        principalColumn: "DigSiteID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fossils_Museums_MuseumID",
                        column: x => x.MuseumID,
                        principalTable: "Museums",
                        principalColumn: "MuseumID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fossils_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DigSites_UserID",
                table: "DigSites",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Fossils_DigSiteID",
                table: "Fossils",
                column: "DigSiteID");

            migrationBuilder.CreateIndex(
                name: "IX_Fossils_MuseumID",
                table: "Fossils",
                column: "MuseumID");

            migrationBuilder.CreateIndex(
                name: "IX_Fossils_UserID",
                table: "Fossils",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Museums_UserID",
                table: "Museums",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fossils");

            migrationBuilder.DropTable(
                name: "DigSites");

            migrationBuilder.DropTable(
                name: "Museums");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
