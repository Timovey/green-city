using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MissionService.Database.LogMigrations
{
    public partial class logs_initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "logs",
                columns: table => new
                {
                    log_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    log_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    log_level = table.Column<byte>(type: "tinyint", nullable: false),
                    category_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    message = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logs", x => x.log_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "logs");
        }
    }
}
