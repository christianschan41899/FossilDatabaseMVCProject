using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FossilID",
                table: "Museums");

            migrationBuilder.DropColumn(
                name: "FossilID",
                table: "DigSites");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FossilID",
                table: "Museums",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FossilID",
                table: "DigSites",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
