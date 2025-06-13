using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IntegratedAppraisalControl.Models;
using IntegratedAppraisalControl.Models.DTO;
using IntegratedAppraisalControl.Business;
using System.Security.Claims;
using IntegratedAppraisalControl.Classes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace IntegratedAppraisalControl.Controllers
{
    [Authorize]
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentBusiness _DepartmentBusiness;
        private readonly IClientBusiness _ClientBusiness;
        private IHostingEnvironment _hostingEnvironment;
        public DepartmentController(
            IDepartmentBusiness departmentBusiness,
            IClientBusiness clientBusiness,
            IHostingEnvironment environment
            )
        {
            _DepartmentBusiness = departmentBusiness;
            _hostingEnvironment = environment;
            _ClientBusiness = clientBusiness;
        }

        public async Task<IActionResult> Index()
        {
            return View(new TblDepartmentsDTO());
        }

        [HttpGet]
        public async Task<JsonResult> DepartmentList()
        {
            DepartmentSearchCriteria criteria = new
            DepartmentSearchCriteria()
            {
                ClientID = BaseClientId,
                IsSuperAdmin = BaseSuperAdmin,
                IsClientAdmin = BaseClientAdmin
            };
            var DepartmentList = await _DepartmentBusiness.GetDepartmentList(criteria);

            return Json(new
            {
                data =
                DepartmentList.Select(data =>
                    new
                    {
                        DepartmentId = data.DepartmentId,
                        DepartmentCode = data.DepartmentCode,
                        DepartmentName = data.DepartmentName,
                        MarkForDeletion = data.MarkForDeletion.HasValue ? data.MarkForDeletion.Value : false
                    })
            });
        }

        [HttpGet]
        public async Task<ActionResult> _Department(int DepartmentId)
        {
            TblDepartmentsDTO tblDepartments = new TblDepartmentsDTO();
            if (DepartmentId != 0)
            {
                DepartmentSearchCriteria criteria = new
                   DepartmentSearchCriteria(){
                        ClientID = BaseClientId,
                        IsSuperAdmin = BaseSuperAdmin,
                        IsClientAdmin = BaseClientAdmin,
                        DepartmentId = DepartmentId
                    };
                tblDepartments = await _DepartmentBusiness.GetDepartment(criteria);
            }

            if (tblDepartments == null)
            {
                tblDepartments = new TblDepartmentsDTO();
            }
            if (tblDepartments.DepartmentId == 0)
            {
                TblClientsDTO tblClientsDTO = await _ClientBusiness.GeClientDetails(new ClientSearchCriteriaModel { ClientId = BaseClientId });
                if (tblClientsDTO.NextRoomNumber > 0)
                {
                    tblDepartments.DepartmentCode = Convert.ToString(tblClientsDTO.NextDepartmentNumber);
                }
                else
                {
                    tblDepartments.DepartmentCode = "9000";
                }
            }
            return PartialView(tblDepartments);
        }
        [HttpPost]
        public async Task<JsonResult> AddUpdate(TblDepartmentsDTO tblDepartments, bool IsDuplicate)
        {
            bool Status = false;
            string Message = "", Data = "";
            try
            {

                DepartmentSearchCriteria criteria = new
                    DepartmentSearchCriteria()
                {
                    ClientID = BaseClientId,
                    IsSuperAdmin = BaseSuperAdmin,
                    IsClientAdmin = BaseClientAdmin,
                    DepartmentId = tblDepartments.DepartmentId,
                    DepartmentCode = tblDepartments.DepartmentCode
                };

                if (Convert.ToBoolean(IsDuplicate))
                {
                    criteria.DepartmentId = 0;
                }

                if (await _DepartmentBusiness.CheckDepartmentCodeExistance(criteria))
                {
                    Status = false;
                    Message = "Please check department code. Department code is duplicate.";
                }
                else
                {
                    if (tblDepartments.DepartmentId > 0)
                    {
                        criteria.DepartmentId = tblDepartments.DepartmentId;
                        var tblDepartmentsOld = await _DepartmentBusiness.GetDepartment(criteria);
                        if (IsDuplicate)
                        {
                            tblDepartmentsOld.DepartmentId = 0;
                        }
                        tblDepartmentsOld.ClientId = BaseClientId;
                        tblDepartmentsOld.DepartmentCode = tblDepartments.DepartmentCode;
                        tblDepartmentsOld.DepartmentName = tblDepartments.DepartmentName;
                        tblDepartmentsOld.MarkForDeletion = tblDepartments.MarkForDeletion;

                        tblDepartmentsOld = await _DepartmentBusiness.AddUpdateDepartment(tblDepartmentsOld);

                        if (IsDuplicate)
                        {
                            Message = "Record duplicated successfully. You will find it on listing.";
                        }
                        else
                        {
                            Message = "Record updated successfully.";
                        }

                        if (Convert.ToBoolean(tblDepartments.MarkForDeletion) && IsDuplicate == false)
                        {
                            Message = "Record marked as deleted.";
                        }

                    }
                    else
                    {
                        Message = "Record added successfully.";
                        tblDepartments.ClientId = BaseClientId;
                        tblDepartments.Deleted = false;
                        tblDepartments.MarkForDeletion = false;

                        tblDepartments = await _DepartmentBusiness.AddUpdateDepartment(tblDepartments);
                    }

                   
                    Status = true;
                }
            }
            catch (Exception ex)
            {
                Status = false;
                Data = ex.Message;
                Message = "There is some issue. Please try again after sometime.";
            }

            return Json(new
            {
                Status = Status,
                Message = Message,
                Data = Data
            });
        }
    }
}
