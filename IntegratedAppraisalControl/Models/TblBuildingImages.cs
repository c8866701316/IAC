using System;
using System.Collections.Generic;

namespace IntegratedAppraisalControl.Models
{
    public partial class TblBuildingImages
    {
        public int BuildingImageId { get; set; }
        public int BuildingId { get; set; }
        public string ImageName { get; set; }
        public string ImageDescription { get; set; }
        public bool MainPhoto { get; set; }
        public bool MainPict { get; set; }
        public bool? Plat { get; set; }

        public TblBuildings Building { get; set; }
    }
}
