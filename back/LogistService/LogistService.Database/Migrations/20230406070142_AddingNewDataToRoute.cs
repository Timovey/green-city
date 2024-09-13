using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogistService.Database.Migrations
{
    public partial class AddingNewDataToRoute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "route_finish_place",
                table: "route_history",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true,
                comment: "Место выгрузки");

            migrationBuilder.AddColumn<decimal>(
                name: "route_total_volume_on_finish",
                table: "route_history",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "Объем выгрузки");

            migrationBuilder.AddColumn<string>(
                name: "route_finish_place",
                table: "route",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true,
                comment: "Место выгрузки");

            migrationBuilder.AddColumn<decimal>(
                name: "route_total_volume_on_finish",
                table: "route",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "Объем выгрузки");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "route_finish_place",
                table: "route_history");

            migrationBuilder.DropColumn(
                name: "route_total_volume_on_finish",
                table: "route_history");

            migrationBuilder.DropColumn(
                name: "route_finish_place",
                table: "route");

            migrationBuilder.DropColumn(
                name: "route_total_volume_on_finish",
                table: "route");
        }
    }
}
