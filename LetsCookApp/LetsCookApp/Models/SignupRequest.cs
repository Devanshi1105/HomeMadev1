using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsCookApp.Models
{
   public class SignupRequest
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }
        public string Hobbies { get; set; }
        public string Picture { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string UserId { get; set; }
    }
    public class SignupResponse : BaseResponseModel
    {
        public virtual bool Succeeded { get; set; }


    }
}
