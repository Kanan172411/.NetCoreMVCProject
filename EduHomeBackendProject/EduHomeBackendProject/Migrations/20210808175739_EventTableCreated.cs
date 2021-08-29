using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBackendProject.Migrations
{
    public partial class EventTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(maxLength: 50, nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    Venue = table.Column<string>(maxLength: 50, nullable: false),
                    EventContent = table.Column<string>(maxLength: 1500, nullable: false),
                    Image = table.Column<string>(maxLength: 50, nullable: false),
                    StartDateDayMonth = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
