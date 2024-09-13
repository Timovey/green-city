using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestEf.Migrations
{
    public partial class added_is_deleted_to_Classes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "StudentItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SchoolClasses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "StudentItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SchoolClasses");
        }
    }
}
