using IntegratedAppraisalControl.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegratedAppraisalControl.Models
{
    public class InventoryViewModel
    {
        public TblInventoryDTO Inventory { get; set; }
        public List<TblBuildingsDTO> listBuildings { get; set; }
        public List<TblRoomsDTO> lstRooms { get; set; }
        public List<TblDepartmentsDTO> lstDepartments { get; set; }
        public List<TblAssetClassDTO> lstAssetClass { get; set; }
        public List<TblChangeTypeDTO> lstChangesTypeDTO { get; set; }
        public TblInventoryMaintenanceDTO objtblInventoryMaintenanceDTO { get; set; }
        public bool IsDuplicate { get; set; }

    }
}
