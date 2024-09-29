using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace teste_jose_api.Migrations
{
    /// <inheritdoc />
    public partial class RenomearPropriedadeTentativasDeAcesso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TentativasDeAcess0",
                table: "Usuarios",
                newName: "TentativasDeAcesso");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TentativasDeAcesso",
                table: "Usuarios",
                newName: "TentativasDeAcess0");
        }
    }
}
