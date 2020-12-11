using Microsoft.EntityFrameworkCore.Migrations;

namespace GameSource.Data.Migrations
{
    public partial class GameCoverImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverImageFilePath",
                table: "Game",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImageFilePath",
                table: "Game");
        }
    }
}
