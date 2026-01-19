using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistrarPonto.Migrations
{
    /// <inheritdoc />
    public partial class changecolumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Senha",
                table: "Employees",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Employees",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Cargo",
                table: "Employees",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Employees",
                newName: "Senha");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Employees",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Employees",
                newName: "Cargo");
        }
    }
}
