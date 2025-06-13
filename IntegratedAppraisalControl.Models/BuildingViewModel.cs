using IntegratedAppraisalControl.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegratedAppraisalControl.Models
{
    public class BuildingViewModel
    {
        public TblBuildingsDTO tblBuildingsDTO { get; set; }
        public List<TblBuildingImagesDTO> listTblBuildingImagesDTO { get; set; }
        public List<TblChangeTypeDTO> lstChangesTypeDTO { get; set; }
    }
}
