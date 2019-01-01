using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsCookApp.Models
{
    public class Category
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public object NoOfRecipes { get; set; }
    }

    public class CategoryResponse : BaseResponseModel
    {
        public List<Category> Categories { get; set; }
    }
}