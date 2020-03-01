namespace HealthAssistApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RecipeIngredients
    {

        //To Do
        //Foreign Key i Model Creating da si napravq

        [Required]
        public int RecipeId { get; set; }
        public virtual Recipe Recipe {get;set;}

        [Required]
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

    }
}