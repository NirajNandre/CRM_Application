using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    public partial class SalarySheet12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NetPay",
                table: "SalarySheet");

            migrationBuilder.DropColumn(
                name: "NetSal_Y",
                table: "SalarySheet");

            migrationBuilder.RenameColumn(
                name: "TDS_Y",
                table: "SalarySheet",
                newName: "NetSal");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NetSal",
                table: "SalarySheet",
                newName: "TDS_Y");

            migrationBuilder.AddColumn<decimal>(
                name: "NetPay",
                table: "SalarySheet",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "NetSal_Y",
                table: "SalarySheet",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
