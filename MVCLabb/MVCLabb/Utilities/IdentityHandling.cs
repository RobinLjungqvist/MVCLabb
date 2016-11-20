using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace MVCLabb.Utilities
{
    public class IdentityHandling
    {
        public static int? GetUserID(ClaimsIdentity identity)
        {
            var sid = identity.Claims.First(x => x.Type == ClaimTypes.Sid);
            int? userID = int.Parse(sid.Value);

            return userID;
        }
    }
}