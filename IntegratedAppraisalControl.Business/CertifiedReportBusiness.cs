using AutoMapper;
using IntegratedAppraisalControl.Data;
using IntegratedAppraisalControl.Data.Models;
using IntegratedAppraisalControl.Models;
using IntegratedAppraisalControl.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Business
{
    public class CertifiedReportBusiness : ICertifiedReportBusiness
    {
        private readonly ICertifiedReportAccess _certifiedReportAccess;
        private readonly IMapper _mapper;
        public CertifiedReportBusiness(ICertifiedReportAccess certifiedReportAccess, IMapper mapper)
        {
            _certifiedReportAccess = certifiedReportAccess;
            _mapper = mapper;
        }

        public async Task<List<TblClientAttachmentsDTO>> GetCertifiedReportList(CertifiedReportSearchCriteria criteria)
        {
            var results = await _certifiedReportAccess.GetCertifiedReportList(criteria);
            return _mapper.Map<List<TblClientAttachmentsDTO>>(results);
        }

    }
}
