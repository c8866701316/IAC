using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Models.DTO
{
    public class TblDepartmentsDTO
    {
        public int DepartmentId { get; set; }
        public int? ClientId { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public bool? MarkForDeletion { get; set; }
        public bool? Deleted { get; set; }
    }
}
