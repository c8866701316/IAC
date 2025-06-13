using System;
using System.Collections.Generic;

namespace IntegratedAppraisalControl.Models
{
    public partial class TblBuildingAdditionalData
    {
        public int BuildingAdditionalDataId { get; set; }
        public int BuildingId { get; set; }
        public string AdditionalDataName { get; set; }
        public string AdditionalDataDescription { get; set; }
        public bool Deleted { get; set; }

        public TblBuildings Building { get; set; }
    }
}
