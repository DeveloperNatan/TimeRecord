using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeRecord.Migrations
{
    /// <inheritdoc />
    public partial class change_name_marking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Markings_Companies_CompanyId",
                table: "Markings");

            migrationBuilder.DropForeignKey(
                name: "FK_Markings_Employees_EmployeeId",
                table: "Markings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Markings",
                table: "Markings");

            migrationBuilder.RenameTable(
                name: "Markings",
                newName: "TimeRecords");

            migrationBuilder.RenameIndex(
                name: "IX_Markings_EmployeeId",
                table: "TimeRecords",
                newName: "IX_TimeRecords_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Markings_CompanyId",
                table: "TimeRecords",
                newName: "IX_TimeRecords_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeRecords",
                table: "TimeRecords",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeRecords_Companies_CompanyId",
                table: "TimeRecords",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeRecords_Employees_EmployeeId",
                table: "TimeRecords",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeRecords_Companies_CompanyId",
                table: "TimeRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeRecords_Employees_EmployeeId",
                table: "TimeRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeRecords",
                table: "TimeRecords");

            migrationBuilder.RenameTable(
                name: "TimeRecords",
                newName: "Markings");

            migrationBuilder.RenameIndex(
                name: "IX_TimeRecords_EmployeeId",
                table: "Markings",
                newName: "IX_Markings_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_TimeRecords_CompanyId",
                table: "Markings",
                newName: "IX_Markings_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Markings",
                table: "Markings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Markings_Companies_CompanyId",
                table: "Markings",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Markings_Employees_EmployeeId",
                table: "Markings",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
