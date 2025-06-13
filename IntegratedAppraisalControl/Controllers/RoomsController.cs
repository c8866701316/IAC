using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IntegratedAppraisalControl.Models;
using IntegratedAppraisalControl.Models.DTO;
using IntegratedAppraisalControl.Business;
using Microsoft.AspNetCore.Hosting;

namespace IntegratedAppraisalControl.Controllers
{
    [Authorize]
    public class RoomsController : BaseController
    {
        private readonly IRoomsBusiness _roomsBusiness;
        private readonly IClientBusiness _ClientBusiness;
        private IHostingEnvironment _hostingEnvironment;
        private readonly IBuildingBusiness _BuildingBusiness;
        public RoomsController(
            IRoomsBusiness roomsBusiness,
            IBuildingBusiness buildingBusiness,
            IClientBusiness clientBusiness,
            IHostingEnvironment environment
        )
        {
            _roomsBusiness = roomsBusiness;
            _BuildingBusiness = buildingBusiness;
            _hostingEnvironment = environment;
            _ClientBusiness = clientBusiness;

        }

        public async Task<IActionResult> Index()
        {
            return View(new TblRoomsDTO());
        }

        [HttpGet]
        public async Task<JsonResult> RoomList()
        {
            RoomsearchCriteria criteria = new
            RoomsearchCriteria()
            {
                ClientID = BaseClientId,
                IsSuperAdmin = BaseSuperAdmin,
                IsClientAdmin = BaseClientAdmin
            };
            var RoomList = await _roomsBusiness.GetRoomsList(criteria);

            return Json(new
            {
                data =
                RoomList.Select(data =>
                    new
                    {
                        BuildingCode = data.BuildingCode,
                        RoomCode = data.RoomCode,
                        RoomDescription = data.RoomDescription,
                        MarkForDeletion = data.MarkForDeletion.HasValue ? data.MarkForDeletion.Value : false,
                        RoomId = data.RoomId
                    })
            });
        }

        [HttpGet]
        public async Task<ActionResult> _Room(int RoomId)
        {
            TblRoomsDTO tblRooms = new TblRoomsDTO();
            if (RoomId != 0)
            {
                RoomsearchCriteria criteria = new
               RoomsearchCriteria()
                {
                    ClientID = BaseClientId,
                    IsSuperAdmin = BaseSuperAdmin,
                    IsClientAdmin = BaseClientAdmin,
                    RoomId = RoomId
                };
                tblRooms = await _roomsBusiness.GetRooms(criteria);
            }

            ViewBag.listBuildings = await _BuildingBusiness.GetBuildingListBySearchCriteriaAsync(
              new BuildingSearchCriteriaModel()
              {
                  ClientID = BaseClientId,
                  IsSuperAdmin = BaseSuperAdmin
              });

            if (tblRooms == null)
            {
                tblRooms = new TblRoomsDTO();
            }

            if(tblRooms.RoomId == 0)
            {
                TblClientsDTO tblClientsDTO =  await _ClientBusiness.GeClientDetails(new ClientSearchCriteriaModel { ClientId = BaseClientId });
                if(tblClientsDTO.NextRoomNumber > 0)
                {
                    tblRooms.RoomCode = Convert.ToString(tblClientsDTO.NextRoomNumber);
                }
                else
                {
                    tblRooms.RoomCode = "9000";
                }
            }
            return PartialView(tblRooms);
        }
        [HttpPost]
        public async Task<JsonResult> AddUpdate(TblRoomsDTO tblRooms, bool IsDuplicate)
        {
            bool Status = false;
            string Message = "", Data = "";
            try
            {
                RoomsearchCriteria criteria = new
                    RoomsearchCriteria()
                {
                    ClientID = BaseClientId,
                    IsSuperAdmin = BaseSuperAdmin,
                    IsClientAdmin = BaseClientAdmin,
                    RoomId = tblRooms.RoomId,
                    RoomCode = tblRooms.RoomCode,
                    BuildingId = Convert.ToInt32(tblRooms.BuildingId)
                };

                if (Convert.ToBoolean(IsDuplicate))
                {
                    criteria.RoomId = 0;
                }

                if (await _roomsBusiness.CheckRoomsCodeExistance(criteria))
                {
                    Status = false;
                    Message = "Please check Room code. Room code is duplicate.";
                }
                else
                {
                    if (tblRooms.RoomId > 0)
                    {
                        criteria.RoomId = tblRooms.RoomId;
                        var tblRoomsOld = await _roomsBusiness.GetRooms(criteria);
                        if (IsDuplicate)
                        {
                            tblRoomsOld.RoomId = 0;
                        }
                        tblRoomsOld.ClientId = BaseClientId;
                        tblRoomsOld.RoomCode = tblRooms.RoomCode;
                        tblRoomsOld.RoomDescription = tblRooms.RoomDescription;
                        tblRoomsOld.MarkForDeletion = tblRooms.MarkForDeletion;


                        tblRoomsOld = await _roomsBusiness.AddUpdateRooms(tblRoomsOld);

                        if (IsDuplicate)
                        {
                            Message = "Record duplicated successfully. You will find it on listing.";
                        }
                        else
                        {
                            Message = "Record updated successfully.";
                        }

                        if (Convert.ToBoolean(tblRooms.MarkForDeletion))
                        {
                            Message = "Record marked as deleted.";
                        }
                    }
                    else
                    {
                        Message = "Record added successfully.";
                        tblRooms.ClientId = BaseClientId;
                        tblRooms.Deleted = false;
                        tblRooms.MarkForDeletion = false;

                        tblRooms = await _roomsBusiness.AddUpdateRooms(tblRooms);
                    }


                    Status = true;
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
    }
}
