using System;
using System.Collections.Generic;

namespace IntegratedAppraisalControl.Data.Models
{
    public partial class TblBuildingSectionPricing
    {
        public int BuildingSectionId { get; set; }
        public int? BuildingId { get; set; }
        public string SectionDesc { get; set; }
        public int? Yracq { get; set; }
        public int? Cost { get; set; }
        public int? HistoricalCost { get; set; }
        public byte? PercentOf { get; set; }
        public short? Isoclass { get; set; }
        public short? Life { get; set; }
    }
}
