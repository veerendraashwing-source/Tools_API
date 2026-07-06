using Easychit_Infrastructure;
using Easychit_Infrastructure.Settings.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easychit_Repository.Interfaces.Settings
{
    public interface IUserSettings
    {

        bool UpdateUserPassword(UserRegistrationDTO userRegistrationDTO, string globalSchema, string con);

        bool ChangeUserPassword(UserRegistrationDTO userRegistrationDTO, string globalSchema, string con);

        bool AGROChangeUserPassword(UserRegistrationDTO userRegistrationDTO, string globalSchema, string con);

        Task<List<UserRightsAgroParentModuleDTO>> GetUserRightsAGROBasedonRoleAnduserId(string userId, string globalSchema, string con);

        Task<List<UserRightsAgroParentModuleDTO>> GetParentModuleAGRO_By_UserId(string userId, string globalSchema, string con);

        Task<List<UserRightsAgroSubModuleDTO>> GetSubModuleAGRO(string userId, long parentmoduleid, string globalSchema, string con);


        bool SaveAddFormToMenuAGRO(FormsAGRODTO _FormsDTO, string globalSchema, string Connectionstring);
        Task<List<UsersDTO>> GetUserNamesAGRO(string connectionString, string globalSchema);


        Task<List<formaccesscheckAGRODto>> GetUserRightsFromsGridAGRO(string connectionString, string globalSchema, long user_id, string submoduleid);

        bool SaveUserRightsAGRO(UserRightsAGRODTO _FormsDTO, string globalSchema, string Connectionstring, List<formaccesscheckAGRODto> _ListSubForms);

        bool SaveUserCreationAGRO(UsersAGRODTO _FormsDTO, string globalSchema, string Connectionstring);


    }
}
