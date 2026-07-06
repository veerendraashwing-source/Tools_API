
using Easychit_Infrastructure;
using Easychit_Infrastructure.Settings.Users;
using Easychit_Repository.Interfaces.Settings;
using EasyChitRepository;
using HelperManager;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Easychit_Repository.DataAccess.Settings.Users
{
    public class UserSettingsDAL : CommonDAL, IUserSettings
    {
        NpgsqlConnection con = new NpgsqlConnection(NPGSqlHelper.SQLConnString);
        NpgsqlConnection constring = new NpgsqlConnection(NPGSqlHelper.SQLConnString);

        NpgsqlTransaction trans = null;
        public List<UserWiseBranches> _UserwisebranchDTOList = null;





        public bool UpdateUserPassword(UserRegistrationDTO userRegistrationDTO, string globalSchema, string connectionstring)
        {
            bool Issaved = false;
            string _UserUpdate = string.Empty;
            try
            {
                con = new NpgsqlConnection(connectionstring);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }



                trans = con.BeginTransaction();
                _UserUpdate = "UPDATE " + AddDoubleQuotes(globalSchema) + ".tbl_mst_employee SET password='" + userRegistrationDTO.pConfirmPassWord + "',userrandomnumber=''  WHERE userrandomnumber='" + userRegistrationDTO.pRandomNumber + "'";
                NPGSqlHelper.ExecuteNonQuery(trans, CommandType.Text, _UserUpdate.ToString());

                trans.Commit();
                Issaved = true;

            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Dispose();
                    con.Close();

                    trans.Dispose();
                }
            }

            return Issaved;
        }


        public bool ChangeUserPassword(UserRegistrationDTO userRegistrationDTO, string globalSchema, string connectionstring)
        {
            bool Issaved = false;
            string _UserUpdate = string.Empty;
            try
            {
                con = new NpgsqlConnection(connectionstring);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }



                trans = con.BeginTransaction();
                _UserUpdate = "UPDATE " + AddDoubleQuotes(globalSchema) + ".tbl_mst_employee SET password='" + userRegistrationDTO.pConfirmPassWord + "'  WHERE contact_id=" + userRegistrationDTO.pContactRefID + " and upper(emailid)='" + userRegistrationDTO.pEmployeeName.ToUpper() + "'";

                NPGSqlHelper.ExecuteNonQuery(trans, CommandType.Text, _UserUpdate.ToString());

                trans.Commit();
                Issaved = true;

            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Dispose();
                    con.Close();

                    trans.Dispose();
                }
            }

            return Issaved;
        }

        public bool AGROChangeUserPassword(UserRegistrationDTO userRegistrationDTO, string globalSchema, string connectionstring)
        {
            bool Issaved = false;
            string _UserUpdate = string.Empty;
            try
            {
                con = new NpgsqlConnection(connectionstring);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                trans = con.BeginTransaction();

                _UserUpdate = "UPDATE " + AddDoubleQuotes(globalSchema) + ".tabuserinfo SET userpassword='" + userRegistrationDTO.pPassWord + "' where userid=" + userRegistrationDTO.pUserID + "";

                NPGSqlHelper.ExecuteNonQuery(trans, CommandType.Text, _UserUpdate.ToString());

                trans.Commit();
                Issaved = true;

            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Dispose();
                    con.Close();

                    trans.Dispose();
                }
            }

            return Issaved;
        }







        public String OTPGenerator()
        {
            string OTP = string.Empty;
            try
            {
                Random objran = new Random();
                OTP = objran.Next(1000000).ToString();
                if (OTP.Length != 6)
                {
                    OTP = OTPGenerator();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return OTP;
        }




        public async Task<List<UserRightsAgroParentModuleDTO>> GetUserRightsAGROBasedonRoleAnduserId(string userId, string globalSchema, string connectionString)
        {
            List<UserRightsAgroParentModuleDTO> ParentModuleDTOlist = new List<UserRightsAgroParentModuleDTO>();
            List<UserRightsAgroFormsDTO> lstFormsDTO = new List<UserRightsAgroFormsDTO>();

            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {


                    Query = "select record_id as parent_module_id,parent_module_name from tab_inv_parent_modules where record_id in(select parent_module_id from tab_inv_modules where record_id in(select distinct f.module_id as moduleid from tab_inv_userrights u join tab_inv_allforms f on u.formid = f.record_id where chrnewright='Y' AND userid = '" + userId + "' ))";


                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(connectionString, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            UserRightsAgroParentModuleDTO _pmoduleDTO = new UserRightsAgroParentModuleDTO();
                            _pmoduleDTO.pparentmoduleid = Convert.ToInt64(dr["parent_module_id"]);
                            _pmoduleDTO.pparentmodulename = dr["parent_module_name"].ToString();
                            _pmoduleDTO.lstSubModuleDTO = new List<UserRightsAgroSubModuleDTO>();


                            Query = "select record_id as module_id,module_name as modulename from tab_inv_modules where parent_module_id=" + _pmoduleDTO.pparentmoduleid + " and record_id in(select distinct f.module_id as moduleid from tab_inv_userrights u join tab_inv_allforms f on u.formid = f.record_id where chrnewright='Y' AND userid ='" + userId + "' )";



                            using (NpgsqlDataReader dr1 = NPGSqlHelper.ExecuteReader(connectionString, CommandType.Text, Query))
                            {
                                Dictionary<long, UserRightsAgroSubModuleDTO> SubDict = new Dictionary<long, UserRightsAgroSubModuleDTO>();
                                while (dr1.Read())
                                {
                                    long subId = Convert.ToInt64(dr1["module_id"]);
                                    string subName = dr1["modulename"].ToString();

                                    Query = "select u.formid as formid, f.formname as formname, f.module_id as moduleid, f.module_name as modulename,f.url from tab_inv_userrights u join tab_inv_allforms f on u.formid = f.record_id where userid = '" + userId + "' and moduleid = " + subId + "  and u.chrnewright='Y' ";

                                    using (NpgsqlDataReader dr2 = NPGSqlHelper.ExecuteReader(connectionString, CommandType.Text, Query))
                                    {
                                        while (dr2.Read())
                                        {
                                            long formId = Convert.ToInt64(dr2["formid"]);
                                            string formName = dr2["formname"].ToString();
                                            string url = dr2["url"].ToString();




                                            // Check if the sub already exists
                                            if (!SubDict.ContainsKey(subId))
                                            {
                                                UserRightsAgroSubModuleDTO SubModule = new UserRightsAgroSubModuleDTO();
                                                SubModule.psubmoduleid = subId;
                                                SubModule.psubmodulename = subName;
                                                SubModule.lstFormsDTO = new List<UserRightsAgroFormsDTO>();

                                                SubDict[subId] = SubModule;
                                                _pmoduleDTO.lstSubModuleDTO.Add(SubModule);
                                            }



                                            // Add the forms to the existing sub
                                            UserRightsAgroFormsDTO forms = new UserRightsAgroFormsDTO();
                                            forms.pFormid = formId;
                                            forms.pFormname = formName;
                                            forms.url = url;
                                            SubDict[subId].lstFormsDTO.Add(forms);
                                        }
                                    }
                                }
                            }
                            ParentModuleDTOlist.Add(_pmoduleDTO);

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });

            return ParentModuleDTOlist;
        }


        public async Task<List<UserRightsAgroParentModuleDTO>> GetParentModuleAGRO_By_UserId(string userId, string globalSchema, string connectionString)
        {
            List<UserRightsAgroParentModuleDTO> ParentModuleDTOlist = new List<UserRightsAgroParentModuleDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "select record_id as parent_module_id ,parent_module_name from tab_inv_parent_modules";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(connectionString, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            UserRightsAgroParentModuleDTO _pmoduleDTO = new UserRightsAgroParentModuleDTO();
                            _pmoduleDTO.pparentmoduleid = Convert.ToInt64(dr["parent_module_id"]);
                            _pmoduleDTO.pparentmodulename = dr["parent_module_name"].ToString();
                            ParentModuleDTOlist.Add(_pmoduleDTO);
                        }

                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return ParentModuleDTOlist;
        }


        public async Task<List<UserRightsAgroSubModuleDTO>> GetSubModuleAGRO(string userId, long parentmoduleid, string globalSchema, string connectionString)
        {
            List<UserRightsAgroSubModuleDTO> SubModuleDTOlist = new List<UserRightsAgroSubModuleDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {

                    Query = "select record_id as submodule_id,module_name as submodule_name from tab_inv_modules where parent_module_id = " + parentmoduleid + " ";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(connectionString, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            UserRightsAgroSubModuleDTO _psubmoduleDTO = new UserRightsAgroSubModuleDTO();
                            _psubmoduleDTO.psubmoduleid = Convert.ToInt64(dr["submodule_id"]);
                            _psubmoduleDTO.psubmodulename = dr["submodule_name"].ToString();
                            SubModuleDTOlist.Add(_psubmoduleDTO);
                        }

                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return SubModuleDTOlist;
        }

        public bool SaveAddFormToMenuAGRO(FormsAGRODTO _FormsDTO, string globalSchema, string Connectionstring)
        {
            bool Issaved = false;
            try
            {
                con = new NpgsqlConnection(Connectionstring);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                trans = con.BeginTransaction();
                string query = "select count(*) from tab_inv_allforms where module_name = '" + _FormsDTO.pSubmodulename + "' and url = '" + _FormsDTO.pFormurl + "';";
                int count = Convert.ToInt32(NPGSqlHelper.ExecuteScalar(Connectionstring, CommandType.Text, query));
                if (count == 0)
                {
                    string selectQuery = "select coalesce(max(form_menu_order), 0) + 1 " +
                                     "from tab_inv_allforms where module_id = " + _FormsDTO.pSubmoduleId;

                    long v_form_menu_order = Convert.ToInt64(
                        NPGSqlHelper.ExecuteScalar(trans, CommandType.Text, selectQuery)
                    );

                    string Query = "insert into tab_inv_allforms(formname,module_id,module_name,form_menu_order,url,statusid,createdby,createddate) values('" + _FormsDTO.pFormname + "'," + _FormsDTO.pSubmoduleId + ",'" + _FormsDTO.pSubmodulename + "' , " + v_form_menu_order + ", '" + _FormsDTO.pFormurl + "', 1, " + _FormsDTO.pUserId + ", CURRENT_TIMESTAMP) ";

                    _ = NPGSqlHelper.ExecuteNonQuery(trans, CommandType.Text, Query);


                    trans.Commit();
                    Issaved = true;
                }


            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Dispose();
                    con.Close();

                    trans.Dispose();
                }
            }
            return Issaved;
        }




        public async Task<List<UsersDTO>> GetUserNamesAGRO(string GlobalSchema, string ConnectionString)
        {
            List<UsersDTO> _UserList = new List<UsersDTO>();
            string _Query = string.Empty;

            await Task.Run(() =>
            {
                try
                {
                    _Query = "select distinct userid, username  from " + AddDoubleQuotes(GlobalSchema) + ".tabuserinfo where statusid = 1;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(ConnectionString, CommandType.Text, _Query))
                    {
                        while (dr.Read())
                        {
                            _UserList.Add(new UsersDTO
                            {
                                userid = Convert.ToInt64(dr["userid"]),
                                username = Convert.ToString(dr["username"])


                            }); ;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {

                }
            });
            return _UserList;
        }


        public async Task<List<formaccesscheckAGRODto>> GetUserRightsFromsGridAGRO(string GlobalSchema, string ConnectionString, long user_id, string submoduleid)
        {
            List<formaccesscheckAGRODto> _formsList = new List<formaccesscheckAGRODto>();
            string _Query = string.Empty;

            await Task.Run(() =>
            {
                try
                {
                    _Query = "select f.formid, f.formname,coalesce(u.chrnewright, 'N') as chrnewright from(select  record_id as formid, formname from tab_inv_allforms where module_id = " + submoduleid + " order by form_menu_order) f left join (select  userid, formid, chrnewright from tab_inv_userrights where formid in (select record_id as formid from tab_inv_allforms where module_id = " + submoduleid + " order by form_menu_order)   and userid = " + user_id + " ) u on f.formid = u.formid; ";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(ConnectionString, CommandType.Text, _Query))
                    {
                        while (dr.Read())
                        {
                            _formsList.Add(new formaccesscheckAGRODto
                            {
                                //userid = Convert.ToInt64(dr["userid"]),
                                //username = Convert.ToString(dr["username"])
                                formid = Convert.ToInt64(dr["formid"]),
                                formname = Convert.ToString(dr["formname"]),
                                chrnewright = Convert.ToString(dr["chrnewright"])

                            }); ;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {

                }
            });
            return _formsList;
        }



        public bool SaveUserRightsAGRO(UserRightsAGRODTO _FormsDTO, string globalSchema, string Connectionstring, List<formaccesscheckAGRODto> _ListSubForms)
        {
            bool IsSaved = false;
            string _query = string.Empty;
            try
            {
                con = new NpgsqlConnection(Connectionstring);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                trans = con.BeginTransaction();

                long userid = _FormsDTO.user_id;
                for (int i = 0; i < _ListSubForms.Count; i++)
                {
                    // long formid = _ListSubForms[i].formid;
                    string formRightValue = _ListSubForms[i].chrnewright;

                    string roleidCount = " SELECT COUNT(*) FROM " + AddDoubleQuotes(globalSchema) + ". tab_inv_userrights WHERE  userid = " + _FormsDTO.user_id + " AND formid = " + _ListSubForms[i].formid + ";";

                    int useridCount = Convert.ToInt32(NPGSqlHelper.ExecuteScalar(Connectionstring, CommandType.Text, roleidCount));

                    //string submodule= "select record_id ad form_id,module_id as submodule_id from tab_inv_allforms where statusid=1 and formname='"+ formid + "'";
                    //

                    if (useridCount > 0)
                    {
                        _query = _query + "UPDATE " + AddDoubleQuotes(globalSchema) + ".tab_inv_userrights SET chrnewright = '" + formRightValue + "',chreditright = '" + formRightValue + "',chrdeleteright = '" + formRightValue + "',chrviewright = '" + formRightValue + "',chrprintright = '" + formRightValue + "' WHERE userid = " + _FormsDTO.user_id + " AND  formid = " + _ListSubForms[i].formid + ";";
                    }
                    else
                    {
                        if (formRightValue == "Y")
                        {
                            _query = _query + "insert into " + AddDoubleQuotes(globalSchema) + ".tab_inv_userrights (userid,  formid, moduleid,chrnewright,chreditright,chrdeleteright,chrviewright, chrprintright) values(" + _FormsDTO.user_id + ",  " + _ListSubForms[i].formid + "," + _FormsDTO.submoduleid + ", 'Y', 'Y', 'Y', 'Y', 'Y');";
                        }
                        else if (formRightValue == "N")
                        {

                            _query = _query + "UPDATE " + AddDoubleQuotes(globalSchema) + ".tab_inv_userrights SET chrnewright = '" + formRightValue + "',chreditright = '" + formRightValue + "',chrdeleteright = '" + formRightValue + "',chrviewright = '" + formRightValue + "',chrprintright = '" + formRightValue + "' WHERE userid = " + _FormsDTO.user_id + " AND formid = " + _ListSubForms[i].formid + ";";

                        }
                    }
                }


                NPGSqlHelper.ExecuteNonQuery(trans, CommandType.Text, _query);

                trans.Commit();
                IsSaved = true;

            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Dispose();
                    con.Close();

                    trans.Dispose();
                }
            }
            return IsSaved;
        }



        public bool SaveUserCreationAGRO(UsersAGRODTO _FormsDTO, string globalSchema, string Connectionstring)
        {
            bool IsSaved = false;
            string _query = string.Empty;
            try
            {
                con = new NpgsqlConnection(Connectionstring);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                trans = con.BeginTransaction();

                _query = "select max(userid)+1 from tabuserinfo";

                int userid = Convert.ToInt32(NPGSqlHelper.ExecuteScalar(Connectionstring, CommandType.Text, _query));


                _query = "insert into " + AddDoubleQuotes(globalSchema) + ".tabuserinfo(userid,username,userpassword,applicationid,usertype,createddate,statusid)values(" + userid + ",'" + _FormsDTO.username + "','" + _FormsDTO.password + "',1,1,CURRENT_TIMESTAMP,1)";




                NPGSqlHelper.ExecuteNonQuery(trans, CommandType.Text, _query);

                trans.Commit();
                IsSaved = true;

            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Dispose();
                    con.Close();

                    trans.Dispose();
                }
            }
            return IsSaved;
        }


    }

}



