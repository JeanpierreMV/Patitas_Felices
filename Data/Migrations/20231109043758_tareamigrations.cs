using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Patitas_Felices.Data.Migrations
{
    /// <inheritdoc />
    public partial class tareamigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "fechayhora",
                table: "TAREA",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fechayhora",
                table: "TAREA");
        }
    }
}
