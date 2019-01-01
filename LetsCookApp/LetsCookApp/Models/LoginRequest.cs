using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsCookApp.Models
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class GetProfileRequest
    {
        public string EmailId { get; set; }
        public string UserId { get; set; }
    }

    public class LoginResponse: BaseResponseModel
    {
        public UserData UserData { get; set; }
    }
    public class UserData
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
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
        public string PhotoURL { get; set; }
        public string DateOfBirth { get; set; }
        public string AboutMe { get; set; }
    }

    public class BaseResponseModel
    {
        public virtual string Message { get; set; }
        public virtual int StatusCode { get; set; }
        public virtual bool Success { get; set; }
    }


}
