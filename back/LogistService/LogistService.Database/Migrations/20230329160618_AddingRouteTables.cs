using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogistService.Database.Migrations
{
    public partial class AddingRouteTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "route",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    route_name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false, comment: "Название рейса"),
                    route_number_on_day = table.Column<int>(name: "route_number_on_day ", type: "int", nullable: false, comment: "Порядковый номер рейса на дню"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата рейса"),
                    transport_work_shift_id = table.Column<int>(type: "int", nullable: false, comment: "ИД экипажа"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_route", x => x.id);
                },
                comment: "Рейс");

            migrationBuilder.CreateTable(
                name: "route_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    route_name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false, comment: "Название рейса"),
                    route_number_on_day = table.Column<int>(name: "route_number_on_day ", type: "int", nullable: false, comment: "Порядковый номер рейса на дню"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата рейса"),
                    transport_work_shift_id = table.Column<int>(type: "int", nullable: false, comment: "ИД экипажа"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи"),
                    data_id = table.Column<int>(type: "int", nullable: false, comment: "ИД отслеживаемой записи"),
                    change_dt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата изменения записи"),
                    change_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя изменившего запись"),
                    change_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя изменившего запись")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_route_history", x => x.id);
                },
                comment: "Историческая таблица для таблицы route");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "route");

            migrationBuilder.DropTable(
                name: "route_history");
        }
    }
}
