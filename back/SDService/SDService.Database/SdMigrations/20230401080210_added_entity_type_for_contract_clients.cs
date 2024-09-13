using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SDService.Database.SdMigrations
{
    public partial class added_entity_type_for_contract_clients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "entity_type",
                table: "contract_client_history",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                comment: "Тип сущности - доступная или недоступная для отображения пользователю");

            migrationBuilder.AddColumn<byte>(
                name: "entity_type",
                table: "contract_client",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                comment: "Тип сущности - доступная или недоступная для отображения пользователю");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "entity_type",
                table: "contract_client_history");

            migrationBuilder.DropColumn(
                name: "entity_type",
                table: "contract_client");
        }
    }
}
