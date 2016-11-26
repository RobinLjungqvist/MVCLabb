using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace MVCLabb.Utilities
{
    public static class Helpers
    {
        public static bool DoesUserOwnImage(this IIdentity identity, int imageId)
        {
            return false;
        }
        public static string GetSid(this IIdentity identity)
        {
            var ident = (ClaimsIdentity)identity;
            var claim = ident.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid);

            var sid = claim.Value;

            return (sid != null) ? claim.Value : string.Empty;
        }

    }
}