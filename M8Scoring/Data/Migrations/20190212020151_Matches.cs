using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace M8Scoring.Data.Migrations
{
    public partial class Matches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MatchSetPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlayerId = table.Column<int>(nullable: true),
                    Rate = table.Column<int>(nullable: false),
                    Forfeit = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchSetPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchSetPlayers_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatchSets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SetNumber = table.Column<int>(nullable: false),
                    Player1Id = table.Column<int>(nullable: true),
                    Player2Id = table.Column<int>(nullable: true),
                    Win = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchSets_MatchSetPlayers_Player1Id",
                        column: x => x.Player1Id,
                        principalTable: "MatchSetPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchSets_MatchSetPlayers_Player2Id",
                        column: x => x.Player2Id,
                        principalTable: "MatchSetPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    WinMultiplier = table.Column<int>(nullable: false),
                    OverUnderPenalty = table.Column<int>(nullable: false),
                    PenaltyMultiplier = table.Column<int>(nullable: false),
                    NonRatedPlayerRate = table.Column<int>(nullable: false),
                    IsRegularSeason = table.Column<bool>(nullable: false),
                    TotalScore = table.Column<int>(nullable: false),
                    TotalOpponentScore = table.Column<int>(nullable: false),
                    TotalRate = table.Column<int>(nullable: false),
                    TotalOpponentRate = table.Column<int>(nullable: false),
                    TeamBonusOrPenalty = table.Column<int>(nullable: false),
                    OpponentBonusOrPenalty = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: true),
                    OpponentId = table.Column<int>(nullable: true),
                    Set1Id = table.Column<int>(nullable: true),
                    Set2Id = table.Column<int>(nullable: true),
                    Set3Id = table.Column<int>(nullable: true),
                    Set4Id = table.Column<int>(nullable: true),
                    Set5Id = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_OpponentId",
                        column: x => x.OpponentId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_MatchSets_Set1Id",
                        column: x => x.Set1Id,
                        principalTable: "MatchSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_MatchSets_Set2Id",
                        column: x => x.Set2Id,
                        principalTable: "MatchSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_MatchSets_Set3Id",
                        column: x => x.Set3Id,
                        principalTable: "MatchSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_MatchSets_Set4Id",
                        column: x => x.Set4Id,
                        principalTable: "MatchSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_MatchSets_Set5Id",
                        column: x => x.Set5Id,
                        principalTable: "MatchSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_OpponentId",
                table: "Matches",
                column: "OpponentId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamId",
                table: "Matches",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchSetPlayers_PlayerId",
                table: "MatchSetPlayers",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchSets_Player1Id",
                table: "MatchSets",
                column: "Player1Id");

            migrationBuilder.CreateIndex(
                name: "IX_MatchSets_Player2Id",
                table: "MatchSets",
                column: "Player2Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "MatchSets");

            migrationBuilder.DropTable(
                name: "MatchSetPlayers");
        }
    }
}
