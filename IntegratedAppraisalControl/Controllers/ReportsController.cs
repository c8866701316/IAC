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
    public class ReportsController : BaseController
    {
        private readonly IInventoryBusiness _inventoryBusiness;
        private readonly IDepartmentBusiness _DepartmentBusiness;
        private readonly IRoomsBusiness _roomsBusiness;
        private readonly IClientBusiness _clientBusiness;

        public ReportsController(IInventoryBusiness inventoryBusiness,
            IRoomsBusiness roomsBusiness,
            IDepartmentBusiness departmentBusiness, IClientBusiness clientBusiness)
        {
            _inventoryBusiness = inventoryBusiness;
            _DepartmentBusiness = departmentBusiness;
            _roomsBusiness = roomsBusiness;
            _clientBusiness = clientBusiness;
        }


        public async Task<IActionResult> Index()
        {
            TblClientsDTO clie = await _clientBusiness.GeClientDetails(new ClientSearchCriteriaModel { ClientId = BaseClientId });
            if(clie != null)
            {
                ViewBag.ClientAccYear = clie.AccountingYear;
                ViewBag.FirstYearDepreciation = clie.FirstYearDepreciationD;
            }
            else
            {
                ViewBag.ClientAccYear = "2019";
                ViewBag.FirstYearDepreciation = "10";
            }
            return View();
        }

        public async Task<JsonResult> GetReportsData()
        {
            InventorySearchCriteria invCriteria = new InventorySearchCriteria { ClientID = BaseClientId };
            DepartmentSearchCriteria deptCriteria = new DepartmentSearchCriteria { ClientID = BaseClientId };
            RoomsearchCriteria roomCriteria = new RoomsearchCriteria { ClientID = BaseClientId };
            List<TblAssetClassDTO> lstAssetClass = await _inventoryBusiness.GetAssetClassList(invCriteria);
            List<TblDepartmentsDTO> lstDepartment = await _DepartmentBusiness.GetDepartmentList(deptCriteria);
            List<TblRoomsDTO> lstRooms = await _roomsBusiness.GetRoomsList(roomCriteria);
            List<TblInventoryDTO> lstInventory = await _inventoryBusiness.GetInventoryList(invCriteria);

            //TblClientsDTO clie = await _clientBusiness.GeClientDetails(new ClientSearchCriteriaModel { ClientId = BaseClientId });
            //var ClieAccountingYear = clie.AccountingYear;

            return Json(new
            {
                lstAssetClass = lstAssetClass.Select(data =>
                new
                {
                    ClassCode = data.ClassCode,
                    AssetClassName = data.AssetClassName
                }),

                RoomList = lstRooms.Select(data =>
                    new
                    {
                        BuildingCode = data.BuildingCode,
                        RoomCode = data.RoomCode,
                        RoomDescription = data.RoomDescription,
                        MarkForDeletion = data.MarkForDeletion.HasValue ? data.MarkForDeletion.Value : false,
                        RoomId = data.RoomId
                    }),

                DepartmentList = lstDepartment.Select(data =>
                new
                {
                    DepartmentId = data.DepartmentId,
                    DepartmentCode = data.DepartmentCode,
                    DepartmentName = data.DepartmentName,
                    MarkForDeletion = data.MarkForDeletion.HasValue ? data.MarkForDeletion.Value : false
                }),
            });
        }

        public async Task<JsonResult> GetReportsDataForInventoryDetails(string ClientYear)
        {
            List<TblInventoryDTO> lstInventory = await _inventoryBusiness.GetInventoryList(new InventorySearchCriteria { ClientID = BaseClientId });

            TblClientsDTO clie = await _clientBusiness.GeClientDetails(new ClientSearchCriteriaModel { ClientId = BaseClientId });
            string AccountingYear = clie.AccountingYear;
            string FirstYearDep = Convert.ToString(clie.FirstYearDepreciationText).Trim();

            return Json(new
            {
                ProjectionsList = lstInventory.Select(data =>
                new
                {
                    Tag = data.Tag,
                    BuildingCode = data.BuildingCode,
                    DepartmentCode = data.DepartmentCode,
                    AssetClassCode = data.AssetClassCode,
                    RoomCode = data.RoomCode,
                    Floor = data.Floor,
                    Qty = data.Qty,
                    Descr = data.Descr,
                    Month = data.Month,
                    Year = data.Year,
                    ReplacementValue = data.Crn,
                    AcqCost = data.Cost,
                    AccumDepreciation = "",
                    SalvageValue = data.SalvageValue,
                    PO = data.Pono,
                    Fund = data.Fund,
                    deps = GetCalFields(Convert.ToInt64(data.Life), Convert.ToInt64(data.Cost), Convert.ToInt64(data.SalvageValue), ClientYear, AccountingYear, FirstYearDep, data.AssetClassCode)
                })
            });
        }

        private dep GetCalFields(long ItemLife, long ItemCost, long ItemSalvage, string ItemYear, string ItemRy_lookup, string ItemFyd_lookup, string assetClass)
        {
            dep dp = new dep();
            assetClass = string.IsNullOrEmpty(assetClass) ? "" : assetClass;
            long lim = (ItemLife * 12); //Life In Months
            double bfd = (ItemCost - ItemSalvage); //Basis for Depreciation
            double mr = (lim > 0) ? RoundCorrect((bfd / lim), 4) : 0; //Monthly Rate 
            long yu = ParseCalcLong(ItemRy_lookup) - ParseCalcLong(ItemYear);//Years Used
            long fycm = 6; //First Year Convention Months

            if (ItemFyd_lookup.Trim().ToString() == "Half Year")
            {

                fycm = 6;
            }
            else
            {

                fycm = 0;
            }
            long mu = (yu * 12) + fycm; //Months Used


            long adry = (long)RoundCorrect((mr * mu), 0); // Accumulated Depreciation Reporting Year

            //If Past Life Accumulated Depreciation Reporting Year is Basis for Depreciation
            if (lim == 0)
            {
                adry = (long)bfd;
            }
            if (mu >= lim)
            {
                adry = (long)bfd;
            }
            long adly = (mu > 12) ? (long)RoundCorrect(((mu - 12) * mr), 0) : 0; // Accumulated Depreciation Last Year


            long adny = (long)((mu + 12) * mr);  // Accumulated Depreciation Next Year

            long cd = ((adry - adly) > 0) ? (adry - adly) : 0; // Current Depreciation

            long pd = adny - adry; // Projected Depreciation

            long ud = (long)(bfd - adry);// Underappreciated value

            if (adry >= bfd)
            {
                ud = 0;
            }
            if (mu == 0)
            {
                ud = 0;
            }
            if (mu > lim)
            {
                ud = 0;
            }
            List<Tuple<string, long>> vals = new List<Tuple<string, long>>();
            if (assetClass.CompareTo("01") == 0)
            {
                adry = 0;
                ud = 0;
                cd = 0;
            }
            if(adry < 0)
            {
                adry = 0;
            }
            if(ud < 0)
            {
                ud = 0;
            }
            if(cd < 0)
            {
                cd = 0;
            }

            dp.AccumulatedDepreciation = adry;
            dp.UnderappreciatedValue = ud;
            dp.CurrentDepreciation = cd;
            return dp;
        }
        private long ParseCalcLong(string test)
        {
            long ret;
            if (long.TryParse(test, out ret) == true)
            {
                return ret;
            }
            else
            {
                return 0;
            }
        }
        public double RoundCorrect(double d, int decimals)
        {
            double multiplier = Math.Pow(10, decimals);

            if (d < 0)
                multiplier *= -1;

            return Math.Floor((d * multiplier) + 0.5) / multiplier;

        }

        public class dep
        {
            public long AccumulatedDepreciation { get; set; }
            public long UnderappreciatedValue { get; set; }
            public long CurrentDepreciation { get; set; }
        }
    }
}