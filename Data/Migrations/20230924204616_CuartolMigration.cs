using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Patitas_Felices.Data.Migrations
{
    /// <inheritdoc />
    public partial class CuartolMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "tipo",
                table: "Mascotas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ADOPCION",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fot_DNI = table.Column<byte[]>(type: "bytea", nullable: false),
                    R_luz = table.Column<byte[]>(type: "bytea", nullable: false),
                    R_agua = table.Column<byte[]>(type: "bytea", nullable: false),
                    Q_familiares = table.Column<int>(type: "integer", nullable: false),
                    Q_niños = table.Column<int>(type: "integer", nullable: false),
                    Desc_Domicilio = table.Column<string>(type: "text", nullable: false),
                    Razon = table.Column<string>(type: "text", nullable: false),
                    CLIENTEid = table.Column<int>(type: "integer", nullable: false),
                    MASCOTASid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADOPCION", x => x.id);
                    table.ForeignKey(
                        name: "FK_ADOPCION_Cliente_CLIENTEid",
                        column: x => x.CLIENTEid,
                        principalTable: "Cliente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ADOPCION_Mascotas_MASCOTASid",
                        column: x => x.MASCOTASid,
                        principalTable: "Mascotas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "H_MEDICO",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tip_Sangre = table.Column<string>(type: "text", nullable: false),
                    Fecha_reg = table.Column<string>(type: "text", nullable: false),
                    Adj_img = table.Column<byte[]>(type: "bytea", nullable: false),
                    D_medicinas = table.Column<byte[]>(type: "bytea", nullable: false),
                    D_tratamientos = table.Column<byte[]>(type: "bytea", nullable: false),
                    observaciones = table.Column<string>(type: "text", nullable: false),
                    MASCOTASid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_H_MEDICO", x => x.id);
                    table.ForeignKey(
                        name: "FK_H_MEDICO_Mascotas_MASCOTASid",
                        column: x => x.MASCOTASid,
                        principalTable: "Mascotas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ADOPCION_CLIENTEid",
                table: "ADOPCION",
                column: "CLIENTEid");

            migrationBuilder.CreateIndex(
                name: "IX_ADOPCION_MASCOTASid",
                table: "ADOPCION",
                column: "MASCOTASid");

            migrationBuilder.CreateIndex(
                name: "IX_H_MEDICO_MASCOTASid",
                table: "H_MEDICO",
                column: "MASCOTASid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ADOPCION");

            migrationBuilder.DropTable(
                name: "H_MEDICO");

            migrationBuilder.DropColumn(
                name: "tipo",
                table: "Mascotas");
        }
    }
}
