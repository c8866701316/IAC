using System;
using System.Collections.Generic;

namespace IntegratedAppraisalControl.Models
{
    public partial class TblUsersClients
    {
        public int UsersClientId { get; set; }
        public int UserId { get; set; }
        public int? ClientId { get; set; }
    }
}
