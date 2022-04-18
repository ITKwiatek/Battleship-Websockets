using Microsoft.EntityFrameworkCore.Migrations;

namespace Battleship_Websockets.Migrations
{
    public partial class ganebf5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShipParts_Ships_ShipId1",
                table: "ShipParts");

            migrationBuilder.RenameColumn(
                name: "ShipId1",
                table: "ShipParts",
                newName: "ShipModelId");

            migrationBuilder.RenameIndex(
                name: "IX_ShipParts_ShipId1",
                table: "ShipParts",
                newName: "IX_ShipParts_ShipModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShipParts_Ships_ShipModelId",
                table: "ShipParts",
                column: "ShipModelId",
                principalTable: "Ships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShipParts_Ships_ShipModelId",
                table: "ShipParts");

            migrationBuilder.RenameColumn(
                name: "ShipModelId",
                table: "ShipParts",
                newName: "ShipId1");

            migrationBuilder.RenameIndex(
                name: "IX_ShipParts_ShipModelId",
                table: "ShipParts",
                newName: "IX_ShipParts_ShipId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ShipParts_Ships_ShipId1",
                table: "ShipParts",
                column: "ShipId1",
                principalTable: "Ships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
