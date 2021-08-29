using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBackendProject.Migrations
{
    public partial class SettingTableChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BannerinAboutText",
                table: "Setting",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstBannerTextPart",
                table: "Setting",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecondBannerText",
                table: "Setting",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannerinAboutText",
                table: "Setting");

            migrationBuilder.DropColumn(
                name: "FirstBannerTextPart",
                table: "Setting");

            migrationBuilder.DropColumn(
                name: "SecondBannerText",
                table: "Setting");
        }
    }
}
