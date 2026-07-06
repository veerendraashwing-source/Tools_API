using System;
using System.Collections.Generic;
using System.Text;
using Easychit_Infrastructure.Dar.Dar_Masters;
using Easychit_Repository.Interfaces.Dar.Dar_Masters;
using System.Threading.Tasks;
using HelperManager;
using Npgsql;
using System.Data;

namespace Easychit_Repository.DataAccess.Dar.Dar_Masters
{
    public class DarMastersDAL : CommonDAL, IDarMasters
    {

        NpgsqlTransaction trans = null;
        NpgsqlConnection con = new NpgsqlConnection(NPGSqlHelper.SQLConnString);

        public async Task<List<UOMDTO>> GetUOM(string globalSchema, string con)
        {
            List<UOMDTO> uOMDTOlist = new List<UOMDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "SELECT vchuomid,vchuomdescription FROM tabuommst ORDER BY vchuomdescription;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            UOMDTO uomDTO = new UOMDTO();
                            uomDTO.vchuomid = dr["vchuomid"] == DBNull.Value ? "" : Convert.ToString(dr["vchuomid"]);
                            uomDTO.vchuom_description = dr["vchuomdescription"] == DBNull.Value ? "": Convert.ToString(dr["vchuomdescription"]);
                            uOMDTOlist.Add(uomDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return uOMDTOlist;
        }


        public async Task<List<GSTDTO>> GetGST(string globalSchema, string con)
        {
            List<GSTDTO> gstDTOlist = new List<GSTDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "SELECT gstid,gst_percentage FROM tabgstmst order by gstid;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            GSTDTO gstDTO = new GSTDTO();
                            gstDTO.gstid = dr["gstid"] == DBNull.Value ? 0 : Convert.ToInt64(dr["gstid"]);
                            gstDTO.gst_percentage = dr["gst_percentage"] == DBNull.Value ? "": Convert.ToString(dr["gst_percentage"]);
                            gstDTOlist.Add(gstDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return gstDTOlist;
        }


        public async Task<List<ProductTypeDTO>> GetVendertype(string globalSchema, string con)
        {
            List<ProductTypeDTO> productTypeDTOlist = new List<ProductTypeDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "SELECT vchmodulename,vchmoduleid FROM tabmodulemaster WHERE statusid =1;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            ProductTypeDTO productTypeDTO = new ProductTypeDTO();
                            productTypeDTO.vchmodulename = dr["vchmodulename"] == DBNull.Value ? "": Convert.ToString(dr["vchmodulename"]);
                            productTypeDTO.vchmoduleid = dr["vchmoduleid"] == DBNull.Value ? "" :  Convert.ToString(dr["vchmoduleid"]);
                            productTypeDTOlist.Add(productTypeDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return productTypeDTOlist;
        }


        public async Task<List<CategoryTypeDTO>> GetCategoryType(string globalSchema, string con)
        {
            List<CategoryTypeDTO> categoryTypeDTOlist = new List<CategoryTypeDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "SELECT vchcategoryname,vchcategoryid FROM tabcategorymaster WHERE statusid=1 ORDER BY vchcategoryname;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            CategoryTypeDTO categoryTypeDTO = new CategoryTypeDTO();
                            categoryTypeDTO.vchcategoryid = dr["vchcategoryid"] == DBNull.Value ? "" : Convert.ToString(dr["vchcategoryid"]);
                            categoryTypeDTO.vchcategoryname = dr["vchcategoryname"] == DBNull.Value ? "" : Convert.ToString(dr["vchcategoryname"]);
                            categoryTypeDTOlist.Add(categoryTypeDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return categoryTypeDTOlist;
        }


        public async Task<List<CompanyNameDTO>> GetProductCompanyName(string globalSchema, string con)
        {
            List<CompanyNameDTO> companyNameDTOlist = new List<CompanyNameDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "SELECT DISTINCT vchprodcompanyname,vchprodcompanyid FROM tabproductcompanymaster WHERE statusid=1 AND vchprodcompanyname <>''  ORDER BY vchprodcompanyname;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            CompanyNameDTO companyNameDTO = new CompanyNameDTO();
                            companyNameDTO.vchprodcompanyname = dr["vchprodcompanyname"] == DBNull.Value ? "" : Convert.ToString(dr["vchprodcompanyname"]);
                            companyNameDTO.vchprodcompanyid = dr["vchprodcompanyid"] == DBNull.Value ? "" :  Convert.ToString(dr["vchprodcompanyid"]);
                            companyNameDTOlist.Add(companyNameDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return companyNameDTOlist;
        }

        public bool AddProductCompanyName(CompanyNameDTO companyNameDTO, string globalSchema, string connectionString)
        {
            bool isSaved = false;
            string strVendorsave = string.Empty;
            string strcheck = string.Empty;
            string strNextID = string.Empty;

            try
            {
                con = new NpgsqlConnection(connectionString);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }


                trans = con.BeginTransaction();

                strcheck = "SELECT Count(*) FROM " + AddDoubleQuotes(globalSchema) + ".tabproductcompanymaster WHERE vchprodcompanyname='" + companyNameDTO.vchprodcompanyname + "';";

                int Count = Convert.ToInt32(NPGSqlHelper.ExecuteScalar(con, CommandType.Text, strcheck));
                if (Count == 0)
                {

                    strNextID = GetnextId("tabproductcompanymaster", "vchprodcompanyid", "3", "PC", connectionString);

                    strVendorsave = "INSERT INTO tabproductcompanymaster(vchprodcompanyid,vchprodcompanyname,statusid,createdby,createddate)";
                    strVendorsave = strVendorsave + "values('" + ManageQuote(strNextID) + "','" + ManageQuote(companyNameDTO.vchprodcompanyname).Trim() + "',1,1,CURRENT_TIMESTAMP);";

                    NPGSqlHelper.ExecuteNonQuery(trans, CommandType.Text, strVendorsave);
                    trans.Commit();
                    isSaved = true;
                }
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
            return isSaved;
        }
          
           

        public bool SaveProductCreation(ProductMasterDTO objmaster, string globalSchema, string connectionstring)
        {
            bool isSaved = false;
            string strVendorsave = string.Empty;
            string strcheck = string.Empty;
            string strNextID = string.Empty;
            string prodNextID = string.Empty;
            try
            {
                con = new NpgsqlConnection(connectionstring);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }


                trans = con.BeginTransaction();

                strcheck = "SELECT Count(*) FROM " + AddDoubleQuotes(globalSchema) + ".tabproductmaster WHERE vchproductname='" + objmaster.vchproductname + "' AND  vchuom='" + objmaster.vchuom + "' AND vchtype='" + objmaster.vchtype + "' AND vchcategoryname='" + objmaster.vchcategoryname + "' AND vchprodcompanyname='" + objmaster.vchprodcompanyname + "';";

                int Count = Convert.ToInt32(NPGSqlHelper.ExecuteScalar(con, CommandType.Text, strcheck));
                if (Count == 0)
                {

                    strNextID = GetnextId("tabproductmaster", "vchproductid", "2", "P", connectionstring);

                    strVendorsave = "INSERT INTO " + AddDoubleQuotes(globalSchema) + ".tabproductmaster(vchproductid,vchproductname, vchtype, statusid, createdby,createddate,vchuom,vchcategoryid,vchcategoryname,vchprodcompanyid,vchprodcompanyname,gst_percentage)";

                    strVendorsave = strVendorsave + "values('" + ManageQuote(strNextID) + "','" + objmaster.vchproductname + "','" + objmaster.vchtype + "',1,'" + objmaster.createdby + "',CURRENT_TIMESTAMP,'" + objmaster.vchuom + "','" + objmaster.vchcategoryid + "','" + objmaster.vchcategoryname + "','" + objmaster.vchprodcompanyid + "','" + objmaster.vchprodcompanyname + "'," + objmaster.gst_percentage + ");";

                    NPGSqlHelper.ExecuteNonQuery(trans, CommandType.Text, strVendorsave);
                    trans.Commit();
                    isSaved = true;
                }
              
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
            return isSaved;
        }


        public async Task<List<ProductMasterDTO>> GetProductDetails(string globalSchema, string con)
        {
            List<ProductMasterDTO> productMasterDTOList = new List<ProductMasterDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "SELECT vchproductid, vchproductname,vchtype,vchuom,vchcategoryname,COALESCE(vchprodcompanyname,'') as vchprodcompanyname FROM " + AddDoubleQuotes(globalSchema) + ".tabproductmaster;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            ProductMasterDTO productMasterDTO = new ProductMasterDTO();
                            productMasterDTO.vchproductid = dr["vchproductid"] == DBNull.Value ? "" : Convert.ToString(dr["vchproductid"]);
                            productMasterDTO.vchproductname = dr["vchproductname"] == DBNull.Value ? "" : Convert.ToString(dr["vchproductname"]);
                            productMasterDTO.vchtype = dr["vchtype"] == DBNull.Value ? "" : Convert.ToString(dr["vchtype"]);
                            productMasterDTO.vchuom = dr["vchuom"] == DBNull.Value ? "" : Convert.ToString(dr["vchuom"]);
                            productMasterDTO.vchcategoryname = dr["vchcategoryname"] == DBNull.Value ? "" : Convert.ToString(dr["vchcategoryname"]);
                            productMasterDTO.vchprodcompanyname = dr["vchprodcompanyname"] == DBNull.Value ? "" : Convert.ToString(dr["vchprodcompanyname"]);
                            productMasterDTOList.Add(productMasterDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return productMasterDTOList;
        }


        public async Task<List<StateNamesDTO>> GetStateNames(string globalSchema, string con)
        {
            List<StateNamesDTO> stateNamesDTOlist = new List<StateNamesDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "SELECT stateid,statename FROM tabstate ORDER BY statename;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            StateNamesDTO stateNameDTO = new StateNamesDTO();
                            stateNameDTO.stateid = dr["stateid"] == DBNull.Value ? 0 :  Convert.ToInt32(dr["stateid"]);
                            stateNameDTO.statename = dr["statename"] == DBNull.Value ? "" : Convert.ToString(dr["statename"]);
                            stateNamesDTOlist.Add(stateNameDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return stateNamesDTOlist;
        }


        public bool SaveVendordDetails(VendorDTO objmaster, string globalSchema, string connectionstring)
        {
            bool isSaved = false;
            string strVendorsave = string.Empty;
            string strcheck = string.Empty;
            string strNextID = string.Empty;
            
            try
            {
                con = new NpgsqlConnection(connectionstring);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }


                trans = con.BeginTransaction();

                strcheck = "select Count(*) from tabvendormaster where vchvendorname='" + objmaster.vendorname + "' and vchtype= '" + ManageQuote(objmaster.vendortype) + "';";

                int Count = Convert.ToInt32(NPGSqlHelper.ExecuteScalar(con, CommandType.Text, strcheck));
                if (Count == 0)
                {
                    
                    strNextID = GetnextId("tabvendormaster", "vchvendorid", "2", "V", connectionstring);

                    strVendorsave = "insert into tabvendormaster(vchvendorid,vchvendorname,vchmobilenumber,vcharea,vchcity,vchtype,statusid,createdby,stateid,createddate)";
                    strVendorsave = strVendorsave + "values('" + ManageQuote(strNextID) + "','" + ManageQuote(objmaster.vendorname) + "','" + objmaster.mobile_no + "','" + ManageQuote(objmaster.area) + "','" + ManageQuote(objmaster.city) + "','" + ManageQuote(objmaster.vendortype) + "'," + objmaster.statusid + ",'" + objmaster.createdby + "','" + objmaster.stateid + "',CURRENT_TIMESTAMP);";
                    
                }
                else if (Count > 0)
                {
                    strVendorsave = "update tabvendormaster set stateid = '" + objmaster.stateid + "', vchmobilenumber = '" + objmaster.mobile_no + "', vchcity = '" + ManageQuote(objmaster.city) + "', vcharea = '" + ManageQuote(objmaster.area) + "', statusid = " + objmaster.statusid + ", modifiedby = " + objmaster.modifiedby + ", modifieddate = CURRENT_TIMESTAMP where vchvendorname = '" + objmaster.vendorname + "' and vchtype = '" + ManageQuote(objmaster.vendortype) + "';";
                }

                NPGSqlHelper.ExecuteNonQuery(trans, CommandType.Text, strVendorsave);
                trans.Commit();
                isSaved = true;

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
            return isSaved;
        }


        public async Task<List<VendorDTO>> GetVendorDetails(string globalSchema, string con)
        {
            List<VendorDTO> vendorsDTOlist = new List<VendorDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "SELECT vm.vchvendorid, vm.vchvendorname,vm.vchtype,vm.vchmobilenumber,vm.vcharea,vm.vchcity,vm.statusid,stateid,(SELECT statename FROM tabstate WHERE stateid=vm.stateid) statename FROM tabvendormaster vm;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            VendorDTO vendorDTO = new VendorDTO();
                            vendorDTO.vchvendorid = dr["vchvendorid"] == DBNull.Value ? "" : Convert.ToString(dr["vchvendorid"]);
                            vendorDTO.vendorname = dr["vchvendorname"] == DBNull.Value ? "" : Convert.ToString(dr["vchvendorname"]);
                            vendorDTO.vendortype = dr["vchtype"] == DBNull.Value ? "" : Convert.ToString(dr["vchtype"]);
                            vendorDTO.mobile_no = dr["vchmobilenumber"] == DBNull.Value ? "" : Convert.ToString(dr["vchmobilenumber"]);
                            vendorDTO.area = dr["vcharea"] == DBNull.Value ? "" : Convert.ToString(dr["vcharea"]);
                            vendorDTO.city = dr["vchcity"] == DBNull.Value ? "" : Convert.ToString(dr["vchcity"]);
                            vendorDTO.statusid = dr["statusid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["statusid"]);
                            vendorDTO.stateid = dr["stateid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["stateid"]);
                            vendorDTO.statename = dr["statename"] == DBNull.Value ? "" : Convert.ToString(dr["statename"]);
                            vendorsDTOlist.Add(vendorDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return vendorsDTOlist;
        }



        public bool SaveTankDetails(TanksDTO tanksDTO, string globalSchema, string connectionstring)
        {
            bool isSaved = false;
            string strtanksave = string.Empty;
            string strcheck = string.Empty;

            try
            {
                con = new NpgsqlConnection(connectionstring);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                trans = con.BeginTransaction();

                strcheck = "SELECT COUNT(*)  FROM tabtankslist WHERE vchtankname='" + ManageQuote(tanksDTO.vchtankname) + "' OR  vchtankid='" + ManageQuote(tanksDTO.vchtankid) + "';";

                int Count = Convert.ToInt32(NPGSqlHelper.ExecuteScalar(con, CommandType.Text, strcheck));
                if (Count == 0)
                {
                    strtanksave = "insert into tabtankslist (vchtankname,statusid,createddate,createdby,vchtankid) values('" + ManageQuote(tanksDTO.vchtankname) + "'," + tanksDTO.statusid + ",current_timestamp,1,'" + ManageQuote(tanksDTO.vchtankid) + "');";

                    NPGSqlHelper.ExecuteNonQuery(trans, CommandType.Text, strtanksave);
                    trans.Commit();
                    isSaved = true;
                } 
                
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
            return isSaved;
        }


        public async Task<List<TanksDTO>> GetTanksDetails(string globalSchema, string con)
        {
            List<TanksDTO> tanksDTOlist = new List<TanksDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "SELECT vchtankid,vchtankname FROM " + AddDoubleQuotes(globalSchema) + ".tabtankslist;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            TanksDTO tanksDTO = new TanksDTO();
                            tanksDTO.vchtankid = dr["vchtankid"] == DBNull.Value ? "" : Convert.ToString(dr["vchtankid"]);
                            tanksDTO.vchtankname = dr["vchtankname"] == DBNull.Value ? "" : Convert.ToString(dr["vchtankname"]);
                            tanksDTOlist.Add(tanksDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return tanksDTOlist;
        }

        public bool SaveMainValveDetails(MainValveDTO mainValveDTO, string globalSchema, string connectionstring)
        {
            bool isSaved = false;
            string strmainValvesave = string.Empty;
            string strcheck = string.Empty;

            try
            {
                con = new NpgsqlConnection(connectionstring);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                trans = con.BeginTransaction();

                strcheck = "SELECT COUNT(*)  from tabnewmainvalvelist where vchtankid='" + ManageQuote(mainValveDTO.vchtankid) + "' AND vchmainvalve='" + ManageQuote(mainValveDTO.vchmainvalve) + "' ;";

                int Count = Convert.ToInt32(NPGSqlHelper.ExecuteScalar(con, CommandType.Text, strcheck));
                if (Count == 0)
                {
                    strmainValvesave = "INSERT INTO tabnewmainvalvelist (vchtankid,vchmainvalve,statusid,createddate,createdby) values('" + ManageQuote(mainValveDTO.vchtankid) + "','" + ManageQuote(mainValveDTO.vchmainvalve) + "'," + mainValveDTO.statusid + ",current_timestamp,1);";

                    NPGSqlHelper.ExecuteNonQuery(trans, CommandType.Text, strmainValvesave);
                    trans.Commit();
                    isSaved = true;
                }

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
            return isSaved;
        }


        public async Task<List<MainValveDTO>> GetMainValveDetails(string globalSchema, string con)
        {
            List<MainValveDTO> mainValvesDTOlist = new List<MainValveDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "SELECT vchtankid,vchmainvalve  FROM " + AddDoubleQuotes(globalSchema) + ".tabnewmainvalvelist;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            MainValveDTO mainValvesDTO = new MainValveDTO();
                            mainValvesDTO.vchtankid = dr["vchtankid"] == DBNull.Value ? "" : Convert.ToString(dr["vchtankid"]);
                            mainValvesDTO.vchmainvalve = dr["vchmainvalve"] == DBNull.Value ? "" : Convert.ToString(dr["vchmainvalve"]);
                            mainValvesDTOlist.Add(mainValvesDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return mainValvesDTOlist;
        }


        public async Task<List<MainValveDTO>> GetMainValvesByTankId(string tankId, string globalSchema, string con)
        {
            List<MainValveDTO> mainValvesDTOlist = new List<MainValveDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "SELECT vchmainvalve  FROM " + AddDoubleQuotes(globalSchema) + ".tabnewmainvalvelist Where vchtankid = '"+ tankId + "';";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            MainValveDTO mainValvesDTO = new MainValveDTO();
                            mainValvesDTO.vchmainvalve = dr["vchmainvalve"] == DBNull.Value ? "" : Convert.ToString(dr["vchmainvalve"]);
                            mainValvesDTOlist.Add(mainValvesDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return mainValvesDTOlist;
        }

        public bool SaveSubValveDetails(SubValveDTO subValveDTO, string globalSchema, string connectionstring)
        {
            bool isSaved = false;
            string strsubValvesave = string.Empty;
            string strcheck = string.Empty;

            try
            {
                con = new NpgsqlConnection(connectionstring);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                trans = con.BeginTransaction();

                strcheck = "SELECT COUNT(*)  FROM tabnewsubvalvelist WHERE vchtankid='" + ManageQuote(subValveDTO.vchtankid) + "' AND vchmainvalve='" + ManageQuote(subValveDTO.vchmainvalve) + "' AND vchsubvalve='" + ManageQuote(subValveDTO.vchsubvalve) + "';";

                int Count = Convert.ToInt32(NPGSqlHelper.ExecuteScalar(con, CommandType.Text, strcheck));
                if (Count == 0)
                {
                    strsubValvesave = "INSERT INTO tabnewsubvalvelist (vchtankid,vchmainvalve,vchsubvalve,statusid,createddate,createdby) VALUES('" + ManageQuote(subValveDTO.vchtankid) + "','" + ManageQuote(subValveDTO.vchmainvalve) + "','" + ManageQuote(subValveDTO.vchsubvalve) + "'," + subValveDTO.statusid + ",current_timestamp,1);";

                    NPGSqlHelper.ExecuteNonQuery(trans, CommandType.Text, strsubValvesave);
                    trans.Commit();
                    isSaved = true;
                }

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
            return isSaved;
        }

        public async Task<List<SubValveDTO>> GetSubValveDetails(string globalSchema, string con)
        {
            List<SubValveDTO> subValvesDTOlist = new List<SubValveDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "SELECT vchtankid,vchmainvalve,vchsubvalve FROM tabnewsubvalvelist;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            SubValveDTO subValvesDTO = new SubValveDTO();
                            subValvesDTO.vchtankid = dr["vchtankid"] == DBNull.Value ? "" : Convert.ToString(dr["vchtankid"]);
                            subValvesDTO.vchmainvalve = dr["vchmainvalve"] == DBNull.Value ? "" : Convert.ToString(dr["vchmainvalve"]);
                            subValvesDTO.vchsubvalve = dr["vchsubvalve"] == DBNull.Value ? "" : Convert.ToString(dr["vchsubvalve"]);
                            subValvesDTOlist.Add(subValvesDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return subValvesDTOlist;
        }

        public async Task<List<RouteNamesDTO>> GetRouteNames(string globalSchema, string con)
        {
            List<RouteNamesDTO> routeNamesDTOlist = new List<RouteNamesDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "SELECT delivery_routename FROM tabdairyroutemst WHERE statusid =1 ORDER BY delivery_routename;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            RouteNamesDTO routeNamesDTO = new RouteNamesDTO();
                            routeNamesDTO.delivery_routename = dr["delivery_routename"] == DBNull.Value ? "" : Convert.ToString(dr["delivery_routename"]);
                            routeNamesDTOlist.Add(routeNamesDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return routeNamesDTOlist;
        }





        public bool SaveCustomerDetails(CustomerDTO customerDTO, string globalSchema, string connectionstring)
        {
            bool isSaved = false;
            string strCustomerDetailssave = string.Empty;
            string strCustomersave = string.Empty;
            string strcheck = string.Empty;
            string strNextID = string.Empty;
            string strPurchaseno = string.Empty;
            

            try
            {
                con = new NpgsqlConnection(connectionstring);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                trans = con.BeginTransaction();
                if (customerDTO.typeofoperation == "Save")
                {
                    strcheck = @"select tcm.vchcustomerid, tcm.vchcustomername,tcmd.vchtype from tabcustomerdetailsmaster  tcmd, tabcustomermaster  tcm where tcm.vchcustomerid=tcmd.vchcustomerid and tcm.vchcustomername='" + customerDTO.vchcustomername + "'";
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(strcheck, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        trans.Rollback();
                        isSaved = false;
                    }
                    else
                    {

                        strNextID = GetnextId("tabcustomermaster", "vchcustomerid", "2", "C", connectionstring);

                        strCustomersave = "insert into tabcustomermaster(vchcustomerid,vchcustomername,vchmobilenumber,vcharea,vchcity,statusid,createdby,delivery_routename,createddate,stateid)";
                        strCustomersave = strCustomersave + " values('" + ManageQuote(strNextID) + "','" + ManageQuote(customerDTO.vchcustomername) + "','" + customerDTO.vchmobilenumber + "','" + ManageQuote(customerDTO.vcharea) + "','" + ManageQuote(customerDTO.vchcity) + "'," + customerDTO.statusid + ",'" + customerDTO.createdby + "','" + ManageQuote(customerDTO.delivery_routename) + "',CURRENT_TIMESTAMP,"+customerDTO.stateid+");";
                        NPGSqlHelper.ExecuteNonQuery(trans, CommandType.Text, strCustomersave) ;

                        strCustomerDetailssave = "insert into tabcustomerdetailsmaster(vchcustomerid,vchcustomername,vchtype,statusid,createdby,createddate)";
                        strCustomerDetailssave = strCustomerDetailssave + " values('" + ManageQuote(strNextID) + "','" + ManageQuote(customerDTO.vchcustomername) + "','" + ManageQuote(customerDTO.vchtype) + "'," + customerDTO.statusid + ",'" + customerDTO.createdby + "',CURRENT_TIMESTAMP);";
                        NPGSqlHelper.ExecuteNonQuery(trans, CommandType.Text, strCustomerDetailssave);

                        trans.Commit();
                        isSaved = true;
                    }
                }
                if (customerDTO.typeofoperation == "Update")
                {
                    strcheck = @"select tcm.vchcustomerid, tcm.vchcustomername,tcmd.vchtype from tabcustomerdetailsmaster  tcmd, tabcustomermaster  tcm where tcm.vchcustomerid=tcmd.vchcustomerid and vchtype= '" + ManageQuote(customerDTO.vchtype) + "' and tcm.vchcustomername='" + customerDTO.vchcustomername + "';";
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(strcheck, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        trans.Rollback();
                        isSaved = false;
                    }
                    else
                    {
                        strcheck = @"select tcm.vchcustomerid, tcm.vchcustomername,tcmd.vchtype from tabcustomerdetailsmaster  tcmd, tabcustomermaster  tcm where tcm.vchcustomerid=tcmd.vchcustomerid and  tcm.vchcustomername='" + customerDTO.vchcustomername + "';";
                        da = new NpgsqlDataAdapter(strcheck, con);
                        ds = new DataSet();
                        da.Fill(ds);
                        strNextID = ds.Tables[0].Rows[0]["vchcustomerid"].ToString();
                        strCustomerDetailssave = "insert into tabcustomerdetailsmaster(vchcustomerid,vchcustomername,vchtype,statusid,createdby,createddate)";
                        strCustomerDetailssave = strCustomerDetailssave + "values('" + ManageQuote(strNextID) + "','" + ManageQuote(customerDTO.vchcustomername) + "','" + ManageQuote(customerDTO.vchtype) + "'," + customerDTO.statusid + ",'" + customerDTO.createdby + "',CURRENT_TIMESTAMP);";
                        NPGSqlHelper.ExecuteNonQuery(trans, CommandType.Text, strCustomerDetailssave);

                        trans.Commit();
                        isSaved = true;
                    }

                    strcheck = @"select tcm.vchcustomerid, tcm.vchcustomername,tcmd.vchtype,tcm.delivery_routename from tabcustomerdetailsmaster  tcmd, tabcustomermaster  tcm where tcm.vchcustomerid=tcmd.vchcustomerid and vchtype= '" + ManageQuote(customerDTO.vchtype) + "' and delivery_routename='" + customerDTO.delivery_routename + "' and tcm.vchcustomername='" + customerDTO.vchcustomername + "';";
                    NpgsqlDataAdapter daroute = new NpgsqlDataAdapter(strcheck, con);
                    DataSet dsr = new DataSet();
                    daroute.Fill(dsr);

                    if (dsr.Tables[0].Rows.Count == 1)
                    {

                        string Qry = "Update tabcustomermaster Set delivery_routename='" + ManageQuote(customerDTO.delivery_routename) + "',vchmobilenumber='" + customerDTO.vchmobilenumber + "',vchcity='" + ManageQuote(customerDTO.vchcity) + "',vcharea='" + ManageQuote(customerDTO.vcharea) + "',statusid='" + customerDTO.statusid + "',modifiedby='" + customerDTO.modifiedby + "',modifieddate=current_timestamp where vchcustomername='" + customerDTO.vchcustomername + "';";
                        int i = NPGSqlHelper.ExecuteNonQuery(con, CommandType.Text, Qry);
                        isSaved = true;
                    }

                }

            }
            catch (Exception ex)
            {
                trans.Rollback();
                isSaved = false;
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
            return isSaved;
        }

        
        public async  Task<List<CustomerDTO>> GetCustomerDetails(string globalSchema, string con)
        {
            List<CustomerDTO> customersDTOlist = new List<CustomerDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "select tcm.vchcustomerid, tcm.vchcustomername,tcmd.vchtype,tcm.vchmobilenumber,tcm.vcharea,tcm.vchcity,coalesce(tcm.delivery_routename, '') as delivery_routename,tcm.statusid,tcm.stateid,(select statename from tabstate where stateid = tcm.stateid) statename from tabcustomerdetailsmaster tcmd, tabcustomermaster tcm where tcm.vchcustomerid = tcmd.vchcustomerid  order by tcm.vchcustomername ,tcmd.vchtype;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            CustomerDTO customersDTO = new CustomerDTO();
                            customersDTO.delivery_routename = dr["delivery_routename"] == DBNull.Value ? "" : Convert.ToString(dr["delivery_routename"]);
                            customersDTO.vchcustomername = dr["vchcustomername"] == DBNull.Value ? "" : Convert.ToString(dr["vchcustomername"]);
                            customersDTO.vchtype = dr["vchtype"] == DBNull.Value ? "" : Convert.ToString(dr["vchtype"]);
                            customersDTO.vchmobilenumber = dr["vchmobilenumber"] == DBNull.Value ? "" : Convert.ToString(dr["vchmobilenumber"]);
                            customersDTO.vcharea = dr["vcharea"] == DBNull.Value ? "" : Convert.ToString(dr["vcharea"]);
                            customersDTO.vchcity = dr["vchcity"] == DBNull.Value ? "" : Convert.ToString(dr["vchcity"]);
                            customersDTO.vchcustomerid = dr["vchcustomerid"] == DBNull.Value ? "" : Convert.ToString(dr["vchcustomerid"]);
                            customersDTO.stateid = dr["stateid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["stateid"]);
                            customersDTO.statename = dr["statename"] == DBNull.Value ? "" : Convert.ToString(dr["statename"]);
                            customersDTO.statusid = dr["statusid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["statusid"]);
                            customersDTOlist.Add(customersDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return customersDTOlist;
        }



        public bool SaveWeatherDetails(WeatherEntryDTO weatherentryDTO, string globalSchema, string connectionstring)
        {
            bool isSaved = false;
            string strSave = string.Empty;
            string strWeatherDuplicate = string.Empty;
            string strWeatherId = string.Empty;
            List<WeatherEntryDTO> weatherentryDTOlist = new List<WeatherEntryDTO>();

            try
            {
                con = new NpgsqlConnection(connectionstring);

                strWeatherDuplicate = @"select count(*) from tabweatherdetails where datweatherdate ='" + FormatDate(weatherentryDTO.WeatherDate) + "' and vchtime='" + weatherentryDTO.WeatherTime + "';";

                int count = Convert.ToInt32(NPGSqlHelper.ExecuteScalar(con, CommandType.Text, strWeatherDuplicate));


                if (count == 0)
                {
                    strWeatherId = @"insert into tabweatherdetails (datentrydate,datweatherdate,vchtime,numtempmin,numtempmax,vchrain,numraininmm,numhumidity,statusid,createdby,createddate) values ( '" + FormatDate(weatherentryDTO.WeatherEntryDate) + "','" + FormatDate(weatherentryDTO.WeatherDate) + "','" + weatherentryDTO.WeatherTime + "'," + weatherentryDTO.TemparatureMin + "," + weatherentryDTO.TemparatureMax + ",'" + weatherentryDTO.Rain + "'," + weatherentryDTO.RainInMM + "," + weatherentryDTO.Humidity + "," + weatherentryDTO.statusid + "," + weatherentryDTO.createdby + ",CURRENT_TIMESTAMP);";
                    NPGSqlHelper.ExecuteNonQuery(con, CommandType.Text, strWeatherId);
                    trans.Commit();
                    isSaved = true;
                }

            }
            catch (Exception ex)
            {
                trans.Rollback();
                isSaved = false;
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

            return isSaved;
        }


        public async Task<List<ServiceNosDTO>> GetServiceNos(string globalSchema, string con)
        {
            List<ServiceNosDTO> serviceNosDTOlist = new List<ServiceNosDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "select serviceno from  electricitymst;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            ServiceNosDTO serviceNosDTO = new ServiceNosDTO();
                            serviceNosDTO.serviceno = dr["serviceno"] == DBNull.Value ? "" : Convert.ToString(dr["serviceno"]);
                            serviceNosDTOlist.Add(serviceNosDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return serviceNosDTOlist;
        }

        public async Task<ServiceNosDTO> GetUscNoValues(string Serviceno,string globalSchema, string con)
        {
            ServiceNosDTO serviceNosDTO = new ServiceNosDTO();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "select uscno, companyname, location from electricitymst where serviceno = '" + Serviceno + "';";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            serviceNosDTO.uscno = dr["uscno"] == DBNull.Value ? "" : Convert.ToString(dr["uscno"]);
                            serviceNosDTO.companyname = dr["companyname"] == DBNull.Value ? "" : Convert.ToString(dr["companyname"]);
                            serviceNosDTO.location = dr["location"] == DBNull.Value ? "" : Convert.ToString(dr["location"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return serviceNosDTO;
        }


        public bool SaveElectricityBillDetails(ElectricityBillDTO electricityBillDTO, string globalSchema, string connectionString)
        {
            bool isSaved = false;
            string strVendorsave = string.Empty;
            string strcheck = string.Empty;

            try
            {
                con = new NpgsqlConnection(connectionString);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                trans = con.BeginTransaction();
                strcheck = "select vchcompanyname, vchmonth, vchuscno  from tabelectricityformvalues where vchcompanyname='" + ManageQuote(electricityBillDTO.eleComapnyName) + "' and vchdate='" + electricityBillDTO.eleDate + "' and vchuscno='" + electricityBillDTO.USCNo + "' and vchscno='" + electricityBillDTO.SCNo + "'  and vchmonth='" + electricityBillDTO.eleMonth + "' and vchlocation ='" + electricityBillDTO.eleLocation + "';";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(strcheck, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    trans.Rollback();
                    isSaved = false;
                }
                else
                {
                     strVendorsave = "INSERT INTO tabelectricityformvalues(vchcompanyname, vchdate, vchuscno, vchscno, vchlocation, vchmonth, vchfromunit, vchtounit,vchnoofunits,vchrate,vchamount, statusid, createdby, createddate)VALUES('" + ManageQuote(electricityBillDTO.eleComapnyName) + "','" + electricityBillDTO.eleDate + "','" + electricityBillDTO.USCNo + "','" + electricityBillDTO.SCNo + "','" + ManageQuote(electricityBillDTO.eleLocation) + "','" + electricityBillDTO.eleMonth + "','" + ManageQuote(electricityBillDTO.FromUnits) + "','" + electricityBillDTO.Tounits + "','" + electricityBillDTO.NoOfUnits + "','" + electricityBillDTO.Rate + "','" + electricityBillDTO.Amount + "',1,1,current_timestamp" + ");";
                     NPGSqlHelper.ExecuteNonQuery(trans, CommandType.Text, strVendorsave);


                    trans.Commit();
                    isSaved = true;
                }

            }
            catch (Exception ex)
            {
                trans.Rollback();
                isSaved = false;
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
            return isSaved;
        }

















        public string GetnextId(string strTablename, string strcolumn, string strprefixlength, string prifixletter,string connection)
        {


            string strnextId = string.Empty;
            try
            {
                strnextId = Convert.ToString(NPGSqlHelper.ExecuteScalar(connection, CommandType.Text, "SELECT (MAX(CAST(COALESCE(SUBSTRING(" + strcolumn + "," + strprefixlength + ",LENGTH(" + strcolumn + ")),'0') AS NUMERIC))+1) AS NEXTID FROM " + strTablename + ""));
                if (strnextId == null || strnextId == "")
                {
                    strnextId = prifixletter + "1";
                }
                else
                {
                    strnextId = prifixletter + strnextId;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return strnextId;
        }


        

    }
}
