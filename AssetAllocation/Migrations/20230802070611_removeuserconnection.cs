using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    public partial class removeuserconnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRecord_Users_ApprovedBy",
                table: "LeaveRecord");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRecord_ApprovedBy",
                table: "LeaveRecord");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
