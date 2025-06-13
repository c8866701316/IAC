using System;
using System.Collections.Generic;

namespace IntegratedAppraisalControl.Data.Models
{
    public partial class TblFirstYearDepreciation
    {
        public int FirstYearDepreciationId { get; set; }
        public string FirstYearDepreciationText { get; set; }
        public bool? Active { get; set; }
    }
}
