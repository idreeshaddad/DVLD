using Microsoft.EntityFrameworkCore.Migrations;

namespace DVLD.Data.Migrations
{
    public partial class Car_Policy_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InsurancePolicyId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_InsurancePolicyId",
                table: "Cars",
                column: "InsurancePolicyId",
                unique: true,
                filter: "[InsurancePolicyId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_InsurancePolicies_InsurancePolicyId",
                table: "Cars",
                column: "InsurancePolicyId",
                principalTable: "InsurancePolicies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_InsurancePolicies_InsurancePolicyId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_InsurancePolicyId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "InsurancePolicyId",
                table: "Cars");
        }
    }
}
