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
    public class LocationAccess : ILocationAccess
    {
        private readonly IntegratedAppraisalControlContext _dbContext;

        public LocationAccess()
        {
            _dbContext = new IntegratedAppraisalControlContext();
        }

        public async Task<List<TblClientsDTO>> GetLocationList(LocationSearchCriteria criteria)
        {


            return await (from clie in _dbContext.TblClients
                          //join uclie in _dbContext.TblUsersClients on clie.ClientId equals uclie.ClientId
                          //into ucli
                          //from uclie in ucli.DefaultIfEmpty()

                          join annu in _dbContext.TblAnnualDepreciation on clie.AnnualDepreciationId equals annu.AnnualDepreciationId
                          into annual
                          from annu in annual.DefaultIfEmpty()

                          join fydep in _dbContext.TblFirstYearDepreciation on clie.FirstYearDepreciationD equals fydep.FirstYearDepreciationId
                            into fydeps
                          from fydep in fydeps.DefaultIfEmpty()

                              //join dep in _dbContext.TblDepartments on inv.DeptId equals dep.DepartmentId
                              //into dept
                              //from dep in dept.DefaultIfEmpty()

                              //join room in _dbContext.TblRooms on inv.RoomId equals room.RoomId
                              //into rooms
                              //from room in rooms.DefaultIfEmpty()

                          //where uclie.UserId == (criteria.IsSuperAdmin ? uclie.UserId : criteria.BaseUserId)
                          //clie.ClientId == criteria.ClientID &&

                          orderby clie.ClientId descending
                          select new TblClientsDTO
                          {
                              ClientId = clie.ClientId,
                              ClientName = clie.ClientName,
                              Address1 = clie.Address1,
                              Address2 = clie.Address2,
                              City = clie.City,
                              State = clie.State,
                              ZipCode = clie.ZipCode,
                              PointOfContact = clie.PointOfContact,
                              Telephone = clie.Telephone,
                              ReportYear = clie.ReportYear,
                              AccountingYear = clie.AccountingYear,
                              AquisitionCostCutOff = clie.AquisitionCostCutOff,
                              LastUpdated = clie.LastUpdated,
                              AnnualDepreciationText = annu.AnnualDepreciationText, //clie.AnnualDepreciationId,
                              FirstYearDepreciationText = fydep.FirstYearDepreciationText,
                              Active = clie.Active,
                              Sdf1label = clie.Sdf1label,
                              Sdf2label = clie.Sdf2label,
                              Sdf3label = clie.Sdf3label,
                              Sdf4label = clie.Sdf4label,
                              FileNo = clie.FileNo,
                              ClientStatusId = clie.ClientStatusId,
                              AppraisalDate = clie.AppraisalDate,
                              UpdatedTo = clie.UpdatedTo,
                              Accounting = clie.Accounting.HasValue ? clie.Accounting.Value : false,
                              Insurance = clie.Insurance.HasValue ? clie.Insurance.Value : false,
                              Fmv = clie.Fmv.HasValue ? clie.Insurance.Value : false,
                              AccountDataAsOf = clie.AccountDataAsOf
                          }).ToListAsync();
        }

        public async Task<List<TblAnnualDepreciationDTO>> GetAnnualDepreciationListDDL(LocationSearchCriteria criteria)
        {
            return await (from annual in _dbContext.TblAnnualDepreciation.Where(m => m.Active == true)
                          select new TblAnnualDepreciationDTO
                          {
                              AnnualDepreciationId = annual.AnnualDepreciationId,
                              //AnnualDepreciationText = annual.AnnualDepreciationId + " : " + annual.AnnualDepreciationText,
                              AnnualDepreciationText = annual.AnnualDepreciationText,

                          }).ToListAsync();
        }

        public async Task<List<TblFirstYearDepreciationDTO>> GetFirstYearDepreciationListDDL(LocationSearchCriteria criteria)
        {
            return await (from fyd in _dbContext.TblFirstYearDepreciation.Where(m => m.Active == true)
                          select new TblFirstYearDepreciationDTO
                          {
                              FirstYearDepreciationId = fyd.FirstYearDepreciationId,
                              //AnnualDepreciationText = fyd.FirstYearDepreciationId + " : " + fyd.FirstYearDepreciationText,
                              FirstYearDepreciationText = fyd.FirstYearDepreciationText,

                          }).ToListAsync();
        }

        public async Task<TblClients> GetClients(LocationSearchCriteria criteria)
        {
            return await _dbContext.TblClients.AsNoTracking().Where(m => m.ClientId == criteria.LocationId).FirstOrDefaultAsync();
        }

        public async Task<TblClients> AddUpdateClients(TblClients tblClients)
        {
            if (tblClients.ClientId == 0)
            {
                await _dbContext.TblClients.AddAsync(tblClients);
            }
            else
            {
                _dbContext.TblClients.Update(tblClients);
            }
            _dbContext.SaveChanges();
            _dbContext.Entry(tblClients).State = EntityState.Detached;

            return tblClients;
        }

    }
}