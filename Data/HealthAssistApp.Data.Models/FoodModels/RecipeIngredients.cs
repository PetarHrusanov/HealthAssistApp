namespace HealthAssistApp.Data.Models
{
    public class RecipeIngredients
    {

        //To Do
        //Foreign Key i Model Creating da si napravq

        public int RecipeId { get; set; }
        public virtual Recipe Recipe {get;set;}

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

    }
}