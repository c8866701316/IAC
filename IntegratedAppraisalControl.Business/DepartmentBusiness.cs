using AutoMapper;
using IntegratedAppraisalControl.Data;
using IntegratedAppraisalControl.Data.Models;
using IntegratedAppraisalControl.Models;
using IntegratedAppraisalControl.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Business
{
    public class DepartmentBusiness : IDepartmentBusiness
    {
        private readonly IDepartmentAccess _departmentAccess;
        private readonly IMapper _mapper;
        public DepartmentBusiness(IDepartmentAccess departmentAccess, IMapper mapper)
        {
            _departmentAccess = departmentAccess;
            _mapper = mapper;
        }
        
        public async Task<List<TblDepartmentsDTO>> GetDepartmentList(DepartmentSearchCriteria criteria)
        {
            var results = await _departmentAccess.GetDepartmentList(criteria);
            return _mapper.Map<List<TblDepartmentsDTO>>(results);
        }
        public async Task<bool> CheckDepartmentCodeExistance(DepartmentSearchCriteria criteria)
        {
            return await _departmentAccess.CheckDepartmentCodeExistance(criteria);
        }

        public async Task<TblDepartmentsDTO> GetDepartment(DepartmentSearchCriteria criteria)
        {
            var results = await _departmentAccess.GetDepartment(criteria);
            return _mapper.Map<TblDepartmentsDTO>(results);
        }
        public async Task<TblDepartmentsDTO> AddUpdateDepartment(TblDepartmentsDTO tblDepartments)
        {
            var results = await _departmentAccess.AddUpdateDepartment(_mapper.Map<TblDepartments>(tblDepartments));
            return _mapper.Map<TblDepartmentsDTO>(results);
        }
    }
}