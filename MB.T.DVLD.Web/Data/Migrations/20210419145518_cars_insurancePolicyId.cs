using Microsoft.EntityFrameworkCore.Migrations;

namespace DVLD.Data.Migrations
{
    public partial class cars_insurancePolicyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cars_InsurancePolicyId",
                table: "Cars");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_InsurancePolicyId",
                table: "Cars",
                column: "InsurancePolicyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cars_InsurancePolicyId",
                table: "Cars");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_InsurancePolicyId",
                table: "Cars",
                column: "InsurancePolicyId",
                unique: true,
                filter: "[InsurancePolicyId] IS NOT NULL");
        }
    }
}
