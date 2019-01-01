using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsCookApp.Models
{
   
    public class Ingredient
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string Unit { get; set; }
        public string Ingredientfor { get; set; }
    } 

    public class RecipeDishView
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public string Serving { get; set; }
        public object Cooking_time { get; set; }
        public string Prep_time { get; set; }
        public string Total_time { get; set; }
        public string Ingredient_count { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public string Preparation { get; set; }
    }

    public class DishViewResponse
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public RecipeDishView Recipe { get; set; }
    }
}
