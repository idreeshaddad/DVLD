using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore_LINQ.Migrations
{
    public partial class add_list_courses_to_student : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_StudentId",
                table: "Courses",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Students_StudentId",
                table: "Courses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Students_StudentId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_StudentId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Courses");
        }
    }
}
