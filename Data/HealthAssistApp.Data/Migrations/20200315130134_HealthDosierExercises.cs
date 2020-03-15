using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthAssistApp.Data.Migrations
{
    public partial class HealthDosierExercises : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthDosiers_FoodRegimens_FoodRegimenId1",
                table: "HealthDosiers");

            migrationBuilder.DropIndex(
                name: "IX_HealthDosiers_FoodRegimenId1",
                table: "HealthDosiers");

            migrationBuilder.DropColumn(
                name: "FoodRegimenId1",
                table: "HealthDosiers");

            migrationBuilder.AlterColumn<string>(
                name: "FoodRegimenId",
                table: "HealthDosiers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_HealthDosiers_FoodRegimenId",
                table: "HealthDosiers",
                column: "FoodRegimenId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthDosiers_FoodRegimens_FoodRegimenId",
                table: "HealthDosiers",
                column: "FoodRegimenId",
                principalTable: "FoodRegimens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthDosiers_FoodRegimens_FoodRegimenId",
                table: "HealthDosiers");

            migrationBuilder.DropIndex(
                name: "IX_HealthDosiers_FoodRegimenId",
                table: "HealthDosiers");

            migrationBuilder.AlterColumn<int>(
                name: "FoodRegimenId",
                table: "HealthDosiers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FoodRegimenId1",
                table: "HealthDosiers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HealthDosiers_FoodRegimenId1",
                table: "HealthDosiers",
                column: "FoodRegimenId1");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthDosiers_FoodRegimens_FoodRegimenId1",
                table: "HealthDosiers",
                column: "FoodRegimenId1",
                principalTable: "FoodRegimens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
