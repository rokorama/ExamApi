using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamApi.DataAccess.Migrations
{
    public partial class Removeredundantchildobjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_PersonalInfos_Id",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_PersonalInfos_PersonalInfoId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PersonalInfoId",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "PersonalInfos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalInfos_AddressId",
                table: "PersonalInfos",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInfos_Addresses_AddressId",
                table: "PersonalInfos",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInfos_Addresses_AddressId",
                table: "PersonalInfos");

            migrationBuilder.DropIndex(
                name: "IX_PersonalInfos_AddressId",
                table: "PersonalInfos");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "PersonalInfos");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonalInfoId",
                table: "Users",
                column: "PersonalInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_PersonalInfos_Id",
                table: "Addresses",
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
    }
}
