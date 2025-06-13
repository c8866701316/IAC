using IntegratedAppraisalControl.Models;
using IntegratedAppraisalControl.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Business
{
    public interface IRoomsBusiness
    {
        Task<List<TblRoomsDTO>> GetRoomsList(RoomsearchCriteria criteria);
        Task<bool> CheckRoomsCodeExistance(RoomsearchCriteria criteria);
        Task<TblRoomsDTO> GetRooms(RoomsearchCriteria criteria);
        Task<TblRoomsDTO> AddUpdateRooms(TblRoomsDTO tblRooms);
    }
}