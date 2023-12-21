using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SolarApp.Migrations
{
    /// <inheritdoc />
    public partial class RefactorLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Bill");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Project",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Client",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Bill",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    State = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_LocationId",
                table: "Project",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_LocationId",
                table: "Client",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_LocationId",
                table: "Bill",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bill_Location_LocationId",
                table: "Bill",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_Location_LocationId",
                table: "Client",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Location_LocationId",
                table: "Project",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bill_Location_LocationId",
                table: "Bill");

            migrationBuilder.DropForeignKey(
                name: "FK_Client_Location_LocationId",
                table: "Client");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Location_LocationId",
                table: "Project");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Project_LocationId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Client_LocationId",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Bill_LocationId",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Bill");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Client",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Client",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Client",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Bill",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Bill",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Bill",
                type: "text",
                nullable: true);
        }
    }
}
