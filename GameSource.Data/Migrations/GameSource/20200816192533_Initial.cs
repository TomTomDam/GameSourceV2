using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameSource.Data.Migrations.GameSource
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Developer",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developer", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PlatformType",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserProfileCommentPermission",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileCommentPermission", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserProfileVisibility",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileVisibility", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Platform",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    PlatformTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platform", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Platform_PlatformType_PlatformTypeID",
                        column: x => x.PlatformTypeID,
                        principalTable: "PlatformType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 20, nullable: true),
                    LastName = table.Column<string>(maxLength: 20, nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    AvatarFilePath = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    UserStatusID = table.Column<int>(nullable: false),
                    UserRoleID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_UserRole_UserRoleID",
                        column: x => x.UserRoleID,
                        principalTable: "UserRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_UserStatus_UserStatusID",
                        column: x => x.UserStatusID,
                        principalTable: "UserStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    GenreID = table.Column<int>(nullable: false),
                    DeveloperID = table.Column<int>(nullable: false),
                    PublisherID = table.Column<int>(nullable: false),
                    PlatformID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Game_Developer_DeveloperID",
                        column: x => x.DeveloperID,
                        principalTable: "Developer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Game_Genre_GenreID",
                        column: x => x.GenreID,
                        principalTable: "Genre",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Game_Platform_PlatformID",
                        column: x => x.PlatformID,
                        principalTable: "Platform",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Game_Publisher_PublisherID",
                        column: x => x.PublisherID,
                        principalTable: "Publisher",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsArticle",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    AuthoredByID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsArticle", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NewsArticle_User_AuthoredByID",
                        column: x => x.AuthoredByID,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Biography = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: false),
                    UserProfileVisibilityID = table.Column<int>(nullable: false),
                    UserProfileCommentPermissionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserProfile_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfile_UserProfileCommentPermission_UserProfileCommentPermissionID",
                        column: x => x.UserProfileCommentPermissionID,
                        principalTable: "UserProfileCommentPermission",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfile_UserProfileVisibility_UserProfileVisibilityID",
                        column: x => x.UserProfileVisibilityID,
                        principalTable: "UserProfileVisibility",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfileComment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Body = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    CreatedByID = table.Column<int>(nullable: false),
                    UserProfileID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileComment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserProfileComment_User_CreatedByID",
                        column: x => x.CreatedByID,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfileComment_UserProfile_UserProfileID",
                        column: x => x.UserProfileID,
                        principalTable: "UserProfile",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_DeveloperID",
                table: "Game",
                column: "DeveloperID");

            migrationBuilder.CreateIndex(
                name: "IX_Game_GenreID",
                table: "Game",
                column: "GenreID");

            migrationBuilder.CreateIndex(
                name: "IX_Game_PlatformID",
                table: "Game",
                column: "PlatformID");

            migrationBuilder.CreateIndex(
                name: "IX_Game_PublisherID",
                table: "Game",
                column: "PublisherID");

            migrationBuilder.CreateIndex(
                name: "IX_NewsArticle_AuthoredByID",
                table: "NewsArticle",
                column: "AuthoredByID");

            migrationBuilder.CreateIndex(
                name: "IX_Platform_PlatformTypeID",
                table: "Platform",
                column: "PlatformTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserRoleID",
                table: "User",
                column: "UserRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserStatusID",
                table: "User",
                column: "UserStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_UserID",
                table: "UserProfile",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_UserProfileCommentPermissionID",
                table: "UserProfile",
                column: "UserProfileCommentPermissionID");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_UserProfileVisibilityID",
                table: "UserProfile",
                column: "UserProfileVisibilityID");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileComment_CreatedByID",
                table: "UserProfileComment",
                column: "CreatedByID");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileComment_UserProfileID",
                table: "UserProfileComment",
                column: "UserProfileID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "NewsArticle");

            migrationBuilder.DropTable(
                name: "UserProfileComment");

            migrationBuilder.DropTable(
                name: "Developer");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Platform");

            migrationBuilder.DropTable(
                name: "Publisher");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropTable(
                name: "PlatformType");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserProfileCommentPermission");

            migrationBuilder.DropTable(
                name: "UserProfileVisibility");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserStatus");
        }
    }
}
