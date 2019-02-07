using Microsoft.EntityFrameworkCore.Migrations;

namespace M8Scoring.Data.Migrations
{
    public partial class RefreshTokens2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_AspNetUsers_ApplicationUserId",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_ApplicationUserId",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Tokens");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Tokens",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserId",
                table: "Tokens",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_AspNetUsers_UserId",
                table: "Tokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_AspNetUsers_UserId",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_UserId",
                table: "Tokens");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Tokens",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Tokens",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_ApplicationUserId",
                table: "Tokens",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_AspNetUsers_ApplicationUserId",
                table: "Tokens",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
