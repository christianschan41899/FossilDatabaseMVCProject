using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageSrc",
                table: "Museums");

            migrationBuilder.DropColumn(
                name: "ImageSrc",
                table: "Fossils");

            migrationBuilder.DropColumn(
                name: "ImageSrc",
                table: "DigSites");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    ImageName = table.Column<string>(nullable: true),
                    FossilID = table.Column<int>(nullable: false),
                    MuseumID = table.Column<int>(nullable: false),
                    DigSiteID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageID);
                    table.ForeignKey(
                        name: "FK_Images_DigSites_DigSiteID",
                        column: x => x.DigSiteID,
                        principalTable: "DigSites",
                        principalColumn: "DigSiteID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Images_Fossils_FossilID",
                        column: x => x.FossilID,
                        principalTable: "Fossils",
                        principalColumn: "FossilID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Images_Museums_MuseumID",
                        column: x => x.MuseumID,
                        principalTable: "Museums",
                        principalColumn: "MuseumID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_DigSiteID",
                table: "Images",
                column: "DigSiteID");

            migrationBuilder.CreateIndex(
                name: "IX_Images_FossilID",
                table: "Images",
                column: "FossilID");

            migrationBuilder.CreateIndex(
                name: "IX_Images_MuseumID",
                table: "Images",
                column: "MuseumID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.AddColumn<string>(
                name: "ImageSrc",
                table: "Museums",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageSrc",
                table: "Fossils",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageSrc",
                table: "DigSites",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
