using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scholario.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeScheduleLessonHoursRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ScheduleEntries_LessonHourId",
                table: "ScheduleEntries");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEntries_LessonHourId",
                table: "ScheduleEntries",
                column: "LessonHourId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ScheduleEntries_LessonHourId",
                table: "ScheduleEntries");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEntries_LessonHourId",
                table: "ScheduleEntries",
                column: "LessonHourId",
                unique: true);
        }
    }
}
