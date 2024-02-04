using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    public partial class SalarySheet8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "SalarySheet");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "SalarySheet");

            migrationBuilder.DropColumn(
                name: "ProfessionalTax_M",
                table: "SalaryMaster");

            migrationBuilder.DropColumn(
                name: "ProfessionalTax_Y",
                table: "SalaryMaster");

            migrationBuilder.DropColumn(
                name: "TDS_M",
                table: "SalaryMaster");

            migrationBuilder.DropColumn(
                name: "TDS_Y",
                table: "SalaryMaster");

            migrationBuilder.RenameColumn(
                name: "Part_B_IncomeTax_Y",
                table: "SalarySheet",
                newName: "TotalDeduction_Y");

            migrationBuilder.RenameColumn(
                name: "Part_B_IncomeTax_M",
                table: "SalarySheet",
                newName: "TotalDeduction_M");

            migrationBuilder.RenameColumn(
                name: "NetSal",
                table: "SalarySheet",
                newName: "TDS_Y");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "SalarySheet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmpId",
                table: "SalarySheet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "GrossSalary_M",
                table: "SalarySheet",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "GrossSalary_Y",
                table: "SalarySheet",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

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
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TDS_M",
                table: "SalarySheet",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "struct_Id",
                table: "SalarySheet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SalarySheet_CreatedBy",
                table: "SalarySheet",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SalarySheet_EmpId",
                table: "SalarySheet",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_SalarySheet_struct_Id",
                table: "SalarySheet",
                column: "struct_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalarySheet_EmployeeMaster_EmpId",
                table: "SalarySheet",
                column: "EmpId",
                principalTable: "EmployeeMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalarySheet_SalaryMaster_struct_Id",
                table: "SalarySheet",
                column: "struct_Id",
                principalTable: "SalaryMaster",
                principalColumn: "Struct_ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_SalarySheet_Users_CreatedBy",
                table: "SalarySheet",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalarySheet_EmployeeMaster_EmpId",
                table: "SalarySheet");

            migrationBuilder.DropForeignKey(
                name: "FK_SalarySheet_SalaryMaster_struct_Id",
                table: "SalarySheet");

            migrationBuilder.DropForeignKey(
                name: "FK_SalarySheet_Users_CreatedBy",
                table: "SalarySheet");

            migrationBuilder.DropIndex(
                name: "IX_SalarySheet_CreatedBy",
                table: "SalarySheet");

            migrationBuilder.DropIndex(
                name: "IX_SalarySheet_EmpId",
                table: "SalarySheet");

            migrationBuilder.DropIndex(
                name: "IX_SalarySheet_struct_Id",
                table: "SalarySheet");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SalarySheet");

            migrationBuilder.DropColumn(
                name: "EmpId",
                table: "SalarySheet");

            migrationBuilder.DropColumn(
                name: "GrossSalary_M",
                table: "SalarySheet");

            migrationBuilder.DropColumn(
                name: "GrossSalary_Y",
                table: "SalarySheet");

            migrationBuilder.DropColumn(
                name: "NetPay",
                table: "SalarySheet");

            migrationBuilder.DropColumn(
                name: "NetSal_Y",
                table: "SalarySheet");

            migrationBuilder.DropColumn(
                name: "TDS_M",
                table: "SalarySheet");

            migrationBuilder.DropColumn(
                name: "struct_Id",
                table: "SalarySheet");

            migrationBuilder.RenameColumn(
                name: "TotalDeduction_Y",
                table: "SalarySheet",
                newName: "Part_B_IncomeTax_Y");

            migrationBuilder.RenameColumn(
                name: "TotalDeduction_M",
                table: "SalarySheet",
                newName: "Part_B_IncomeTax_M");

            migrationBuilder.RenameColumn(
                name: "TDS_Y",
                table: "SalarySheet",
                newName: "NetSal");

            migrationBuilder.AddColumn<bool>(
                name: "PaymentStatus",
                table: "SalarySheet",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "SalarySheet",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "ProfessionalTax_M",
                table: "SalaryMaster",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProfessionalTax_Y",
                table: "SalaryMaster",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TDS_M",
                table: "SalaryMaster",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TDS_Y",
                table: "SalaryMaster",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
