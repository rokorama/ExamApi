using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamApi.DataAccess.Migrations
{
    public partial class AdjustPersonalInfoResidenceInforelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResidenceInfos_PersonalInfos_PersonalInfoId",
                table: "ResidenceInfos");

            migrationBuilder.DropIndex(
                name: "IX_ResidenceInfos_PersonalInfoId",
                table: "ResidenceInfos");

            migrationBuilder.DropColumn(
                name: "PersonalInfoId",
                table: "ResidenceInfos");

            migrationBuilder.AddForeignKey(
                name: "FK_ResidenceInfos_PersonalInfos_Id",
                table: "ResidenceInfos",
                column: "Id",
                principalTable: "PersonalInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResidenceInfos_PersonalInfos_Id",
                table: "ResidenceInfos");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonalInfoId",
                table: "ResidenceInfos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ResidenceInfos_PersonalInfoId",
                table: "ResidenceInfos",
                column: "PersonalInfoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ResidenceInfos_PersonalInfos_PersonalInfoId",
                table: "ResidenceInfos",
                column: "PersonalInfoId",
                principalTable: "PersonalInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
