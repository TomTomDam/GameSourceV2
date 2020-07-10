using Microsoft.EntityFrameworkCore.Migrations;

namespace GameSource.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Developer",
                columns: table => new
                {
                    Developer_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developer", x => x.Developer_ID);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Genre_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Genre_ID);
                });

            migrationBuilder.CreateTable(
                name: "Platform",
                columns: table => new
                {
                    Platform_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platform", x => x.Platform_ID);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    Publisher_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.Publisher_ID);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Game_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Genre_ID = table.Column<int>(nullable: true),
                    Developer_ID = table.Column<int>(nullable: true),
                    Publisher_ID = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Platform_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Game_ID);
                    table.ForeignKey(
                        name: "FK_Game_Developer_Developer_ID",
                        column: x => x.Developer_ID,
                        principalTable: "Developer",
                        principalColumn: "Developer_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Game_Genre_Genre_ID",
                        column: x => x.Genre_ID,
                        principalTable: "Genre",
                        principalColumn: "Genre_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Game_Platform_Platform_ID",
                        column: x => x.Platform_ID,
                        principalTable: "Platform",
                        principalColumn: "Platform_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Game_Publisher_Publisher_ID",
                        column: x => x.Publisher_ID,
                        principalTable: "Publisher",
                        principalColumn: "Publisher_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_Developer_ID",
                table: "Game",
                column: "Developer_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Game_Genre_ID",
                table: "Game",
                column: "Genre_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Game_Platform_ID",
                table: "Game",
                column: "Platform_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Game_Publisher_ID",
                table: "Game",
                column: "Publisher_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Developer");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Platform");

            migrationBuilder.DropTable(
                name: "Publisher");
        }
    }
}
