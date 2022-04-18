using Microsoft.EntityFrameworkCore.Migrations;

namespace Battleship_Websockets.Migrations
{
    public partial class ganebf8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ships_BattleFields_BattleFieldId",
                table: "Ships");

            migrationBuilder.AlterColumn<int>(
                name: "BattleFieldId",
                table: "Ships",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_BattleFields_BattleFieldId",
                table: "Ships",
                column: "BattleFieldId",
                principalTable: "BattleFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ships_BattleFields_BattleFieldId",
                table: "Ships");

            migrationBuilder.AlterColumn<int>(
                name: "BattleFieldId",
                table: "Ships",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
