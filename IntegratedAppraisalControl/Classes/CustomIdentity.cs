using IntegratedAppraisalControl.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Classes
{
    public static class CustomIdentity
    {
        public static List<Claim> GetClaimsPrincipal(TblUsersDTO tbl)
        {
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, tbl.UserName),
                        new Claim(CustomClaimTypes.FirstName, tbl.FirstName),
                        new Claim(CustomClaimTypes.LastName, tbl.LastName),
                        new Claim(CustomClaimTypes.UserId, tbl.UserId.ToString()),
                        new Claim(ClaimTypes.Role, CustomClaimTypes.Guest) /*Default role*/
                    };

            if (Convert.ToBoolean(tbl.ReadOnly))
            {
                claims.Add(new Claim(ClaimTypes.Role, CustomClaimTypes.ReadOnly));
                claims.Add(new Claim(CustomClaimTypes.ReadOnly, Convert.ToBoolean(tbl.ReadOnly).ToString()));
            }
            if (Convert.ToBoolean(tbl.SuperAdmin))
            {
                claims.Add(new Claim(ClaimTypes.Role, CustomClaimTypes.SuperAdmin));
                claims.Add(new Claim(CustomClaimTypes.SuperAdmin, Convert.ToBoolean(tbl.ReadOnly).ToString()));
            }
            if (Convert.ToBoolean(tbl.ClientAdmin))
            {
                claims.Add(new Claim(ClaimTypes.Role, CustomClaimTypes.ClientAdmin));
                claims.Add(new Claim(CustomClaimTypes.ClientAdmin, Convert.ToBoolean(tbl.ReadOnly).ToString()));
            }

            return claims;
        }
    }
}
