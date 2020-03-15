using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthAssistApp.Data.Migrations
{
    public partial class HealthDosierDisease : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diseases_HealthDosiers_HealthDosierId",
                table: "Diseases");

            migrationBuilder.DropIndex(
                name: "IX_Diseases_HealthDosierId",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "HealthDosierId",
                table: "Diseases");

            migrationBuilder.CreateTable(
                name: "HealthDosierDiseases",
                columns: table => new
                {
                    HealthDosierId = table.Column<string>(nullable: false),
                    DiseaseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthDosierDiseases", x => new { x.DiseaseId, x.HealthDosierId });
                    table.ForeignKey(
                        name: "FK_Disease_HealthDosierDisease",
                        column: x => x.DiseaseId,
                        principalTable: "Diseases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Health_HealthDosierDisease",
                        column: x => x.HealthDosierId,
                        principalTable: "HealthDosiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HealthDosierDiseases_HealthDosierId",
                table: "HealthDosierDiseases",
                column: "HealthDosierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HealthDosierDiseases");

            migrationBuilder.AddColumn<string>(
                name: "HealthDosierId",
                table: "Diseases",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Diseases_HealthDosierId",
                table: "Diseases",
                column: "HealthDosierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diseases_HealthDosiers_HealthDosierId",
                table: "Diseases",
                column: "HealthDosierId",
                principalTable: "HealthDosiers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
