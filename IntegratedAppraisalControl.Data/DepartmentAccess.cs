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
    public class DepartmentAccess : IDepartmentAccess
    {
        private readonly IntegratedAppraisalControlContext _dbContext;
        public DepartmentAccess()
        {
            _dbContext = new IntegratedAppraisalControlContext();
        }
        public async Task<List<TblDepartments>> GetDepartmentList(DepartmentSearchCriteria criteria)
        {
            return await _dbContext.TblDepartments.AsNoTracking().Where(
                m => m.ClientId == criteria.ClientID
                &&
                ((m.Deleted.HasValue ? m.Deleted.Value : false) == ((criteria.IsSuperAdmin || criteria.IsClientAdmin) ? (m.Deleted.HasValue ? m.Deleted.Value : false) : false))).AsNoTracking()
                .ToListAsync();
        }

        public async Task<TblDepartments> GetDepartment(DepartmentSearchCriteria criteria)
        {
            TblDepartments td = await _dbContext.TblDepartments.AsNoTracking().Where(
                m => m.ClientId == criteria.ClientID
                &&
                m.DepartmentId == criteria.DepartmentId
                &&
                ((m.Deleted.HasValue ? m.Deleted.Value : false) == ((criteria.IsSuperAdmin || criteria.IsClientAdmin) ? (m.Deleted.HasValue ? m.Deleted.Value : false) : false)))
                .FirstOrDefaultAsync();

            _dbContext.Entry(td).State = EntityState.Detached;
            return td;
        }

        public async Task<TblDepartments> AddUpdateDepartment(TblDepartments tblDepartments)
        {
            if (tblDepartments.DepartmentId == 0)
            {
                TblClients tc = _dbContext.TblClients.Where(m => m.ClientId == tblDepartments.ClientId).FirstOrDefault();
                if (tc.NextDepartmentNumber > 0)
                {
                    tc.NextDepartmentNumber = tc.NextDepartmentNumber + 1;
                }
                else
                {
                    tc.NextDepartmentNumber = 9001;
                }
                _dbContext.TblClients.Update(tc);
                await _dbContext.TblDepartments.AddAsync(tblDepartments);
            }
            else
            {
                _dbContext.TblDepartments.Update(tblDepartments);
            }
            _dbContext.SaveChanges();
            _dbContext.Entry(tblDepartments).State = EntityState.Detached;
            return tblDepartments;
        }

        public async Task<bool> CheckDepartmentCodeExistance(DepartmentSearchCriteria criteria)
        {
            return await _dbContext.TblDepartments.AnyAsync(
            m => m.ClientId == criteria.ClientID
            && m.DepartmentCode == criteria.DepartmentCode
            && m.DepartmentId != criteria.DepartmentId
            );
        }
    }
}