using AutoMapper;
using IntegratedAppraisalControl.Data;
using IntegratedAppraisalControl.Data.Models;
using IntegratedAppraisalControl.Models;
using IntegratedAppraisalControl.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Business
{
    public class BuildingBusiness : IBuildingBusiness
    {
        private readonly IBuildingAccess _buildingAccess;
        private readonly IMapper _mapper;
        public BuildingBusiness(IBuildingAccess buildingAccess, IMapper mapper)
        {
            _buildingAccess = buildingAccess;
            _mapper = mapper;
        }
        public async Task<List<TblBuildingsDTO>> GetBuildingListBySearchCriteriaAsync(BuildingSearchCriteriaModel criteria)
        {
            if (criteria == null)
            {
                criteria = new BuildingSearchCriteriaModel();
            }
            var results = await _buildingAccess.GetBuildingListBySearchCriteriaAsync(criteria);
            return _mapper.Map<List<TblBuildingsDTO>>(results);
        }

        public async Task<List<TblBuildingsDTO>> GetBuildingListDDlBySearchCriteriaAsync(BuildingSearchCriteriaModel criteria)
        {
            if (criteria == null)
            {
                criteria = new BuildingSearchCriteriaModel();
            }
            return await _buildingAccess.GetBuildingListDDlBySearchCriteriaAsync(criteria);
        }

        public async Task<List<TblBuildingImagesDTO>> GetBuildingImagesSearchCriteriaAsync(BuildingSearchCriteriaModel criteria)
        {
            var results = await _buildingAccess.GetBuildingImagesSearchCriteriaAsync(criteria);
            return _mapper.Map<List<TblBuildingImagesDTO>>(results);
        }

        public async Task<TblBuildingsDTO> GetBuildingDetailsAsync(BuildingSearchCriteriaModel criteria)
        {
            if (criteria == null)
            {
                criteria = new BuildingSearchCriteriaModel();
            }
            return await _buildingAccess.GetBuildingDetailsAsync(criteria);
        }

        public async Task<TblBuildingsDTO> AddUpdateBuilding(TblBuildingsDTO tblBuildingsDTO)
        {
            var results = await _buildingAccess.AddUpdateBuilding(_mapper.Map<TblBuildings>(tblBuildingsDTO));
            return _mapper.Map<TblBuildingsDTO>(results);
        }
        public async Task<bool> CheckBuildingCodeExistance(BuildingCodeSearchCriteria criteria)
        {
            return await _buildingAccess.CheckBuildingCodeExistance(criteria);
        }

        public async Task<List<TblBuildingSectionPricingDTO>> GetBuildingSectionPricingAsync(BuildingSearchCriteriaModel criteria)
        {
            var results = await _buildingAccess.GetBuildingSectionPricingAsync(criteria);
            return _mapper.Map<List<TblBuildingSectionPricingDTO>>(results);
        }
        public async Task<List<TblBuildingAdditionalDataDTO>> GetBuildingAdditionalDataAsync(BuildingSearchCriteriaModel criteria)
        {
            var results = await _buildingAccess.GetBuildingAdditionalDataAsync(criteria);
            return _mapper.Map<List<TblBuildingAdditionalDataDTO>>(results);
        }
        public async Task<List<TblBuildingChangesDTO>> GetBuildingChangesAsync(BuildingSearchCriteriaModel criteria)
        {
            return await _buildingAccess.GetBuildingChangesAsync(criteria);
        }
        public async Task<List<TblBuildingMaintenanceDTO>> GetBuildingMaintenanceAsync(BuildingSearchCriteriaModel criteria)
        {
            return await _buildingAccess.GetBuildingMaintenanceAsync(criteria);
        }
        public async Task<TblBuildingChangesDTO> AddUpdateBuildingChangesAsync(TblBuildingChangesDTO tblBuildingChangesDTO)
        {
            return await _buildingAccess.AddUpdateBuildingChangesAsync(_mapper.Map<TblBuildingChanges>(tblBuildingChangesDTO));
        }
        public async Task<TblBuildingChangesDTO> GetBuildingChangeAsync(BuildingSearchCriteriaModel criteria)
        {
            return await _buildingAccess.GetBuildingChangeAsync(criteria);
        }
        public async Task<TblBuildingMaintenanceDTO> AddUpdateBuildingMaintenanceAsync(TblBuildingMaintenanceDTO tblBuildingMaintenanceDTO)
        {
            return await _buildingAccess.AddUpdateBuildingMaintenanceAsync(_mapper.Map<TblBuildingMaintenance>(tblBuildingMaintenanceDTO));
        }
        public async Task<TblBuildingMaintenanceDTO> GetBuildingMaintenanceByIdAsync(BuildingSearchCriteriaModel criteria)
        {
            return await _buildingAccess.GetBuildingMaintenanceByIdAsync(criteria);
        }

        public async Task<bool> DeleteBuildingChanges(int BuildingChangesId)
        {
            return await _buildingAccess.DeleteBuildingChanges(BuildingChangesId);
        }
        public async Task<bool> DeleteBuildingMaintanance(int BuildingMaintenanceId)
        {
            return await _buildingAccess.DeleteBuildingMaintanance(BuildingMaintenanceId);
        }

    }
}