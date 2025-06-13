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
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserAccess _userAccess;
        private readonly IMapper _mapper;
        public UserBusiness(IUserAccess userAccess, IMapper mapper)
        //public UserBusiness(IUserAccess userAccess)
        {
            _userAccess = userAccess;
            _mapper = mapper;
        }

        public TblUsersDTO ValidateActiveUser(string userName, string password)
        {
            var user = _userAccess.ValidateActiveUser(userName, password);
            var model = _mapper.Map<TblUsersDTO>(user);
            return model;
        }

        public TblUsersDTO FindUser(int Id)
        {
            var user = _userAccess.FindUser(Id);
            var model = _mapper.Map<TblUsersDTO>(user);
            return model;
        }

        public async Task<List<TblUsersDTO>> GetUsersList(UserSearchCriteria criteria)
        {
            var results = await _userAccess.GetUserList(criteria);
            return _mapper.Map<List<TblUsersDTO>>(results);
        }

        public async Task<TblUsersDTO> GetUsers(UserSearchCriteria criteria)
        {
            var results = await _userAccess.GetUser(criteria);
            return _mapper.Map<TblUsersDTO>(results);
        }

        public async Task<bool> CheckUserNameExistance(UserSearchCriteria criteria)
        {
            return await _userAccess.CheckUserNameExistance(criteria);
        }

        public async Task<TblUsersDTO> AddUpdateUsers(TblUsersDTO tblusers)
        {
            var results = await _userAccess.AddUpdateUser(_mapper.Map<TblUsers>(tblusers));
            return _mapper.Map<TblUsersDTO>(results);
        }

        /*
         * Client user mapping start
         */
        public async Task<List<TblUsersClientsDTO>> GetClientListByUser(int UserId)
        {
            return await _userAccess.GetClientListByUser(UserId);
        }
        public async Task<bool> DeleteUsersClientsMapping(int UsersClientId)
        {
            return await _userAccess.DeleteUsersClientsMapping(UsersClientId);
        }
        public async Task<TblUsersClientsDTO> AddUpdateUsersClientsMapping(TblUsersClientsDTO UsersClient)
        {
            var results = await _userAccess.AddUpdateUsersClientsMapping(_mapper.Map<TblUsersClients>(UsersClient));
            return _mapper.Map<TblUsersClientsDTO>(results);
        }
        /*
        * Client user mapping end
        */
    }
}
