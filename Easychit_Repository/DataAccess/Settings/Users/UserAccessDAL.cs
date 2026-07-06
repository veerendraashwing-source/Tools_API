using Easychit_Infrastructure.Settings.Users;
using Easychit_Repository.Interfaces.Security;
using HelperManager;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easychit_Repository.DataAccess.Settings.Users
{
    public class UserAccessDAL : CommonDAL, IUserAccess
    {


        public UserAccessDTO AGROCheckUser(string UserName, string PassWord, string globalSchema, string connectionString)
        {

            UserAccessDTO userDTO = new UserAccessDTO();
            try
            {


                string Query = "select count(*) as count from " + AddDoubleQuotes(globalSchema) + ".tabuserinfo  where upper(username)='" + ManageQuote(UserName.ToUpper().Trim()) + "' and statusid=1;";
                int count = Convert.ToInt32(NPGSqlHelper.ExecuteScalar(connectionString, CommandType.Text, Query));
                if (count > 0)
                {
                    Query = "select * from " + AddDoubleQuotes(globalSchema) + ".tabuserinfo  where upper(username)='" + ManageQuote(UserName.ToUpper().Trim()) + "' and userpassword='" + ManageQuote(PassWord.Trim()) + "' and statusid=1";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(connectionString, CommandType.Text, Query))
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                userDTO.pUserName = dr["username"].ToString();
                                userDTO.pPassword = dr["userpassword"].ToString();
                                userDTO.pUserID = Convert.ToInt32(dr["userid"].ToString());
                                userDTO.isbiometric = false;
                                userDTO.ispassword = true;
                                userDTO.type = "PASS";
                                userDTO.pRoleid = 1;
                                userDTO.pBranchschema = "public";

                            }

                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return userDTO;
        }
    }
}
