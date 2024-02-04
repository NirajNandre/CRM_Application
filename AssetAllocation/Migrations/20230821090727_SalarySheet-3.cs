using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    public partial class SalarySheet3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UAN",
                table: "EmployeeMaster",
                newName: "AdharNo");

            migrationBuilder.RenameColumn(
                name: "PAN_No",
                table: "EmployeeMaster",
                newName: "pan");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "pan",
                table: "EmployeeMaster",
                newName: "PAN_No");

            migrationBuilder.RenameColumn(
                name: "AdharNo",
                table: "EmployeeMaster",
                newName: "UAN");
        }
    }
}
