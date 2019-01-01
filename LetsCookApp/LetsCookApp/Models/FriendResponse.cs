using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsCookApp.Models
{
    
    public class FriendsData
    {
        public string UserId { get; set; }
        public string FriendId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
    }
    public class FriendRequest
    { 
        public int UserId { get; set; }
    }

    public class FriendResponse : BaseResponseModel
    {
        public List<FriendsData> FriendsData { get; set; }
    }
}
