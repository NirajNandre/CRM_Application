using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    public partial class renameEmpIdLR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRecord_EmployeeMaster_EmployeeId",
                table: "LeaveRecord");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRecord_EmployeeId",
                table: "LeaveRecord");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "LeaveRecord");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRecord_EmpId",
                table: "LeaveRecord",
                column: "EmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRecord_EmployeeMaster_EmpId",
                table: "LeaveRecord",
                column: "EmpId",
                principalTable: "EmployeeMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRecord_EmployeeMaster_EmpId",
                table: "LeaveRecord");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRecord_EmpId",
                table: "LeaveRecord");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "LeaveRecord",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRecord_EmployeeId",
                table: "LeaveRecord",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRecord_EmployeeMaster_EmployeeId",
                table: "LeaveRecord",
                column: "EmployeeId",
                principalTable: "EmployeeMaster",
                principalColumn: "Id");
        }
    }
}
