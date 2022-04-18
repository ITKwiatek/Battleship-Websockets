using Microsoft.EntityFrameworkCore.Migrations;

namespace Battleship_Websockets.Migrations
{
    public partial class ganebf10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ships_BattleFields_BattleFieldId",
                table: "Ships");

            migrationBuilder.DropIndex(
                name: "IX_Ships_BattleFieldId",
                table: "Ships");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Ships_BattleFieldId",
                table: "Ships",
                column: "BattleFieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_BattleFields_BattleFieldId",
                table: "Ships",
                column: "BattleFieldId",
                principalTable: "BattleFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
