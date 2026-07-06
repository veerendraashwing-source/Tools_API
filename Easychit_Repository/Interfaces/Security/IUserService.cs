using Easychit_Infrastructure.Settings.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easychit_Repository.Interfaces.Security
{
    public interface IUserService
    {

        Task<List<UserWiseBranches>> GetALLBranchesBasedOnRegion(string globalSchema, string region_Name, string con);
        

        Task<List<UserWiseBranches>> RegionName(string globalSchema, string con);

        UserAccessDTO AGROAuthenticate(string username, string password, string Schema, string Con);

    }
}
