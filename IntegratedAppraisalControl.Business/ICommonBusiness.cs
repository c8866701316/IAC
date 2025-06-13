using IntegratedAppraisalControl.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IntegratedAppraisalControl.Models;
using IntegratedAppraisalControl.Data.Models;

namespace IntegratedAppraisalControl.Business
{
    public interface ICommonBusiness
    {
        Task<List<TblChangeTypeDTO>> GetChangeTypeSearchCriteriaAsync(ChangeTypeCriteriaModel criteria);
        Task<List<tblTransactionsDTO>> GetTransaction(TransactionsSearchCritria criteria);

        Task<TblClients> GetClientDetails(string fileNo);
        Task<TblBuildings> GetBuildingDetails(int clientId, string buildingCode);
    }
}
