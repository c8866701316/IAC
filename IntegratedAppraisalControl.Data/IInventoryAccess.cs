using IntegratedAppraisalControl.Data.Models;
using IntegratedAppraisalControl.Models;
using IntegratedAppraisalControl.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Data
{
    public interface IInventoryAccess
    {
        Task<TblInventoryDTO> GetInventory(InventorySearchCriteria criteria);
        Task<List<TblInventoryDTO>> GetInventoryList(InventorySearchCriteria criteria);
        Task<List<TblRooms>> GetRoomsList(InventorySearchCriteria criteria);
        Task<List<TblDepartments>> GetDepartmentList(InventorySearchCriteria criteria);
        Task<List<TblInventoryMaintenanceDTO>> GetInventoryMaintanance(InventorySearchCriteria criteria);
        Task<List<TblAssetClass>> GetAssetClassList(InventorySearchCriteria criteria);
        Task<TblInventory> AddUpdateInventory(TblInventory tblInventory);
        Task<bool> CheckInventoryTagExistance(InventorySearchCriteria criteria);
        Task<bool> AddUpdateInventoryMaintenanceAsync(TblInventoryMaintenance tblInventoryMaintenance);

        Task<List<TblRoomsDTO>> GetRoomsListDDL(InventorySearchCriteria criteria);
        Task<List<TblDepartmentsDTO>> GetDepartmentListDDL(InventorySearchCriteria criteria);
        Task<List<TblAssetClassDTO>> GetAssetClassListDDL(InventorySearchCriteria criteria);
        Task<bool> DeleteInventoryMaintenance(int InventoryMaintenanceId);
    }
}