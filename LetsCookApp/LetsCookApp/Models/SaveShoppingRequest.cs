using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsCookApp.Models
{
    public class SaveShoppingRequest
    {
        public int Recipe_Id { get; set; }
        public int Ingredient_Id { get; set; }
        public int Member_Id { get; set; }
    }
}
