using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileService.Database.Migrations
{
    public partial class add_folder_file : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "path",
                table: "file_in_store_history",
                newName: "filename");

            migrationBuilder.RenameColumn(
                name: "path",
                table: "file_in_store",
                newName: "filename");

            migrationBuilder.AddColumn<string>(
                name: "folder",
                table: "file_in_store_history",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true,
                comment: "Путь до файла ");

            migrationBuilder.AddColumn<string>(
                name: "folder",
                table: "file_in_store",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true,
                comment: "Путь до файла ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "folder",
                table: "file_in_store_history");

            migrationBuilder.DropColumn(
                name: "folder",
                table: "file_in_store");

            migrationBuilder.RenameColumn(
                name: "filename",
                table: "file_in_store_history",
                newName: "path");

            migrationBuilder.RenameColumn(
                name: "filename",
                table: "file_in_store",
                newName: "path");
        }
    }
}
