using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistrarPonto.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Markings_Employees_MatriculaId",
                table: "Markings");

            migrationBuilder.DropIndex(
                name: "IX_Markings_MatriculaId",
                table: "Markings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Markings_MatriculaId",
                table: "Markings",
                column: "MatriculaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Markings_Employees_MatriculaId",
                table: "Markings",
                column: "MatriculaId",
                principalTable: "Employees",
                principalColumn: "MatriculaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
