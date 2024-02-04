using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    public partial class SalarySheet14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrossSalary_Y",
                table: "SalarySheet");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "GrossSalary_Y",
                table: "SalarySheet",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
