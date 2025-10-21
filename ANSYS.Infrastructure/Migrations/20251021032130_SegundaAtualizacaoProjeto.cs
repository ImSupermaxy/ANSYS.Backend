using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ANSYS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SegundaAtualizacaoProjeto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Usuarios",
                newSchema: "public");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Usuarios",
                schema: "public",
                newName: "Usuarios");
        }
    }
}
