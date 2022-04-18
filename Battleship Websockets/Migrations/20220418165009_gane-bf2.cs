using Microsoft.EntityFrameworkCore.Migrations;

namespace Battleship_Websockets.Migrations
{
    public partial class ganebf2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "BattleFields",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BattleFields_Games_GameId",
                table: "BattleFields");

            migrationBuilder.DropIndex(
                name: "IX_BattleFields_GameId",
                table: "BattleFields");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "BattleFields",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
