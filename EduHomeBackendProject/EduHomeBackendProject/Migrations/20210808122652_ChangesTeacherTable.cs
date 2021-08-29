using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBackendProject.Migrations
{
    public partial class ChangesTeacherTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Communication",
                table: "Teachers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Design",
                table: "Teachers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Development",
                table: "Teachers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Innovation",
                table: "Teachers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "Teachers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamLeader",
                table: "Teachers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Communication",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Design",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Development",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Innovation",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "TeamLeader",
                table: "Teachers");
        }
    }
}
