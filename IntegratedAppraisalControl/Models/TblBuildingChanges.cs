using System;
using System.Collections.Generic;

namespace IntegratedAppraisalControl.Models
{
    public partial class TblBuildingChanges
    {
        public int BuildingChangesId { get; set; }
        public int? BuildingId { get; set; }
        public int? ChangeTypeId { get; set; }
        public string Description { get; set; }
        public int? MonthAcq { get; set; }
        public int? YearAcq { get; set; }
        public int? Cost { get; set; }
        public int? Auser { get; set; }
        public DateTime? AdateTime { get; set; }
        public int? Cuser { get; set; }
        public DateTime? CdateTime { get; set; }
    }
}
