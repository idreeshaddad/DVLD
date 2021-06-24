using Microsoft.EntityFrameworkCore.Migrations;

namespace DVLD.Data.Migrations
{
    public partial class incpector_departments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InspectorId",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Inspectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BadgeNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inspectors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_InspectorId",
                table: "Departments",
                column: "InspectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Inspectors_InspectorId",
                table: "Departments",
                column: "InspectorId",
                principalTable: "Inspectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Inspectors_InspectorId",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "Inspectors");

            migrationBuilder.DropIndex(
                name: "IX_Departments_InspectorId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "InspectorId",
                table: "Departments");
        }
    }
}
