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

namespace IntegratedAppraisalControl.Controllers
{
    [Authorize]
    public class BuildingsController : BaseController
    {
        private readonly IClientBusiness _ClientBusiness;
        private readonly IUserBusiness _UserBusiness;
        private readonly IBuildingBusiness _BuildingBusiness;
        private readonly ICommonBusiness _CommonBusiness;
        public BuildingsController(
            IClientBusiness clientBusiness,
            IUserBusiness userBusiness,
            IBuildingBusiness buildingBusiness,
            ICommonBusiness commonBusiness
            )
        {
            _ClientBusiness = clientBusiness;
            _UserBusiness = userBusiness;
            _BuildingBusiness = buildingBusiness;
            _CommonBusiness = commonBusiness;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<JsonResult> BuildingList()
        {
            var userId = User.FindFirst(CustomClaimTypes.UserId)?.Value;
            int _ClientId = IdentityExtensions.GetClientId(User.Identity);

            var BuildingList = await _BuildingBusiness.GetBuildingListBySearchCriteriaAsync(new BuildingSearchCriteriaModel() { ClientID = BaseClientId, IsSuperAdmin = BaseSuperAdmin });

            return Json(new
            {
                data =
                BuildingList.Select(data =>
                    new
                    {
                        BuildingCode = data.BuildingCode,
                        BuildingName = data.BuildingName,
                        Address1 = data.Address1,
                        City = data.City,
                        BuildingId = data.BuildingId
                    })
            });
        }

        [HttpGet]
        public async Task<JsonResult> BuildingSectionPricingList(int BuildingId = 0)
        {
            var BuildingSectionPricingList = await _BuildingBusiness.GetBuildingSectionPricingAsync(new BuildingSearchCriteriaModel() { BuildingId = BuildingId, ClientID = BaseClientId, IsSuperAdmin = BaseSuperAdmin });

            return Json(new
            {
                data =
                BuildingSectionPricingList.Select(data =>
                    new
                    {
                        SectionDesc = data.SectionDesc,
                        PercentOf = data.PercentOf,
                        Yracq = data.Yracq,
                        Isoclass = data.Isoclass,
                        Life = data.Life,
                        TotalCost = data.Cost,
                        HistoricalCost = data.HistoricalCost,
                    })
            });
        }

        [HttpGet]
        public async Task<JsonResult> BuildingAdditionalDataDescriptionList(int BuildingId = 0)
        {
            var BuildingAdditionalDataList = await _BuildingBusiness.GetBuildingAdditionalDataAsync(new BuildingSearchCriteriaModel() { BuildingId = BuildingId, ClientID = BaseClientId, IsSuperAdmin = BaseSuperAdmin });

            return Json(new
            {
                data =
                BuildingAdditionalDataList.Select(data =>
                    new
                    {
                        AdditionalDataDescription = data.AdditionalDataDescription
                    })
            });
        }

        [HttpGet]
        public async Task<JsonResult> BuildingChangesList(int BuildingId = 0)
        {
            var BuildingChangesList = await _BuildingBusiness.GetBuildingChangesAsync(new BuildingSearchCriteriaModel() { BuildingId = BuildingId, ClientID = BaseClientId, IsSuperAdmin = BaseSuperAdmin });

            return Json(new
            {
                data =
                BuildingChangesList.Select(data =>
                    new
                    {
                        ChangeType = data.ChangeType,
                        Description = data.Description,
                        MonthAcq = data.MonthAcq,
                        YearAcq = data.YearAcq,
                        Cost = data.Cost,
                        BuildingChangesId = data.BuildingChangesId
                    })
            });
        }

        public async Task<IActionResult> _BuildingChange(int Id, int BuildingId)
        {
            TblBuildingChangesDTO buildingChange = new TblBuildingChangesDTO
            {
                BuildingId = BuildingId
            };
            ViewBag.lstChangesTypeDTO = await _CommonBusiness.GetChangeTypeSearchCriteriaAsync(new ChangeTypeCriteriaModel() { Active = true, IsSuperAdmin = BaseSuperAdmin });

            if (Id > 0)
            {
                buildingChange = await _BuildingBusiness.GetBuildingChangeAsync(new BuildingSearchCriteriaModel() { ClientID = BaseClientId, IsSuperAdmin = BaseSuperAdmin, BuildingChangesId = Id, BuildingId = BuildingId });
                if (buildingChange == null)
                {
                    buildingChange = new TblBuildingChangesDTO
                    {
                        BuildingId = BuildingId
                    };
                }
            }

            return PartialView("_BuildingChange", buildingChange);
        }

        [HttpGet]
        public async Task<JsonResult> BuildingMaintananceList(int BuildingId = 0)
        {
            var BuildingMaintananceList = await _BuildingBusiness.GetBuildingMaintenanceAsync(new BuildingSearchCriteriaModel() { BuildingId = BuildingId, ClientID = BaseClientId, IsSuperAdmin = BaseSuperAdmin });

            return Json(new
            {
                data =
                BuildingMaintananceList.Select(data =>
                    new
                    {
                        ChangeType = data.ChangeType,
                        Description = data.Description,
                        MonthAcq = data.MonthAcq,
                        YearAcq = data.YearAcq,
                        Cost = data.Cost,
                        BuildingMaintenanceId = data.BuildingMaintenanceId
                    })
            });
        }

        public async Task<IActionResult> _BuildingMaintanance(int Id, int BuildingId)
        {
            TblBuildingMaintenanceDTO buildingMaintenance = new TblBuildingMaintenanceDTO
            {
                BuildingId = BuildingId
            };
            ViewBag.lstChangesTypeDTO = await _CommonBusiness.GetChangeTypeSearchCriteriaAsync(new ChangeTypeCriteriaModel() { Active = true, IsSuperAdmin = BaseSuperAdmin });

            if (Id > 0)
            {
                buildingMaintenance = await _BuildingBusiness.GetBuildingMaintenanceByIdAsync(new BuildingSearchCriteriaModel() { ClientID = BaseClientId, IsSuperAdmin = BaseSuperAdmin, BuildingMaintenanceId = Id, BuildingId = BuildingId });
                if (buildingMaintenance == null)
                {
                    buildingMaintenance = new TblBuildingMaintenanceDTO
                    {
                        BuildingId = BuildingId
                    };
                }
            }

            return PartialView("_BuildingMaintanance", buildingMaintenance);
        }

        public async Task<IActionResult> Building(int Id)
        {
            BuildingSearchCriteriaModel criteria = new BuildingSearchCriteriaModel() { BuildingId = Id, IsSuperAdmin = BaseSuperAdmin, ClientID = BaseClientId };
            ChangeTypeCriteriaModel changeTypeCriteria = new ChangeTypeCriteriaModel() { Active = true, IsSuperAdmin = BaseSuperAdmin };
            BuildingViewModel buildingViewModel = new BuildingViewModel();
            buildingViewModel.tblBuildingsDTO = await _BuildingBusiness.GetBuildingDetailsAsync(criteria);

            if (buildingViewModel.tblBuildingsDTO == null)
            {
                return View("AccessDenied");
            }

            buildingViewModel.listTblBuildingImagesDTO = await _BuildingBusiness.GetBuildingImagesSearchCriteriaAsync(criteria);
            buildingViewModel.lstChangesTypeDTO = await _CommonBusiness.GetChangeTypeSearchCriteriaAsync(changeTypeCriteria);
            return View(buildingViewModel);
        }

        [HttpPost]
        public async Task<JsonResult> Edit(BuildingViewModel buildingViewModel)
        {
            bool Status = false;
            string Message = "Building updated successsfully.", Data = "";
            try
            {
                TblBuildingsDTO tblBuildingsDTO = buildingViewModel.tblBuildingsDTO;
                if (BaseReadOnly == false)
                {
                    TblBuildingsDTO tblBuildingsOldData = await _BuildingBusiness.GetBuildingDetailsAsync(new BuildingSearchCriteriaModel() { BuildingId = tblBuildingsDTO.BuildingId, IsSuperAdmin = BaseSuperAdmin });
                    tblBuildingsOldData.BuildingCode = tblBuildingsDTO.BuildingCode;
                    tblBuildingsOldData.BuildingName = tblBuildingsDTO.BuildingName;
                    tblBuildingsOldData.State = tblBuildingsDTO.State;
                    tblBuildingsOldData.Address1 = tblBuildingsDTO.Address1;
                    tblBuildingsOldData.ZipCode = tblBuildingsDTO.ZipCode;
                    tblBuildingsOldData = await _BuildingBusiness.AddUpdateBuilding(tblBuildingsOldData);
                    Status = true;
                }
                else
                {
                    Status = false;
                    Message = "You can not update this building";
                }
            }
            catch (Exception ex)
            {
                Status = false;
                Data = ex.Message;
                Message = "You can not update this building.";
            }

            return Json(new
            {
                Status = Status,
                Message = Message,
                Data = Data
            });
        }

        [HttpPost]
        public async Task<JsonResult> Add(TblBuildingsDTO tblBuildingsDTO)
        {
            bool Status = false;
            string Message = "Building added successsfully.", Data = "";
            try
            {
                if (BaseReadOnly == false)
                {
                    if (await _BuildingBusiness.CheckBuildingCodeExistance(new BuildingCodeSearchCriteria { BuildingCode = tblBuildingsDTO.BuildingCode, ClientID = BaseClientId, IsSuperAdmin = BaseSuperAdmin }))
                    {
                        Status = false;
                        Message = "Duplicate building no, Please add another building number.";
                    }
                    else
                    {
                        tblBuildingsDTO.ClientId = BaseClientId;
                        tblBuildingsDTO.MarkForDeletion = false;
                        tblBuildingsDTO.Deleted = false;
                        tblBuildingsDTO = await _BuildingBusiness.AddUpdateBuilding(tblBuildingsDTO);
                        Status = true;
                    }
                }
                else
                {
                    Status = false;
                    Message = "You can not add this building";
                }
            }
            catch (Exception ex)
            {

                Status = false;
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

        [HttpPost]
        public async Task<JsonResult> AddChange(TblBuildingChangesDTO tblBuildingChangesDTO)
        {
            bool Status = false;
            string Message = "", Data = "";
            try
            {
                if (BaseReadOnly == false)
                {
                    if (tblBuildingChangesDTO.BuildingChangesId > 0)
                    {
                        BuildingSearchCriteriaModel criteria = new BuildingSearchCriteriaModel
                        {
                            BuildingId = Convert.ToInt32(tblBuildingChangesDTO.BuildingId),
                            ClientID = BaseClientId,
                            IsSuperAdmin = BaseSuperAdmin,
                            BuildingChangesId = tblBuildingChangesDTO.BuildingChangesId
                        };

                        TblBuildingChangesDTO tblBuildingChangesOldDTO = await _BuildingBusiness.GetBuildingChangeAsync(criteria);

                        tblBuildingChangesOldDTO.ChangeTypeId = tblBuildingChangesDTO.ChangeTypeId;
                        tblBuildingChangesOldDTO.Cost = tblBuildingChangesDTO.Cost;
                        tblBuildingChangesOldDTO.YearAcq = tblBuildingChangesDTO.YearAcq;
                        tblBuildingChangesOldDTO.MonthAcq = tblBuildingChangesDTO.MonthAcq;
                        tblBuildingChangesOldDTO.Description = tblBuildingChangesDTO.Description;

                        tblBuildingChangesOldDTO.Cuser = BaseUserId;
                        tblBuildingChangesOldDTO.CdateTime = DateTime.Now;
                        tblBuildingChangesOldDTO = await _BuildingBusiness.AddUpdateBuildingChangesAsync(tblBuildingChangesOldDTO);
                        Message = "Building changes updated successsfully.";
                    }
                    else
                    {
                        tblBuildingChangesDTO.Auser = BaseUserId;
                        tblBuildingChangesDTO.AdateTime = DateTime.Now;
                        tblBuildingChangesDTO.BuildingId = tblBuildingChangesDTO.BuildingId;
                        tblBuildingChangesDTO = await _BuildingBusiness.AddUpdateBuildingChangesAsync(tblBuildingChangesDTO);
                        Message = "Building changes added successsfully.";
                    }

                    Status = true;
                }
                else
                {
                    Status = false;
                    Message = "You can not add changes.";
                }
            }
            catch (Exception ex)
            {
                Status = false;
                Data = ex.Message;
                Message = "There is some issue while adding changes.";
            }

            return Json(new
            {
                Status = Status,
                Message = Message,
                Data = Data
            });
        }

        [HttpPost]
        public async Task<JsonResult> AddChangesMaintanance(TblBuildingMaintenanceDTO tblBuildingMaintenanceDTO)
        {
            bool Status = false;
            string Message = "", Data = "";
            try
            {
                if (BaseReadOnly == false)
                {
                    if (tblBuildingMaintenanceDTO.BuildingMaintenanceId > 0)
                    {
                        BuildingSearchCriteriaModel criteria = new BuildingSearchCriteriaModel()
                        {
                            ClientID = BaseClientId,
                            IsSuperAdmin = BaseSuperAdmin,
                            BuildingMaintenanceId = tblBuildingMaintenanceDTO.BuildingMaintenanceId,
                            BuildingId = Convert.ToInt32(tblBuildingMaintenanceDTO.BuildingId)
                        };

                        TblBuildingMaintenanceDTO tblBuildingMaintenanceOldDTO = await _BuildingBusiness.GetBuildingMaintenanceByIdAsync(criteria);

                        tblBuildingMaintenanceOldDTO.ChangeTypeId = tblBuildingMaintenanceDTO.ChangeTypeId;
                        tblBuildingMaintenanceOldDTO.Cost = tblBuildingMaintenanceDTO.Cost;
                        tblBuildingMaintenanceOldDTO.YearAcq = tblBuildingMaintenanceDTO.YearAcq;
                        tblBuildingMaintenanceOldDTO.MonthAcq = tblBuildingMaintenanceDTO.MonthAcq;
                        tblBuildingMaintenanceOldDTO.Description = tblBuildingMaintenanceDTO.Description;

                        tblBuildingMaintenanceOldDTO.Cuser = BaseUserId;
                        tblBuildingMaintenanceOldDTO.CdateTime = DateTime.Now;
                        tblBuildingMaintenanceOldDTO = await _BuildingBusiness.AddUpdateBuildingMaintenanceAsync(tblBuildingMaintenanceOldDTO);
                        Message = "Building Maintenance updated successsfully.";
                    }
                    else
                    {
                        tblBuildingMaintenanceDTO.Auser = BaseUserId;
                        tblBuildingMaintenanceDTO.AdateTime = DateTime.Now;
                        tblBuildingMaintenanceDTO.BuildingId = tblBuildingMaintenanceDTO.BuildingId;
                        tblBuildingMaintenanceDTO = await _BuildingBusiness.AddUpdateBuildingMaintenanceAsync(tblBuildingMaintenanceDTO);
                        Message = "Building Maintenance added successsfully.";
                    }

                    Status = true;
                }
                else
                {
                    Status = false;
                    Message = "You can not add changes.";
                }
            }
            catch (Exception ex)
            {
                Status = false;
                Data = ex.Message;
                Message = "There is some issue while adding changes.";
            }

            return Json(new
            {
                Status = Status,
                Message = Message,
                Data = Data
            });
        }

        [HttpPost]
        public async Task<JsonResult> AddMaintanance(TblBuildingMaintenanceDTO tblBuildingMaintenanceDTO)
        {
            bool Status = false;
            string Message = "Building maintanance added successsfully.", Data = "";
            try
            {
                if (BaseReadOnly == false)
                {
                    if (tblBuildingMaintenanceDTO.BuildingMaintenanceId > 0)
                    {
                        tblBuildingMaintenanceDTO.Cuser = BaseUserId;
                        tblBuildingMaintenanceDTO.CdateTime = DateTime.Now;
                    }
                    else
                    {
                        tblBuildingMaintenanceDTO.Auser = BaseUserId;
                        tblBuildingMaintenanceDTO.AdateTime = DateTime.Now;
                    }
                    tblBuildingMaintenanceDTO = await _BuildingBusiness.AddUpdateBuildingMaintenanceAsync(tblBuildingMaintenanceDTO);
                    Status = true;
                }
                else
                {
                    Status = false;
                    Message = "You can not add maintanance.";
                }
            }
            catch (Exception ex)
            {
                Status = false;
                Data = ex.Message;
                Message = "There is some issue while adding maintanance.";
            }

            return Json(new
            {
                Status = Status,
                Message = Message,
                Data = Data
            });
        }

        [HttpGet]
        public async Task<JsonResult> GetBuildingTransaction()
        {
            var lstBuildingTransactions = await _CommonBusiness.GetTransaction(new TransactionsSearchCritria { ClientId = BaseClientId, superadmin = (int)transactionsType.Building });
            return Json(new
            {
                Status = "",
                Message = "",
                Data = lstBuildingTransactions
            });
        }

        [HttpPost]
        public async Task<JsonResult> DeleteBuildingChanges(int BuildingChangesId)
        {
            bool Status = false;
            string Message = "", Data = "";
            try
            {

                if (await _BuildingBusiness.DeleteBuildingChanges(BuildingChangesId))
                {
                    Status = true;
                    Message = "Record deleted successfully.";
                }
                else
                {
                    Message = "There is some issue while delete users client. Please contact administrator.";
                }
            }
            catch (Exception ex)
            {
                Status = false;
                Data = ex.Message;
                Message = "There is some issue, Please try again after sometime.";
            }

            return Json(new
            {
                Status = Status,
                Message = Message,
                Data = Data
            });
        }

        [HttpPost]
        public async Task<JsonResult> DeleteBuildingMaintanance(int BuildingMaintenanceId)
        {
            bool Status = false;
            string Message = "", Data = "";
            try
            {

                if (await _BuildingBusiness.DeleteBuildingMaintanance(BuildingMaintenanceId))
                {
                    Status = true;
                    Message = "Record deleted successfully.";
                }
                else
                {
                    Message = "There is some issue while delete users client. Please contact administrator.";
                }
            }
            catch (Exception ex)
            {
                Status = false;
                Data = ex.Message;
                Message = "There is some issue, Please try again after sometime.";
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
