using IntegratedAppraisalControl.Data.Models;
using IntegratedAppraisalControl.Models;
using IntegratedAppraisalControl.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Data
{
    public interface ILocationAccess
    {
        Task<List<TblClientsDTO>> GetLocationList(LocationSearchCriteria criteria);
        Task<List<TblAnnualDepreciationDTO>> GetAnnualDepreciationListDDL(LocationSearchCriteria criteria);
        Task<List<TblFirstYearDepreciationDTO>> GetFirstYearDepreciationListDDL(LocationSearchCriteria criteria);
        Task<TblClients> GetClients(LocationSearchCriteria criteria);
        Task<TblClients> AddUpdateClients(TblClients tblClients);
    }
}