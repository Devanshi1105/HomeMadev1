using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsCookApp.Models
{
    public class GetShoppingListByUserIdRequest
    {
        public int UserId { get; set; }
    }
   

public class ShoppingList
    {
        public string RecipeId { get; set; }
        public string IngredientId { get; set; }
        public string Ingredient { get; set; }
        public string UserId { get; set; }
    }

    public class GetShoppingListByUserIdResponse:BaseResponseModel
    { 
        public List<ShoppingList> ShoppingList { get; set; }
    }
}
