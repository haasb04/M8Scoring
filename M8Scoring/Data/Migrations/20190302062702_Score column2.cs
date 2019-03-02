using Microsoft.EntityFrameworkCore.Migrations;

namespace M8Scoring.Data.Migrations
{
    public partial class Scorecolumn2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "MatchSetPlayers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "MatchSetPlayers");
        }
    }
}
