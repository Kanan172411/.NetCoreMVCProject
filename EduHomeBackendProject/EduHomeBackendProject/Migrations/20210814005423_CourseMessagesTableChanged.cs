using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBackendProject.Migrations
{
    public partial class CourseMessagesTableChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "CourseMessages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SendedAt",
                table: "CourseMessages",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_CourseMessages_CourseId",
                table: "CourseMessages",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseMessages_Courses_CourseId",
                table: "CourseMessages",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseMessages_Courses_CourseId",
                table: "CourseMessages");

            migrationBuilder.DropIndex(
                name: "IX_CourseMessages_CourseId",
                table: "CourseMessages");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "CourseMessages");

            migrationBuilder.DropColumn(
                name: "SendedAt",
                table: "CourseMessages");
        }
    }
}
