using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    public partial class salaryMaster_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalaryMaster",
                columns: table => new
                {
                    Struct_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpId = table.Column<int>(type: "int", nullable: false),
                    TotalCTC_Year = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCTC_Month = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalSal_Fixyear = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalSal_Fixmonth = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PartA_Basic_Salary_Y = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PartA_Basic_Salary_M = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PartA_HRA_Y = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PartA_HRA_M = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PartA_SA_Y = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PartA_SA_M = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PartB_OtherContribution_Y = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PartB_OtherContribution_M = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PartB_PT_Y = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PartB_PT_M = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PartB_EPF_Y = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PartB_EPF_M = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AssuredPayout = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsEnableEPF = table.Column<bool>(type: "bit", nullable: false),
                    ApplicableFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicableTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryMaster", x => x.Struct_ID);
                    table.ForeignKey(
                        name: "FK_SalaryMaster_EmployeeMaster_EmpId",
                        column: x => x.EmpId,
                        principalTable: "EmployeeMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalaryMaster_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalaryMaster_CreatedBy",
                table: "SalaryMaster",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryMaster_EmpId",
                table: "SalaryMaster",
                column: "EmpId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalaryMaster");
        }
    }
}
