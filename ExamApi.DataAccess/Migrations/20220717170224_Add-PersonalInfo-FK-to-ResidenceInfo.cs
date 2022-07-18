using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamApi.DataAccess.Migrations
{
    public partial class AddPersonalInfoFKtoResidenceInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResidenceInfos_PersonalInfos_ResidenceInfoId",
                table: "ResidenceInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_PersonalInfos_PersonalInfoId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonalInfos",
                table: "PersonalInfos");

            migrationBuilder.RenameColumn(
                name: "ResidenceInfoId",
                table: "ResidenceInfos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PersonalInfoId",
                table: "PersonalInfos",
                newName: "ResidenceInfoGuid");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "PersonalInfos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonalInfos",
                table: "PersonalInfos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResidenceInfos_PersonalInfos_Id",
                table: "ResidenceInfos",
                column: "Id",
                principalTable: "PersonalInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_ResidenceInfos_PersonalInfos_Id",
                table: "ResidenceInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_PersonalInfos_PersonalInfoId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonalInfos",
                table: "PersonalInfos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PersonalInfos");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ResidenceInfos",
                newName: "ResidenceInfoId");

            migrationBuilder.RenameColumn(
                name: "ResidenceInfoGuid",
                table: "PersonalInfos",
                newName: "PersonalInfoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonalInfos",
                table: "PersonalInfos",
                column: "PersonalInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResidenceInfos_PersonalInfos_ResidenceInfoId",
                table: "ResidenceInfos",
                column: "ResidenceInfoId",
                principalTable: "PersonalInfos",
                principalColumn: "PersonalInfoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_PersonalInfos_PersonalInfoId",
                table: "Users",
                column: "PersonalInfoId",
                principalTable: "PersonalInfos",
                principalColumn: "PersonalInfoId");
        }
    }
}
