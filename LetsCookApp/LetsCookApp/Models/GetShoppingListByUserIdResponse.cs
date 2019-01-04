using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsCookApp.Models
{
    public class GroupedIngredientDetailModel : ObservableCollection<IngredientDetail>
    {
        public string LongName { get; set; }
        public string ShortName { get; set; }
    }

    public class IngredientDetail
    {
        public string IngredientId { get; set; }
        public string IngredientName { get; set; }
    }

    public class RecipeDetails
    {
        public string RecipeId { get; set; }
        public string RecipeTitle { get; set; }
        public string UserId { get; set; }
        public string Serving { get; set; }
        public string TotalTime { get; set; }
        public List<IngredientDetail> IngredientDetails { get; set; }
    }

    public class ShoppingList
    {
        public RecipeDetails recipeDetails { get; set; }
    }


    public class GetShoppingListByUserIdResponse : BaseResponseModel
    {
        public List<ShoppingList> ShoppingList { get; set; }
    } 


    public class GetShoppingListByUserIdRequest
    {
        public int UserId { get; set; }
    }
}