using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    public partial class updateManagerColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeMaster_Users_LastUpdatedBy",
                table: "EmployeeMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_Leave_Users_Manager",
                table: "Leave");

            migrationBuilder.DropIndex(
                name: "IX_Leave_Manager",
                table: "Leave");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeMaster_LastUpdatedBy",
                table: "EmployeeMaster");

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "EmployeeMaster",
                type: "int",
                nullable: false,
                defaultValue: 1);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeMaster_Users_ManagerId",
                table: "EmployeeMaster");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeMaster_ManagerId",
                table: "EmployeeMaster");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "EmployeeMaster");

            migrationBuilder.CreateIndex(
                name: "IX_Leave_Manager",
                table: "Leave",
                column: "Manager");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Leave_Users_Manager",
                table: "Leave",
                column: "Manager",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
