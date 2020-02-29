namespace HealthAssistApp.Data.Models
{
    public class RecipeIngredients
    {

        public int RecipeId { get; set; }
        public virtual Recipe Recipe {get;set;}

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

    }
}