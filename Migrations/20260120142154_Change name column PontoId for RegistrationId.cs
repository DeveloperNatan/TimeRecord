using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistrarPonto.Migrations
{
    /// <inheritdoc />
    public partial class ChangenamecolumnPontoIdforRegistrationId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MatriculaId",
                table: "Markings",
                newName: "RegistrationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegistrationId",
                table: "Markings",
                newName: "MatriculaId");
        }
    }
}
