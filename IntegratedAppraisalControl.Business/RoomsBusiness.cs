using AutoMapper;
using IntegratedAppraisalControl.Business;
using IntegratedAppraisalControl.Data;
using IntegratedAppraisalControl.Data.Models;
using IntegratedAppraisalControl.Models;
using IntegratedAppraisalControl.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Business
{
    public class RoomsBusiness : IRoomsBusiness
    {
        private readonly IRoomsAccess _RoomsAccess;
        private readonly IMapper _mapper;
        public RoomsBusiness(IRoomsAccess RoomsAccess, IMapper mapper)
        {
            _RoomsAccess = RoomsAccess;
            _mapper = mapper;
        }

        public async Task<List<TblRoomsDTO>> GetRoomsList(RoomsearchCriteria criteria)
        {
            return await _RoomsAccess.GetRoomsList(criteria);
        }
        public async Task<bool> CheckRoomsCodeExistance(RoomsearchCriteria criteria)
        {
            return await _RoomsAccess.CheckRoomsCodeExistance(criteria);
        }

        public async Task<TblRoomsDTO> GetRooms(RoomsearchCriteria criteria)
        {
            return await _RoomsAccess.GetRooms(criteria);
        }
        public async Task<TblRoomsDTO> AddUpdateRooms(TblRoomsDTO tblRooms)
        {
            var results = await _RoomsAccess.AddUpdateRooms(_mapper.Map<TblRooms>(tblRooms));
            return _mapper.Map<TblRoomsDTO>(results);
        }
    }
}