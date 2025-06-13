using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Models.DTO
{
    public class TblUsersClientsDTO
    {
        public int UsersClientId { get; set; }
        public int UserId { get; set; }
        public int? ClientId { get; set; }
        public string ClientName { get; set; }
    }
}
