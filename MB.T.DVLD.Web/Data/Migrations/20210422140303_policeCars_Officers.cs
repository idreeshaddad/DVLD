using Microsoft.EntityFrameworkCore.Migrations;

namespace DVLD.Data.Migrations
{
    public partial class policeCars_Officers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PoliceCar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliceCar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfficerPoliceCar",
                columns: table => new
                {
                    OfficersId = table.Column<int>(type: "int", nullable: false),
                    PoliceCarsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficerPoliceCar", x => new { x.OfficersId, x.PoliceCarsId });
                    table.ForeignKey(
                        name: "FK_OfficerPoliceCar_Officers_OfficersId",
                        column: x => x.OfficersId,
                        principalTable: "Officers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfficerPoliceCar_PoliceCar_PoliceCarsId",
                        column: x => x.PoliceCarsId,
                        principalTable: "PoliceCar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfficerPoliceCar_PoliceCarsId",
                table: "OfficerPoliceCar",
                column: "PoliceCarsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfficerPoliceCar");

            migrationBuilder.DropTable(
                name: "PoliceCar");
        }
    }
}
