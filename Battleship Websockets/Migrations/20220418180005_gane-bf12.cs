using Microsoft.EntityFrameworkCore.Migrations;

namespace Battleship_Websockets.Migrations
{
    public partial class ganebf12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShipParts_Ships_ShipModelId",
                table: "ShipParts");

            migrationBuilder.DropIndex(
                name: "IX_ShipParts_ShipModelId",
                table: "ShipParts");

            migrationBuilder.DropColumn(
                name: "ShipModelId",
                table: "ShipParts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShipModelId",
                table: "ShipParts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShipParts_ShipModelId",
                table: "ShipParts",
                column: "ShipModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShipParts_Ships_ShipModelId",
                table: "ShipParts",
                column: "ShipModelId",
                principalTable: "Ships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
