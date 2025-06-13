using IntegratedAppraisalControl.Business;
using IntegratedAppraisalControl.Models;
using IntegratedAppraisalControl.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Controllers
{
    public class CommonController : BaseController
    {
        private readonly ICommonBusiness _commonBusiness;
        private readonly ILocationBusiness _locationBusiness;
        private readonly IBuildingBusiness _buildingBusiness;

        public CommonController(ICommonBusiness commonBusiness, ILocationBusiness locationBusiness, IBuildingBusiness buildingBusiness)
        {
            _commonBusiness = commonBusiness;
            _locationBusiness = locationBusiness;
            _buildingBusiness = buildingBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> GetClientDetails(string fileNo)
        {
            var clientDetails = await _commonBusiness.GetClientDetails(fileNo);
            if (clientDetails == null) return NotFound();
            return Ok(clientDetails);
        }

        [HttpGet]
        public async Task<IActionResult> GetBuildingDetails(int clientId, string buildingCode)
        {
            var result = await _commonBusiness.GetBuildingDetails(clientId, buildingCode);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<JsonResult> AddClient(TblClientsDTO client)
        {
            bool status = false;
            string message = "";
            string data = "";

            try
            {
                if (BaseReadOnly)
                {
                    message = "You have readonly permission.";
                }
                else
                {
                    var validationMsg = ValidateClient(client);
                    if (validationMsg != null)
                    {
                        message = validationMsg;
                    }
                    else
                    {
                        client.LastUpdated = DateTime.Now.ToString("dd/MM/yyyy");
                        client.ClientStatusId = Convert.ToInt32(client.Active);

                        client = await _locationBusiness.AddUpdateClients(client);
                        status = true;
                        message = client.ClientId > 0 ? "Record updated successfully." : "Record inserted successfully.";
                    }
                }
            }
            catch (Exception ex)
            {
                status = false;
                message = "There is some issue, Please try again later.";
                data = ex.Message;
            }

            return Json(new { Status = status, Message = message, Data = data });
        }

        [HttpPost]
        public async Task<JsonResult> AddBuilding(TblBuildingsDTO building)
        {
            bool status = false;
            string message = "";
            string data = "";
            int clientId = 16854;

            try
            {
                if (BaseReadOnly)
                {
                    message = "You cannot add this building.";
                }
                else
                {
                    if (await _buildingBusiness.CheckBuildingCodeExistance(new BuildingCodeSearchCriteria
                    {
                        BuildingCode = building.BuildingCode,
                        ClientID = clientId,
                        IsSuperAdmin = BaseSuperAdmin
                    }))
                    {
                        message = "Duplicate building no, Please add another building number.";
                    }
                    else
                    {
                        var validationMsg = ValidateBuilding(building);
                        if (validationMsg != null)
                        {
                            message = validationMsg;
                        }
                        else
                        {
                            building.ClientId = clientId;
                            building.BuildingId = 0;
                            building.MarkForDeletion = false;
                            building.Deleted = false;

                            building = await _buildingBusiness.AddUpdateBuilding(building);
                            status = true;
                            message = "Building added successfully.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = "There is some issue while adding building.";
                data = ex.Message;
            }

            return Json(new { Status = status, Message = message, Data = data });
        }

        private string ValidateClient(TblClientsDTO client)
        {
            if (string.IsNullOrWhiteSpace(client.ClientName)) return "Please add client name.";
            if (string.IsNullOrWhiteSpace(client.Address1)) return "Please add client address.";
            if (string.IsNullOrWhiteSpace(client.City)) return "Please add client city.";
            if (string.IsNullOrWhiteSpace(client.State)) return "Please add client state.";
            if (string.IsNullOrWhiteSpace(client.ZipCode)) return "Please add client zipcode.";
            if (string.IsNullOrWhiteSpace(client.PointOfContact)) return "Please add point of contact.";
            if (string.IsNullOrWhiteSpace(client.Telephone)) return "Please add telephone number.";
            if (string.IsNullOrWhiteSpace(client.ReportYear)) return "Please add report year.";
            if (string.IsNullOrWhiteSpace(client.AccountingYear)) return "Please add accounting year.";
            if (client.AquisitionCostCutOff == null || client.AquisitionCostCutOff == 0) return "Please add acquisition cost cut off.";
            if (client.AnnualDepreciationId == null || client.AnnualDepreciationId == 0) return "Please select annual depreciation.";
            if (client.FirstYearDepreciationD == null || client.FirstYearDepreciationD == 0) return "Please select first year depreciation.";
            if (string.IsNullOrWhiteSpace(client.FileNo)) return "Please add file no.";
            if (string.IsNullOrWhiteSpace(client.AppraisalDate)) return "Please add appraisal date.";
            if (string.IsNullOrWhiteSpace(client.UpdatedTo)) return "Please add updated date.";
            if (client.AccountDataAsOf == null) return "Please add account date.";
            if (!client.Accounting && !client.Insurance && !client.Fmv) return "Please select at least one from accounting, insurance, and FMV.";
            if (client.NextRoomNumber == null || client.NextRoomNumber == 0) return "Please add next room number.";
            if (client.NextDepartmentNumber == null || client.NextDepartmentNumber == 0) return "Please add next department number.";

            return null;
        }

        private string ValidateBuilding(TblBuildingsDTO building)
        {
            if (string.IsNullOrWhiteSpace(building.BuildingCode)) return "Please add building number.";
            if (string.IsNullOrWhiteSpace(building.BuildingName)) return "Please add building name.";
            if (string.IsNullOrWhiteSpace(building.Address1)) return "Please add building address.";
            if (string.IsNullOrWhiteSpace(building.City)) return "Please add city.";
            if (string.IsNullOrWhiteSpace(building.State)) return "Please add state.";
            if (string.IsNullOrWhiteSpace(building.ZipCode)) return "Please add zipcode.";
            if (string.IsNullOrWhiteSpace(building.YearAcq)) return "Please add year acquired.";
            if (string.IsNullOrWhiteSpace(building.MonthAcq)) return "Please add month acquired.";
            if (string.IsNullOrWhiteSpace(building.Cost)) return "Please add building cost.";

            return null;
        }
    }
}
