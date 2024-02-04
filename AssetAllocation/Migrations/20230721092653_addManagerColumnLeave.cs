using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    public partial class addManagerColumnLeave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApprovedBy",
                table: "LeaveRecord",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "LeaveStatus",
                table: "LeaveRecord",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Manager",
                table: "Leave",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRecord_ApprovedBy",
                table: "LeaveRecord",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Leave_Manager",
                table: "Leave",
                column: "Manager");

            migrationBuilder.AddForeignKey(
                name: "FK_Leave_Users_Manager",
                table: "Leave",
                column: "Manager",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRecord_Users_ApprovedBy",
                table: "LeaveRecord",
                column: "ApprovedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leave_Users_Manager",
                table: "Leave");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRecord_Users_ApprovedBy",
                table: "LeaveRecord");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRecord_ApprovedBy",
                table: "LeaveRecord");

            migrationBuilder.DropIndex(
                name: "IX_Leave_Manager",
                table: "Leave");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "LeaveRecord");

            migrationBuilder.DropColumn(
                name: "LeaveStatus",
                table: "LeaveRecord");

            migrationBuilder.DropColumn(
                name: "Manager",
                table: "Leave");
        }
    }
}
