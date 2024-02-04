using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    public partial class addUserIdtoLeave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Leave",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "Leave",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leave_UsersId",
                table: "Leave",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leave_Users_UsersId",
                table: "Leave",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leave_Users_UsersId",
                table: "Leave");

            migrationBuilder.DropIndex(
                name: "IX_Leave_UsersId",
                table: "Leave");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Leave");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Leave");
        }
    }
}
