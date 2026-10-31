using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scholario.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSubjectAndGrade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FinalAssessment",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SemiFinalAssessment",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GradeWeight",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalAssessment",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "SemiFinalAssessment",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "GradeWeight",
                table: "Grades");
        }
    }
}
