using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.AuthMigrations
{
    public partial class added_user_to_orgs_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_to_org",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя"),
                    org_ids_json = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Идентификаторы организаций"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_to_org", x => x.id);
                },
                comment: "Таблица сопоставления пользователя с организациями");

            migrationBuilder.CreateTable(
                name: "user_to_org_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя"),
                    org_ids_json = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Идентификаторы организаций"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи"),
                    data_id = table.Column<int>(type: "int", nullable: false, comment: "ИД отслеживаемой записи"),
                    change_dt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата изменения записи"),
                    change_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя изменившего запись"),
                    change_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя изменившего запись")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_to_org_history", x => x.id);
                },
                comment: "Таблица изменений сопоставлений пользователя с организациями");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_to_org");

            migrationBuilder.DropTable(
                name: "user_to_org_history");
        }
    }
}
