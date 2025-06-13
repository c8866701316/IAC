using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Models.DTO
{
    public class TblUsersDTO
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
