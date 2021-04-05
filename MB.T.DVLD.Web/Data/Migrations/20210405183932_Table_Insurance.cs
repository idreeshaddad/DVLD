using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DVLD.Data.Migrations
{
    public partial class Table_Insurance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InsuranceId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Insurance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsuranceType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurance", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_InsuranceId",
                table: "Cars",
                column: "InsuranceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Insurance_InsuranceId",
                table: "Cars",
                column: "InsuranceId",
                principalTable: "Insurance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Insurance_InsuranceId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "Insurance");

            migrationBuilder.DropIndex(
                name: "IX_Cars_InsuranceId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "InsuranceId",
                table: "Cars");
        }
    }
}
