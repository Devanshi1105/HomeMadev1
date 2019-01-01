using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsCookApp.Models
{
   
    public class SaveFavRecipeRequest
    {
        public int Recipe_Id { get; set; }
        public string Favorite { get; set; }
        public int Member_Id { get; set; }
        public string Comments { get; set; }
    }


}
