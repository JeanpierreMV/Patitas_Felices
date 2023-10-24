using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Patitas_Felices.Data.Migrations
{
    /// <inheritdoc />
    public partial class R_tercMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "ADOPCION",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "ADOPCION");
        }
    }
}
