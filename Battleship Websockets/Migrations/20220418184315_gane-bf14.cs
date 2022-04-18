using Microsoft.EntityFrameworkCore.Migrations;

namespace Battleship_Websockets.Migrations
{
    public partial class ganebf14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "Ships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShipType",
                table: "Ships",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Length",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "ShipType",
                table: "Ships");
        }
    }
}
