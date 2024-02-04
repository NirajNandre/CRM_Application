using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    public partial class SalarySheet7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalarySheet",
                columns: table => new
                {
                    SID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Part_B_IncomeTax_Y = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Part_B_IncomeTax_M = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Part_B_ProfessionalTax_Y = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Part_B_ProfessionalTax_M = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Part_B_LOP_M = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetSal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetSalInWords = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalYear = table.Column<int>(type: "int", nullable: false),
                    SalMonth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    workingDays = table.Column<int>(type: "int", nullable: false),
                    PaidDays = table.Column<int>(type: "int", nullable: false),
                    LOPDays = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentStatus = table.Column<bool>(type: "bit", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalarySheet", x => x.SID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalarySheet");
        }
    }
}
