using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamApi.DataAccess.Migrations
{
    public partial class AdjustPersonalInfoResidenceInforelationship2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResidenceInfos_PersonalInfos_Id",
                table: "ResidenceInfos");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ResidenceInfos",
                newName: "ResidenceInfoId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PersonalInfos",
                newName: "PersonalInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResidenceInfos_PersonalInfos_ResidenceInfoId",
                table: "ResidenceInfos",
                column: "ResidenceInfoId",
                principalTable: "PersonalInfos",
                principalColumn: "PersonalInfoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResidenceInfos_PersonalInfos_ResidenceInfoId",
                table: "ResidenceInfos");

            migrationBuilder.RenameColumn(
                name: "ResidenceInfoId",
                table: "ResidenceInfos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PersonalInfoId",
                table: "PersonalInfos",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResidenceInfos_PersonalInfos_Id",
                table: "ResidenceInfos",
                column: "Id",
                principalTable: "PersonalInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
