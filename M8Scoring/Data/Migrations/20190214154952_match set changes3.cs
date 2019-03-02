using Microsoft.EntityFrameworkCore.Migrations;

namespace M8Scoring.data.migrations
{
    public partial class matchsetchanges3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchSets_MatchSetPlayers_Player1Id",
                table: "MatchSets");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchSets_MatchSetPlayers_Player2Id",
                table: "MatchSets");

            migrationBuilder.DropIndex(
                name: "IX_MatchSets_Player1Id",
                table: "MatchSets");

            migrationBuilder.DropIndex(
                name: "IX_MatchSets_Player2Id",
                table: "MatchSets");

            migrationBuilder.DropColumn(
                name: "Player1Id",
                table: "MatchSets");

            migrationBuilder.DropColumn(
                name: "Player2Id",
                table: "MatchSets");

            migrationBuilder.AddColumn<int>(
                name: "MatchSetId",
                table: "MatchSetPlayers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "MatchSetPlayers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MatchSetPlayers_MatchSetId",
                table: "MatchSetPlayers",
                column: "MatchSetId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchSetPlayers_MatchSets_MatchSetId",
                table: "MatchSetPlayers",
                column: "MatchSetId",
                principalTable: "MatchSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchSetPlayers_MatchSets_MatchSetId",
                table: "MatchSetPlayers");

            migrationBuilder.DropIndex(
                name: "IX_MatchSetPlayers_MatchSetId",
                table: "MatchSetPlayers");

            migrationBuilder.DropColumn(
                name: "MatchSetId",
                table: "MatchSetPlayers");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "MatchSetPlayers");

            migrationBuilder.AddColumn<int>(
                name: "Player1Id",
                table: "MatchSets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Player2Id",
                table: "MatchSets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchSets_Player1Id",
                table: "MatchSets",
                column: "Player1Id");

            migrationBuilder.CreateIndex(
                name: "IX_MatchSets_Player2Id",
                table: "MatchSets",
                column: "Player2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchSets_MatchSetPlayers_Player1Id",
                table: "MatchSets",
                column: "Player1Id",
                principalTable: "MatchSetPlayers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchSets_MatchSetPlayers_Player2Id",
                table: "MatchSets",
                column: "Player2Id",
                principalTable: "MatchSetPlayers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
