using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Models.DTO
{
    public class TblBuildingChangesDTO
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
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
        public string ChangeType { get; set; }
    }

}
