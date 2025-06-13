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
    public class LocationBusiness : ILocationBusiness
    {
        private readonly ILocationAccess _locationAccess;
        private readonly IMapper _mapper;

        public LocationBusiness(ILocationAccess locationAccess, IMapper mapper)
        {
            _locationAccess = locationAccess;
            _mapper = mapper;
        }
        
        public async Task<List<TblClientsDTO>> GetLocationList(LocationSearchCriteria criteria)
        {
            return await _locationAccess.GetLocationList(criteria);
        }

        public async Task<List<TblAnnualDepreciationDTO>> GetAnnualDepreciationListDDL(LocationSearchCriteria criteria)
        {
            var results = await _locationAccess.GetAnnualDepreciationListDDL(criteria);
            return _mapper.Map<List<TblAnnualDepreciationDTO>>(results);
        }

        public async Task<List<TblFirstYearDepreciationDTO>> GetFirstYearDepreciationListDDL(LocationSearchCriteria criteria)
        {
            var results = await _locationAccess.GetFirstYearDepreciationListDDL(criteria);
            return _mapper.Map<List<TblFirstYearDepreciationDTO>>(results);
        }

        public async Task<TblClientsDTO> GetClients(LocationSearchCriteria criteria)
        {
            var results = await _locationAccess.GetClients(criteria);
            return _mapper.Map<TblClientsDTO>(results);
        }

        public async Task<TblClientsDTO> AddUpdateClients(TblClientsDTO tblClientsDTO)
        {
            var results = await _locationAccess.AddUpdateClients(_mapper.Map<TblClients>(tblClientsDTO));
            return _mapper.Map<TblClientsDTO>(results);
        }

        
    }
}

