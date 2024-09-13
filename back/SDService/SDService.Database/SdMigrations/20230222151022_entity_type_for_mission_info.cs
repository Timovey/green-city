using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SDService.Database.SdMigrations
{
    public partial class entity_type_for_mission_info : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "actual_photo_path",
                table: "stand_history");

            migrationBuilder.DropColumn(
                name: "actual_photo_path",
                table: "stand");

            migrationBuilder.DropColumn(
                name: "is_actual",
                table: "container_history");

            migrationBuilder.DropColumn(
                name: "is_actual",
                table: "container");

            migrationBuilder.AddColumn<byte>(
                name: "entity_type",
                table: "stand_history",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                comment: "Тип сущности - доступная или недоступная для отображения пользователю");

            migrationBuilder.AddColumn<byte>(
                name: "entity_type",
                table: "stand",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                comment: "Тип сущности - доступная или недоступная для отображения пользователю");

            migrationBuilder.AlterColumn<int>(
                name: "contract_client_id",
                table: "contract_history",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<byte>(
                name: "entity_type",
                table: "contract_history",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                comment: "Тип сущности - доступная или недоступная для отображения пользователю");

            migrationBuilder.AlterColumn<int>(
                name: "contract_client_id",
                table: "contract",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<byte>(
                name: "entity_type",
                table: "contract",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                comment: "Тип сущности - доступная или недоступная для отображения пользователю");

            migrationBuilder.AlterColumn<decimal>(
                name: "volume",
                table: "container_type",
                type: "decimal(18,2)",
                nullable: false,
                comment: "Объем тары",
                oldClrType: typeof(float),
                oldType: "real",
                oldComment: "Объем тары");

            migrationBuilder.AddColumn<byte>(
                name: "entity_type",
                table: "container_type",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                comment: "Тип сущности - доступная или недоступная для отображения пользователю");

            migrationBuilder.AddColumn<byte>(
                name: "entity_type",
                table: "container_history",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                comment: "Тип сущности - доступная или недоступная для отображения пользователю");

            migrationBuilder.AddColumn<byte>(
                name: "entity_type",
                table: "container",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                comment: "Тип сущности - доступная или недоступная для отображения пользователю");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "entity_type",
                table: "stand_history");

            migrationBuilder.DropColumn(
                name: "entity_type",
                table: "stand");

            migrationBuilder.DropColumn(
                name: "entity_type",
                table: "contract_history");

            migrationBuilder.DropColumn(
                name: "entity_type",
                table: "contract");

            migrationBuilder.DropColumn(
                name: "entity_type",
                table: "container_type");

            migrationBuilder.DropColumn(
                name: "entity_type",
                table: "container_history");

            migrationBuilder.DropColumn(
                name: "entity_type",
                table: "container");

            migrationBuilder.AddColumn<string>(
                name: "actual_photo_path",
                table: "stand_history",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "actual_photo_path",
                table: "stand",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "contract_client_id",
                table: "contract_history",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "contract_client_id",
                table: "contract",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "volume",
                table: "container_type",
                type: "real",
                nullable: false,
                comment: "Объем тары",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "Объем тары");

            migrationBuilder.AddColumn<int>(
                name: "is_actual",
                table: "container_history",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "is_actual",
                table: "container",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
