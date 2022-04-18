using Microsoft.EntityFrameworkCore.Migrations;

namespace Battleship_Websockets.Migrations
{
    public partial class ganebf6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShipParts_Ships_ShipId",
                table: "ShipParts");

            migrationBuilder.CreateIndex(
                name: "IX_ShipParts_Number",
                table: "ShipParts",
                column: "Number");

            migrationBuilder.AddForeignKey(
                name: "FK_ShipParts_Ships_Number",
                table: "ShipParts",
                column: "Number",
                principalTable: "Ships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShipParts_Ships_Number",
                table: "ShipParts");

            migrationBuilder.DropIndex(
                name: "IX_ShipParts_Number",
                table: "ShipParts");

            migrationBuilder.AddForeignKey(
                name: "FK_ShipParts_Ships_ShipId",
                table: "ShipParts",
                column: "ShipId",
                principalTable: "Ships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
