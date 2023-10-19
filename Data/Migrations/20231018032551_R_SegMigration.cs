using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Patitas_Felices.Data.Migrations
{
    /// <inheritdoc />
    public partial class R_SegMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Ant_Penales",
                table: "ADOPCION",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "Domicilio",
                table: "ADOPCION",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ant_Penales",
                table: "ADOPCION");

            migrationBuilder.DropColumn(
                name: "Domicilio",
                table: "ADOPCION");
        }
    }
}
