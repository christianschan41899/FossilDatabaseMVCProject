using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class FifthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_DigSites_DigSiteID",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Museums_MuseumID",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_DigSiteID",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_MuseumID",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "DigSiteID",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "MuseumID",
                table: "Images");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DigSiteID",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MuseumID",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Images_DigSiteID",
                table: "Images",
                column: "DigSiteID");

            migrationBuilder.CreateIndex(
                name: "IX_Images_MuseumID",
                table: "Images",
                column: "MuseumID");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_DigSites_DigSiteID",
                table: "Images",
                column: "DigSiteID",
                principalTable: "DigSites",
                principalColumn: "DigSiteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Museums_MuseumID",
                table: "Images",
                column: "MuseumID",
                principalTable: "Museums",
                principalColumn: "MuseumID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
