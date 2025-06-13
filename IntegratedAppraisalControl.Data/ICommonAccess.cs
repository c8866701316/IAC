using IntegratedAppraisalControl.Data.Models;
using IntegratedAppraisalControl.Models;
using IntegratedAppraisalControl.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Data
{
    public interface ICommonAccess
    {
        Task<List<TblChangeType>> GetChangeTypeSearchCriteriaAsync(ChangeTypeCriteriaModel criteria);
        Task<List<tblTransactionsDTO>> GetTransaction(TransactionsSearchCritria criteria);

        Task<TblClients> GetClientDetails(string fileNo);
        Task<TblBuildings> GetBuildingDetails(int clientId, string buildingCode);
    }
}
