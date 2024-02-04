using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    public partial class SM1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalSal_Fixyear",
                table: "SalaryMaster",
                newName: "TotalCTC_Y");

            migrationBuilder.RenameColumn(
                name: "TotalSal_Fixmonth",
                table: "SalaryMaster",
                newName: "TotalCTC_M");

            migrationBuilder.RenameColumn(
                name: "TotalCTC_Year",
                table: "SalaryMaster",
                newName: "TDS_Y");

            migrationBuilder.RenameColumn(
                name: "TotalCTC_Month",
                table: "SalaryMaster",
                newName: "TDS_M");

            migrationBuilder.RenameColumn(
                name: "PartB_OtherContribution_Y",
                table: "SalaryMaster",
                newName: "SpecialAllowance_Y");

            migrationBuilder.RenameColumn(
                name: "PartB_OtherContribution_M",
                table: "SalaryMaster",
                newName: "SpecialAllowance_M");

            migrationBuilder.RenameColumn(
                name: "PartB_EPF_Y",
                table: "SalaryMaster",
                newName: "ProfessionalTax_Y");

            migrationBuilder.RenameColumn(
                name: "PartB_EPF_M",
                table: "SalaryMaster",
                newName: "ProfessionalTax_M");

            migrationBuilder.RenameColumn(
                name: "PartA_SA_Y",
                table: "SalaryMaster",
                newName: "HRA_Y");

            migrationBuilder.RenameColumn(
                name: "PartA_SA_M",
                table: "SalaryMaster",
                newName: "HRA_M");

            migrationBuilder.RenameColumn(
                name: "PartA_HRA_Y",
                table: "SalaryMaster",
                newName: "EPF_Y");

            migrationBuilder.RenameColumn(
                name: "PartA_HRA_M",
                table: "SalaryMaster",
                newName: "EPF_M");

            migrationBuilder.RenameColumn(
                name: "PartA_Basic_Salary_Y",
                table: "SalaryMaster",
                newName: "BaseSalary_Y");

            migrationBuilder.RenameColumn(
                name: "PartA_Basic_Salary_M",
                table: "SalaryMaster",
                newName: "BaseSalary_M");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalCTC_Y",
                table: "SalaryMaster",
                newName: "TotalSal_Fixyear");

            migrationBuilder.RenameColumn(
                name: "TotalCTC_M",
                table: "SalaryMaster",
                newName: "TotalSal_Fixmonth");

            migrationBuilder.RenameColumn(
                name: "TDS_Y",
                table: "SalaryMaster",
                newName: "TotalCTC_Year");

            migrationBuilder.RenameColumn(
                name: "TDS_M",
                table: "SalaryMaster",
                newName: "TotalCTC_Month");

            migrationBuilder.RenameColumn(
                name: "SpecialAllowance_Y",
                table: "SalaryMaster",
                newName: "PartB_OtherContribution_Y");

            migrationBuilder.RenameColumn(
                name: "SpecialAllowance_M",
                table: "SalaryMaster",
                newName: "PartB_OtherContribution_M");

            migrationBuilder.RenameColumn(
                name: "ProfessionalTax_Y",
                table: "SalaryMaster",
                newName: "PartB_EPF_Y");

            migrationBuilder.RenameColumn(
                name: "ProfessionalTax_M",
                table: "SalaryMaster",
                newName: "PartB_EPF_M");

            migrationBuilder.RenameColumn(
                name: "HRA_Y",
                table: "SalaryMaster",
                newName: "PartA_SA_Y");

            migrationBuilder.RenameColumn(
                name: "HRA_M",
                table: "SalaryMaster",
                newName: "PartA_SA_M");

            migrationBuilder.RenameColumn(
                name: "EPF_Y",
                table: "SalaryMaster",
                newName: "PartA_HRA_Y");

            migrationBuilder.RenameColumn(
                name: "EPF_M",
                table: "SalaryMaster",
                newName: "PartA_HRA_M");

            migrationBuilder.RenameColumn(
                name: "BaseSalary_Y",
                table: "SalaryMaster",
                newName: "PartA_Basic_Salary_Y");

            migrationBuilder.RenameColumn(
                name: "BaseSalary_M",
                table: "SalaryMaster",
                newName: "PartA_Basic_Salary_M");
        }
    }
}
