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
    public class LocationController : BaseController
    {
        private readonly ILocationBusiness _locationBusiness;

        public LocationController(
            ILocationBusiness locationBusiness
            )
        {
            _locationBusiness = locationBusiness;
        }

        public async Task<IActionResult> Index()
        {
            LocationSearchCriteria criteria = new
            LocationSearchCriteria()
            {
                LocationId = 0,
                ClientID = BaseClientId,
                IsSuperAdmin = BaseSuperAdmin,
                IsClientAdmin = BaseClientAdmin
            };

            ViewBag.lstAnnualDepreciation = await _locationBusiness.GetAnnualDepreciationListDDL(criteria);
            ViewBag.lstFirstYearDepreciationList = await _locationBusiness.GetFirstYearDepreciationListDDL(criteria);

            return View(new TblClientsDTO());
        }

        [HttpGet]
        public async Task<JsonResult> LocationList()
        {
            var LocationList = await _locationBusiness.GetLocationList(
                new LocationSearchCriteria()
                {
                    ClientID = BaseClientId,
                    IsSuperAdmin = BaseSuperAdmin,
                    IsClientAdmin = BaseClientAdmin,
                    BaseUserId = BaseUserId
                });


            return Json(new
            {
                data =
                LocationList.Select(data =>
                    new
                    {
                        Name = data.ClientName,
                        FileNo = data.FileNo,
                        Addrees1 = data.Address1,
                        Address2 = data.Address2,
                        City = data.City,
                        State = data.State,
                        ZipCode = data.ZipCode,
                        PointOfContact = data.PointOfContact,
                        Telephone = data.Telephone,
                        ReportYear = data.ReportYear,
                        AccountingYear = data.AccountingYear,
                        AquisitionCostCutOff = data.AquisitionCostCutOff,
                        LastUpdated = data.LastUpdated,
                        AnnualDepreciationId = data.AnnualDepreciationId,
                        FirstYearDepreciationD = data.FirstYearDepreciationD,
                        Active = Convert.ToBoolean(data.Active) ? "Active" : "Inactive",
                        ClientId = data.ClientId,
                        FirstYearDepreciationText = data.FirstYearDepreciationText,
                        AnnualDepreciationText = data.AnnualDepreciationText
                    })
            });
        }

        public async Task<IActionResult> _AddEdit(int Id)
        {
            TblClientsDTO tblClients = new TblClientsDTO();

            LocationSearchCriteria criteria = new
            LocationSearchCriteria()
            {
                LocationId = Id,
                ClientID = BaseClientId,
                IsSuperAdmin = BaseSuperAdmin,
                IsClientAdmin = BaseClientAdmin
            };

            if (Id != 0)
            {
                tblClients = await _locationBusiness.GetClients(criteria);
            }

            if (tblClients == null)
            {
                tblClients = new TblClientsDTO();
            }

            tblClients.Active = tblClients.Active.HasValue ? tblClients.Active.Value : false;

            ViewBag.lstAnnualDepreciation = await _locationBusiness.GetAnnualDepreciationListDDL(criteria);
            ViewBag.lstFirstYearDepreciationList = await _locationBusiness.GetFirstYearDepreciationListDDL(criteria);

            return PartialView(tblClients);

        }

        public async Task<IActionResult> Edit(int Id)
        {
            TblClientsDTO tblClients = new TblClientsDTO();

            LocationSearchCriteria criteria = new
            LocationSearchCriteria()
            {
                LocationId = Id,
                ClientID = BaseClientId,
                IsSuperAdmin = BaseSuperAdmin,
                IsClientAdmin = BaseClientAdmin
            };

            if (Id != 0)
            {
                tblClients = await _locationBusiness.GetClients(criteria);
            }

            if (tblClients == null)
            {
                tblClients = new TblClientsDTO();
            }

            tblClients.Active = tblClients.Active.HasValue ? tblClients.Active.Value : false;

            ViewBag.lstAnnualDepreciation = await _locationBusiness.GetAnnualDepreciationListDDL(criteria);
            ViewBag.lstFirstYearDepreciationList = await _locationBusiness.GetFirstYearDepreciationListDDL(criteria);

            return View(tblClients);
        }

        [HttpPost]
        public async Task<JsonResult> Add(TblClientsDTO client)
        {
            bool Status = false;
            string Message = "Record updated.", Data = "";

            LocationSearchCriteria criteria = new
            LocationSearchCriteria()
            {
                LocationId = 0,
                ClientID = 0,
                IsSuperAdmin = BaseSuperAdmin,
                IsClientAdmin = BaseClientAdmin,
            };

            try
            {
                //if (await _locationBusiness.CheckInventoryTagExistance(criteria))
                //{
                //    Status = false;
                //    Message = "Please check tag no. Tag no is duplicate.";
                //}
                //else
                //{
                if (!BaseReadOnly)
                {
                    //client.ClientId = BaseClientId;
                    client.LastUpdated = System.DateTime.Now.ToString("dd/mm/yyyy");
                    client.ClientStatusId = Convert.ToInt32(client.Active);

                    if(client.ClientId > 0)
                    {
                        Message = "Record updated successfully.";
                    }
                    else
                    {
                        Message = "Record inserted successfully.";
                    }

                    client = await _locationBusiness.AddUpdateClients(client);
                    Status = true;
                }
                else
                {
                    Status = false;
                    Message = "You have readonly permission.";
                }
                //}
            }
            catch (Exception ex)
            {
                Status = false;
                Message = "There is some issue, Please try again after sometime.";
                Data = ex.Message;
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