using System;
using System.Collections.Generic;

namespace IntegratedAppraisalControl.Data.Models
{
    public partial class TblClientStatus
    {
        public int ClientStatusId { get; set; }
        public string ClientStatusText { get; set; }
        public bool? Active { get; set; }
    }
}
