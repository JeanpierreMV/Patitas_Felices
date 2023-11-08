using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Patitas_Felices.Data.Migrations
{
    /// <inheritdoc />
    public partial class segMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VOLUNTARIO",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaYHora = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CLIENTEid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VOLUNTARIO", x => x.id);
                    table.ForeignKey(
                        name: "FK_VOLUNTARIO_Cliente_CLIENTEid",
                        column: x => x.CLIENTEid,
                        principalTable: "Cliente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TAREA",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Completada = table.Column<bool>(type: "boolean", nullable: false),
                    VOLUNTARIOid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAREA", x => x.id);
                    table.ForeignKey(
                        name: "FK_TAREA_VOLUNTARIO_VOLUNTARIOid",
                        column: x => x.VOLUNTARIOid,
                        principalTable: "VOLUNTARIO",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TAREA_VOLUNTARIOid",
                table: "TAREA",
                column: "VOLUNTARIOid");

            migrationBuilder.CreateIndex(
                name: "IX_VOLUNTARIO_CLIENTEid",
                table: "VOLUNTARIO",
                column: "CLIENTEid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TAREA");

            migrationBuilder.DropTable(
                name: "VOLUNTARIO");
        }
    }
}
