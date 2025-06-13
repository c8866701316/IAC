using IntegratedAppraisalControl.Business;
using IntegratedAppraisalControl.Data;
using IntegratedAppraisalControl.Models;
using IntegratedAppraisalControl.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Controllers
{
    public class CommonController : BaseController
    {
        private readonly ICommonBusiness _CommonBusiness;
        private readonly ILocationBusiness _locationBusiness;
        private readonly IBuildingBusiness _BuildingBusiness;

        public CommonController(ICommonBusiness business, ILocationBusiness locationBusiness, IBuildingBusiness buildingBusiness)
        {
            _CommonBusiness = business;
            _locationBusiness = locationBusiness;
            _BuildingBusiness = buildingBusiness;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetClientDetails(string fileNo)
        {
            var clientDetails = await _CommonBusiness.GetClientDetails(fileNo);

            if (clientDetails == null)
            {
                return NotFound(); // Returns 404
            }
            return Ok(); // Returns 200
        }

        [HttpGet]
        public async Task<IActionResult> GetBuildingDetails(int clientId, string buildingCode)
        {
            var buildingDetails = await _CommonBusiness.GetBuildingDetails(clientId, buildingCode);

            if (buildingDetails == null)
            {
                return NotFound(); // Returns 404
            }
            return Ok(); // Returns 200
        }

        [HttpPost]
        public async Task<JsonResult> AddClient(TblClientsDTO client)
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
                if (!BaseReadOnly)
                {
                    //client.ClientId = BaseClientId;
                    client.LastUpdated = System.DateTime.Now.ToString("dd/mm/yyyy");
                    client.ClientStatusId = Convert.ToInt32(client.Active);

                    if (string.IsNullOrEmpty(client.ClientName))
                    {
                        Message = "Please add client name.";
                    }
                    else if (string.IsNullOrEmpty(client.Address1))
                    {
                        Message = "Please add client address.";
                    }
                    else if (string.IsNullOrEmpty(client.City))
                    {
                        Message = "Please add client city.";
                    }
                    else if (string.IsNullOrEmpty(client.State))
                    {
                        Message = "Please add client state.";
                    }
                    else if (string.IsNullOrEmpty(client.ZipCode))
                    {
                        Message = "Please add client zipcode.";
                    }
                    else if (string.IsNullOrEmpty(client.PointOfContact))
                    {
                        Message = "Please add point of contact.";
                    }
                    else if (string.IsNullOrEmpty(client.Telephone))
                    {
                        Message = "Please add telephone number.";
                    }
                    else if (string.IsNullOrEmpty(client.ReportYear))
                    {
                        Message = "Please add report year.";
                    }
                    else if (string.IsNullOrEmpty(client.AccountingYear))
                    {
                        Message = "Please add accounting year.";
                    }
                    else if (client.AquisitionCostCutOff == 0 || client.AquisitionCostCutOff == null)
                    {
                        Message = "Please add aquisition cost cut off.";
                    }
                    else if (client.AnnualDepreciationId == 0 || client.AnnualDepreciationId == null)
                    {
                        Message = "Please select annual description.";
                    }
                    else if (client.FirstYearDepreciationD == 0 || client.FirstYearDepreciationD == null)
                    {
                        Message = "Please select first year description.";
                    }
                    //else if (client.ClientStatusId == 0 || client.ClientStatusId == null)
                    //{
                    //    Message = "Please select client status.";
                    //}
                    else if (string.IsNullOrEmpty(client.FileNo))
                    {
                        Message = "Please add file no.";
                    }
                    else if (string.IsNullOrEmpty(client.AppraisalDate))
                    {
                        Message = "Please add appraisal date.";
                    }
                    else if (string.IsNullOrEmpty(client.UpdatedTo))
                    {
                        Message = "Please add updated date.";
                    }
                    else if (client.AccountDataAsOf == null)
                    {
                        Message = "Please add account date.";
                    }
                    else if (client.Accounting == false && client.Insurance == false && client.Fmv == false)
                    {
                        Message = "Please select atleast one from accounting, insurance and fmv.";
                    }
                    else if (client.NextRoomNumber == 0 || client.NextRoomNumber == null)
                    {
                        Message = "Please add next room number.";
                    }
                    else if (client.NextDepartmentNumber == 0 || client.NextDepartmentNumber == null)
                    {
                        Message = "Please add next department number.";
                    }
                    else
                    {

                        if (client.ClientId > 0)
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

        [HttpPost]
        //public async Task<JsonResult> AddBuilding(TblBuildingsDTO tblBuildingsDTO)
        public async Task<JsonResult> AddBuilding(TblBuildingsDTO tblBuildingsDTO)
        {
            tblBuildingsDTO.BuildingId = 0;
            int clientId = 16854;
            bool Status = false;
            string Message = "Building added successsfully.", Data = "";
            try
            {
                if (BaseReadOnly == false)
                {
                    if (await _BuildingBusiness.CheckBuildingCodeExistance(new BuildingCodeSearchCriteria { BuildingCode = tblBuildingsDTO.BuildingCode, ClientID = clientId/*BaseClientId*/, IsSuperAdmin = BaseSuperAdmin }))
                    {
                        Message = "Duplicate building no, Please add another building number.";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(tblBuildingsDTO.BuildingCode))
                        {
                            Message = "Please add building number.";
                        }
                        else if (string.IsNullOrEmpty(tblBuildingsDTO.BuildingName))
                        {
                            Message = "Please add building name.";
                        }
                        else if (string.IsNullOrEmpty(tblBuildingsDTO.Address1))
                        {
                            Message = "Please add building address.";
                        }
                        else if (string.IsNullOrEmpty(tblBuildingsDTO.City))
                        {
                            Message = "Please add city.";
                        }
                        else if (string.IsNullOrEmpty(tblBuildingsDTO.State))
                        {
                            Message = "Please add state.";
                        }
                        else if (string.IsNullOrEmpty(tblBuildingsDTO.ZipCode))
                        {
                            Message = "Please add zipcode.";
                        }
                        else if (string.IsNullOrEmpty(tblBuildingsDTO.YearAcq))
                        {
                            Message = "Please add year acquired.";
                        }
                        else if (string.IsNullOrEmpty(tblBuildingsDTO.MonthAcq))
                        {
                            Message = "Please add month acquired.";
                        }
                        else if (string.IsNullOrEmpty(tblBuildingsDTO.Cost))
                        {
                            Message = "Please add building cost.";
                        }
                        else
                        {
                            tblBuildingsDTO.ClientId = clientId/*BaseClientId*/;
                            tblBuildingsDTO.MarkForDeletion = false;
                            tblBuildingsDTO.Deleted = false;
                            tblBuildingsDTO = await _BuildingBusiness.AddUpdateBuilding(tblBuildingsDTO);
                            Status = true;
                        }
                    }
                }
                else
                {
                    Message = "You can not add this building";
                }
            }
            catch (Exception ex)
            {
                Data = ex.Message;
                Message = "There is some issue while adding building.";
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
