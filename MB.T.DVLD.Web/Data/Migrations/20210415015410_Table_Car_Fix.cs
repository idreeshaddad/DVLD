using Microsoft.EntityFrameworkCore.Migrations;

namespace DVLD.Data.Migrations
{
    public partial class Table_Car_Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_InsuranceCompanies_InsuranceId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_InsuranceId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "InsuranceId",
                table: "Cars");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InsuranceId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_InsuranceId",
                table: "Cars",
                column: "InsuranceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_InsuranceCompanies_InsuranceId",
                table: "Cars",
                column: "InsuranceId",
                principalTable: "InsuranceCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
