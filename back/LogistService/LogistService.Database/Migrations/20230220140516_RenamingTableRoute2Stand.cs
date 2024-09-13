using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogistService.Database.Migrations
{
    public partial class RenamingTableRoute2Stand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "route2stand");

            migrationBuilder.DropTable(
                name: "route2stand_history");

            migrationBuilder.CreateTable(
                name: "base_route2stand",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    base_route_id = table.Column<int>(type: "int", nullable: false, comment: "ИД базового маршрута"),
                    priority = table.Column<int>(type: "int", nullable: false, comment: "Приоритет"),
                    stand_id = table.Column<int>(type: "int", nullable: false, comment: "ИД контейнерной площадки"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_base_route2stand", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "base_route2stand_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    base_route_id = table.Column<int>(type: "int", nullable: false, comment: "ИД базового маршрута"),
                    priority = table.Column<int>(type: "int", nullable: false, comment: "Приоритет"),
                    stand_id = table.Column<int>(type: "int", nullable: false, comment: "ИД контейнерной площадки"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи"),
                    data_id = table.Column<int>(type: "int", nullable: false, comment: "ИД отслеживаемой записи"),
                    change_dt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата изменения записи"),
                    change_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя изменившего запись"),
                    change_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя изменившего запись")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_base_route2stand_history", x => x.id);
                },
                comment: "Историческая таблица для таблицы base_route2stand");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "base_route2stand");

            migrationBuilder.DropTable(
                name: "base_route2stand_history");

            migrationBuilder.CreateTable(
                name: "route2stand",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    base_route_id = table.Column<int>(type: "int", nullable: false, comment: "ИД базового маршрута"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи"),
                    priority = table.Column<int>(type: "int", nullable: false, comment: "Приоритет"),
                    stand_id = table.Column<int>(type: "int", nullable: false, comment: "ИД контейнерной площадки")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_route2stand", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "route2stand_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    base_route_id = table.Column<int>(type: "int", nullable: false, comment: "ИД базового маршрута"),
                    change_dt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата изменения записи"),
                    change_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя изменившего запись"),
                    change_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя изменившего запись"),
                    data_id = table.Column<int>(type: "int", nullable: false, comment: "ИД отслеживаемой записи"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи"),
                    priority = table.Column<int>(type: "int", nullable: false, comment: "Приоритет"),
                    stand_id = table.Column<int>(type: "int", nullable: false, comment: "ИД контейнерной площадки")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_route2stand_history", x => x.id);
                },
                comment: "Историческая таблица для таблицы route2stand");
        }
    }
}
