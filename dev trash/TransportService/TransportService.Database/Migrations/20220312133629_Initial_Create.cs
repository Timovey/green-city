using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TransportService.Database.Migrations
{
    public partial class Initial_Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    OrganizationId = table.Column<int>(type: "integer", nullable: false),
                    Rent = table.Column<bool>(type: "boolean", nullable: false),
                    RegNumber = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    Sts = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    TransportType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BodyType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BodyColor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TankFence = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TankType = table.Column<byte>(type: "smallint", nullable: false),
                    LoadCapacity = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    CargoVolume = table.Column<int>(type: "integer", nullable: false),
                    CargoVolumeCoef = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Mileage = table.Column<int>(type: "integer", nullable: false),
                    ProductionYear = table.Column<int>(type: "integer", nullable: false),
                    Se = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    SpendingLimit = table.Column<string>(type: "text", nullable: true),
                    GsmNumber = table.Column<string>(type: "text", nullable: true),
                    LastTO = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    RepairsTO = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transports", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transports");
        }
    }
}
