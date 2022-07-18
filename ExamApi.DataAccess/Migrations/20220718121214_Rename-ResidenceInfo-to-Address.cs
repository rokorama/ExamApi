using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamApi.DataAccess.Migrations
{
    public partial class RenameResidenceInfotoAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResidenceInfos");

            migrationBuilder.RenameColumn(
                name: "ResidenceInfoGuid",
                table: "PersonalInfos",
                newName: "AddressId");

            migrationBuilder.CreateTable(
                name: "Addresses",
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
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_PersonalInfos_Id",
                        column: x => x.Id,
                        principalTable: "PersonalInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "PersonalInfos",
                newName: "ResidenceInfoGuid");

            migrationBuilder.CreateTable(
                name: "ResidenceInfos",
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
                    table.PrimaryKey("PK_ResidenceInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResidenceInfos_PersonalInfos_Id",
                        column: x => x.Id,
                        principalTable: "PersonalInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
