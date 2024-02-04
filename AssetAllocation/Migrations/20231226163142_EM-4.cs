using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    public partial class EM4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeMaster_Users_ManagerId",
                table: "EmployeeMaster");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeMaster_ManagerId",
                table: "EmployeeMaster");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMaster_LastUpdatedBy",
                table: "EmployeeMaster",
                column: "LastUpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeMaster_Users_LastUpdatedBy",
                table: "EmployeeMaster",
                column: "LastUpdatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeMaster_Users_LastUpdatedBy",
                table: "EmployeeMaster");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeMaster_LastUpdatedBy",
                table: "EmployeeMaster");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMaster_ManagerId",
                table: "EmployeeMaster",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeMaster_Users_ManagerId",
                table: "EmployeeMaster",
                column: "ManagerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
