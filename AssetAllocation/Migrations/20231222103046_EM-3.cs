using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    public partial class EM3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeMaster_Users_UsersId",
                table: "EmployeeMaster");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeMaster_UsersId",
                table: "EmployeeMaster");

            migrationBuilder.DropColumn(
                name: "UsersId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeMaster_Users_ManagerId",
                table: "EmployeeMaster");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeMaster_ManagerId",
                table: "EmployeeMaster");

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "EmployeeMaster",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMaster_UsersId",
                table: "EmployeeMaster",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeMaster_Users_UsersId",
                table: "EmployeeMaster",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
