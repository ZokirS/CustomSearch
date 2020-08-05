using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomSearch.Migrations
{
    public partial class Id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "YendexResults",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "GoogleResults",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BingResults",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_YendexResults",
                table: "YendexResults",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GoogleResults",
                table: "GoogleResults",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BingResults",
                table: "BingResults",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_YendexResults",
                table: "YendexResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GoogleResults",
                table: "GoogleResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BingResults",
                table: "BingResults");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "YendexResults");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GoogleResults");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BingResults");
        }
    }
}
