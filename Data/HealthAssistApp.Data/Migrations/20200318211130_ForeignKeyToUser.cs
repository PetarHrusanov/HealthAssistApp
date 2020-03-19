using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthAssistApp.Data.Migrations
{
    public partial class ForeignKeyToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "WorkoutPrograms",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPrograms_ApplicationUserId",
                table: "WorkoutPrograms",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPrograms_AspNetUsers_ApplicationUserId",
                table: "WorkoutPrograms",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPrograms_AspNetUsers_ApplicationUserId",
                table: "WorkoutPrograms");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutPrograms_ApplicationUserId",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "WorkoutPrograms");
        }
    }
}
