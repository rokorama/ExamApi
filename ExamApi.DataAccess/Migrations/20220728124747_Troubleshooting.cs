using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamApi.DataAccess.Migrations
{
    public partial class Troubleshooting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInfos_Addresses_AddressId",
                table: "PersonalInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_PersonalInfos_PersonalInfoId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PersonalInfoId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_PersonalInfos_AddressId",
                table: "PersonalInfos");

            migrationBuilder.AlterColumn<decimal>(
                name: "PersonalNumber",
                table: "PersonalInfos",
                type: "decimal(20,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonalInfoId",
                table: "Users",
                column: "PersonalInfoId",
                unique: true,
                filter: "[PersonalInfoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalInfos_AddressId",
                table: "PersonalInfos",
                column: "AddressId",
                unique: true,
                filter: "[AddressId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInfos_Addresses_AddressId",
                table: "PersonalInfos",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_PersonalInfos_PersonalInfoId",
                table: "Users",
                column: "PersonalInfoId",
                principalTable: "PersonalInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInfos_Addresses_AddressId",
                table: "PersonalInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_PersonalInfos_PersonalInfoId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PersonalInfoId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_PersonalInfos_AddressId",
                table: "PersonalInfos");

            migrationBuilder.AlterColumn<decimal>(
                name: "PersonalNumber",
                table: "PersonalInfos",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonalInfoId",
                table: "Users",
                column: "PersonalInfoId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Users_PersonalInfos_PersonalInfoId",
                table: "Users",
                column: "PersonalInfoId",
                principalTable: "PersonalInfos",
                principalColumn: "Id");
        }
    }
}
