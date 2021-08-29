using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBackendProject.Migrations
{
    public partial class SettingTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeaderLogo = table.Column<string>(maxLength: 50, nullable: false),
                    ContactPhone1 = table.Column<string>(maxLength: 50, nullable: false),
                    FooterLogo = table.Column<string>(maxLength: 50, nullable: false),
                    FooterDescription = table.Column<string>(maxLength: 300, nullable: false),
                    Address = table.Column<string>(maxLength: 200, nullable: false),
                    ContactPhone2 = table.Column<string>(maxLength: 50, nullable: false),
                    Website = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Setting");
        }
    }
}
