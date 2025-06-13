using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Models.DTO
{
    public class TblBuildingImagesDTO
    {
        public int BuildingImageId { get; set; }
        public int BuildingId { get; set; }
        public string ImageName { get; set; }
        public string ImageDescription { get; set; }
        public bool MainPhoto { get; set; }
        public bool MainPict { get; set; }
        public bool? Plat { get; set; }
    }
}
