using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamApi.DataAccess.Migrations
{
    public partial class ChangechildidtoobjectinUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonalInfoId",
                table: "Users",
                column: "PersonalInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_PersonalInfos_PersonalInfoId",
                table: "Users",
                column: "PersonalInfoId",
                principalTable: "PersonalInfos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_PersonalInfos_PersonalInfoId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PersonalInfoId",
                table: "Users");
        }
    }
}
