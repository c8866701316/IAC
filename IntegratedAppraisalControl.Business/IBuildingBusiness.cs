using IntegratedAppraisalControl.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IntegratedAppraisalControl.Models;

namespace IntegratedAppraisalControl.Business
{
    public interface IBuildingBusiness
    {
        Task<List<TblBuildingsDTO>> GetBuildingListBySearchCriteriaAsync(BuildingSearchCriteriaModel criteria);
        Task<List<TblBuildingsDTO>> GetBuildingListDDlBySearchCriteriaAsync(BuildingSearchCriteriaModel criteria);
        Task<TblBuildingsDTO> GetBuildingDetailsAsync(BuildingSearchCriteriaModel criteria);
        Task<TblBuildingsDTO> AddUpdateBuilding(TblBuildingsDTO tblBuildingsDTO);
        Task<List<TblBuildingImagesDTO>> GetBuildingImagesSearchCriteriaAsync(BuildingSearchCriteriaModel criteria);
        Task<List<TblBuildingSectionPricingDTO>> GetBuildingSectionPricingAsync(BuildingSearchCriteriaModel criteria);
        Task<List<TblBuildingAdditionalDataDTO>> GetBuildingAdditionalDataAsync(BuildingSearchCriteriaModel criteria);
        Task<List<TblBuildingChangesDTO>> GetBuildingChangesAsync(BuildingSearchCriteriaModel criteria);
        Task<List<TblBuildingMaintenanceDTO>> GetBuildingMaintenanceAsync(BuildingSearchCriteriaModel criteria);
        Task<TblBuildingChangesDTO> AddUpdateBuildingChangesAsync(TblBuildingChangesDTO tblBuildingChangesDTO);
        Task<TblBuildingMaintenanceDTO> AddUpdateBuildingMaintenanceAsync(TblBuildingMaintenanceDTO tblBuildingMaintenanceDTO);
        Task<bool> CheckBuildingCodeExistance(BuildingCodeSearchCriteria criteria);
        Task<TblBuildingChangesDTO> GetBuildingChangeAsync(BuildingSearchCriteriaModel criteria);
        Task<TblBuildingMaintenanceDTO> GetBuildingMaintenanceByIdAsync(BuildingSearchCriteriaModel criteria);

        Task<bool> DeleteBuildingChanges(int BuildingChangesId);
        Task<bool> DeleteBuildingMaintanance(int BuildingMaintenanceId);
    }
}
