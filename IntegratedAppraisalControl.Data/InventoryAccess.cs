using IntegratedAppraisalControl.Data.Models;
using IntegratedAppraisalControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using IntegratedAppraisalControl.Models.DTO;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace IntegratedAppraisalControl.Data
{
    public class InventoryAccess : IInventoryAccess
    {
        private readonly IntegratedAppraisalControlContext _dbContext;
        public InventoryAccess()
        {
            _dbContext = new IntegratedAppraisalControlContext();
        }
        public async Task<TblInventoryDTO> GetInventory(InventorySearchCriteria criteria)
        {
            return await (from inv in _dbContext.TblInventory
                          join clie in _dbContext.TblClients on inv.ClientId equals clie.ClientId

                          join fydep in _dbContext.TblFirstYearDepreciation on clie.FirstYearDepreciationD equals fydep.FirstYearDepreciationId
                            into fydeps
                          from fydep in fydeps.DefaultIfEmpty()

                          where inv.ClientId == criteria.ClientID && inv.InventoryId == criteria.InventoryId
                          select new TblInventoryDTO
                          {
                              InventoryId = inv.InventoryId,
                              ClientId = inv.ClientId,
                              BuildingId = inv.BuildingId,
                              ValuationId = inv.ValuationId,
                              Tag = inv.Tag,
                              RoomId = inv.RoomId,
                              DeptId = inv.DeptId,
                              AssetClassId = inv.AssetClassId,
                              Qty = inv.Qty,
                              Descr = inv.Descr,
                              Pono = inv.Pono,
                              Fund = inv.Fund,
                              Funct = inv.Funct,
                              Crn = inv.Crn,
                              SoundValue = inv.SoundValue,
                              Cost = inv.Cost,
                              SalvageValue = inv.SalvageValue,
                              Size = inv.Size,
                              Month = inv.Month,
                              Year = inv.Year,
                              Depr = inv.Depr,
                              Life = inv.Life,
                              SerNo = inv.SerNo,
                              Mfg = inv.Mfg,
                              Model = inv.Model,
                              ImageFileName = inv.ImageFileName,
                              Comment = inv.Comment,
                              Sdf1 = inv.Sdf1,
                              Sdf2 = inv.Sdf2,
                              MarkForDeletion = inv.MarkForDeletion,
                              Deletion = inv.Deletion,
                              Auser = inv.Auser,
                              AdateTime = inv.AdateTime,
                              Cuser = inv.Cuser,
                              CdateTime = inv.CdateTime,
                              Sdf3 = inv.Sdf3,
                              Sdf4 = inv.Sdf4,
                              Floor = inv.Floor,
                              Duser = inv.Duser,
                              DdateTime = inv.DdateTime,
                              SystemNumber = inv.SystemNumber,
                              Sdf1label = clie.Sdf1label,
                              Sdf2label = clie.Sdf2label,
                              Sdf3label = clie.Sdf3label,
                              Sdf4label = clie.Sdf4label,
                              FirstYearDepreciationText = fydep.FirstYearDepreciationText,
                              ReportYear = clie.ReportYear,
                              Accounting = clie.Accounting
                          }).FirstOrDefaultAsync();
        }
        public async Task<List<TblInventoryDTO>> GetInventoryList(InventorySearchCriteria criteria)
        {
            return await (from inv in _dbContext.TblInventory
                          join buil in _dbContext.TblBuildings on inv.BuildingId equals buil.BuildingId

                          //join cli in _dbContext.TblClients on inv.ClientId equals cli.ClientId

                          join asst in _dbContext.TblAssetClass on inv.AssetClassId equals asst.AssetClassId
                          into assts
                          from asst in assts.DefaultIfEmpty()

                          join dep in _dbContext.TblDepartments on inv.DeptId equals dep.DepartmentId
                          into dept
                          from dep in dept.DefaultIfEmpty()

                          join room in _dbContext.TblRooms on inv.RoomId equals room.RoomId
                          into rooms
                          from room in rooms.DefaultIfEmpty()

                          where inv.ClientId == criteria.ClientID
                          &&
                          (inv.Deletion.HasValue ? inv.Deletion.Value : false) ==
                          ((criteria.IsSuperAdmin || criteria.IsClientAdmin) ? (inv.Deletion.HasValue ? inv.Deletion.Value : false) : false)

                          orderby inv.ClientId, inv.BuildingId, inv.InventoryId descending
                          select new TblInventoryDTO
                          {
                              InventoryId = inv.InventoryId,
                              ClientId = inv.ClientId,
                              BuildingId = inv.BuildingId,
                              ValuationId = inv.ValuationId,
                              Tag = inv.Tag,
                              RoomId = inv.RoomId,
                              DeptId = inv.DeptId,
                              AssetClassId = inv.AssetClassId,
                              Qty = inv.Qty,
                              Descr = inv.Descr,
                              Pono = inv.Pono,
                              Fund = inv.Fund,
                              Funct = inv.Funct,
                              Crn = inv.Crn,
                              SoundValue = inv.SoundValue,
                              Cost = inv.Cost,
                              SalvageValue = inv.SalvageValue,
                              Size = inv.Size,
                              Month = inv.Month,
                              Year = inv.Year,
                              Depr = inv.Depr,
                              Life = inv.Life,
                              SerNo = inv.SerNo,
                              Mfg = inv.Mfg,
                              Model = inv.Model,
                              ImageFileName = inv.ImageFileName,
                              Comment = inv.Comment,
                              Sdf1 = inv.Sdf1,
                              Sdf2 = inv.Sdf2,
                              MarkForDeletion = inv.MarkForDeletion,
                              Deletion = inv.Deletion,
                              Auser = inv.Auser,
                              AdateTime = inv.AdateTime,
                              Cuser = inv.Cuser,
                              CdateTime = inv.CdateTime,
                              Sdf3 = inv.Sdf3,
                              Sdf4 = inv.Sdf4,
                              Floor = inv.Floor,
                              Duser = inv.Duser,
                              DdateTime = inv.DdateTime,
                              SystemNumber = inv.SystemNumber,
                              /*Extra fields*/
                              BuildingCode = buil.BuildingCode,
                              DepartmentCode = dep.DepartmentCode,
                              AssetClassCode = asst.ClassCode,
                              RoomCode = room.RoomCode
                              //AccountingYear = cli.AccountingYear
                          }).ToListAsync();
        }
        public async Task<List<TblRooms>> GetRoomsList(InventorySearchCriteria criteria)
        {
            return await _dbContext.TblRooms.Where(m => m.ClientId == criteria.ClientID && m.BuildingId == criteria.BuildingId).ToListAsync();
        }
        public async Task<List<TblDepartments>> GetDepartmentList(InventorySearchCriteria criteria)
        {
            return await _dbContext.TblDepartments.Where(
                m => m.ClientId == criteria.ClientID
                &&
                //((m.Deleted.HasValue ? m.Deleted.Value : false) == (criteria.IsSuperAdmin || criteria.IsClientAdmin) ? (m.Deleted.HasValue ? m.Deleted.Value : false) : false))
                ((m.Deleted.HasValue ? m.Deleted.Value : false) == ((criteria.IsSuperAdmin || criteria.IsClientAdmin) ? (m.Deleted.HasValue ? m.Deleted.Value : false) : false)))
                .ToListAsync();
        }

        public async Task<List<TblRoomsDTO>> GetRoomsListDDL(InventorySearchCriteria criteria)
        {
            return await (from room in _dbContext.TblRooms.Where(m => m.ClientId == criteria.ClientID && m.BuildingId == criteria.BuildingId)
                          select new TblRoomsDTO
                          {
                              RoomId = room.RoomId,
                              BuildingId = room.BuildingId,
                              ClientId = room.ClientId,
                              RoomCode = room.RoomCode,
                              Floor = room.Floor,
                              RoomDescription = room.RoomCode + " : " + room.RoomDescription,
                          }).ToListAsync();

            //return await _dbContext.TblRooms.Where(m => m.ClientId == criteria.ClientID && m.BuildingId == criteria.BuildingId).ToListAsync();
        }
        public async Task<List<TblDepartmentsDTO>> GetDepartmentListDDL(InventorySearchCriteria criteria)
        {
            return await (from department in _dbContext.TblDepartments.Where(
                m => m.ClientId == criteria.ClientID
                &&
                //((m.Deleted.HasValue ? m.Deleted.Value : false) == (criteria.IsSuperAdmin || criteria.IsClientAdmin) ? (m.Deleted.HasValue ? m.Deleted.Value : false) : false))
                ((m.Deleted.HasValue ? m.Deleted.Value : false) == ((criteria.IsSuperAdmin || criteria.IsClientAdmin) ? (m.Deleted.HasValue ? m.Deleted.Value : false) : false)))
                select new TblDepartmentsDTO
                {
                    DepartmentId = department.DepartmentId,
                    DepartmentCode = department.DepartmentCode,
                    ClientId = department.ClientId,
                    DepartmentName = department.DepartmentCode + " : " + department.DepartmentName,
                }).ToListAsync();
        }
        public async Task<List<TblAssetClassDTO>> GetAssetClassListDDL(InventorySearchCriteria criteria)
        {
            return await (from asset in _dbContext.TblAssetClass.Where(m => m.ClientId == criteria.ClientID)
                          select new TblAssetClassDTO
                          {
                              AssetClassId = asset.AssetClassId,
                              ClassCode = asset.ClassCode,
                              ClientId = asset.ClientId,
                              AssetClassName = asset.ClassCode + " : " + asset.AssetClassName,

                          }).ToListAsync();
        }

        public async Task<List<TblInventoryMaintenanceDTO>> GetInventoryMaintanance(InventorySearchCriteria criteria)
        {
            return await (from maint in _dbContext.TblInventoryMaintenance
                          join inv in _dbContext.TblInventory on maint.InventoryId equals inv.InventoryId
                          join chngtype in _dbContext.TblChangeType on maint.ChangeTypeId equals chngtype.ChangeTypeId

                          where inv.ClientId == criteria.ClientID
                          &&
                          (inv.Deletion.HasValue ? inv.Deletion.Value : false) ==
                          ((criteria.IsSuperAdmin || criteria.IsClientAdmin) ? (inv.Deletion.HasValue ? inv.Deletion.Value : false) : false)

                          orderby inv.ClientId, inv.BuildingId, inv.InventoryId descending
                          select new TblInventoryMaintenanceDTO
                          {
                              InventoryMaintenanceId = maint.InventoryMaintenanceId,
                              InventoryId = maint.InventoryId,
                              ChangeTypeId = maint.ChangeTypeId,
                              Description = maint.Description,
                              MonthAcq = maint.MonthAcq,
                              YearAcq = maint.YearAcq,
                              Cost = maint.Cost,
                              Auser = maint.Auser,
                              AdateTime = maint.AdateTime,
                              Cuser = maint.Cuser,
                              CdateTime = maint.CdateTime,
                              ChangeType = chngtype.ChangeType
                          }).ToListAsync();
        }
        public async Task<List<TblAssetClass>> GetAssetClassList(InventorySearchCriteria criteria)
        {
            return await _dbContext.TblAssetClass.Where(m => m.ClientId == criteria.ClientID).ToListAsync();
        }
        public async Task<TblInventory> AddUpdateInventory(TblInventory tblInventory)
        {
            if (tblInventory.InventoryId == 0)
            {
                await _dbContext.TblInventory.AddAsync(tblInventory);
            }
            else
            {
                _dbContext.TblInventory.Update(tblInventory);
            }
            _dbContext.SaveChanges();
            _dbContext.Entry(tblInventory).State = EntityState.Detached;

            return tblInventory;
        }
        public async Task<bool> CheckInventoryTagExistance(InventorySearchCriteria criteria)
        {
            return await _dbContext.TblInventory.AnyAsync(
            m => m.ClientId == criteria.ClientID
            && m.Tag == criteria.TagNo
            && m.InventoryId != criteria.InventoryId
            );
        }
        public async Task<bool> AddUpdateInventoryMaintenanceAsync(TblInventoryMaintenance tblInventoryMaintenance)
        {
            bool isSuccess = false;
            try
            {
                if (tblInventoryMaintenance.InventoryMaintenanceId == 0)
                {
                    await _dbContext.TblInventoryMaintenance.AddAsync(tblInventoryMaintenance);
                    isSuccess = true;
                }
                else
                {
                    _dbContext.TblInventoryMaintenance.Update(tblInventoryMaintenance);
                    isSuccess = true;
                }
                _dbContext.SaveChanges();
                _dbContext.Entry(tblInventoryMaintenance).State = EntityState.Detached;

            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }


        public async Task<bool> DeleteInventoryMaintenance(int InventoryMaintenanceId)
        {
            if (_dbContext.TblInventoryMaintenance.Any(m => m.InventoryMaintenanceId == InventoryMaintenanceId))
            {
                _dbContext.TblInventoryMaintenance.Remove(await _dbContext.TblInventoryMaintenance.Where(m => m.InventoryMaintenanceId == InventoryMaintenanceId).FirstOrDefaultAsync());
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}