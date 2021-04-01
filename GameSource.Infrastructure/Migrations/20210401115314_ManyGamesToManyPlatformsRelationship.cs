using Microsoft.EntityFrameworkCore.Migrations;

namespace GameSource.Infrastructure.Migrations
{
    public partial class ManyGamesToManyPlatformsRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Platform_PlatformID",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_PlatformID",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "PlatformID",
                table: "Game");

            migrationBuilder.CreateTable(
                name: "GamePlatform",
                columns: table => new
                {
                    GamesID = table.Column<int>(type: "int", nullable: false),
                    PlatformsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlatform", x => new { x.GamesID, x.PlatformsID });
                    table.ForeignKey(
                        name: "FK_GamePlatform_Game_GamesID",
                        column: x => x.GamesID,
                        principalTable: "Game",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlatform_Platform_PlatformsID",
                        column: x => x.PlatformsID,
                        principalTable: "Platform",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamePlatform_PlatformsID",
                table: "GamePlatform",
                column: "PlatformsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamePlatform");

            migrationBuilder.AddColumn<int>(
                name: "PlatformID",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Game_PlatformID",
                table: "Game",
                column: "PlatformID");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Platform_PlatformID",
                table: "Game",
                column: "PlatformID",
                principalTable: "Platform",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
