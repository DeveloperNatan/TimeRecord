using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeRecord.Migrations
{
    /// <inheritdoc />
    public partial class changenamecolumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegistrationId",
                table: "Markings",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "PontoId",
                table: "Markings",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Markings",
                newName: "RegistrationId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Markings",
                newName: "PontoId");
        }
    }
}
