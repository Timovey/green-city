using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.AuthMigrations
{
    public partial class added_orgs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "organization",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Название"),
                    address = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false, comment: "Адрес"),
                    phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Телефон"),
                    ogrn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "ОГРН"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization", x => x.id);
                },
                comment: "Организация");

            migrationBuilder.CreateTable(
                name: "organization_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "Первичный ключ")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Название"),
                    address = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false, comment: "Адрес"),
                    phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Телефон"),
                    ogrn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "ОГРН"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, comment: "Признак удаленности записи"),
                    data_id = table.Column<int>(type: "int", nullable: false, comment: "ИД отслеживаемой записи"),
                    change_dt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата изменения записи"),
                    change_user_id = table.Column<int>(type: "int", nullable: false, comment: "ИД пользователя изменившего запись"),
                    change_user_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Имя пользователя изменившего запись")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_history", x => x.id);
                },
                comment: "История изменений ораганизаций");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "organization");

            migrationBuilder.DropTable(
                name: "organization_history");
        }
    }
}
