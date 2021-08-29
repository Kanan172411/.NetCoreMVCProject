using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBackendProject.Migrations
{
    public partial class EventMessagesTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(maxLength: 100, nullable: false),
                    Message = table.Column<string>(maxLength: 100, nullable: false),
                    AppUserId = table.Column<string>(nullable: true),
                    SendedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    EventId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventMessages_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventMessages_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventMessages_AppUserId",
                table: "EventMessages",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EventMessages_EventId",
                table: "EventMessages",
                column: "EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventMessages");
        }
    }
}
