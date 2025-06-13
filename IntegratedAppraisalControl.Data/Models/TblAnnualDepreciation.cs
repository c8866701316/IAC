using System;
using System.Collections.Generic;

namespace IntegratedAppraisalControl.Data.Models
{
    public partial class TblAnnualDepreciation
    {
        public int AnnualDepreciationId { get; set; }
        public string AnnualDepreciationText { get; set; }
        public bool? Active { get; set; }
    }
}
