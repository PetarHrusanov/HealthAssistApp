using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthAssistApp.Data.Migrations
{
    public partial class ForeignKeysToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HealthDosierDiseases",
                table: "HealthDosierDiseases");

            migrationBuilder.DropIndex(
                name: "IX_HealthDosierDiseases_HealthDosierId",
                table: "HealthDosierDiseases");

            migrationBuilder.AddColumn<int>(
                name: "ExerciseComplexity",
                table: "WorkoutPrograms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "HealthParameters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Allergies",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HealthDosierDiseases",
                table: "HealthDosierDiseases",
                columns: new[] { "HealthDosierId", "DiseaseId" });

            migrationBuilder.CreateIndex(
                name: "IX_HealthParameters_ApplicationUserId",
                table: "HealthParameters",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthDosierDiseases_DiseaseId",
                table: "HealthDosierDiseases",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Allergies_ApplicationUserId",
                table: "Allergies",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Allergies_AspNetUsers_ApplicationUserId",
                table: "Allergies",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthParameters_AspNetUsers_ApplicationUserId",
                table: "HealthParameters",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allergies_AspNetUsers_ApplicationUserId",
                table: "Allergies");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthParameters_AspNetUsers_ApplicationUserId",
                table: "HealthParameters");

            migrationBuilder.DropIndex(
                name: "IX_HealthParameters_ApplicationUserId",
                table: "HealthParameters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HealthDosierDiseases",
                table: "HealthDosierDiseases");

            migrationBuilder.DropIndex(
                name: "IX_HealthDosierDiseases_DiseaseId",
                table: "HealthDosierDiseases");

            migrationBuilder.DropIndex(
                name: "IX_Allergies_ApplicationUserId",
                table: "Allergies");

            migrationBuilder.DropColumn(
                name: "ExerciseComplexity",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "HealthParameters");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Allergies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HealthDosierDiseases",
                table: "HealthDosierDiseases",
                columns: new[] { "DiseaseId", "HealthDosierId" });

            migrationBuilder.CreateIndex(
                name: "IX_HealthDosierDiseases_HealthDosierId",
                table: "HealthDosierDiseases",
                column: "HealthDosierId");
        }
    }
}
