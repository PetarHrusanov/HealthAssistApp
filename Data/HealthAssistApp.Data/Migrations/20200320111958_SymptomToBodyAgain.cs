using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthAssistApp.Data.Migrations
{
    public partial class SymptomToBodyAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BodySystemId",
                table: "Symptoms",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BodySystemId",
                table: "Symptoms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
