using IntegratedAppraisalControl.Models;
using IntegratedAppraisalControl.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Business
{
    public interface IUserBusiness
    {
        TblUsersDTO ValidateActiveUser(string userName, string password);
        TblUsersDTO FindUser(int Id);
        Task<List<TblUsersDTO>> GetUsersList(UserSearchCriteria criteria);
        Task<TblUsersDTO> GetUsers(UserSearchCriteria criteria);
        Task<bool> CheckUserNameExistance(UserSearchCriteria criteria);
        Task<TblUsersDTO> AddUpdateUsers(TblUsersDTO tblUsers);
        Task<List<TblUsersClientsDTO>> GetClientListByUser(int UserId);
        Task<bool> DeleteUsersClientsMapping(int UsersClientId);
        Task<TblUsersClientsDTO> AddUpdateUsersClientsMapping(TblUsersClientsDTO UsersClient);
    }
}
