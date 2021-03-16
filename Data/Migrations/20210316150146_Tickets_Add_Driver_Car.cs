using Microsoft.EntityFrameworkCore.Migrations;

namespace DVLD.Data.Migrations
{
    public partial class Tickets_Add_Driver_Car : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LicensePlate",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "VehicleModel",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "VehicleOwnerName",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DriverId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LicensePlate",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CarId",
                table: "Tickets",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_DriverId",
                table: "Tickets",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Cars_CarId",
                table: "Tickets",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Drivers_DriverId",
                table: "Tickets",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Cars_CarId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Drivers_DriverId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CarId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_DriverId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "LicensePlate",
                table: "Cars");

            migrationBuilder.AddColumn<string>(
                name: "LicensePlate",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VehicleModel",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VehicleOwnerName",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
