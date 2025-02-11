﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scholario.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMessagesStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MessageType",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageType",
                table: "Messages");
        }
    }
}
