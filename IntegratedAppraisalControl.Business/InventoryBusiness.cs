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
    public class InventoryBusiness : IInventoryBusiness
    {
        private readonly IInventoryAccess _inventoryAccess;
        private readonly IMapper _mapper;
        public InventoryBusiness(IInventoryAccess inventoryAccess, IMapper mapper)
        {
            _inventoryAccess = inventoryAccess;
            _mapper = mapper;
        }
        public async Task<List<TblInventoryDTO>> GetInventoryList(InventorySearchCriteria criteria)
        {
            return await _inventoryAccess.GetInventoryList(criteria);
        }
        public async Task<TblInventoryDTO> GetInventory(InventorySearchCriteria criteria)
        {
            if (criteria == null)
            {
                criteria = new InventorySearchCriteria();
            }
            return await _inventoryAccess.GetInventory(criteria);
        }

        public async Task<List<TblRoomsDTO>> GetRoomsList(InventorySearchCriteria criteria)
        {
            var results = await _inventoryAccess.GetRoomsList(criteria);
            return _mapper.Map<List<TblRoomsDTO>>(results);
        }

        public async Task<List<TblDepartmentsDTO>> GetDepartmentList(InventorySearchCriteria criteria)
        {
            var results = await _inventoryAccess.GetDepartmentList(criteria);
            return _mapper.Map<List<TblDepartmentsDTO>>(results);
        }

        public async Task<List<TblDepartmentsDTO>> GetDepartmentListDDL(InventorySearchCriteria criteria)
        {
            var results = await _inventoryAccess.GetDepartmentListDDL(criteria);
            return _mapper.Map<List<TblDepartmentsDTO>>(results);
        }
        public async Task<List<TblRoomsDTO>> GetRoomsListDDL(InventorySearchCriteria criteria)
        {
            var results = await _inventoryAccess.GetRoomsListDDL(criteria);
            return _mapper.Map<List<TblRoomsDTO>>(results);
        }
        public async Task<List<TblAssetClassDTO>> GetAssetClassListDDL(InventorySearchCriteria criteria)
        {
            var results = await _inventoryAccess.GetAssetClassListDDL(criteria);
            return _mapper.Map<List<TblAssetClassDTO>>(results);
        }

        public async Task<List<TblInventoryMaintenanceDTO>> GetInventoryMaintanance(InventorySearchCriteria criteria)
        {
            return await _inventoryAccess.GetInventoryMaintanance(criteria);
        }

        public async Task<List<TblAssetClassDTO>> GetAssetClassList(InventorySearchCriteria criteria)
        {
            var results = await _inventoryAccess.GetAssetClassList(criteria);
            return _mapper.Map<List<TblAssetClassDTO>>(results);
        }

        public async Task<TblInventoryDTO> AddUpdateInventory(TblInventoryDTO tblInventoryDTO)
        {
            var results = await _inventoryAccess.AddUpdateInventory(_mapper.Map<TblInventory>(tblInventoryDTO));
            return _mapper.Map<TblInventoryDTO>(results);
        }
        public async Task<bool> CheckInventoryTagExistance(InventorySearchCriteria criteria)
        {
            return await _inventoryAccess.CheckInventoryTagExistance(criteria);
        }

        public async Task<bool> AddInventoryMaintenanceChangesAsync(TblInventoryMaintenanceDTO tblInventoryMaintenanceDTO)
        {
            return await _inventoryAccess.AddUpdateInventoryMaintenanceAsync(_mapper.Map<TblInventoryMaintenance>(tblInventoryMaintenanceDTO));
        }

        public async Task<bool> DeleteInventoryMaintenance(int InventoryMaintenanceId)
        {
            return await _inventoryAccess.DeleteInventoryMaintenance(InventoryMaintenanceId);
        }
    }
}

