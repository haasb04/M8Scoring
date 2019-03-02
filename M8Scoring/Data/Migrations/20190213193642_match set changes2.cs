using Microsoft.EntityFrameworkCore.Migrations;

namespace M8Scoring.data.migrations
{
    public partial class matchsetchanges2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchSets_Matches_MatchId",
                table: "MatchSets");

            migrationBuilder.AlterColumn<int>(
                name: "MatchId",
                table: "MatchSets",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchSets_Matches_MatchId",
                table: "MatchSets",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchSets_Matches_MatchId",
                table: "MatchSets");

            migrationBuilder.AlterColumn<int>(
                name: "MatchId",
                table: "MatchSets",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_MatchSets_Matches_MatchId",
                table: "MatchSets",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
