using IntegratedAppraisalControl.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IntegratedAppraisalControl.Models;

namespace IntegratedAppraisalControl.Business
{
    public interface IClientBusiness
    {
        Task<IEnumerable<TblClientsDTO>> GetClientListBySearchCriteriaAsync(ClientSearchCriteriaModel criteria);
        Task<TblInventoryMaintenanceDTO> GetInventoryMaintenance(InventoryMaintenanceSearchCriteria criteria);
        Task<List<TblClientsDTO>> GetClientList();
        Task<TblClientsDTO> GeClientDetails(ClientSearchCriteriaModel criteria);
    }
}
