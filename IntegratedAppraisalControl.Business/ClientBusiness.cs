using AutoMapper;
using IntegratedAppraisalControl.Data;
using IntegratedAppraisalControl.Models;
using IntegratedAppraisalControl.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Business
{
    public class ClientBusiness : IClientBusiness
    {
        private readonly IClientAccess _clientAccess;
        private readonly IMapper _mapper;
        public ClientBusiness(IClientAccess clientAccess, IMapper mapper)
        {
            _clientAccess = clientAccess;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TblClientsDTO>> GetClientListBySearchCriteriaAsync(ClientSearchCriteriaModel criteria)
        {
            if (criteria == null)
            {
                criteria = new ClientSearchCriteriaModel();
            }
            var results = await _clientAccess.GetClientListBySearchCriteriaAsync(criteria);
            return _mapper.Map<List<TblClientsDTO>>(results);
        }

        public async Task<TblInventoryMaintenanceDTO> GetInventoryMaintenance(InventoryMaintenanceSearchCriteria criteria)
        {
            var results = await _clientAccess.GetInventoryMaintenance(criteria);
            return _mapper.Map<TblInventoryMaintenanceDTO>(results);
        }

        public async Task<List<TblClientsDTO>> GetClientList()
        {
            var results = await _clientAccess.GetClientList();
            return _mapper.Map<List<TblClientsDTO>>(results);
        }

        public async Task<TblClientsDTO> GeClientDetails(ClientSearchCriteriaModel criteria)
        {
            return await _clientAccess.GeClientDetails(criteria);
        }
    }
}