using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportService.Database.TransportMigrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "data_range",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    create_date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата создания сущности"),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата начала работы"),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата окончания работы"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_range", x => x.id);
                },
                comment: "Базовый класс диапазонов дат");

            migrationBuilder.CreateTable(
                name: "data_range_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    create_date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата создания сущности"),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата начала работы"),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата окончания работы"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи"),
                    data_id = table.Column<int>(type: "int", nullable: false, comment: "ИД отслеживаемой записи"),
                    change_dt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата изменения записи"),
                    change_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя изменившего запись"),
                    change_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя изменившего запись")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_range_history", x => x.id);
                },
                comment: "Историческая таблица для диапазонов дат");

            migrationBuilder.CreateTable(
                name: "driver",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    table_number = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Табличный номер"),
                    is_fired = table.Column<bool>(type: "bit", nullable: false, comment: "Флаг уволенности"),
                    fired_date = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Дата увольнения"),
                    driver_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Имя водителя"),
                    driver_surname = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Фамилия водителя"),
                    driver_patronymic = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Отчество водителя "),
                    accept_work_date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата принятия на работу"),
                    driver_position_type = table.Column<byte>(type: "tinyint", nullable: false, comment: "Должность"),
                    organization_id = table.Column<int>(type: "int", nullable: true, comment: "ИД организации"),
                    skill_class = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Квалификаицонный разряд"),
                    driving_skills_json = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Чем может управлять в формате Json"),
                    contact_number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true, comment: "Контактный телефон"),
                    drivers_license = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true, comment: "Водительское удостоверение"),
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
                    table.PrimaryKey("PK_driver", x => x.id);
                },
                comment: "Водитель");

            migrationBuilder.CreateTable(
                name: "driver_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    table_number = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Табличный номер"),
                    is_fired = table.Column<bool>(type: "bit", nullable: false, comment: "Флаг уволенности"),
                    fired_date = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Дата увольнения"),
                    driver_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Имя водителя"),
                    driver_surname = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Фамилия водителя"),
                    driver_patronymic = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Отчество водителя "),
                    accept_work_date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата принятия на работу"),
                    driver_position_type = table.Column<byte>(type: "tinyint", nullable: false, comment: "Должность"),
                    organization_id = table.Column<int>(type: "int", nullable: true, comment: "ИД организации"),
                    skill_class = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Квалификаицонный разряд"),
                    driving_skills_json = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Чем может управлять в формате Json"),
                    contact_number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true, comment: "Контактный телефон"),
                    drivers_license = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true, comment: "Водительское удостоверение"),
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
                    table.PrimaryKey("PK_driver_history", x => x.id);
                },
                comment: "Историческая таблица для таблицы driver");

            migrationBuilder.CreateTable(
                name: "transport",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Название транспортного средства"),
                    organization_id = table.Column<int>(type: "int", nullable: true, comment: "ИД организации"),
                    reg_number = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false, comment: "Регистрационный номер"),
                    sts = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false, comment: "СТС"),
                    transport_type = table.Column<byte>(type: "tinyint", nullable: false, comment: "Тип транспортного средства"),
                    body_type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "Тип кузова"),
                    body_color = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "Цвет кузова"),
                    tank_fence = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "Забор бака"),
                    tank_types_json = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Тип бака"),
                    load_capacity = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "Грузоподъемность"),
                    cargo_volume = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Объем кузова"),
                    cargo_volume_coef = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Объем кузова с коефицентом"),
                    production_year = table.Column<int>(type: "int", nullable: false, comment: "Год выпуска"),
                    spending_limit = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Лимит затрат"),
                    gsm_number_winter_city = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "Номер расхода ГСМ зима город"),
                    gsm_number_winter_road = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "Номер расхода ГСМ зима трасса"),
                    gsm_number_summer_city = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "Номер расхода ГСМ лето город"),
                    gsm_number_summer_road = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "Номер расхода ГСМ лето трасса"),
                    norm_one_moto_hour = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "Норма на 1 моточас"),
                    norm_one_academy_hour = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "Норма на 1 академический час"),
                    norm_one_minute = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "Норма на 1 минуту"),
                    last_to_date = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Дата последнего ТО"),
                    repairs_to = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "ТО и ремонты"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transport", x => x.id);
                },
                comment: "Паспорт ТС");

            migrationBuilder.CreateTable(
                name: "transport_duty_shift",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    transport_id = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transport_duty_shift", x => x.id);
                },
                comment: "Дежурная смена");

            migrationBuilder.CreateTable(
                name: "transport_duty_shift_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    transport_id = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи"),
                    data_id = table.Column<int>(type: "int", nullable: false, comment: "ИД отслеживаемой записи"),
                    change_dt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата изменения записи"),
                    change_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя изменившего запись"),
                    change_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя изменившего запись")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transport_duty_shift_history", x => x.id);
                },
                comment: "Историческая таблица для таблицы дежурных смен");

            migrationBuilder.CreateTable(
                name: "transport_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Название транспортного средства"),
                    organization_id = table.Column<int>(type: "int", nullable: true, comment: "ИД организации"),
                    reg_number = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false, comment: "Регистрационный номер"),
                    sts = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false, comment: "СТС"),
                    transport_type = table.Column<byte>(type: "tinyint", nullable: false, comment: "Тип транспортного средства"),
                    body_type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "Тип кузова"),
                    body_color = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "Цвет кузова"),
                    tank_fence = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "Забор бака"),
                    tank_types_json = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Тип бака"),
                    load_capacity = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "Грузоподъемность"),
                    cargo_volume = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Объем кузова"),
                    cargo_volume_coef = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Объем кузова с коефицентом"),
                    production_year = table.Column<int>(type: "int", nullable: false, comment: "Год выпуска"),
                    spending_limit = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Лимит затрат"),
                    gsm_number_winter_city = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "Номер расхода ГСМ зима город"),
                    gsm_number_winter_road = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "Номер расхода ГСМ зима трасса"),
                    gsm_number_summer_city = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "Номер расхода ГСМ лето город"),
                    gsm_number_summer_road = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "Номер расхода ГСМ лето трасса"),
                    norm_one_moto_hour = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "Норма на 1 моточас"),
                    norm_one_academy_hour = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "Норма на 1 академический час"),
                    norm_one_minute = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "Норма на 1 минуту"),
                    last_to_date = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Дата последнего ТО"),
                    repairs_to = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "ТО и ремонты"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи"),
                    data_id = table.Column<int>(type: "int", nullable: false, comment: "ИД отслеживаемой записи"),
                    change_dt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата изменения записи"),
                    change_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя изменившего запись"),
                    change_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя изменившего запись")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transport_history", x => x.id);
                },
                comment: "Историческая таблица для таблицы transport");

            migrationBuilder.CreateTable(
                name: "transport_work_shift",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    transport_id = table.Column<int>(type: "int", nullable: false),
                    date_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    organization_ids_json = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    driver_id = table.Column<int>(type: "int", nullable: false),
                    is_in_route = table.Column<bool>(type: "bit", nullable: false),
                    DriverWorkDateType = table.Column<byte>(type: "tinyint", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transport_work_shift", x => x.id);
                },
                comment: "Рабочая смена машины на число");

            migrationBuilder.CreateTable(
                name: "transport_work_shift_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    transport_id = table.Column<int>(type: "int", nullable: false),
                    date_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    organization_ids_json = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    driver_id = table.Column<int>(type: "int", nullable: false),
                    is_in_route = table.Column<bool>(type: "bit", nullable: false),
                    DriverWorkDateType = table.Column<byte>(type: "tinyint", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи"),
                    data_id = table.Column<int>(type: "int", nullable: false, comment: "ИД отслеживаемой записи"),
                    change_dt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата изменения записи"),
                    change_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя изменившего запись"),
                    change_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя изменившего запись")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transport_work_shift_history", x => x.id);
                },
                comment: "Историческая таблица для рабочей смены машины");

            migrationBuilder.CreateTable(
                name: "driver_vacation",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ"),
                    driver_id = table.Column<int>(type: "int", nullable: false, comment: "ИД водителя"),
                    replacement_driver_id = table.Column<int>(type: "int", nullable: true, comment: "ИД сменного Водителя"),
                    vacation_type = table.Column<byte>(type: "tinyint", nullable: false, comment: "Отпуск/Больничный")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_driver_vacation", x => x.id);
                    table.ForeignKey(
                        name: "FK_driver_vacation_data_range_id",
                        column: x => x.id,
                        principalTable: "data_range",
                        principalColumn: "id");
                },
                comment: "Отпуск/больничный водителя");

            migrationBuilder.CreateTable(
                name: "driver2transport",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ"),
                    driver_id = table.Column<int>(type: "int", nullable: false, comment: "ИД Водителя"),
                    transport_id = table.Column<int>(type: "int", nullable: false, comment: "ИД Транспорта"),
                    work_shift = table.Column<byte>(type: "tinyint", nullable: false, comment: "С какой смены выходит водитель")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_driver2transport", x => x.id);
                    table.ForeignKey(
                        name: "FK_driver2transport_data_range_id",
                        column: x => x.id,
                        principalTable: "data_range",
                        principalColumn: "id");
                },
                comment: "Базовый класс диапазонов дат");

            migrationBuilder.CreateTable(
                name: "rent",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ"),
                    transport_id = table.Column<int>(type: "int", nullable: false),
                    organization_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rent", x => x.id);
                    table.ForeignKey(
                        name: "FK_rent_data_range_id",
                        column: x => x.id,
                        principalTable: "data_range",
                        principalColumn: "id");
                },
                comment: "Аренда транспорта");

            migrationBuilder.CreateTable(
                name: "repair_card",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ"),
                    @event = table.Column<byte>(name: "event", type: "tinyint", nullable: false, comment: "Событие"),
                    organization_id = table.Column<int>(type: "int", nullable: true, comment: "ИД организации"),
                    transport_id = table.Column<int>(type: "int", nullable: false, comment: "ИД транспортного средства"),
                    work_shift = table.Column<byte>(type: "tinyint", nullable: false, comment: "В какую смену производиться ремонт/ТО при графике 6/3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_repair_card", x => x.id);
                    table.ForeignKey(
                        name: "FK_repair_card_data_range_id",
                        column: x => x.id,
                        principalTable: "data_range",
                        principalColumn: "id");
                },
                comment: "Карточка ремонта/ТО");

            migrationBuilder.CreateTable(
                name: "transport_shedule",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ"),
                    transport_id = table.Column<int>(type: "int", nullable: false, comment: "ID транспорта"),
                    work_shedule_type = table.Column<byte>(type: "tinyint", nullable: false, comment: "Типовой трафик")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transport_shedule", x => x.id);
                    table.ForeignKey(
                        name: "FK_transport_shedule_data_range_id",
                        column: x => x.id,
                        principalTable: "data_range",
                        principalColumn: "id");
                },
                comment: "Типовой трафик");

            migrationBuilder.CreateTable(
                name: "driver_vacation_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ"),
                    driver_id = table.Column<int>(type: "int", nullable: false, comment: "ИД водителя"),
                    replacement_driver_id = table.Column<int>(type: "int", nullable: true, comment: "ИД сменного Водителя"),
                    vacation_type = table.Column<byte>(type: "tinyint", nullable: false, comment: "Отпуск/Больничный")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_driver_vacation_history", x => x.id);
                    table.ForeignKey(
                        name: "FK_driver_vacation_history_data_range_history_id",
                        column: x => x.id,
                        principalTable: "data_range_history",
                        principalColumn: "id");
                },
                comment: "Историческая таблица для отпуска/больничного водителя");

            migrationBuilder.CreateTable(
                name: "driver2transport_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ"),
                    driver_id = table.Column<int>(type: "int", nullable: false, comment: "ИД Водителя"),
                    transport_id = table.Column<int>(type: "int", nullable: false, comment: "ИД Транспорта"),
                    work_shift = table.Column<byte>(type: "tinyint", nullable: false, comment: "С какой смены выходит водитель")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_driver2transport_history", x => x.id);
                    table.ForeignKey(
                        name: "FK_driver2transport_history_data_range_history_id",
                        column: x => x.id,
                        principalTable: "data_range_history",
                        principalColumn: "id");
                },
                comment: "Историческая таблица для диапазонов дат");

            migrationBuilder.CreateTable(
                name: "rent_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ"),
                    transport_id = table.Column<int>(type: "int", nullable: false),
                    organization_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rent_history", x => x.id);
                    table.ForeignKey(
                        name: "FK_rent_history_data_range_history_id",
                        column: x => x.id,
                        principalTable: "data_range_history",
                        principalColumn: "id");
                },
                comment: "Историческая таблица для таблицы rent");

            migrationBuilder.CreateTable(
                name: "repair_card_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ"),
                    @event = table.Column<byte>(name: "event", type: "tinyint", nullable: false, comment: "Событие"),
                    organization_id = table.Column<int>(type: "int", nullable: true, comment: "ИД организации"),
                    transport_id = table.Column<int>(type: "int", nullable: false, comment: "ИД транспортного средства"),
                    work_shift = table.Column<byte>(type: "tinyint", nullable: false, comment: "В какую смену производиться ремонт/ТО при графике 6/3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_repair_card_history", x => x.id);
                    table.ForeignKey(
                        name: "FK_repair_card_history_data_range_history_id",
                        column: x => x.id,
                        principalTable: "data_range_history",
                        principalColumn: "id");
                },
                comment: "Карточка ремонта/ТО - Историческая таблица");

            migrationBuilder.CreateTable(
                name: "transport_shedule_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ"),
                    transport_id = table.Column<int>(type: "int", nullable: false),
                    work_shedule_type = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transport_shedule_history", x => x.id);
                    table.ForeignKey(
                        name: "FK_transport_shedule_history_data_range_history_id",
                        column: x => x.id,
                        principalTable: "data_range_history",
                        principalColumn: "id");
                },
                comment: "Историческая таблица для таблицы типовых графиков");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "driver");

            migrationBuilder.DropTable(
                name: "driver_history");

            migrationBuilder.DropTable(
                name: "driver_vacation");

            migrationBuilder.DropTable(
                name: "driver_vacation_history");

            migrationBuilder.DropTable(
                name: "driver2transport");

            migrationBuilder.DropTable(
                name: "driver2transport_history");

            migrationBuilder.DropTable(
                name: "rent");

            migrationBuilder.DropTable(
                name: "rent_history");

            migrationBuilder.DropTable(
                name: "repair_card");

            migrationBuilder.DropTable(
                name: "repair_card_history");

            migrationBuilder.DropTable(
                name: "transport");

            migrationBuilder.DropTable(
                name: "transport_duty_shift");

            migrationBuilder.DropTable(
                name: "transport_duty_shift_history");

            migrationBuilder.DropTable(
                name: "transport_history");

            migrationBuilder.DropTable(
                name: "transport_shedule");

            migrationBuilder.DropTable(
                name: "transport_shedule_history");

            migrationBuilder.DropTable(
                name: "transport_work_shift");

            migrationBuilder.DropTable(
                name: "transport_work_shift_history");

            migrationBuilder.DropTable(
                name: "data_range");

            migrationBuilder.DropTable(
                name: "data_range_history");
        }
    }
}
