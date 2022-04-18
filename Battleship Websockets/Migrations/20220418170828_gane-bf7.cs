using Microsoft.EntityFrameworkCore.Migrations;

namespace Battleship_Websockets.Migrations
{
    public partial class ganebf7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShipParts_Ships_Number",
                table: "ShipParts");

            migrationBuilder.DropTable(
                name: "PlayerGameManyToMany");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShipParts",
                table: "ShipParts");

            migrationBuilder.DropIndex(
                name: "IX_ShipParts_Number",
                table: "ShipParts");

            migrationBuilder.AlterColumn<int>(
                name: "ShipId",
                table: "ShipParts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ShipParts",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShipParts",
                table: "ShipParts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ShipParts_ShipId",
                table: "ShipParts",
                column: "ShipId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShipParts_Ships_ShipId",
                table: "ShipParts",
                column: "ShipId",
                principalTable: "Ships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShipParts_Ships_ShipId",
                table: "ShipParts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShipParts",
                table: "ShipParts");

            migrationBuilder.DropIndex(
                name: "IX_ShipParts_ShipId",
                table: "ShipParts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ShipParts");

            migrationBuilder.AlterColumn<int>(
                name: "ShipId",
                table: "ShipParts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShipParts",
                table: "ShipParts",
                columns: new[] { "ShipId", "Number" });

            migrationBuilder.CreateTable(
                name: "PlayerGameManyToMany",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerGameManyToMany", x => new { x.GameId, x.PlayerId });
                });

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
    }
}
