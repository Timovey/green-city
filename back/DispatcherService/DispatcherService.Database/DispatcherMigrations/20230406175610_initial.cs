using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DispatcherService.Database.DispatcherMigrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "auxiliary_worker",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    table_number = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Табличный номер"),
                    is_fired = table.Column<bool>(type: "bit", nullable: false, comment: "Флаг уволенности"),
                    fired_date = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Дата увольнения"),
                    name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Имя"),
                    surname = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Фамилия"),
                    patronymic = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Отчество"),
                    accept_work_date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата принятия на работу"),
                    organization_id = table.Column<int>(type: "int", nullable: true, comment: "ИД организации"),
                    contact_number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true, comment: "Контактный телефон"),
                    birth_date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата рождения"),
                    birth_place = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Место рождения"),
                    reg_address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Адрес регистрации"),
                    fact_address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Адрес фактический"),
                    passport_serie = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true, comment: "Серия паспорта"),
                    passport_number = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true, comment: "Номер паспорта"),
                    devision_code = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true, comment: "Код подразделения"),
                    issue_date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата выдачи"),
                    issuer = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Кем выдан"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auxiliary_worker", x => x.id);
                },
                comment: "Подсобный рабочий");

            migrationBuilder.CreateTable(
                name: "auxiliary_worker_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    table_number = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Табличный номер"),
                    is_fired = table.Column<bool>(type: "bit", nullable: false, comment: "Флаг уволенности"),
                    fired_date = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Дата увольнения"),
                    name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Имя"),
                    surname = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Фамилия"),
                    patronymic = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Отчество"),
                    accept_work_date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата принятия на работу"),
                    organization_id = table.Column<int>(type: "int", nullable: true, comment: "ИД организации"),
                    contact_number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true, comment: "Контактный телефон"),
                    birth_date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата рождения"),
                    birth_place = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Место рождения"),
                    reg_address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Адрес регистрации"),
                    fact_address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Адрес фактический"),
                    passport_serie = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true, comment: "Серия паспорта"),
                    passport_number = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true, comment: "Номер паспорта"),
                    devision_code = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true, comment: "Код подразделения"),
                    issue_date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата выдачи"),
                    issuer = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Кем выдан"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи"),
                    data_id = table.Column<int>(type: "int", nullable: false, comment: "ИД отслеживаемой записи"),
                    change_dt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата изменения записи"),
                    change_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя изменившего запись"),
                    change_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя изменившего запись")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auxiliary_worker_history", x => x.id);
                },
                comment: "Историческая таблица для подсобного рабочего");

            migrationBuilder.CreateTable(
                name: "brigade",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата работы бригады"),
                    driver_id = table.Column<int>(type: "int", nullable: false, comment: "ИД водителя"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brigade", x => x.id);
                },
                comment: "Бригада");

            migrationBuilder.CreateTable(
                name: "brigade_2_aux_worker",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    brigade_id = table.Column<int>(type: "int", nullable: false, comment: "ИД бригады"),
                    aux_worker_id = table.Column<int>(type: "int", nullable: false, comment: "ИД подсобного рабочего"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brigade_2_aux_worker", x => x.id);
                },
                comment: "Связка бригады с подсобным рабочим");

            migrationBuilder.CreateTable(
                name: "brigade_2_aux_worker_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    brigade_id = table.Column<int>(type: "int", nullable: false, comment: "ИД бригады"),
                    aux_worker_id = table.Column<int>(type: "int", nullable: false, comment: "ИД подсобного рабочего"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи"),
                    data_id = table.Column<int>(type: "int", nullable: false, comment: "ИД отслеживаемой записи"),
                    change_dt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата изменения записи"),
                    change_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя изменившего запись"),
                    change_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя изменившего запись")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brigade_2_aux_worker_history", x => x.id);
                },
                comment: "Историческая таблица связок бригады с подсобными рабочими");

            migrationBuilder.CreateTable(
                name: "brigade_2_route",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    brigade_id = table.Column<int>(type: "int", nullable: false, comment: "ИД бригады"),
                    route_id = table.Column<int>(type: "int", nullable: false, comment: "ИД рейса"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brigade_2_route", x => x.id);
                },
                comment: "Связка бригады с рейсом");

            migrationBuilder.CreateTable(
                name: "brigade_2_route_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    brigade_id = table.Column<int>(type: "int", nullable: false, comment: "ИД бригады"),
                    route_id = table.Column<int>(type: "int", nullable: false, comment: "ИД рейса"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи"),
                    data_id = table.Column<int>(type: "int", nullable: false, comment: "ИД отслеживаемой записи"),
                    change_dt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата изменения записи"),
                    change_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя изменившего запись"),
                    change_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя изменившего запись")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brigade_2_route_history", x => x.id);
                },
                comment: "Связка бригады с рейсом");

            migrationBuilder.CreateTable(
                name: "brigade_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата работы бригады"),
                    driver_id = table.Column<int>(type: "int", nullable: false, comment: "ИД водителя"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи"),
                    data_id = table.Column<int>(type: "int", nullable: false, comment: "ИД отслеживаемой записи"),
                    change_dt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата изменения записи"),
                    change_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя изменившего запись"),
                    change_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя изменившего запись")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brigade_history", x => x.id);
                },
                comment: "Историческая таблица бригад");

            migrationBuilder.CreateTable(
                name: "shift_task",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата"),
                    travel_sheet_id = table.Column<int>(type: "int", nullable: false, comment: "ИД путевого листа"),
                    work_shift_id = table.Column<int>(type: "int", nullable: false, comment: "ИД экипажа"),
                    org_id = table.Column<int>(type: "int", nullable: false, comment: "ИД организации"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shift_task", x => x.id);
                },
                comment: "Сменное задание");

            migrationBuilder.CreateTable(
                name: "shift_task_2_route",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    shift_task_id = table.Column<int>(type: "int", nullable: false, comment: "ИД сменного задания"),
                    route_id = table.Column<int>(type: "int", nullable: false, comment: "ИД рейса"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shift_task_2_route", x => x.id);
                },
                comment: "Связка сменного задания с рейсом");

            migrationBuilder.CreateTable(
                name: "shift_task_2_route_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    shift_task_id = table.Column<int>(type: "int", nullable: false, comment: "ИД сменного задания"),
                    route_id = table.Column<int>(type: "int", nullable: false, comment: "ИД рейса"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи"),
                    data_id = table.Column<int>(type: "int", nullable: false, comment: "ИД отслеживаемой записи"),
                    change_dt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата изменения записи"),
                    change_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя изменившего запись"),
                    change_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя изменившего запись")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shift_task_2_route_history", x => x.id);
                },
                comment: "Историческая таблица связки сменного задания с рейсом");

            migrationBuilder.CreateTable(
                name: "shift_task_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата"),
                    travel_sheet_id = table.Column<int>(type: "int", nullable: false, comment: "ИД путевого листа"),
                    work_shift_id = table.Column<int>(type: "int", nullable: false, comment: "ИД экипажа"),
                    org_id = table.Column<int>(type: "int", nullable: false, comment: "ИД организации"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи"),
                    data_id = table.Column<int>(type: "int", nullable: false, comment: "ИД отслеживаемой записи"),
                    change_dt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата изменения записи"),
                    change_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя изменившего запись"),
                    change_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя изменившего запись")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shift_task_history", x => x.id);
                },
                comment: "Историческая таблица сменных заданий");

            migrationBuilder.CreateTable(
                name: "travel_sheet",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    number_to_show = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: "Номер ПЛ для отображения"),
                    number_per_org = table.Column<int>(type: "int", nullable: false, comment: "Порядковый номер ПЛ в разрезе орагнизации"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата ПЛ"),
                    car_id = table.Column<int>(type: "int", nullable: false, comment: "ИД машины"),
                    driver_id = table.Column<int>(type: "int", nullable: false, comment: "ИД водителя"),
                    org_id = table.Column<int>(type: "int", nullable: false, comment: "ИД организации"),
                    start_drive_time = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true, comment: "Дата выезда"),
                    finish_drive_time = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true, comment: "Дата заезда"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_travel_sheet", x => x.id);
                },
                comment: "Путевой лист - ПЛ");

            migrationBuilder.CreateTable(
                name: "travel_sheet_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    number_to_show = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: "Номер ПЛ для отображения"),
                    number_per_org = table.Column<int>(type: "int", nullable: false, comment: "Порядковый номер ПЛ в разрезе орагнизации"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата ПЛ"),
                    car_id = table.Column<int>(type: "int", nullable: false, comment: "ИД машины"),
                    driver_id = table.Column<int>(type: "int", nullable: false, comment: "ИД водителя"),
                    org_id = table.Column<int>(type: "int", nullable: false, comment: "ИД организации"),
                    start_drive_time = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true, comment: "Дата выезда"),
                    finish_drive_time = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true, comment: "Дата заезда"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи"),
                    data_id = table.Column<int>(type: "int", nullable: false, comment: "ИД отслеживаемой записи"),
                    change_dt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата изменения записи"),
                    change_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя изменившего запись"),
                    change_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя изменившего запись")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_travel_sheet_history", x => x.id);
                },
                comment: "Историческа таблица путевых листов");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "auxiliary_worker");

            migrationBuilder.DropTable(
                name: "auxiliary_worker_history");

            migrationBuilder.DropTable(
                name: "brigade");

            migrationBuilder.DropTable(
                name: "brigade_2_aux_worker");

            migrationBuilder.DropTable(
                name: "brigade_2_aux_worker_history");

            migrationBuilder.DropTable(
                name: "brigade_2_route");

            migrationBuilder.DropTable(
                name: "brigade_2_route_history");

            migrationBuilder.DropTable(
                name: "brigade_history");

            migrationBuilder.DropTable(
                name: "shift_task");

            migrationBuilder.DropTable(
                name: "shift_task_2_route");

            migrationBuilder.DropTable(
                name: "shift_task_2_route_history");

            migrationBuilder.DropTable(
                name: "shift_task_history");

            migrationBuilder.DropTable(
                name: "travel_sheet");

            migrationBuilder.DropTable(
                name: "travel_sheet_history");
        }
    }
}
