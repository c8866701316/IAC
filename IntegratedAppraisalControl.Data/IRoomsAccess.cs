using IntegratedAppraisalControl.Data.Models;
using IntegratedAppraisalControl.Models;
using IntegratedAppraisalControl.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Data
{
    public interface IRoomsAccess
    {
        Task<List<TblRoomsDTO>> GetRoomsList(RoomsearchCriteria criteria);
        Task<TblRoomsDTO> GetRooms(RoomsearchCriteria criteria);
        Task<TblRooms> AddUpdateRooms(TblRooms tblRooms);
        Task<bool> CheckRoomsCodeExistance(RoomsearchCriteria criteria);
    }
}