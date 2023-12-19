using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolarApp.Migrations
{
    /// <inheritdoc />
    public partial class ChangesOnEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Modules",
                table: "Project",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<decimal>(
                name: "ModulesPower",
                table: "Project",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Tariff",
                table: "Bill",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModulesPower",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Tariff",
                table: "Bill");

            migrationBuilder.AlterColumn<int>(
                name: "Modules",
                table: "Project",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }
    }
}
