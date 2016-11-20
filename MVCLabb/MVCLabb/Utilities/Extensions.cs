using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace MVCLabb.Utilities
{
    public static class Extensions
    {
        public static string GetSid(IIdentity identity)
        {
            var ident = (ClaimsIdentity)identity;
            var claim = ident.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid);

            var sid = claim.Value;

            return (sid != null) ? claim.Value : string.Empty;
        }

    }
}