using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScheduleService.Database.ScheduleMigrations
{
    public partial class schedule_service_initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "red_date",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата праздника"),
                    name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true, comment: "Название праздника"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_red_date", x => x.id);
                },
                comment: "Реестр праздников");

            migrationBuilder.CreateTable(
                name: "weekend_work_day",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата рабочего дня выпадающего на выходной"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weekend_work_day", x => x.id);
                },
                comment: "Реестр рабочих дней выпадающих на выходные");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "red_date");

            migrationBuilder.DropTable(
                name: "weekend_work_day");
        }
    }
}
