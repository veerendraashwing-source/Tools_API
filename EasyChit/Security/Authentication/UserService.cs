
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Easychit_Repository.DataAccess.Settings.Users;
using Easychit_Infrastructure.Settings.Users;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Easychit_Repository.DataAccess;
using Npgsql;
using HelperManager;
using System.Data;
using Easychit_Repository.Interfaces.Security;

namespace Easychit_Api.Security.Authentication
{
    public class UserService : CommonDAL, IUserService
    {
        private readonly AppSettings _appSettings;
        IUserAccess objUserAccess = new UserAccessDAL();
        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }




        public async Task<List<UserWiseBranches>> RegionName(string globalSchema, string con)
        
        {
            List<UserWiseBranches> _UserwisebranchDTOList = new List<UserWiseBranches>();
            await Task.Run(() =>
            {

                try
                
                
                {

                    //string Query = "SELECT DISTINCT REGION_NAME FROM tabmsterdb WHERE REGION_NAME IS NOT NULL AND REGION_NAME<> '<NULL>' AND TRIM(REGION_NAME) <> ''";
                    string Query = "SELECT DISTINCT REGION_NAME FROM tabmsterdb WHERE vchCOMPANYNAME like '%AGRO%' AND REGION_NAME IS NOT NULL;";

                    using (NpgsqlDataReader dataReader = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dataReader.Read())
                        {

                            UserWiseBranches _UserBranchDTO = new UserWiseBranches();
                            _UserBranchDTO.region_name = Convert.ToString(dataReader["REGION_NAME"]);

                            _UserwisebranchDTOList.Add(_UserBranchDTO);

                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            });

            return _UserwisebranchDTOList;
        }

        public UserAccessDTO AGROAuthenticate(string username, string password, string Schema, string Con)
        {
            var user = objUserAccess.AGROCheckUser(username, password, Schema, Con);

            if (user.type == "PASS")
            {
                // authentication successful so generate jwt token          
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.pRoleid.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(180),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.pToken = tokenHandler.WriteToken(token);

                // remove password before returning
                user.pPassword = null;

                return user;
            }
            else
            {
                return null;
            }

        }


      


        public async Task<List<UserWiseBranches>> GetALLBranchesBasedOnRegion(string globalSchema, string region_Name, string con)
        {
            List<UserWiseBranches> _UserwisebranchDTOList = new List<UserWiseBranches>();
            await Task.Run(() =>
            {

                try
                {

                    string Query = "select recordid, VCHCOMPANYNAME, vchdbname from " + AddDoubleQuotes(globalSchema) + ".tabmsterdb WHERE  vchCOMPANYNAME like '%AGRO%' AND REGION_NAME = '" + region_Name + "'; ";


                    using (NpgsqlDataReader dataReader = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dataReader.Read())
                        {

                            UserWiseBranches _UserBranchDTO = new UserWiseBranches();
                            _UserBranchDTO.pBranchid = Convert.ToString(dataReader["recordid"]);
                           
                            
                             _UserBranchDTO.pBranchname = Convert.ToString(dataReader["vchcompanyname"]);
                           
                            _UserBranchDTO.pBranchschema = Convert.ToString(dataReader["vchdbname"]);
                            _UserwisebranchDTOList.Add(_UserBranchDTO);
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            });

            return _UserwisebranchDTOList;
        }



    }
}
