using System;
using System.Collections.Generic;

namespace IntegratedAppraisalControl.Models
{
    public partial class TblDepartments
    {
        public int DepartmentId { get; set; }
        public int? ClientId { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public bool? MarkForDeletion { get; set; }
        public bool? Deleted { get; set; }
    }
}
