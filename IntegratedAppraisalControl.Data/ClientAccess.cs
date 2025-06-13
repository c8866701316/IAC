using IntegratedAppraisalControl.Data.Models;
using IntegratedAppraisalControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using IntegratedAppraisalControl.Models.DTO;

namespace IntegratedAppraisalControl.Data
{
    public class ClientAccess : IClientAccess
    {
        private readonly IntegratedAppraisalControlContext _dbContext;
        public ClientAccess()
        {
            _dbContext = new IntegratedAppraisalControlContext();
        }
        public async Task<List<TblClients>> GetClientListBySearchCriteriaAsync(ClientSearchCriteriaModel criteria)
        {
            criteria.ClientName = string.IsNullOrEmpty(criteria.ClientName) ? "" : criteria.ClientName;
            criteria.FileNumber = string.IsNullOrEmpty(criteria.FileNumber) ? "" : criteria.FileNumber;

            if (criteria.IsSuperAdmin)
            {
                return await _dbContext.TblClients.ToListAsync();
            }
            else
            {
                return await (from client in _dbContext.TblClients
                              join usecli in _dbContext.TblUsersClients on client.ClientId equals usecli.ClientId
                              where usecli.UserId == criteria.UserId
                              && client.ClientName.Contains(criteria.ClientName)
                              && client.FileNo.Contains(criteria.FileNumber)
                              select client).ToListAsync();
            }
            
        }

        public async Task<List<TblClients>> GetClientList()
        {
            return await _dbContext.TblClients.ToListAsync();
        }

        public async Task<TblInventoryMaintenance> GetInventoryMaintenance(InventoryMaintenanceSearchCriteria criteria)
        {
            return await _dbContext.TblInventoryMaintenance.Where(
                m => m.InventoryMaintenanceId == criteria.InventoryMaintenanceID).FirstOrDefaultAsync();
        }


        public async Task<TblClientsDTO> GeClientDetails(ClientSearchCriteriaModel criteria)
        {
            //return await (from clie in _dbContext.TblClients

            //              where clie.ClientId == criteria.ClientId
            //              select new TblClientsDTO
            //              {
            //                  AccountingYear = clie.AccountingYear,
            //                  ClientId = clie.ClientId

            //              }).FirstOrDefaultAsync();


            return await (from clie in _dbContext.TblClients
                          join uclie in _dbContext.TblUsersClients on clie.ClientId equals uclie.ClientId

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

                          where uclie.ClientId == criteria.ClientId

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
                              AccountDataAsOf = clie.AccountDataAsOf,
                              NextRoomNumber = clie.NextRoomNumber,
                              NextDepartmentNumber = clie.NextDepartmentNumber
                          }).FirstOrDefaultAsync();

        }

    }
}
