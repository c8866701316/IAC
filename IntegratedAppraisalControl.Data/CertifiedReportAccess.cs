using IntegratedAppraisalControl.Data;
using IntegratedAppraisalControl.Data.Models;
using IntegratedAppraisalControl.Models;
using IntegratedAppraisalControl.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Data
{
    public class CertifiedReportAccess : ICertifiedReportAccess
    {
        private readonly IntegratedAppraisalControlContext _dbContext;
        public CertifiedReportAccess()
        {
            _dbContext= new IntegratedAppraisalControlContext();
        }
        public async Task<List<TblClientAttachments>> GetCertifiedReportList(CertifiedReportSearchCriteria criteria)
        {
            return await _dbContext.TblClientAttachments.AsNoTracking().Where(m => m.ClientId == criteria.ClientID).ToListAsync();
        }
    }
}
