using IntegratedAppraisalControl.Data.Models;
using IntegratedAppraisalControl.Models;
using IntegratedAppraisalControl.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Data
{
    public interface IClientAccess
    {
        Task<List<TblClients>> GetClientListBySearchCriteriaAsync(ClientSearchCriteriaModel criteria);
        Task<TblInventoryMaintenance> GetInventoryMaintenance(InventoryMaintenanceSearchCriteria criteria);
        Task<List<TblClients>> GetClientList();
        Task<TblClientsDTO> GeClientDetails(ClientSearchCriteriaModel criteria);
    }
}
