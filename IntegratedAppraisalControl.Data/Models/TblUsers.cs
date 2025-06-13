using System;
using System.Collections.Generic;

namespace IntegratedAppraisalControl.Data.Models
{
    public partial class TblUsers
    {
        public int UserId { get; set; }
        public int? ClientId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? SuperAdmin { get; set; }
        public bool? ClientAdmin { get; set; }
        public bool? ReadOnly { get; set; }
        public bool? MarkForDeletion { get; set; }
        public bool? Deleted { get; set; }
    }
}
