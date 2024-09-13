using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SDService.Database.SdMigrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "container",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    container_name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    container_type = table.Column<int>(type: "int", nullable: false, comment: "Тип контейнера"),
                    stand_id = table.Column<int>(type: "int", nullable: true),
                    trash_type = table.Column<byte>(type: "tinyint", nullable: false, comment: "Тип мусора"),
                    is_actual = table.Column<int>(type: "int", nullable: false),
                    is_in_contract = table.Column<bool>(type: "bit", nullable: false),
                    is_validated = table.Column<bool>(type: "bit", nullable: false, comment: "Флаг проверки записи сотрудником"),
                    created_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя в базе AuthService создавшего запись"),
                    created_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя создавшего запись"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_container", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "container_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    container_name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    container_type = table.Column<int>(type: "int", nullable: false, comment: "Тип контейнера"),
                    stand_id = table.Column<int>(type: "int", nullable: true),
                    trash_type = table.Column<byte>(type: "tinyint", nullable: false, comment: "Тип мусора"),
                    is_actual = table.Column<int>(type: "int", nullable: false),
                    is_in_contract = table.Column<bool>(type: "bit", nullable: false),
                    is_validated = table.Column<bool>(type: "bit", nullable: false, comment: "Флаг проверки записи сотрудником"),
                    created_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя в базе AuthService создавшего запись"),
                    created_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя создавшего запись"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи"),
                    data_id = table.Column<int>(type: "int", nullable: false, comment: "ИД отслеживаемой записи"),
                    change_dt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата изменения записи"),
                    change_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя изменившего запись"),
                    change_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя изменившего запись")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_container_history", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "container_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Название типа тары"),
                    volume = table.Column<float>(type: "real", nullable: false, comment: "Объем тары"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_container_type", x => x.id);
                },
                comment: "Тип тары");

            migrationBuilder.CreateTable(
                name: "contract",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contract_number = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    contract_client_id = table.Column<int>(type: "int", nullable: false),
                    contract_status = table.Column<bool>(type: "bit", nullable: false, comment: "Статус подписания договора"),
                    create_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    send_docs_type = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    comment = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true, comment: "Комментарий к договору"),
                    is_validated = table.Column<bool>(type: "bit", nullable: false, comment: "Флаг проверки записи сотрудником"),
                    created_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя в базе AuthService создавшего запись"),
                    created_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя создавшего запись"),
                    is_autorenew_schedules = table.Column<bool>(type: "bit", nullable: false, comment: "Включить ли автопродление графика"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contract", x => x.id);
                },
                comment: "Договор с контрагентом");

            migrationBuilder.CreateTable(
                name: "contract_client",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    client_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    phone = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    inn = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    kpp = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ogrn = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    r_schet = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    bank = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    bik = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    k_schet = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    legal_address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    postal_address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    supervizor_name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    supervizor_position = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    responsible_tko_person_fio = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    responsible_tko_person_phone = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    is_validated = table.Column<bool>(type: "bit", nullable: false, comment: "Флаг проверки записи сотрудником"),
                    created_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя в базе AuthService создавшего запись"),
                    created_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя создавшего запись"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contract_client", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "contract_client_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    client_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    phone = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    inn = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    kpp = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ogrn = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    r_schet = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    bank = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    bik = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    k_schet = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    legal_address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    postal_address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    supervizor_name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    supervizor_position = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    responsible_tko_person_fio = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    responsible_tko_person_phone = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    is_validated = table.Column<bool>(type: "bit", nullable: false, comment: "Флаг проверки записи сотрудником"),
                    created_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя в базе AuthService создавшего запись"),
                    created_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя создавшего запись"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи"),
                    data_id = table.Column<int>(type: "int", nullable: false, comment: "ИД отслеживаемой записи"),
                    change_dt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата изменения записи"),
                    change_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя изменившего запись"),
                    change_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя изменившего запись")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contract_client_history", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "contract_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contract_number = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    contract_client_id = table.Column<int>(type: "int", nullable: false),
                    contract_status = table.Column<bool>(type: "bit", nullable: false, comment: "Статус подписания договора"),
                    create_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    send_docs_type = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    comment = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true, comment: "Комментарий к договору"),
                    is_validated = table.Column<bool>(type: "bit", nullable: false, comment: "Флаг проверки записи сотрудником"),
                    created_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя в базе AuthService создавшего запись"),
                    created_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя создавшего запись"),
                    is_autorenew_schedules = table.Column<bool>(type: "bit", nullable: false, comment: "Включить ли автопродление графика"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи"),
                    data_id = table.Column<int>(type: "int", nullable: false, comment: "ИД отслеживаемой записи"),
                    change_dt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата изменения записи"),
                    change_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя изменившего запись"),
                    change_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя изменившего запись")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contract_history", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "contract_schedule__container2schedule",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    container_id = table.Column<int>(type: "int", nullable: false),
                    contract_schedule_json = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Сериализованный экземпляр массива объектов расписания ContractScheduleModel"),
                    schedule_comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    work_time_from = table.Column<TimeSpan>(type: "time", nullable: false, comment: "С какого времени можно вывести мусор"),
                    work_time_to = table.Column<TimeSpan>(type: "time", nullable: false, comment: "До какого времени можно вывести мусор"),
                    is_in_registry = table.Column<bool>(type: "bit", nullable: false, comment: "Флаг нахождения контейнера в гос реестре контейнеров"),
                    in_registry_from = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "С какой даты контейнер находится в гос реестре"),
                    contract_id = table.Column<int>(type: "int", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contract_schedule__container2schedule", x => x.id);
                },
                comment: "Таблица-связка контейнера с графиком вывоза и договором");

            migrationBuilder.CreateTable(
                name: "contract_schedule__container2schedule_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    container_id = table.Column<int>(type: "int", nullable: false),
                    contract_schedule_json = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Сериализованный экземпляр массива объектов расписания ContractScheduleModel"),
                    schedule_comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    work_time_from = table.Column<TimeSpan>(type: "time", nullable: false, comment: "С какого времени можно вывести мусор"),
                    work_time_to = table.Column<TimeSpan>(type: "time", nullable: false, comment: "До какого времени можно вывести мусор"),
                    is_in_registry = table.Column<bool>(type: "bit", nullable: false, comment: "Флаг нахождения контейнера в гос реестре контейнеров"),
                    in_registry_from = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "С какой даты контейнер находится в гос реестре"),
                    contract_id = table.Column<int>(type: "int", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи"),
                    data_id = table.Column<int>(type: "int", nullable: false, comment: "ИД отслеживаемой записи"),
                    change_dt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата изменения записи"),
                    change_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя изменившего запись"),
                    change_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя изменившего запись")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contract_schedule__container2schedule_history", x => x.id);
                },
                comment: "История изменений таблицы-связки контейнера с графиком вывоза и договором");

            migrationBuilder.CreateTable(
                name: "contract_schedule__mission",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contract_id = table.Column<int>(type: "int", nullable: false, comment: "Ид договора"),
                    stand_id = table.Column<int>(type: "int", nullable: false, comment: "Ид контейнерной площадки"),
                    mission_date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата в которую необходимо выполнить вывоз"),
                    is_completed = table.Column<bool>(type: "bit", nullable: false, comment: "Выполнена ли задача"),
                    is_urgent = table.Column<bool>(type: "bit", nullable: false, comment: "Является ли задача срочной true - срочная, false - сгенерированная автоматически"),
                    is_failed = table.Column<bool>(type: "bit", nullable: false, comment: "Была ли задача провалена (пропущен вызов)"),
                    created_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя создавшего запись"),
                    created_user_name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true, comment: "Имя пользователя создавшего запись"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contract_schedule__mission", x => x.id);
                },
                comment: "Табличка с заданием вывоза на дату");

            migrationBuilder.CreateTable(
                name: "contract_schedule__mission_2_container",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mission_id = table.Column<int>(type: "int", nullable: false, comment: "ИД задания на вывоз"),
                    container_id = table.Column<int>(type: "int", nullable: false, comment: "ИД контейнера"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contract_schedule__mission_2_container", x => x.id);
                },
                comment: "Таблица-связка задания на вывоз с контейнерами");

            migrationBuilder.CreateTable(
                name: "contract_schedule__mission_2_container_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mission_id = table.Column<int>(type: "int", nullable: false, comment: "ИД задания на вывоз"),
                    container_id = table.Column<int>(type: "int", nullable: false, comment: "ИД контейнера"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи"),
                    data_id = table.Column<int>(type: "int", nullable: false, comment: "ИД отслеживаемой записи"),
                    change_dt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата изменения записи"),
                    change_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя изменившего запись"),
                    change_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя изменившего запись")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contract_schedule__mission_2_container_history", x => x.id);
                },
                comment: "Таблица истории связок задания на вывоз с контейнерами");

            migrationBuilder.CreateTable(
                name: "contract_schedule__mission_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contract_id = table.Column<int>(type: "int", nullable: false, comment: "Ид договора"),
                    stand_id = table.Column<int>(type: "int", nullable: false, comment: "Ид контейнерной площадки"),
                    mission_date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата в которую необходимо выполнить вывоз"),
                    is_completed = table.Column<bool>(type: "bit", nullable: false, comment: "Выполнена ли задача"),
                    is_urgent = table.Column<bool>(type: "bit", nullable: false, comment: "Является ли задача срочной true - срочная, false - сгенерированная автоматически"),
                    is_failed = table.Column<bool>(type: "bit", nullable: false, comment: "Была ли задача провалена (пропущен вызов)"),
                    created_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя создавшего запись"),
                    created_user_name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true, comment: "Имя пользователя создавшего запись"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи"),
                    data_id = table.Column<int>(type: "int", nullable: false, comment: "ИД отслеживаемой записи"),
                    change_dt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата изменения записи"),
                    change_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя изменившего запись"),
                    change_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя изменившего запись")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contract_schedule__mission_history", x => x.id);
                },
                comment: "Табличка с историей изменения задач на вывоз");

            migrationBuilder.CreateTable(
                name: "stand",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    is_high_security_object = table.Column<bool>(type: "bit", nullable: false),
                    address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    coordinates_json = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    drive_way = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    actual_photo_path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contacts = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    stand_type = table.Column<byte>(type: "tinyint", maxLength: 300, nullable: false),
                    have_base = table.Column<bool>(type: "bit", nullable: false, comment: "Есть ли основание"),
                    have_roof = table.Column<bool>(type: "bit", nullable: false, comment: "Есть ли крыша"),
                    have_fence = table.Column<bool>(type: "bit", nullable: false, comment: "Есть ли ограждение"),
                    is_validated = table.Column<bool>(type: "bit", nullable: false, comment: "Флаг проверки записи сотрудником"),
                    created_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя в базе AuthService создавшего запись"),
                    created_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя создавшего запись"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stand", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "stand_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    is_high_security_object = table.Column<bool>(type: "bit", nullable: false),
                    address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    coordinates_json = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    drive_way = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    actual_photo_path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contacts = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    stand_type = table.Column<byte>(type: "tinyint", maxLength: 300, nullable: false),
                    have_base = table.Column<bool>(type: "bit", nullable: false, comment: "Есть ли основание"),
                    have_roof = table.Column<bool>(type: "bit", nullable: false, comment: "Есть ли крыша"),
                    have_fence = table.Column<bool>(type: "bit", nullable: false, comment: "Есть ли ограждение"),
                    is_validated = table.Column<bool>(type: "bit", nullable: false, comment: "Флаг проверки записи сотрудником"),
                    created_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя в базе AuthService создавшего запись"),
                    created_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя создавшего запись"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи"),
                    data_id = table.Column<int>(type: "int", nullable: false, comment: "ИД отслеживаемой записи"),
                    change_dt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата изменения записи"),
                    change_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя изменившего запись"),
                    change_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя изменившего запись")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stand_history", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "container");

            migrationBuilder.DropTable(
                name: "container_history");

            migrationBuilder.DropTable(
                name: "container_type");

            migrationBuilder.DropTable(
                name: "contract");

            migrationBuilder.DropTable(
                name: "contract_client");

            migrationBuilder.DropTable(
                name: "contract_client_history");

            migrationBuilder.DropTable(
                name: "contract_history");

            migrationBuilder.DropTable(
                name: "contract_schedule__container2schedule");

            migrationBuilder.DropTable(
                name: "contract_schedule__container2schedule_history");

            migrationBuilder.DropTable(
                name: "contract_schedule__mission");

            migrationBuilder.DropTable(
                name: "contract_schedule__mission_2_container");

            migrationBuilder.DropTable(
                name: "contract_schedule__mission_2_container_history");

            migrationBuilder.DropTable(
                name: "contract_schedule__mission_history");

            migrationBuilder.DropTable(
                name: "stand");

            migrationBuilder.DropTable(
                name: "stand_history");
        }
    }
}
