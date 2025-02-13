using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scholario.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Student : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Groups_GroupId",
                table: "Persons");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Groups_GroupId",
                table: "Persons",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Groups_GroupId",
                table: "Persons");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Groups_GroupId",
                table: "Persons",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
