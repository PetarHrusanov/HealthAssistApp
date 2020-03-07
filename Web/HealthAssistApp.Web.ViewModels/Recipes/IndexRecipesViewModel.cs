namespace HealthAssistApp.Web.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;

    public class IndexRecipesViewModel
    {
        public IEnumerable<RecipeViewModel> Recipes { get; set; }
    }
}
