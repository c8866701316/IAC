using IntegratedAppraisalControl.Data.Models;
using IntegratedAppraisalControl.Models;
using IntegratedAppraisalControl.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Data
{
    public interface IUserAccess
    {
        TblUsers ValidateActiveUser(string userName, string password);
        TblUsers FindUser(int Id);
        Task<List<TblUsers>> GetUserList(UserSearchCriteria criteria);
        Task<TblUsers> GetUser(UserSearchCriteria criteria);
        Task<bool> CheckUserNameExistance(UserSearchCriteria criteria);
        Task<TblUsers> AddUpdateUser(TblUsers tblUsers);
        Task<TblUsersClientsDTO> AddUpdateUsersClientsMapping(TblUsersClients UsersClient);
        Task<bool> DeleteUsersClientsMapping(int UsersClientId);
        Task<List<TblUsersClientsDTO>> GetClientListByUser(int UserId);
    }
}
