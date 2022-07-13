using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamApi.DataAccess.Migrations
{
    public partial class RenamePlaceOfResidenceclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInfos_PlacesOfResidence_PlaceOfResidenceId",
                table: "PersonalInfos");

            migrationBuilder.DropTable(
                name: "PlacesOfResidence");

            migrationBuilder.RenameColumn(
                name: "PlaceOfResidenceId",
                table: "PersonalInfos",
                newName: "ResidenceInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonalInfos_PlaceOfResidenceId",
                table: "PersonalInfos",
                newName: "IX_PersonalInfos_ResidenceInfoId");

            migrationBuilder.CreateTable(
                name: "ResidenceInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    House = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Flat = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidenceInfos", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInfos_ResidenceInfos_ResidenceInfoId",
                table: "PersonalInfos",
                column: "ResidenceInfoId",
                principalTable: "ResidenceInfos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInfos_ResidenceInfos_ResidenceInfoId",
                table: "PersonalInfos");

            migrationBuilder.DropTable(
                name: "ResidenceInfos");

            migrationBuilder.RenameColumn(
                name: "ResidenceInfoId",
                table: "PersonalInfos",
                newName: "PlaceOfResidenceId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonalInfos_ResidenceInfoId",
                table: "PersonalInfos",
                newName: "IX_PersonalInfos_PlaceOfResidenceId");

            migrationBuilder.CreateTable(
                name: "PlacesOfResidence",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Flat = table.Column<int>(type: "int", nullable: true),
                    House = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacesOfResidence", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInfos_PlacesOfResidence_PlaceOfResidenceId",
                table: "PersonalInfos",
                column: "PlaceOfResidenceId",
                principalTable: "PlacesOfResidence",
                principalColumn: "Id");
        }
    }
}
