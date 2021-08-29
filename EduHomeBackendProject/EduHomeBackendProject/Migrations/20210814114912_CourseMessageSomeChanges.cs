using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBackendProject.Migrations
{
    public partial class CourseMessageSomeChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseMessages_AspNetUsers_AppUserId",
                table: "CourseMessages");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "CourseMessages",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseMessages_AspNetUsers_AppUserId",
                table: "CourseMessages",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseMessages_AspNetUsers_AppUserId",
                table: "CourseMessages");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "CourseMessages",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseMessages_AspNetUsers_AppUserId",
                table: "CourseMessages",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
