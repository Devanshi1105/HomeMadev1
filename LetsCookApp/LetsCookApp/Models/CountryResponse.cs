using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsCookApp.Models
{
  
 public class Country
    {
        public string name { get; set; }
        public string code { get; set; }
    }

    public class CountryResponse: BaseResponseModel
    {
        public List<Country> country { get; set; }
    }
}
