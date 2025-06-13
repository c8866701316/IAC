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
    public class CommonBusiness : ICommonBusiness
    {
        private readonly ICommonAccess _commonAccess;
        private readonly IMapper _mapper;
        public CommonBusiness(ICommonAccess commonAccess, IMapper mapper)
        {
            _commonAccess = commonAccess;
            _mapper = mapper;
        }
        public async Task<List<TblChangeTypeDTO>> GetChangeTypeSearchCriteriaAsync(ChangeTypeCriteriaModel criteria)
        {
            if (criteria == null)
            {
                criteria = new ChangeTypeCriteriaModel();
            }
            var results = await _commonAccess.GetChangeTypeSearchCriteriaAsync(criteria);
            return _mapper.Map<List<TblChangeTypeDTO>>(results);
        }

        public async Task<List<tblTransactionsDTO>> GetTransaction(TransactionsSearchCritria criteria)
        {
            return await _commonAccess.GetTransaction(criteria);
        }

        public async Task<TblClients> GetClientDetails(string fileNo)
        {
            var results = await _commonAccess.GetClientDetails(fileNo);
            return _mapper.Map<TblClients>(results);
        }

        public async Task<TblBuildings> GetBuildingDetails(int clientId, string buildingCode)
        {
            var results = await _commonAccess.GetBuildingDetails(clientId, buildingCode);
            return _mapper.Map<TblBuildings>(results);
        }
    }
}