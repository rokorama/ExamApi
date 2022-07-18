using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamApi.DataAccess.Migrations
{
    public partial class Changepersonalnumberstoulong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInfos_ResidenceInfos_ResidenceInfoId",
                table: "PersonalInfos");

            migrationBuilder.DropIndex(
                name: "IX_PersonalInfos_ResidenceInfoId",
                table: "PersonalInfos");

            migrationBuilder.DropColumn(
                name: "ResidenceInfoId",
                table: "PersonalInfos");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonalInfoId",
                table: "ResidenceInfos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "PersonalNumber",
                table: "PersonalInfos",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "PersonalNumber",
                table: "PersonalInfos",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AddColumn<Guid>(
                name: "ResidenceInfoId",
                table: "PersonalInfos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalInfos_ResidenceInfoId",
                table: "PersonalInfos",
                column: "ResidenceInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInfos_ResidenceInfos_ResidenceInfoId",
                table: "PersonalInfos",
                column: "ResidenceInfoId",
                principalTable: "ResidenceInfos",
                principalColumn: "Id");
        }
    }
}
