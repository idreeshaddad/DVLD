using Microsoft.EntityFrameworkCore.Migrations;

namespace Buildy.Data.Migrations
{
    public partial class exampaper_gradetop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GradeTop",
                table: "ExamPapers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GradeTop",
                table: "ExamPapers");
        }
    }
}
