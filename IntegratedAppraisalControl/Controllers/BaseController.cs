using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IntegratedAppraisalControl.Classes;

namespace IntegratedAppraisalControl.Controllers
{
    public class BaseController : Controller
    {
        
        string _FirstName = "";
        public string BaseFirstName
        {
            get { return _FirstName; }
        }
        string _LastName = "";
        public string BaseLastName
        {
            get { return _LastName; }
        }
        bool _SuperAdmin = false;
        public bool BaseSuperAdmin
        {
            get { return _SuperAdmin; }
        }
        bool _ClientAdmin = false;
        public bool BaseClientAdmin
        {
            get { return _ClientAdmin; }
        }
        bool _ReadOnly = false;
        public bool BaseReadOnly
        {
            get { return _ReadOnly; }
        }

        int _UserId = 0;
        public int BaseUserId
        {
            get { return _UserId; }
        }

        int _PageSize = 0;
        public int BasePageSize
        {
            get { return _PageSize; }
        }
        int _ClientId = 0;
        public int BaseClientId
        {
            get { return _ClientId; }
        }

        string _ClientName = "";
        public string BaseClientName
        {
            get { return _ClientName; }
        }

        string _FileName = "";
        public string BaseFileName
        {
            get { return _FileName; }
        }

        bool _IsLocationChangeAllowed = false;
        public bool BaseIsLocationChangeAllowed
        {
            get { return _IsLocationChangeAllowed; }
        }

        public override void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext filterContext)
        {
            try
            {
                _FirstName = IdentityExtensions.GetFirstName(User.Identity);
                _LastName = IdentityExtensions.GetLastName(User.Identity);
                _ReadOnly = IdentityExtensions.GetReadOnly(User.Identity);
                _SuperAdmin = IdentityExtensions.GetSuperAdmin(User.Identity);
                _ClientAdmin = IdentityExtensions.GetClientAdmin(User.Identity);
                _UserId = IdentityExtensions.GetUserId(User.Identity);
                _ClientId = IdentityExtensions.GetClientId(User.Identity);
                _FileName = IdentityExtensions.GetClientFileName(User.Identity);
                _IsLocationChangeAllowed = IdentityExtensions.GetIsLocationChangeAllowed(User.Identity);
                _ClientName = IdentityExtensions.GetClientName(User.Identity);
                _PageSize = 10;

                ViewBag.BaseFirstName = _FirstName;
                ViewBag.BaseLastName = _LastName;
                ViewBag.BaseReadOnly = _ReadOnly;
                ViewBag.BaseSuperAdmin = _SuperAdmin;
                ViewBag.BaseClientAdmin = _ClientAdmin;
                ViewBag.BaseUserId = _UserId;
                ViewBag.BaseUserId = _UserId;
                ViewBag.BasePageSize = _PageSize;
                ViewBag.BaseClientId = _ClientId;
                ViewBag.BaseFileName = _FileName;
                ViewBag.BaseClientName = _ClientName;
                ViewBag.BaseIsLocationChangeAllowed = _IsLocationChangeAllowed;

                base.OnActionExecuting(filterContext);
            }
            catch (Exception)
            {
                RedirectToAction("Login", "Account");
            }
        }
    }
}
