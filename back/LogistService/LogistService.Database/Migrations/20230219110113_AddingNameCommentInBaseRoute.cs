using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogistService.Database.Migrations
{
    public partial class AddingNameCommentInBaseRoute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "base_route_history",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                comment: "Название БМ",
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "base_route",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                comment: "Название БМ",
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "base_route_history",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512,
                oldComment: "Название БМ");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "base_route",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512,
                oldComment: "Название БМ");
        }
    }
}
