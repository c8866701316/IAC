using IntegratedAppraisalControl.Data;
using IntegratedAppraisalControl.Data.Models;
using IntegratedAppraisalControl.Models;
using IntegratedAppraisalControl.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Data
{
    public class UserAccess : IUserAccess
    {
        private readonly IntegratedAppraisalControlContext _dbContext;
        public UserAccess()
        {
            _dbContext = new IntegratedAppraisalControlContext();
        }
        public TblUsers ValidateActiveUser(string userName, string password)
        {
            return _dbContext.TblUsers.AsNoTracking().Where(m => m.UserName == userName && m.Password == password && m.Deleted != true).FirstOrDefault();
        }
        public TblUsers FindUser(int Id)
        {
            return _dbContext.TblUsers.Find(Id);
        }

        public async Task<List<TblUsers>> GetUserList(UserSearchCriteria criteria)
        {
            return await _dbContext.TblUsers.AsNoTracking().Where(
                m => m.ClientId == criteria.ClientID
                &&
                ((m.Deleted.HasValue ? m.Deleted.Value : false) == ((criteria.IsSuperAdmin || criteria.IsClientAdmin) ? (m.Deleted.HasValue ? m.Deleted.Value : false) : false)))
                .ToListAsync();
        }

        public async Task<TblUsers> GetUser(UserSearchCriteria criteria)
        {
            return await _dbContext.TblUsers.AsNoTracking().Where(m => m.UserId == criteria.UserId).FirstOrDefaultAsync();
        }

        public async Task<bool> CheckUserNameExistance(UserSearchCriteria criteria)
        {
            return await _dbContext.TblUsers.AsNoTracking().AnyAsync(
            m => m.ClientId == criteria.ClientID
            && m.UserId == criteria.UserId
            && m.UserName == criteria.UserName);
        }

        public async Task<TblUsers> AddUpdateUser(TblUsers tblUsers)
        {
            if (tblUsers.UserId == 0)
            {
                await _dbContext.TblUsers.AddAsync(tblUsers);
            }
            else
            {
                var contextEntry = _dbContext.Entry<TblUsers>(tblUsers);
                if (contextEntry.State == EntityState.Detached)
                {
                    _dbContext.Attach(tblUsers);
                }

                _dbContext.Entry(tblUsers).State = EntityState.Modified;
                //_dbContext.TblUsers.Update(tblUsers);
            }
            _dbContext.SaveChanges();
            _dbContext.Entry(tblUsers).State = EntityState.Detached;

            return tblUsers;
        }

        public async Task<List<TblUsersClientsDTO>> GetClientListByUser(int UserId)
        {
            return await (from userclie in _dbContext.TblUsersClients
                          join clie in _dbContext.TblClients on userclie.ClientId equals clie.ClientId

                          where userclie.UserId == UserId
                          orderby clie.ClientName ascending
                          select new TblUsersClientsDTO
                          {
                              ClientName = clie.FileNo+" - " + clie.ClientName,
                              UsersClientId = userclie.UsersClientId,
                              UserId = userclie.UserId,
                              ClientId = userclie.ClientId,
                          }).ToListAsync();
        }
        public async Task<bool> DeleteUsersClientsMapping(int UsersClientId)
        {
            if(_dbContext.TblUsersClients.Any(m => m.UsersClientId == UsersClientId))
            {
                _dbContext.TblUsersClients.Remove(await _dbContext.TblUsersClients.Where(m => m.UsersClientId == UsersClientId).FirstOrDefaultAsync());
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<TblUsersClientsDTO> AddUpdateUsersClientsMapping(TblUsersClients UsersClient)
        {
            if (UsersClient.UsersClientId == 0)
            {
                await _dbContext.TblUsersClients.AddAsync(UsersClient);
            }
            else
            {

                var contextEntry = _dbContext.Entry<TblUsersClients>(UsersClient);
                if (contextEntry.State == EntityState.Detached)
                {
                    _dbContext.Attach(UsersClient);
                }

                _dbContext.Entry(UsersClient).State = EntityState.Modified;
                //_dbContext.TblUsers.Update(tblUsers);
            }
            _dbContext.SaveChanges();
            _dbContext.Entry(UsersClient).State = EntityState.Detached;

            return await (from userclie in _dbContext.TblUsersClients
                          join clie in _dbContext.TblClients on userclie.ClientId equals clie.ClientId

                          where userclie.UsersClientId == UsersClient.UsersClientId
                          orderby clie.ClientName ascending
                          select new TblUsersClientsDTO
                          {
                              ClientName = clie.ClientName,
                              UsersClientId = userclie.UsersClientId,
                              UserId = userclie.UserId,
                              ClientId = userclie.ClientId,
                          }).FirstOrDefaultAsync();
        }
    }
}
