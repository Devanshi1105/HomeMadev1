using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsCookApp.Models
{
   
    public class PopularRecipe
    {
        public string id { get; set; }
        public string name { get; set; }
        public string Image { get; set; }
        public string Likes { get; set; }
        public string Rating { get; set; }
        public string Shares { get; set; }
    }

    public class PopularRecipeResponse : BaseResponseModel 
    { 
        public List<PopularRecipe> PopularRecipes { get; set; }
    }
}
