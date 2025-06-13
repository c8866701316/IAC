using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Classes
{
    public static class IdentityExtensions
    {
        public static string GetFirstName(this IIdentity identity)
        {
            return GetClaimValue(identity, CustomClaimTypes.FirstName);
        }
        public static string GetLastName(this IIdentity identity)
        {
            return GetClaimValue(identity, CustomClaimTypes.LastName);
        }
        public static bool GetSuperAdmin(this IIdentity identity)
        {
            return Convert.ToBoolean(GetClaimValue(identity, CustomClaimTypes.SuperAdmin) == "" ? "False" : "True");
        }
        public static bool GetClientAdmin(this IIdentity identity)
        {
            return Convert.ToBoolean(GetClaimValue(identity, CustomClaimTypes.ClientAdmin) == "" ? "False" : "True");
        }
        public static int GetUserId(this IIdentity identity)
        {
            return GetClaimValue(identity, CustomClaimTypes.UserId) == "" ? 0 : Convert.ToInt32(GetClaimValue(identity, CustomClaimTypes.UserId));
        }
        public static int GetClientId(this IIdentity identity)
        {
            return GetClaimValue(identity, CustomClaimTypes.ClientId) == "" ? 0 : Convert.ToInt32(GetClaimValue(identity, CustomClaimTypes.ClientId));
        }
        public static bool GetReadOnly(this IIdentity identity)
        {
            return Convert.ToBoolean(GetClaimValue(identity, CustomClaimTypes.ReadOnly) == "" ? "False" : "True");
        }

        public static bool GetIsLocationChangeAllowed(this IIdentity identity)
        {
            return Convert.ToBoolean((GetClaimValue(identity, CustomClaimTypes.IsLocationChangeAllowed) == "" || GetClaimValue(identity, CustomClaimTypes.IsLocationChangeAllowed) == "False") ? "False" : "True");
        }

        public static string GetClientFileName(this IIdentity identity)
        {
            return GetClaimValue(identity, CustomClaimTypes.ClientFileName);
        }
        public static string GetClientName(this IIdentity identity)
        {
            return GetClaimValue(identity, CustomClaimTypes.ClientName);
        }

        private static string GetClaimValue(this IIdentity identity,string type)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(type);

            if (claim == null)
                return "";

            return claim.Value;
        }

    }
}
