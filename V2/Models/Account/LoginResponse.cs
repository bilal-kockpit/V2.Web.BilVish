using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace V2.Models.Account
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public int UserId { get; set; }
        public string RoleName { get; set; }
        public string EmailId { get; set; }
    }
}
