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
    public class TransactionController : BaseController
    {
        private readonly IInventoryBusiness _inventoryBusiness;

        public TransactionController(IInventoryBusiness inventoryBusiness,
            IBuildingBusiness buildingBusiness)
        {
            _inventoryBusiness = inventoryBusiness;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}