﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    public partial class SalaryMaster_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropColumn(
                name: "PartB_PT_M",
                table: "SalaryMaster");

            migrationBuilder.DropColumn(
                name: "PartB_PT_Y",
                table: "SalaryMaster");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<decimal>(
                name: "PartB_PT_M",
                table: "SalaryMaster",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PartB_PT_Y",
                table: "SalaryMaster",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
