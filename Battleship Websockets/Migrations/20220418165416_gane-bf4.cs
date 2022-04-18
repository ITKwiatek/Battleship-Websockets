using Microsoft.EntityFrameworkCore.Migrations;

namespace Battleship_Websockets.Migrations
{
    public partial class ganebf4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BattleFields_Games_GameId",
                table: "BattleFields");

            migrationBuilder.DropIndex(
                name: "IX_BattleFields_GameId",
                table: "BattleFields");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "BattleFields");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "BattleFields",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BattleFields_GameId",
                table: "BattleFields",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_BattleFields_Games_GameId",
                table: "BattleFields",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
