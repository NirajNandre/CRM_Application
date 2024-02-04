using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    public partial class addLeaveId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leave_LeaveRecord_LeaveRecordId",
                table: "Leave");

            migrationBuilder.DropIndex(
                name: "IX_Leave_LeaveRecordId",
                table: "Leave");

            migrationBuilder.DropColumn(
                name: "LeaveRecordId",
                table: "Leave");

            migrationBuilder.AddColumn<int>(
                name: "LeaveId",
                table: "LeaveRecord",
                type: "int",
                nullable: true
                );

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRecord_LeaveId",
                table: "LeaveRecord",
                column: "LeaveId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRecord_Leave_LeaveId",
                table: "LeaveRecord",
                column: "LeaveId",
                principalTable: "Leave",
                principalColumn: "LeaveId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRecord_Leave_LeaveId",
                table: "LeaveRecord");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRecord_LeaveId",
                table: "LeaveRecord");

            migrationBuilder.DropColumn(
                name: "LeaveId",
                table: "LeaveRecord");

            migrationBuilder.AddColumn<int>(
                name: "LeaveRecordId",
                table: "Leave",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Leave_LeaveRecordId",
                table: "Leave",
                column: "LeaveRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leave_LeaveRecord_LeaveRecordId",
                table: "Leave",
                column: "LeaveRecordId",
                principalTable: "LeaveRecord",
                principalColumn: "LeaveRecordId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
