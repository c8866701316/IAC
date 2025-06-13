using System;

namespace IntegratedAppraisalControl.Models.DTO
{
    public class TblInventoryMaintenanceDTO
    {
        public int InventoryMaintenanceId { get; set; }
        public int? InventoryId { get; set; }
        public int? ChangeTypeId { get; set; }
        public string Description { get; set; }
        public int? MonthAcq { get; set; }
        public int? YearAcq { get; set; }
        public int? Cost { get; set; }
        public int? Auser { get; set; }
        public DateTime? AdateTime { get; set; }
        public int? Cuser { get; set; }
        public DateTime? CdateTime { get; set; }
        
        /*Extra fields*/
        public string ChangeType { get; set; }
    }
}