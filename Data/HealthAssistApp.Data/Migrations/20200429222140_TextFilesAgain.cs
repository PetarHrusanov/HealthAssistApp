using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthAssistApp.Data.Migrations
{
    public partial class TextFilesAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TextFilesForAdministration",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Content = table.Column<string>(maxLength: 8000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextFilesForAdministration", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TextFilesForAdministration_IsDeleted",
                table: "TextFilesForAdministration",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TextFilesForAdministration");
        }
    }
}
