using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthAssistApp.Data.Migrations
{
    public partial class HealthDosierRelationModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_HealthDosiers_HealthDosierId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_HealthDosierId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HealthDosierId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "HealthDosiers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HealthDosiers_ApplicationUserId",
                table: "HealthDosiers",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthDosiers_AspNetUsers_ApplicationUserId",
                table: "HealthDosiers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthDosiers_AspNetUsers_ApplicationUserId",
                table: "HealthDosiers");

            migrationBuilder.DropIndex(
                name: "IX_HealthDosiers_ApplicationUserId",
                table: "HealthDosiers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "HealthDosiers");

            migrationBuilder.AddColumn<string>(
                name: "HealthDosierId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_HealthDosierId",
                table: "AspNetUsers",
                column: "HealthDosierId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_HealthDosiers_HealthDosierId",
                table: "AspNetUsers",
                column: "HealthDosierId",
                principalTable: "HealthDosiers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
