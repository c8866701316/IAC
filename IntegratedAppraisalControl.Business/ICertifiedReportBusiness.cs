using IntegratedAppraisalControl.Models;
using IntegratedAppraisalControl.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Business
{
    public interface ICertifiedReportBusiness
    {
        Task<List<TblClientAttachmentsDTO>> GetCertifiedReportList(CertifiedReportSearchCriteria criteria);
    }
}
