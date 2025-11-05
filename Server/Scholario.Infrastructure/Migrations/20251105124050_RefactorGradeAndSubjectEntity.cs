using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scholario.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorGradeAndSubjectEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalAssessment",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "SemiFinalAssessment",
                table: "Subjects");

            migrationBuilder.AddColumn<int>(
                name: "GradeType",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GradeType",
                table: "Grades");

            migrationBuilder.AddColumn<float>(
                name: "FinalAssessment",
                table: "Subjects",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SemiFinalAssessment",
                table: "Subjects",
                type: "real",
                nullable: true);
        }
    }
}
