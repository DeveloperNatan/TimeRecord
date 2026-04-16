using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeRecord.Migrations
{
    /// <inheritdoc />
    public partial class fixnamecolumnemployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegistrationId",
                table: "Employees",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Employees",
                newName: "RegistrationId");
        }
    }
}
