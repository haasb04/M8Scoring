using Microsoft.EntityFrameworkCore.Migrations;

namespace M8Scoring.Data.Migrations
{
    public partial class locations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Home",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "Teams",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Home",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Teams");
        }
    }
}
