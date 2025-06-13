using IntegratedAppraisalControl.Models;
using IntegratedAppraisalControl.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Business
{
    public interface ILocationBusiness
    {
        Task<List<TblClientsDTO>> GetLocationList(LocationSearchCriteria criteria);
        Task<List<TblAnnualDepreciationDTO>> GetAnnualDepreciationListDDL(LocationSearchCriteria criteria);
        Task<List<TblFirstYearDepreciationDTO>> GetFirstYearDepreciationListDDL(LocationSearchCriteria criteria);
        Task<TblClientsDTO> GetClients(LocationSearchCriteria criteria);
        Task<TblClientsDTO> AddUpdateClients(TblClientsDTO tblClientsDTO);
    }
}