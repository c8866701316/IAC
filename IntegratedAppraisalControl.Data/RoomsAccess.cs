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
    public class RoomsAccess : IRoomsAccess
    {
        private readonly IntegratedAppraisalControlContext _dbContext;
        public RoomsAccess()
        {
            _dbContext = new IntegratedAppraisalControlContext();
        }

        public async Task<List<TblRoomsDTO>> GetRoomsList(RoomsearchCriteria criteria)
        {
            return await (from room in _dbContext.TblRooms
                          join buil in _dbContext.TblBuildings on room.BuildingId equals buil.BuildingId

                          where room.ClientId == criteria.ClientID
                          &&
                            ((room.Deleted.HasValue ? room.Deleted.Value : false) == ((criteria.IsSuperAdmin || criteria.IsClientAdmin) ? (room.Deleted.HasValue ? room.Deleted.Value : false) : false))
                          select new TblRoomsDTO
                          {
                              RoomId = room.RoomId,
                              BuildingId = room.BuildingId,
                              ClientId = room.ClientId,
                              RoomCode = room.RoomCode,
                              RoomDescription = room.RoomDescription,
                              MarkForDeletion = room.MarkForDeletion,
                              Deleted = room.Deleted,
                              Floor = room.Floor,
                              BuildingCode = buil.BuildingCode

                          }).ToListAsync();
        }

        public async Task<TblRoomsDTO> GetRooms(RoomsearchCriteria criteria)
        {
            return await (from room in _dbContext.TblRooms
                          join buil in _dbContext.TblBuildings on room.BuildingId equals buil.BuildingId

                          where room.ClientId == criteria.ClientID
                          && room.RoomId == criteria.RoomId
                          &&
                            ((room.Deleted.HasValue ? room.Deleted.Value : false) == ((criteria.IsSuperAdmin || criteria.IsClientAdmin) ? (room.Deleted.HasValue ? room.Deleted.Value : false) : false))
                          select new TblRoomsDTO
                          {
                              RoomId = room.RoomId,
                              BuildingId = room.BuildingId,
                              ClientId = room.ClientId,
                              RoomCode = room.RoomCode,
                              RoomDescription = room.RoomDescription,
                              MarkForDeletion = room.MarkForDeletion,
                              Deleted = room.Deleted,
                              Floor = room.Floor,
                              BuildingCode = buil.BuildingCode
                          }).FirstOrDefaultAsync();

        }

        public async Task<TblRooms> AddUpdateRooms(TblRooms tblRooms)
        {
            if (tblRooms.RoomId == 0)
            {
                TblClients tc = _dbContext.TblClients.Where(m => m.ClientId == tblRooms.ClientId).FirstOrDefault();
                if (tc.NextRoomNumber > 0)
                {
                    tc.NextRoomNumber = tc.NextRoomNumber + 1;
                }
                else
                {
                    tc.NextRoomNumber = 9001;
                }
                _dbContext.TblClients.Update(tc);
                await _dbContext.TblRooms.AddAsync(tblRooms);
            }
            else
            {
                _dbContext.TblRooms.Update(tblRooms);
            }
            _dbContext.SaveChanges();
            _dbContext.Entry(tblRooms).State = EntityState.Detached;

            return tblRooms;
        }



        public async Task<bool> CheckRoomsCodeExistance(RoomsearchCriteria criteria)
        {
            return await _dbContext.TblRooms.AnyAsync(
            m => m.ClientId == criteria.ClientID
            && m.RoomCode == criteria.RoomCode
            && m.RoomId != criteria.RoomId
            && m.BuildingId == criteria.BuildingId
            );
        }
    }
}