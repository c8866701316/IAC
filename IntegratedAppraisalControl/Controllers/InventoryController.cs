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
    public class InventoryController : BaseController
    {
        private readonly IClientBusiness _ClientBusiness;
        private readonly IUserBusiness _UserBusiness;
        private readonly IBuildingBusiness _BuildingBusiness;
        private readonly ICommonBusiness _CommonBusiness;
        private readonly IInventoryBusiness _inventoryBusiness;
        private IHostingEnvironment _hostingEnvironment;
        public InventoryController(
            IClientBusiness clientBusiness,
            IUserBusiness userBusiness,
            IBuildingBusiness buildingBusiness,
            ICommonBusiness commonBusiness,
            IInventoryBusiness inventoryBusiness,
            IHostingEnvironment environment
            )
        {
            _ClientBusiness = clientBusiness;
            _UserBusiness = userBusiness;
            _BuildingBusiness = buildingBusiness;
            _CommonBusiness = commonBusiness;
            _inventoryBusiness = inventoryBusiness;
            _hostingEnvironment = environment;
        }

        public async Task<IActionResult> Index()
        {
            InventorySearchCriteria criteria = new
            InventorySearchCriteria()
            {
                InventoryId = 0,
                ClientID = BaseClientId,
                IsSuperAdmin = BaseSuperAdmin,
                IsClientAdmin = BaseClientAdmin
            };

            ViewBag.listBuildings = await _BuildingBusiness.GetBuildingListDDlBySearchCriteriaAsync(
               new BuildingSearchCriteriaModel()
               {
                   ClientID = BaseClientId,
                   IsSuperAdmin = BaseSuperAdmin
               });

            ViewBag.lstAssetClass = await _inventoryBusiness.GetAssetClassListDDL(criteria);
            ViewBag.lstRooms = await _inventoryBusiness.GetRoomsListDDL(criteria);
            ViewBag.lstDepartments = await _inventoryBusiness.GetDepartmentListDDL(criteria);
            return View(new TblInventoryDTO());
        }

        [HttpGet]
        public async Task<JsonResult> InventoryList()
        {
            var InventoryList = await _inventoryBusiness.GetInventoryList(
                new InventorySearchCriteria()
                {
                    ClientID = BaseClientId,
                    IsSuperAdmin = BaseSuperAdmin,
                    IsClientAdmin = BaseClientAdmin
                });


            return Json(new
            {
                data =
                InventoryList.Select(data =>
                    new
                    {
                        Tag = data.Tag,
                        BuildingCode = data.BuildingCode,
                        Floor = data.Floor,
                        RoomCode = data.RoomCode,
                        DepartmentCode = data.DepartmentCode,
                        AssetClassCode = data.AssetClassCode,
                        Qty = data.Qty,
                        Descr = data.Descr,
                        Pono = data.Pono,
                        Crn = data.Crn,
                        SoundValue = data.SoundValue,
                        Cost = data.Cost,
                        Month = data.Month,
                        Year = data.Year,
                        SerNo = data.SerNo,
                        Mfg = data.Mfg,
                        Model = data.Model,
                        InventoryId = data.InventoryId,
                    })
            });
        }

        [HttpGet]
        public async Task<JsonResult> GetRoomListByBuildingId(int BuildingId)
        {
            List<TblRoomsDTO> lstRooms = await _inventoryBusiness.GetRoomsListDDL(new InventorySearchCriteria()
            {
                ClientID = BaseClientId,
                IsSuperAdmin = BaseSuperAdmin,
                IsClientAdmin = BaseClientAdmin,
                BuildingId = BuildingId
            });

            return Json(new
            {
                data = lstRooms.Select(data =>
                    new
                    {
                        Value = data.RoomId,
                        Text = data.RoomDescription,
                    })
            });
        }

        public async Task<IActionResult> Edit(int Id)
        {
            InventorySearchCriteria criteria = new
            InventorySearchCriteria()
            {
                InventoryId = Id,
                ClientID = BaseClientId,
                IsSuperAdmin = BaseSuperAdmin,
                IsClientAdmin = BaseClientAdmin
            };

            InventoryViewModel inventoryViewModel = new InventoryViewModel();
            inventoryViewModel.Inventory = await _inventoryBusiness.GetInventory(criteria);
            if (inventoryViewModel.Inventory == null)
            {
                return View("AccessDenied");
            }

            inventoryViewModel.listBuildings = await _BuildingBusiness.GetBuildingListDDlBySearchCriteriaAsync(
                new BuildingSearchCriteriaModel()
                {
                    ClientID = BaseClientId,
                    IsSuperAdmin = BaseSuperAdmin
                });

            criteria.BuildingId = Convert.ToInt32(inventoryViewModel.Inventory.BuildingId);
            inventoryViewModel.lstAssetClass = await _inventoryBusiness.GetAssetClassListDDL(criteria);
            inventoryViewModel.lstRooms = await _inventoryBusiness.GetRoomsListDDL(criteria);
            inventoryViewModel.lstDepartments = await _inventoryBusiness.GetDepartmentListDDL(criteria);

            inventoryViewModel.lstChangesTypeDTO = await _CommonBusiness.GetChangeTypeSearchCriteriaAsync(new ChangeTypeCriteriaModel() { Active = true, IsSuperAdmin = BaseSuperAdmin });

            return View(inventoryViewModel);
        }
        [HttpPost]
        public async Task<JsonResult> Edit(InventoryViewModel inventoryViewModel, IFormFile file)
        {
            bool Status = false;
            string Message = "", Data = "";
            InventorySearchCriteria criteria = new
            InventorySearchCriteria()
            {
                InventoryId = inventoryViewModel.Inventory.InventoryId,
                ClientID = BaseClientId,
                IsSuperAdmin = BaseSuperAdmin,
                IsClientAdmin = BaseClientAdmin,
                TagNo = inventoryViewModel.Inventory.Tag
            };


            try
            {
                if (Convert.ToBoolean(inventoryViewModel.IsDuplicate))
                {
                    criteria.InventoryId = 0;
                }

                if (!string.IsNullOrEmpty(criteria.TagNo) && await _inventoryBusiness.CheckInventoryTagExistance(criteria))
                {
                    Status = false;
                    Message = "Please check tag no. Tag no is duplicate.";
                }
                else
                {
                    criteria.InventoryId = inventoryViewModel.Inventory.InventoryId;
                    TblInventoryDTO invOld = await _inventoryBusiness.GetInventory(criteria);

                    if (invOld != null)
                    {
                        invOld.ImageFileName = inventoryViewModel.Inventory.ImageFileName;
                        //if (invOld.ImageFileName != null || invOld.ImageFileName != "")
                        //{
                        if (file != null && file.Length > 0)
                        {
                            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                            string FileName = System.IO.Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.ToFileTime() + System.IO.Path.GetExtension(file.FileName);
                            var filePath = Path.Combine(uploads, FileName);

                            if (!System.IO.Directory.Exists(uploads))
                            {
                                System.IO.Directory.CreateDirectory(uploads);
                            }

                            if (System.IO.File.Exists(filePath))
                                System.IO.File.Delete(filePath);

                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }
                            invOld.ImageFileName = FileName;
                        }
                        //}
                        //else
                        //{
                        //    invOld.ImageFileName = "";
                        //}

                        if (inventoryViewModel.IsDuplicate)
                        {
                            invOld.InventoryId = 0;
                            Message = "Record duplicated. You can find that recoerd in list.";
                        }
                        else
                        {
                            Message = "Record updated.";
                        }

                        if (Convert.ToBoolean(inventoryViewModel.Inventory.MarkForDeletion))
                        {
                            Message = "Record mark as deleted.";
                        }


                        invOld.Tag = inventoryViewModel.Inventory.Tag;
                        invOld.BuildingId = inventoryViewModel.Inventory.BuildingId;
                        invOld.RoomId = inventoryViewModel.Inventory.RoomId;
                        invOld.DeptId = inventoryViewModel.Inventory.DeptId;
                        invOld.RoomId = inventoryViewModel.Inventory.RoomId;
                        invOld.Qty = inventoryViewModel.Inventory.Qty;
                        invOld.Floor = inventoryViewModel.Inventory.Floor;
                        invOld.Descr = inventoryViewModel.Inventory.Descr;
                        invOld.Crn = inventoryViewModel.Inventory.Crn;
                        invOld.SoundValue = inventoryViewModel.Inventory.SoundValue;
                        invOld.Cost = inventoryViewModel.Inventory.Cost;
                        invOld.SalvageValue = inventoryViewModel.Inventory.SalvageValue;
                        invOld.Month = inventoryViewModel.Inventory.Month;
                        invOld.Year = inventoryViewModel.Inventory.Year;
                        invOld.Life = inventoryViewModel.Inventory.Life;
                        invOld.SerNo = inventoryViewModel.Inventory.SerNo;
                        invOld.Mfg = inventoryViewModel.Inventory.Mfg;
                        invOld.Sdf1 = inventoryViewModel.Inventory.Sdf1;
                        invOld.Sdf2 = inventoryViewModel.Inventory.Sdf2;
                        invOld.Sdf3 = inventoryViewModel.Inventory.Sdf3;
                        invOld.Sdf4 = inventoryViewModel.Inventory.Sdf4;
                        invOld.Model = inventoryViewModel.Inventory.Model;
                        invOld.Pono = inventoryViewModel.Inventory.Pono;
                        invOld.Fund = inventoryViewModel.Inventory.Fund;
                        invOld.Funct = inventoryViewModel.Inventory.Funct;
                        invOld.AssetClassId = inventoryViewModel.Inventory.AssetClassId;
                        invOld.MarkForDeletion = inventoryViewModel.Inventory.MarkForDeletion;
                        invOld = await _inventoryBusiness.AddUpdateInventory(invOld);
                        Status = true;

                    }
                    else
                    {
                        Status = false;
                        Message = "There is some issue. Please try again after sometime.";
                        Data = "No inventory found with that id and client.";
                    }
                }
            }
            catch (Exception ex)
            {
                Message = "There is some issue. Please try again after sometime.";
                Status = false;
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
        public async Task<JsonResult> Add(TblInventoryDTO inventory, IFormFile file)
        {
            bool Status = false;
            string Message = "", Data = "";
            InventorySearchCriteria criteria = new
            InventorySearchCriteria()
            {
                InventoryId = 0,
                ClientID = BaseClientId,
                IsSuperAdmin = BaseSuperAdmin,
                IsClientAdmin = BaseClientAdmin,
                TagNo = inventory.Tag
            };

            try
            {
                if (!string.IsNullOrEmpty(criteria.TagNo) && await _inventoryBusiness.CheckInventoryTagExistance(criteria))
                {
                    Status = false;
                    Message = "Please check tag no. Tag no is duplicate.";
                }
                else
                {
                    if (!BaseReadOnly)
                    {
                        if (file != null && file.Length > 0)
                        {
                            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                            string FileName = System.IO.Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.ToFileTime() + System.IO.Path.GetExtension(file.FileName);
                            var filePath = Path.Combine(uploads, FileName);

                            if (!System.IO.Directory.Exists(uploads))
                            {
                                System.IO.Directory.CreateDirectory(uploads);
                            }

                            if (System.IO.File.Exists(filePath))
                                System.IO.File.Delete(filePath);

                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }
                            inventory.ImageFileName = FileName;
                        }

                        inventory.Crn = inventory.Cost;
                        inventory.SoundValue = inventory.Cost;
                        inventory.SalvageValue = inventory.Cost;


                        inventory.MarkForDeletion = false;
                        inventory.Deletion = false;
                        inventory.ClientId = BaseClientId;

                        inventory = await _inventoryBusiness.AddUpdateInventory(inventory);
                        Status = true;
                        Message = "Record inserted.";
                    }
                    else
                    {
                        Status = false;
                        Message = "You have readonly permission.";
                    }
                }
            }
            catch (Exception ex)
            {
                Status = false;
                Data = ex.Message;
            }

            return Json(new
            {
                Status = Status,
                Message = Message,
                Data = Data
            });
        }

        [HttpGet]
        public async Task<JsonResult> InventoryDepartmentList(int InventoryId = 0)
        {
            InventorySearchCriteria criteria = new
            InventorySearchCriteria()
            {
                ClientID = BaseClientId,
                IsSuperAdmin = BaseSuperAdmin,
                IsClientAdmin = BaseClientAdmin
            };
            var DepartmentList = await _inventoryBusiness.GetDepartmentList(criteria);

            return Json(new
            {
                data =
                DepartmentList.Select(data =>
                    new
                    {
                        DepartmentCode = data.DepartmentCode,
                        DepartmentName = data.DepartmentName,
                        MarkForDeletion = data.MarkForDeletion.HasValue ? data.MarkForDeletion.Value : false
                    })
            });
        }

        [HttpGet]
        public async Task<JsonResult> InventoryMaintananceList(int InventoryId = 0)
        {
            InventorySearchCriteria criteria = new
            InventorySearchCriteria()
            {
                ClientID = BaseClientId,
                IsSuperAdmin = BaseSuperAdmin,
                IsClientAdmin = BaseClientAdmin
            };
            var InventoryMaintananceList = await _inventoryBusiness.GetInventoryMaintanance(criteria);

            return Json(new
            {
                data =
                InventoryMaintananceList.Select(data =>
                    new
                    {
                        data.InventoryMaintenanceId,
                        data.InventoryId,
                        data.ChangeTypeId,
                        data.Description,
                        data.MonthAcq,
                        data.YearAcq,
                        data.Cost,
                        data.Auser,
                        data.AdateTime,
                        data.Cuser,
                        data.CdateTime,
                        data.ChangeType
                    })
            });
        }

        [HttpPost]
        public async Task<JsonResult> AddChange(TblInventoryMaintenanceDTO tblInventoryMaintenanceDTO)
        {
            bool Status = false;
            string Message = "Building changes added successsfully.", Data = "";
            try
            {
                if (BaseReadOnly == false)
                {



                    if (tblInventoryMaintenanceDTO.InventoryMaintenanceId > 0)
                    {
                        InventoryMaintenanceSearchCriteria criteria = new
               InventoryMaintenanceSearchCriteria()
                        {
                            InventoryMaintenanceID = tblInventoryMaintenanceDTO.InventoryMaintenanceId
                        };
                        TblInventoryMaintenanceDTO tblInventoryMaintenanceOldDTO = await _ClientBusiness.GetInventoryMaintenance(criteria);

                        tblInventoryMaintenanceOldDTO.ChangeTypeId = tblInventoryMaintenanceDTO.ChangeTypeId;
                        tblInventoryMaintenanceOldDTO.Cost = tblInventoryMaintenanceDTO.Cost;
                        tblInventoryMaintenanceOldDTO.YearAcq = tblInventoryMaintenanceDTO.YearAcq;
                        tblInventoryMaintenanceOldDTO.MonthAcq = tblInventoryMaintenanceDTO.MonthAcq;
                        tblInventoryMaintenanceOldDTO.Description = tblInventoryMaintenanceDTO.Description;

                        tblInventoryMaintenanceOldDTO.Cuser = BaseUserId;
                        tblInventoryMaintenanceOldDTO.CdateTime = DateTime.Now;
                        Status = await _inventoryBusiness.AddInventoryMaintenanceChangesAsync(tblInventoryMaintenanceOldDTO);
                        Message = "Inventory maintanance updated successsfully.";
                    }
                    else
                    {
                        tblInventoryMaintenanceDTO.Auser = BaseUserId;
                        tblInventoryMaintenanceDTO.AdateTime = DateTime.Now;
                        tblInventoryMaintenanceDTO.InventoryId = tblInventoryMaintenanceDTO.InventoryId;

                        Status = await _inventoryBusiness.AddInventoryMaintenanceChangesAsync(tblInventoryMaintenanceDTO);
                        Message = "Inventory maintanance added successfully.";
                    }
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

        [HttpGet]
        public async Task<ActionResult> _AddEditInventoryMaintenance(int MaintananceId)
        {
            TblInventoryMaintenanceDTO tblInventoryMaintenance = new TblInventoryMaintenanceDTO();
            if (MaintananceId != 0)
            {
                InventoryMaintenanceSearchCriteria criteria = new
                InventoryMaintenanceSearchCriteria()
                {
                    InventoryMaintenanceID = MaintananceId
                };
                tblInventoryMaintenance = await _ClientBusiness.GetInventoryMaintenance(criteria);
            }

            if (tblInventoryMaintenance == null)
            {
                tblInventoryMaintenance = new TblInventoryMaintenanceDTO();
            }
            InventoryViewModel inventoryViewModel = new InventoryViewModel();
            inventoryViewModel.lstChangesTypeDTO = await _CommonBusiness.GetChangeTypeSearchCriteriaAsync(new ChangeTypeCriteriaModel() { Active = true, IsSuperAdmin = BaseSuperAdmin });
            inventoryViewModel.objtblInventoryMaintenanceDTO = tblInventoryMaintenance;
            return PartialView(inventoryViewModel);
        }

        [HttpGet]
        public async Task<JsonResult> GetInventoryTransaction()
        {
            var lstInventoryTransactions = await _CommonBusiness.GetTransaction(new TransactionsSearchCritria { ClientId = BaseClientId, superadmin = (int)transactionsType.Inventory });
            return Json(new
            {
                Status = "",
                Message = "",
                Data = lstInventoryTransactions
            });
        }

        [HttpPost]
        public async Task<JsonResult> DeleteInventoryMaintanance(int MaintananceId)
        {
            bool Status = false;
            string Message = "", Data = "";
            try
            {

                if (await _inventoryBusiness.DeleteInventoryMaintenance(MaintananceId))
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
