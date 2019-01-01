using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsCookApp.Models
{
   

    public class ProfileResponse: BaseResponseModel
    { 
        public UserData UserData { get; set; }
    }
    
}
