using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsCookApp.Models
{


    public class Recipe
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Serving { get; set; }
        public object Cooking_time { get; set; }
        public string Prep_time { get; set; }
        public string Ingredient_count { get; set; }
        public string Image { get; set; }
        public string ImageUrl { get; set; }
    }

    public class SubCategoryResponse : BaseResponseModel
    {
        public List<Recipe> Recipes { get; set; }
    }
}