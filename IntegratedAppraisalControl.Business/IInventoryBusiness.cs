using IntegratedAppraisalControl.Models;
using IntegratedAppraisalControl.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Business
{
    public interface IInventoryBusiness
    {
        Task<TblInventoryDTO> GetInventory(InventorySearchCriteria criteria);
        Task<List<TblInventoryDTO>> GetInventoryList(InventorySearchCriteria criteria);
        Task<List<TblRoomsDTO>> GetRoomsList(InventorySearchCriteria criteria);
        Task<List<TblDepartmentsDTO>> GetDepartmentList(InventorySearchCriteria criteria);
        Task<List<TblInventoryMaintenanceDTO>> GetInventoryMaintanance(InventorySearchCriteria criteria);
        Task<List<TblAssetClassDTO>> GetAssetClassList(InventorySearchCriteria criteria);
        Task<TblInventoryDTO> AddUpdateInventory(TblInventoryDTO tblInventoryDTO);
        Task<bool> CheckInventoryTagExistance(InventorySearchCriteria criteria);
        Task<bool> AddInventoryMaintenanceChangesAsync(TblInventoryMaintenanceDTO tblInventoryMaintenance);

        Task<List<TblRoomsDTO>> GetRoomsListDDL(InventorySearchCriteria criteria);
        Task<List<TblDepartmentsDTO>> GetDepartmentListDDL(InventorySearchCriteria criteria);
        Task<List<TblAssetClassDTO>> GetAssetClassListDDL(InventorySearchCriteria criteria);
        Task<bool> DeleteInventoryMaintenance(int InventoryMaintenanceId);
    }
}