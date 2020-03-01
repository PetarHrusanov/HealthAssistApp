using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthAssistApp.Data.Migrations
{
    public partial class InitialModelsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Allergies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Milk = table.Column<bool>(nullable: false),
                    Eggs = table.Column<bool>(nullable: false),
                    Fish = table.Column<bool>(nullable: false),
                    Crustacean = table.Column<bool>(nullable: false),
                    TreeNuts = table.Column<bool>(nullable: false),
                    Peanuts = table.Column<bool>(nullable: false),
                    Wheat = table.Column<bool>(nullable: false),
                    Soybeans = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BodySystems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodySystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodRegimens",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodRegimens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HealthParameters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    Height = table.Column<decimal>(nullable: false),
                    BodyMassIndex = table.Column<decimal>(nullable: false),
                    WaterPerDay = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthParameters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Calories = table.Column<int>(nullable: false),
                    GlycemicIndex = table.Column<int>(nullable: false),
                    Allergen = table.Column<int>(nullable: false),
                    Vegan = table.Column<bool>(nullable: false),
                    Vegetarian = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Symptoms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    BodySystemId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symptoms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Symptoms_BodySystems_BodySystemId",
                        column: x => x.BodySystemId,
                        principalTable: "BodySystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    FoodRegimenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meals_FoodRegimens_FoodRegimenId",
                        column: x => x.FoodRegimenId,
                        principalTable: "FoodRegimens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HealthDosiers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    HealthParametersId = table.Column<int>(nullable: true),
                    FoodRegimenId = table.Column<string>(nullable: true),
                    Smoker = table.Column<bool>(nullable: false),
                    AllergiesId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthDosiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthDosiers_Allergies_AllergiesId",
                        column: x => x.AllergiesId,
                        principalTable: "Allergies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HealthDosiers_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HealthDosiers_FoodRegimens_FoodRegimenId",
                        column: x => x.FoodRegimenId,
                        principalTable: "FoodRegimens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HealthDosiers_HealthParameters_HealthParametersId",
                        column: x => x.HealthParametersId,
                        principalTable: "HealthParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    InstructionForPreparation = table.Column<string>(nullable: true),
                    Vegan = table.Column<bool>(nullable: false),
                    Vegetarian = table.Column<bool>(nullable: false),
                    GlycemicIndex = table.Column<int>(nullable: false),
                    Calories = table.Column<int>(nullable: false),
                    MealId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Diseases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Advice = table.Column<string>(nullable: true),
                    GlycemicIndex = table.Column<int>(nullable: true),
                    HealthDosierId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diseases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diseases_HealthDosiers_HealthDosierId",
                        column: x => x.HealthDosierId,
                        principalTable: "HealthDosiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Instructions = table.Column<string>(nullable: true),
                    ExerciseComplexity = table.Column<int>(nullable: false),
                    HealthDosierId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_HealthDosiers_HealthDosierId",
                        column: x => x.HealthDosierId,
                        principalTable: "HealthDosiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredients",
                columns: table => new
                {
                    RecipeId = table.Column<int>(nullable: false),
                    IngredientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredients", x => new { x.RecipeId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_Ingredient_RecipeIngredients",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recipe_RecipeIngredients",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiseaseSymptoms",
                columns: table => new
                {
                    DiseaseId = table.Column<int>(nullable: false),
                    SymptomId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseaseSymptoms", x => new { x.DiseaseId, x.SymptomId });
                    table.ForeignKey(
                        name: "FK_Disease_DiseaseSymptoms",
                        column: x => x.DiseaseId,
                        principalTable: "Diseases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Symptom_DiseaseSymptoms",
                        column: x => x.SymptomId,
                        principalTable: "Symptoms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Diseases_HealthDosierId",
                table: "Diseases",
                column: "HealthDosierId");

            migrationBuilder.CreateIndex(
                name: "IX_Diseases_IsDeleted",
                table: "Diseases",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_DiseaseSymptoms_SymptomId",
                table: "DiseaseSymptoms",
                column: "SymptomId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_HealthDosierId",
                table: "Exercises",
                column: "HealthDosierId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthDosiers_AllergiesId",
                table: "HealthDosiers",
                column: "AllergiesId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthDosiers_ApplicationUserId",
                table: "HealthDosiers",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthDosiers_FoodRegimenId",
                table: "HealthDosiers",
                column: "FoodRegimenId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthDosiers_HealthParametersId",
                table: "HealthDosiers",
                column: "HealthParametersId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthDosiers_IsDeleted",
                table: "HealthDosiers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_HealthParameters_IsDeleted",
                table: "HealthParameters",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_FoodRegimenId",
                table: "Meals",
                column: "FoodRegimenId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_IngredientId",
                table: "RecipeIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_MealId",
                table: "Recipes",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_Symptoms_BodySystemId",
                table: "Symptoms",
                column: "BodySystemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiseaseSymptoms");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "Diseases");

            migrationBuilder.DropTable(
                name: "Symptoms");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "HealthDosiers");

            migrationBuilder.DropTable(
                name: "BodySystems");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "Allergies");

            migrationBuilder.DropTable(
                name: "HealthParameters");

            migrationBuilder.DropTable(
                name: "FoodRegimens");
        }
    }
}
