using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    public partial class SalarySheet1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Acc_No",
                table: "EmployeeMaster",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "EmployeeMaster",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "EmployeeMaster",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PAN_No",
                table: "EmployeeMaster",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UAN",
                table: "EmployeeMaster",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Acc_No",
                table: "EmployeeMaster");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "EmployeeMaster");

            migrationBuilder.DropColumn(
                name: "Designation",
                table: "EmployeeMaster");

            migrationBuilder.DropColumn(
                name: "PAN_No",
                table: "EmployeeMaster");

            migrationBuilder.DropColumn(
                name: "UAN",
                table: "EmployeeMaster");
        }
    }
}
