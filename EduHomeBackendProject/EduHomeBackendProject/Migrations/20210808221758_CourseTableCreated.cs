using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBackendProject.Migrations
{
    public partial class CourseTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "CourseTags");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "CourseTags",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "CourseTags",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(maxLength: 255, nullable: false),
                    CourseContent = table.Column<string>(nullable: false),
                    Image = table.Column<string>(maxLength: 1000, nullable: false),
                    AboutCourse = table.Column<string>(nullable: false),
                    HowToApply = table.Column<string>(nullable: false),
                    Certification = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    ClassDuration = table.Column<int>(nullable: false),
                    Language = table.Column<string>(nullable: false),
                    StudentsCount = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    CourseCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_CourseCategories_CourseCategoryId",
                        column: x => x.CourseCategoryId,
                        principalTable: "CourseCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseTags_CourseId",
                table: "CourseTags",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTags_TagId",
                table: "CourseTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseCategoryId",
                table: "Courses",
                column: "CourseCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTags_Courses_CourseId",
                table: "CourseTags",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTags_Tags_TagId",
                table: "CourseTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTags_Courses_CourseId",
                table: "CourseTags");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseTags_Tags_TagId",
                table: "CourseTags");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_CourseTags_CourseId",
                table: "CourseTags");

            migrationBuilder.DropIndex(
                name: "IX_CourseTags_TagId",
                table: "CourseTags");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "CourseTags");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "CourseTags");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CourseTags",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }
    }
}
