using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using IntegratedAppraisalControl.Classes;
using IntegratedAppraisalControl.Business;
using IntegratedAppraisalControl.Models.DTO;

namespace IntegratedAppraisalControl.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserBusiness _userBusiness;
        public AccountController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginData loginData)
        {
            if (ModelState.IsValid)
            {
                TblUsersDTO tbl = _userBusiness.ValidateActiveUser(loginData.Username.Trim(), loginData.Password);
                if(tbl != null)
                {
                    var claims = CustomIdentity.GetClaimsPrincipal(tbl);
                    
                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.SignInAsync(principal);
                    return RedirectToAction("ClientSelection", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Username and password does not match.");
                    return View();
                }

            }
            else
            {
                ModelState.AddModelError("", "username or password is blank");
                return View();
            }
        }
        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
    public class LoginData
        {
            [Required]
            public string Username { get; set; }

            [Required, DataType(DataType.Password)]
            public string Password { get; set; }

            public bool RememberMe { get; set; }
        }
}
