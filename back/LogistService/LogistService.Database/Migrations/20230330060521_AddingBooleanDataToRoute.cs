using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogistService.Database.Migrations
{
    public partial class AddingBooleanDataToRoute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "route_ready",
                table: "route_history",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Готовность рейса");

            migrationBuilder.AddColumn<bool>(
                name: "route_сompleted",
                table: "route_history",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Завершенность рейса");

            migrationBuilder.AddColumn<bool>(
                name: "route_ready",
                table: "route",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Готовность рейса");

            migrationBuilder.AddColumn<bool>(
                name: "route_сompleted",
                table: "route",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Завершенность рейса");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "route_ready",
                table: "route_history");

            migrationBuilder.DropColumn(
                name: "route_сompleted",
                table: "route_history");

            migrationBuilder.DropColumn(
                name: "route_ready",
                table: "route");

            migrationBuilder.DropColumn(
                name: "route_сompleted",
                table: "route");
        }
    }
}
