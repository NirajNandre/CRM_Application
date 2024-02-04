using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    public partial class addEmpId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leave_EmployeeMaster_EmployeeId",
                table: "Leave");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Leave",
                newName: "EmpId");

            migrationBuilder.RenameIndex(
                name: "IX_Leave_EmployeeId",
                table: "Leave",
                newName: "IX_Leave_EmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leave_EmployeeMaster_EmpId",
                table: "Leave",
                column: "EmpId",
                principalTable: "EmployeeMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leave_EmployeeMaster_EmpId",
                table: "Leave");

            migrationBuilder.RenameColumn(
                name: "EmpId",
                table: "Leave",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Leave_EmpId",
                table: "Leave",
                newName: "IX_Leave_EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leave_EmployeeMaster_EmployeeId",
                table: "Leave",
                column: "EmployeeId",
                principalTable: "EmployeeMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
