using System;
using System.Collections.Generic;

namespace IntegratedAppraisalControl.Data.Models
{
    public partial class TblChangeType
    {
        public int ChangeTypeId { get; set; }
        public string ChangeType { get; set; }
        public bool? Active { get; set; }
    }
}
