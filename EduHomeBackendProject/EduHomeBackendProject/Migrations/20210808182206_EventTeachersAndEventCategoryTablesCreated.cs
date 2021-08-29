using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBackendProject.Migrations
{
    public partial class EventTeachersAndEventCategoryTablesCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventCategory_EventCategoryId",
                table: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventCategory",
                table: "EventCategory");

            migrationBuilder.RenameTable(
                name: "EventCategory",
                newName: "EventCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventCategories",
                table: "EventCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventCategories_EventCategoryId",
                table: "Events",
                column: "EventCategoryId",
                principalTable: "EventCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventCategories_EventCategoryId",
                table: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventCategories",
                table: "EventCategories");

            migrationBuilder.RenameTable(
                name: "EventCategories",
                newName: "EventCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventCategory",
                table: "EventCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventCategory_EventCategoryId",
                table: "Events",
                column: "EventCategoryId",
                principalTable: "EventCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
