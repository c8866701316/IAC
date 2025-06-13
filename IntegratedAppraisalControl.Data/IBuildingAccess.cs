using IntegratedAppraisalControl.Data.Models;
using IntegratedAppraisalControl.Models;
using IntegratedAppraisalControl.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Data
{
    public interface IBuildingAccess
    {
        Task<List<TblBuildings>> GetBuildingListBySearchCriteriaAsync(BuildingSearchCriteriaModel criteria);
        Task<List<TblBuildingsDTO>> GetBuildingListDDlBySearchCriteriaAsync(BuildingSearchCriteriaModel criteria);
        Task<TblBuildingsDTO> GetBuildingDetailsAsync(BuildingSearchCriteriaModel criteria);
        Task<TblBuildings> AddUpdateBuilding(TblBuildings tblBuildings);
        Task<List<TblBuildingImages>> GetBuildingImagesSearchCriteriaAsync(BuildingSearchCriteriaModel criteria);
        Task<List<TblBuildingSectionPricing>> GetBuildingSectionPricingAsync(BuildingSearchCriteriaModel criteria);
        Task<List<TblBuildingAdditionalData>> GetBuildingAdditionalDataAsync(BuildingSearchCriteriaModel criteria);
        Task<List<TblBuildingChangesDTO>> GetBuildingChangesAsync(BuildingSearchCriteriaModel criteria);
        Task<List<TblBuildingMaintenanceDTO>> GetBuildingMaintenanceAsync(BuildingSearchCriteriaModel criteria);
        Task<TblBuildingMaintenanceDTO> AddUpdateBuildingMaintenanceAsync(TblBuildingMaintenance tblBuildingMaintenance);
        Task<TblBuildingChangesDTO> AddUpdateBuildingChangesAsync(TblBuildingChanges tblBuildingChanges);
        Task<bool> CheckBuildingCodeExistance(BuildingCodeSearchCriteria criteria);
        Task<TblBuildingChangesDTO> GetBuildingChangeAsync(BuildingSearchCriteriaModel criteria);
        Task<TblBuildingMaintenanceDTO> GetBuildingMaintenanceByIdAsync(BuildingSearchCriteriaModel criteria);

        Task<bool> DeleteBuildingChanges(int BuildingChangesId);
        Task<bool> DeleteBuildingMaintanance(int BuildingMaintenanceId);
    }
}
