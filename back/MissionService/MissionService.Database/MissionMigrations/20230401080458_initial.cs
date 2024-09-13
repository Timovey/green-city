using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MissionService.Database.MissionMigrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "app",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата заявки"),
                    contract_client_id = table.Column<int>(type: "int", nullable: false, comment: "ИД контрагента"),
                    contract_id = table.Column<int>(type: "int", nullable: false, comment: "ИД договора"),
                    type = table.Column<byte>(type: "tinyint", nullable: false, comment: "Тип заявки"),
                    creator_type = table.Column<byte>(type: "tinyint", nullable: false, comment: "Тип создателя заявки"),
                    comment = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true, comment: "Комментарий"),
                    created_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя создателя"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app", x => x.id);
                },
                comment: "Миссии - заявки на вывоз");

            migrationBuilder.CreateTable(
                name: "app_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата заявки"),
                    contract_client_id = table.Column<int>(type: "int", nullable: false, comment: "ИД контрагента"),
                    contract_id = table.Column<int>(type: "int", nullable: true, comment: "ИД договора"),
                    type = table.Column<byte>(type: "tinyint", nullable: false, comment: "Тип заявки"),
                    creator_type = table.Column<byte>(type: "tinyint", nullable: false, comment: "Тип создателя заявки"),
                    comment = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true, comment: "Комментарий"),
                    created_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя создателя"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи"),
                    data_id = table.Column<int>(type: "int", nullable: false, comment: "ИД отслеживаемой записи"),
                    change_dt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата изменения записи"),
                    change_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя изменившего запись"),
                    change_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя изменившего запись")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_history", x => x.id);
                },
                comment: "Историческая таблица заявок");

            migrationBuilder.CreateTable(
                name: "app2mission",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    app_id = table.Column<int>(type: "int", nullable: false, comment: "ИД заявки"),
                    mission_id = table.Column<int>(type: "int", nullable: false, comment: "ИД миссии"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app2mission", x => x.id);
                },
                comment: "Связка заявки и миссии");

            migrationBuilder.CreateTable(
                name: "app2mission_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    app_id = table.Column<int>(type: "int", nullable: false, comment: "ИД заявки"),
                    mission_id = table.Column<int>(type: "int", nullable: false, comment: "ИД миссии"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи"),
                    data_id = table.Column<int>(type: "int", nullable: false, comment: "ИД отслеживаемой записи"),
                    change_dt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата изменения записи"),
                    change_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя изменившего запись"),
                    change_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя изменившего запись")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app2mission_history", x => x.id);
                },
                comment: "Историческая таблица связок заявок и мисиий");

            migrationBuilder.CreateTable(
                name: "mission",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    container_id = table.Column<int>(type: "int", nullable: false, comment: "ИД контейнера"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата в которую необходимо вывезти"),
                    route_id = table.Column<int>(type: "int", nullable: true, comment: "ИД рейса"),
                    priority = table.Column<int>(type: "int", nullable: false, comment: "Приоритет вывоза"),
                    status = table.Column<byte>(type: "tinyint", nullable: false, comment: "Статус миссии"),
                    status_comment = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Комментарий к статусу"),
                    fact_volume = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "Фактически вывезенный объем"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mission", x => x.id);
                },
                comment: "Миссии - заявки на вывоз");

            migrationBuilder.CreateTable(
                name: "mission_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    container_id = table.Column<int>(type: "int", nullable: false, comment: "ИД контейнера"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата в которую необходимо вывезти"),
                    route_id = table.Column<int>(type: "int", nullable: true, comment: "ИД рейса"),
                    priority = table.Column<int>(type: "int", nullable: false, comment: "Приоритет вывоза"),
                    status = table.Column<byte>(type: "tinyint", nullable: false, comment: "Статус миссии"),
                    status_comment = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Комментарий к статусу"),
                    fact_volume = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "Фактически вывезенный объем"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи"),
                    data_id = table.Column<int>(type: "int", nullable: false, comment: "ИД отслеживаемой записи"),
                    change_dt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата изменения записи"),
                    change_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя изменившего запись"),
                    change_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя изменившего запись")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mission_history", x => x.id);
                },
                comment: "Историческая таблица миссий");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "app");

            migrationBuilder.DropTable(
                name: "app_history");

            migrationBuilder.DropTable(
                name: "app2mission");

            migrationBuilder.DropTable(
                name: "app2mission_history");

            migrationBuilder.DropTable(
                name: "mission");

            migrationBuilder.DropTable(
                name: "mission_history");
        }
    }
}
