using IntegratedAppraisalControl.Data.Models;
using IntegratedAppraisalControl.Models;
using IntegratedAppraisalControl.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Data
{
    public interface ICertifiedReportAccess
    {
        Task<List<TblClientAttachments>> GetCertifiedReportList(CertifiedReportSearchCriteria criteria);
    }
}
