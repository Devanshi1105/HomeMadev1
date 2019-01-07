using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsCookApp.Models
{
    public class GetFavsByUserIdRequest
    {
        public string UserId { get; set; }
       

    }


    public class FavouriteRecipe
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string MemberId { get; set; }
        public string Serving { get; set; }
        public string CookingTime { get; set; }
        public string PrepTime { get; set; }
        public string RestingTime { get; set; }
        public string TotalTime { get; set; }
        public string Calories { get; set; }
        public string IngredientCount { get; set; }
        public string Image { get; set; }

    }


    public class GetFavsByUserIdResponse:BaseResponseModel
    {
        public List<FavouriteRecipe> FavouriteRecipes { get; set; }
    }
}
