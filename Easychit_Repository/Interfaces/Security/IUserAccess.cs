using Easychit_Infrastructure.Settings.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easychit_Repository.Interfaces.Security
{
   public interface IUserAccess
    {
        UserAccessDTO AGROCheckUser(string UserName, string PassWord, string Schema, string connectionString);


    }

}
