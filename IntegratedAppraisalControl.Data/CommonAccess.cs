using IntegratedAppraisalControl.Data.Models;
using IntegratedAppraisalControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using IntegratedAppraisalControl.Models.DTO;
using System.Data.SqlClient;

namespace IntegratedAppraisalControl.Data
{
    public class CommonAccess : ICommonAccess
    {
        private readonly IntegratedAppraisalControlContext _dbContext;
        public CommonAccess()
        {
            _dbContext = new IntegratedAppraisalControlContext();
        }
        public async Task<List<TblChangeType>> GetChangeTypeSearchCriteriaAsync(ChangeTypeCriteriaModel criteria)
        {
            return await _dbContext.TblChangeType.Where(m => m.Active == true).ToListAsync();
        }

        public async Task<List<tblTransactionsDTO>> GetTransaction(TransactionsSearchCritria criteria)
        {
            var prmClientID = new SqlParameter("@clientID", criteria.ClientId);
            var prmSuperadmin = new SqlParameter("@superadmin", Convert.ToBoolean(criteria.superadmin));

            return await _dbContext.Query<tblTransactionsDTO>().AsNoTracking().
                FromSql("TransactionList @clientID,@superadmin", prmClientID, prmSuperadmin).ToListAsync();
        }

        public async Task<TblClients> GetClientDetails(string fileNo)
        {
            TblClients data = await _dbContext.TblClients.AsNoTracking().Where(m => m.FileNo == fileNo).FirstOrDefaultAsync();
            return data;
        }
        public async Task<TblBuildings> GetBuildingDetails(int clientId, string buildingCode)
        {
            TblBuildings data = await _dbContext.TblBuildings.AsNoTracking().Where(m => m.ClientId == clientId && m.BuildingCode == buildingCode).FirstOrDefaultAsync();
                return data;
        }
    }
}
