using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    public partial class Salarysheet16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BaseSalary_M",
                table: "SalarySheet",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "EPF_M",
                table: "SalarySheet",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "HRA_M",
                table: "SalarySheet",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "SpecialAllowances_M",
                table: "SalarySheet",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "assuredPayoutPaid",
                table: "SalarySheet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isAssuredPayoutPaid",
                table: "SalarySheet",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseSalary_M",
                table: "SalarySheet");

            migrationBuilder.DropColumn(
                name: "EPF_M",
                table: "SalarySheet");

            migrationBuilder.DropColumn(
                name: "HRA_M",
                table: "SalarySheet");

            migrationBuilder.DropColumn(
                name: "SpecialAllowances_M",
                table: "SalarySheet");

            migrationBuilder.DropColumn(
                name: "assuredPayoutPaid",
                table: "SalarySheet");

            migrationBuilder.DropColumn(
                name: "isAssuredPayoutPaid",
                table: "SalarySheet");
        }
    }
}
