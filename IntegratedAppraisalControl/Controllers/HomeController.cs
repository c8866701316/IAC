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
    public class HomeController : BaseController
    {
        private readonly IClientBusiness _ClientBusiness;
        private readonly IUserBusiness _UserBusiness;
        private readonly IBuildingBusiness _BuildingBusiness;
        public HomeController(
            IClientBusiness clientBusiness,
            IUserBusiness userBusiness,
            IBuildingBusiness buildingBusiness
            )
        {
            _ClientBusiness = clientBusiness;
            _UserBusiness = userBusiness;
            _BuildingBusiness = buildingBusiness;
        }

        public async Task<IActionResult> ClientSelection()
        {
            //if (BaseSuperAdmin) { }

            var ClienList = await _ClientBusiness.GetClientListBySearchCriteriaAsync(new ClientSearchCriteriaModel() { UserId = BaseUserId , IsSuperAdmin  = BaseSuperAdmin});

            if(BaseSuperAdmin == false && ClienList.Count() == 1)
            {
                return RedirectToAction("SelectClient", new { ClientId = ClienList.FirstOrDefault().ClientId });
            }
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> ClientList()
        {
            var ClienList = await _ClientBusiness.GetClientListBySearchCriteriaAsync(new ClientSearchCriteriaModel() { UserId = BaseUserId, IsSuperAdmin = BaseSuperAdmin });

            //var clientDto = new ClientDto
            //{
            //    searchCriteria = new ClientSearchCriteriaModel(),
            //    data = PaginatedList<TblClientsDTO>.Create(ClienList, 1, BasePageSize)
            //};
            return Json(new
            {




                data = ClienList.Select(data =>
                    new {
                        ClientId = data.ClientId,
                        ClientName = data.ClientName,
                        FileNo = data.FileNo,
                        Address1 = data.Address1,
                        Address2 = data.Address2,
                        City = data.City,
                        State = data.State,
                        ZipCode = data.ZipCode,
                        ReportYear = data.ReportYear
                    })
            });
        }
        
        public async Task<IActionResult> SelectClient(int ClientId)
        {
            try
            {
                TblUsersDTO tbl = _UserBusiness.FindUser(BaseUserId);
                if (tbl != null)
                {
                    var claims = CustomIdentity.GetClaimsPrincipal(tbl);
                    if (Convert.ToBoolean(ClientId))
                    {
                        claims.Add(new Claim(CustomClaimTypes.ClientId, ClientId.ToString()));
                    }

                    IEnumerable<TblClientsDTO> clientList= await _ClientBusiness.GetClientListBySearchCriteriaAsync(new ClientSearchCriteriaModel() { UserId = BaseUserId, IsSuperAdmin = BaseSuperAdmin });
                    TblClientsDTO tblClientsDTO = await _ClientBusiness.GeClientDetails(new ClientSearchCriteriaModel { ClientId = ClientId });

                    if (clientList != null && clientList.Count() > 1)
                    {
                        claims.Add(new Claim(CustomClaimTypes.IsLocationChangeAllowed, "True"));
                    }
                    else
                    {
                        claims.Add(new Claim(CustomClaimTypes.IsLocationChangeAllowed, "False"));
                    }

                    if (tblClientsDTO != null)
                    {
                        claims.Add(new Claim(CustomClaimTypes.ClientFileName, Convert.ToString(tblClientsDTO.FileNo)));
                        claims.Add(new Claim(CustomClaimTypes.ClientName,Convert.ToString(tblClientsDTO.ClientName)));
                    }

                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.SignOutAsync();
                    await HttpContext.SignInAsync(principal);
                }
            }
            catch (Exception ex)
            {
                return RedirectToActionPermanent("ClientList"); 
            }
            
            return RedirectToActionPermanent("Index");
        }


        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
