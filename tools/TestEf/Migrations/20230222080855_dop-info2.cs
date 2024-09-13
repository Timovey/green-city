using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestEf.Migrations
{
    public partial class dopinfo2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Info2",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Info2",
                table: "Students");
        }
    }
}
