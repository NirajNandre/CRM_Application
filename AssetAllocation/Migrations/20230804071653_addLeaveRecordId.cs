using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    public partial class addLeaveRecordId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeaveRecordId",
                table: "Leave",
                type: "int",
                nullable: true
                );

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
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
