using System;
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
    public class UsersController : BaseController
    {
        private readonly IUserBusiness _UserBusiness;
        private readonly IClientBusiness _ClientBusiness;
        //private readonly IUserBusiness _UserBusiness;
        //private readonly IBuildingBusiness _BuildingBusiness;
        public UsersController(IUserBusiness userBusiness, IClientBusiness clientBusiness)
        {
            _UserBusiness = userBusiness;
            _ClientBusiness = clientBusiness;
        }

        public async Task<IActionResult> Index()
        {
            return View(new TblUsersDTO());
        }

        [HttpGet]
        public async Task<JsonResult> UserList()
        {
            UserSearchCriteria criteria = new
            UserSearchCriteria()
            {
                ClientID = BaseClientId,
                IsSuperAdmin = BaseSuperAdmin,
                IsClientAdmin = BaseClientAdmin
            };
            var UserList = await _UserBusiness.GetUsersList(criteria);

            return Json(new
            {
                data =
                UserList.Select(data =>
                    new
                    {
                        UserName = data.UserName,
                        FirstName = data.FirstName,
                        LastName = data.LastName,
                        MarkForDeletion = data.Deleted.HasValue ? data.MarkForDeletion.Value : false,
                        UserId = data.UserId
                    })
            });
        }

        [HttpGet]
        public async Task<ActionResult> _User(int UserId)
        {
            TblUsersDTO tblUsers = new TblUsersDTO();
            if (UserId != 0)
            {
                UserSearchCriteria criteria = new UserSearchCriteria()
                {
                    ClientID = BaseClientId,
                    IsSuperAdmin = BaseSuperAdmin,
                    IsClientAdmin = BaseClientAdmin,
                    UserId = UserId
                };
                tblUsers = await _UserBusiness.GetUsers(criteria);
            }

            if (tblUsers == null)
            {
                tblUsers = new TblUsersDTO();
            }

            tblUsers.ReadOnly = tblUsers.ReadOnly.HasValue ? tblUsers.ReadOnly.Value : false;
            tblUsers.ClientAdmin = tblUsers.ClientAdmin.HasValue ? tblUsers.ClientAdmin.Value : false;
            tblUsers.SuperAdmin = tblUsers.SuperAdmin.HasValue ? tblUsers.SuperAdmin.Value : false;
            List<TblUsersClientsDTO> ListUsersClientsDTO = await _UserBusiness.GetClientListByUser(UserId);
            ViewBag.ClientUserList = ListUsersClientsDTO.ToList();

            List<TblClientsDTO> ListClientDDL = await _ClientBusiness.GetClientList();
            ListClientDDL.RemoveAll(m => ListUsersClientsDTO.Select(i => i.ClientId).Contains(m.ClientId));

            foreach(TblClientsDTO cl in ListClientDDL)
            {
                cl.ClientName = cl.FileNo + " - " + cl.ClientName;
            }

            ViewBag.ListClientDDL = ListClientDDL;
            return PartialView(tblUsers);
        }

        [HttpPost]
        public async Task<JsonResult> AddUser(TblUsersDTO tblUsers, bool IsDuplicate)
        {
            bool Status = false;
            string Message = "", Data = "";
            try
            {
                UserSearchCriteria criteria = new
                    UserSearchCriteria()
                {
                    ClientID = BaseClientId,
                    IsSuperAdmin = BaseSuperAdmin,
                    IsClientAdmin = BaseClientAdmin,
                    UserId = tblUsers.UserId,
                    UserName = tblUsers.UserName
                };

                if (Convert.ToBoolean(IsDuplicate))
                {
                    criteria.ClientID = 0;
                }

                bool Duplicate = await _UserBusiness.CheckUserNameExistance(criteria);
                if (tblUsers.UserId == 0 && Duplicate)
                {
                    Status = false;
                    Message = "Please check UserName. UserName is duplicate.";
                }
                else
                {
                    if (tblUsers.UserId > 0)
                    {
                        criteria.UserId = tblUsers.UserId;
                        var tblUsersOld = await _UserBusiness.GetUsers(criteria);
                        if (IsDuplicate)
                        {
                            tblUsersOld.UserId = 0;
                        }

                        tblUsersOld.FirstName = tblUsers.FirstName;
                        tblUsersOld.LastName = tblUsers.LastName;
                        tblUsersOld.ReadOnly = tblUsers.ReadOnly;
                        tblUsersOld.ClientAdmin = tblUsers.ClientAdmin;
                        tblUsersOld.SuperAdmin = tblUsers.SuperAdmin;
                        tblUsersOld.MarkForDeletion = tblUsers.MarkForDeletion;

                        if(!string.IsNullOrEmpty(tblUsers.Password))
                        {
                            tblUsersOld.Password= tblUsers.Password;
                        }

                        tblUsersOld = await _UserBusiness.AddUpdateUsers(tblUsersOld);

                        if (IsDuplicate)
                        {
                            Message = "Record duplicated successfully. You will find it on listing.";
                        }
                        else
                        {
                            Message = "Record updated successfully.";
                        }

                        if (Convert.ToBoolean(tblUsers.MarkForDeletion) && IsDuplicate != true)
                        {
                            Message = "Record marked as deleted.";
                        }
                    }
                    else
                    {
                        Message = "Record added successfully.";
                        tblUsers.ClientId = BaseClientId;
                        tblUsers.Deleted = false;
                        tblUsers.MarkForDeletion = false;
                        tblUsers = await _UserBusiness.AddUpdateUsers(tblUsers);
                    }
                    Status = true;
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
        public async Task<JsonResult> DeleteUsersClients(int UsersClientsId)
        {
            bool Status = false;
            string Message = "", Data = "";
            try
            {

                if (await _UserBusiness.DeleteUsersClientsMapping(UsersClientsId))
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
        public async Task<JsonResult> AddUsersClients(TblUsersClientsDTO tblUsersClientsDTO)
        {
            bool Status = false;
            string Message = "", Data = "";
            try
            {
                tblUsersClientsDTO = await _UserBusiness.AddUpdateUsersClientsMapping(tblUsersClientsDTO);
                Status = true;
                Message = "Record added successfully.";
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