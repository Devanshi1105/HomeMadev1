using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsCookApp.Models
{

    public class NewlyAddedRecipe
    {
        public string Id { get; set; }
        public string MemberId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Likes { get; set; }
        public string Rating { get; set; }
        public string Shares { get; set; }
    }

    public class NewlyAddedRecipeResponse : BaseResponseModel
    {
        public List<NewlyAddedRecipe> NewlyAddedRecipes { get; set; }
    }
}
