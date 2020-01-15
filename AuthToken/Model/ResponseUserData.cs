using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthToken.Model
{
    public class ResponseUserData
    {
        public string Token { get; set; }
        public bool Authenticated { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
    }
}