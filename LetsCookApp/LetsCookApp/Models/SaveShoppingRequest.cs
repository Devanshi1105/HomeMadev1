using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsCookApp.Models
{
    public class SaveShoppingRequest
    {
        public int RecipeId { get; set; }
        public List<int> IngredientIds { get; set; }
        public int MemberId { get; set; }
    }
}
