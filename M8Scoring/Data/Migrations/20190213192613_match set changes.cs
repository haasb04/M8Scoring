using Microsoft.EntityFrameworkCore.Migrations;

namespace M8Scoring.data.migrations
{
    public partial class matchsetchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_MatchSets_Set1Id",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_MatchSets_Set2Id",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_MatchSets_Set3Id",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_MatchSets_Set4Id",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_MatchSets_Set5Id",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_Set1Id",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_Set2Id",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_Set3Id",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_Set4Id",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_Set5Id",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Set1Id",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Set2Id",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Set3Id",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Set4Id",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Set5Id",
                table: "Matches");

            migrationBuilder.AddColumn<int>(
                name: "MatchId",
                table: "MatchSets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchSets_MatchId",
                table: "MatchSets",
                column: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchSets_Matches_MatchId",
                table: "MatchSets",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchSets_Matches_MatchId",
                table: "MatchSets");

            migrationBuilder.DropIndex(
                name: "IX_MatchSets_MatchId",
                table: "MatchSets");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "MatchSets");

            migrationBuilder.AddColumn<int>(
                name: "Set1Id",
                table: "Matches",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Set2Id",
                table: "Matches",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Set3Id",
                table: "Matches",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Set4Id",
                table: "Matches",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Set5Id",
                table: "Matches",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Set1Id",
                table: "Matches",
                column: "Set1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Set2Id",
                table: "Matches",
                column: "Set2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Set3Id",
                table: "Matches",
                column: "Set3Id");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Set4Id",
                table: "Matches",
                column: "Set4Id");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Set5Id",
                table: "Matches",
                column: "Set5Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_MatchSets_Set1Id",
                table: "Matches",
                column: "Set1Id",
                principalTable: "MatchSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_MatchSets_Set2Id",
                table: "Matches",
                column: "Set2Id",
                principalTable: "MatchSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_MatchSets_Set3Id",
                table: "Matches",
                column: "Set3Id",
                principalTable: "MatchSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_MatchSets_Set4Id",
                table: "Matches",
                column: "Set4Id",
                principalTable: "MatchSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_MatchSets_Set5Id",
                table: "Matches",
                column: "Set5Id",
                principalTable: "MatchSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
