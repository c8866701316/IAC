using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegratedAppraisalControl.Business;
using IntegratedAppraisalControl.Models;
using IntegratedAppraisalControl.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntegratedAppraisalControl.Controllers
{
    [Authorize]
    public class CertifiedReportController : BaseController
    {
        private readonly ICertifiedReportBusiness _CertifiedReport;

        public CertifiedReportController(ICertifiedReportBusiness certifiedReport)
        {
            _CertifiedReport = certifiedReport;
        }


        public async Task<IActionResult> Index()
        {
            return View(new TblUsersDTO());
        }

        [HttpGet]
        public async Task<JsonResult> CertifiedReportList()
        {
            CertifiedReportSearchCriteria criteria = new
            CertifiedReportSearchCriteria()
            {
                ClientID = BaseClientId
            };
            var CertifiedReportList = await _CertifiedReport.GetCertifiedReportList(criteria);

            return Json(new
            {
                data =
                CertifiedReportList.Select(data =>
                    new
                    {
                        Description = data.AttachmentDescription,
                        Type = data.AttachmentName,
                        ID = data.ClientID
                    })
            });
        }


    }
}







//using System.Linq;
//using System.Threading.Tasks;
//using IntegratedAppraisalControl.Business;
//using IntegratedAppraisalControl.Models;
//using IntegratedAppraisalControl.Models.DTO;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace IntegratedAppraisalControl.Controllers
//{
//    [Authorize]
//    public class CertifiedReportController : BaseController
//    {
//        private readonly ICertifiedReportBusiness _CertifiedReport;

//        public CertifiedReportController(ICertifiedReportBusiness certifiedReport)
//        {
//            _CertifiedReport = certifiedReport;
//        }


//        public async Task<IActionResult> Index()
//        {
//            return View(new TblClientAttachmentsDTO());
//        }



//    }
//}