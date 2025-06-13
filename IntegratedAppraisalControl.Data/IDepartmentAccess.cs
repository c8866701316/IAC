using IntegratedAppraisalControl.Data.Models;
using IntegratedAppraisalControl.Models;
using IntegratedAppraisalControl.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Data
{
    public interface IDepartmentAccess
    {
        Task<List<TblDepartments>> GetDepartmentList(DepartmentSearchCriteria criteria);
        Task<bool> CheckDepartmentCodeExistance(DepartmentSearchCriteria criteria);
        Task<TblDepartments> GetDepartment(DepartmentSearchCriteria criteria);
        Task<TblDepartments> AddUpdateDepartment(TblDepartments tblDepartments);
    }
}