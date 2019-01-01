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
        public string MemberId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Likes { get; set; }
        public string Rating { get; set; }
        public string Shares { get; set; }

    }


    public class GetFavsByUserIdResponse:BaseResponseModel
    { 
        public List<FavouriteRecipe> FavouriteRecipes { get; set; }
    }
}
