using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomSearch.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BingResults",
                columns: table => new
                {
                    Title = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    Snippet = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "GoogleResults",
                columns: table => new
                {
                    Title = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    Snippet = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "YendexResults",
                columns: table => new
                {
                    Title = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    Snippet = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BingResults");

            migrationBuilder.DropTable(
                name: "GoogleResults");

            migrationBuilder.DropTable(
                name: "YendexResults");
        }
    }
}
