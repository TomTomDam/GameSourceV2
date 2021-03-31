using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameSource.Infrastructure.Migrations
{
    public partial class RemovedUserFKFromEventsLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventsLog_User_UserID",
                table: "EventsLog");

            migrationBuilder.DropIndex(
                name: "IX_EventsLog_UserID",
                table: "EventsLog");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "EventsLog");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserID",
                table: "EventsLog",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_EventsLog_UserID",
                table: "EventsLog",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_EventsLog_User_UserID",
                table: "EventsLog",
                column: "UserID",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
