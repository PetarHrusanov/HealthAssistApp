namespace HealthAssistApp.Web.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Net;
    using System.Text.RegularExpressions;
    using Ganss.XSS;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Data.Models.Enums;
    using HealthAssistApp.Services.Mapping;

    public class RecipeViewModel : IMapFrom<Recipe>
    {

        public RecipeViewModel()
        {
            this.RecipeIngredients = new HashSet<RecipeIngredients>();
            this.Ingredients = new HashSet<Ingredient>();
            //this.Ingredients = new List<Ingredient>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string InstructionForPreparation { get; set; }

        [DisplayName("Instructions For Preparation")]
        public string ShortInstructionForPreparation
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.InstructionForPreparation, @"<[^>]+>", string.Empty));
                return content.Length > 300
                        ? content.Substring(0, 300) + "..."
                        : content;
            }
        }

        [DisplayName("Instructions For Preparation")]
        public string SanitizedInstructionForPreparation
            => new HtmlSanitizer().Sanitize(this.InstructionForPreparation);

        public string ImageUrl { get; set; }

        public bool Vegan { get; set; }

        public bool Vegetarian { get; set; }

        public int Calories { get; set; }

        [DisplayName("Glycemic Index")]
        public GlycemicIndex GlycemicIndex { get; set; }

        public ICollection<RecipeIngredients> RecipeIngredients { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
    }
}
