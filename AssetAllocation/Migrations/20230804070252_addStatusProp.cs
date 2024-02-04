using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    public partial class addStatusProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeaveStatus",
                table: "LeaveRecord");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRecord_ApprovedBy",
                table: "LeaveRecord",
                column: "ApprovedBy");

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
                name: "FK_LeaveRecord_Users_ApprovedBy",
                table: "LeaveRecord");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRecord_ApprovedBy",
                table: "LeaveRecord");

            migrationBuilder.AddColumn<string>(
                name: "LeaveStatus",
                table: "LeaveRecord",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
