using IntegratedAppraisalControl.Models;
using IntegratedAppraisalControl.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Business
{
    public interface IDepartmentBusiness
    {
        Task<List<TblDepartmentsDTO>> GetDepartmentList(DepartmentSearchCriteria criteria);
        Task<bool> CheckDepartmentCodeExistance(DepartmentSearchCriteria criteria);
        Task<TblDepartmentsDTO> GetDepartment(DepartmentSearchCriteria criteria);
        Task<TblDepartmentsDTO> AddUpdateDepartment(TblDepartmentsDTO tblDepartments);
    }
}