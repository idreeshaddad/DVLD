using Microsoft.EntityFrameworkCore.Migrations;

namespace DVLD.Data.Migrations
{
    public partial class InsuranceCompanyIntoInsurancePolicy_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InsuranceCompanyId",
                table: "InsurancePolicies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicies_InsuranceCompanyId",
                table: "InsurancePolicies",
                column: "InsuranceCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_InsurancePolicies_InsuranceCompanies_InsuranceCompanyId",
                table: "InsurancePolicies",
                column: "InsuranceCompanyId",
                principalTable: "InsuranceCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsurancePolicies_InsuranceCompanies_InsuranceCompanyId",
                table: "InsurancePolicies");

            migrationBuilder.DropIndex(
                name: "IX_InsurancePolicies_InsuranceCompanyId",
                table: "InsurancePolicies");

            migrationBuilder.DropColumn(
                name: "InsuranceCompanyId",
                table: "InsurancePolicies");
        }
    }
}
