using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    public partial class removeLeaveIdfLR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeaveId",
                table: "LeaveRecord",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
