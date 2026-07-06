using System;
using System.Collections.Generic;
using System.Text;
using Easychit_Infrastructure.Dar.Horticulture;
using Easychit_Repository.Interfaces.Dar.Horticulture;
using Easychit_Infrastructure.Dar.Dar_Masters;
using System.Threading.Tasks;
using HelperManager;
using Npgsql;
using System.Data;

namespace Easychit_Repository.DataAccess.Dar.Horticulture
{
    public class HorticultureDAL: CommonDAL, IHorticulture
    {
        NpgsqlTransaction trans = null;
        NpgsqlConnection con = new NpgsqlConnection(NPGSqlHelper.SQLConnString);
        public async Task<List<FruitsDTO>> GetFruits(string globalSchema, string con)
        {
            List<FruitsDTO> fruitssDTOlist = new List<FruitsDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "select vchfruitsid,vchtypeoffruits from tabfruitsmst  where intstatus =1 order by vchtypeoffruits;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            FruitsDTO fruitsDTO = new FruitsDTO();
                            fruitsDTO.vchfruitsid = dr["vchfruitsid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["vchfruitsid"]);
                            fruitsDTO.vchtypeoffruits = dr["vchtypeoffruits"] == DBNull.Value ? "" : Convert.ToString(dr["vchtypeoffruits"]);
                            fruitssDTOlist.Add(fruitsDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return fruitssDTOlist;
        }


        public bool SavePlantationDetails(PlantationDTO plantation, string globalSchema, string connectionstring)
        {
            bool isSaved = false;
            string strVendorsave = string.Empty;
            string strcheck = string.Empty;
            string strNextID = string.Empty;

            string strUsageDate = FormatDate(plantation.datplantaiondate.ToString("dd-MM-yyyy"));

            try
            {
                con = new NpgsqlConnection(connectionstring);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                trans = con.BeginTransaction();
                strcheck = "select vchtankid,vchmainvalve,vchsubvalve,vchfruittype  from tabplantation where vchtankid='" + ManageQuote(plantation.vchtankid) + "' and vchmainvalve='" + ManageQuote(plantation.vchmainvalve) + "' and vchsubvalve='" + ManageQuote(plantation.vchsubvalve) + "' and vchfruittype='" + plantation.vchfruittype + "'  and vchlines='" + plantation.vchlines + "' and numplantsfrom=" + plantation.numplantsfrom + " and statusid=1";
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
                    for (int i = plantation.numplantsfrom; i <= plantation.numplantsto; i++)
                    {
                        int line = i;
                        strVendorsave = "INSERT INTO tabplantation(vchtankid, vchmainvalve, vchsubvalve, vchlines, numplantsfrom, vchfruittype, datplantaiondate, statusid, createddate,createdby,numageofplant)VALUES('" + ManageQuote(plantation.vchtankid) + "','" + ManageQuote(plantation.vchmainvalve) + "','" + ManageQuote(plantation.vchsubvalve) + "','" + ManageQuote(plantation.vchlines) + "'," + line + ",'" + ManageQuote(plantation.vchfruittype) + "','" + strUsageDate + "'," + plantation.statusid + ",current_timestamp,1," + plantation.numageofplant + ");";
                        NPGSqlHelper.ExecuteNonQuery(trans, CommandType.Text, strVendorsave);

                    }


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
                    con.Close();
                    con.Dispose();
                    trans.Dispose();
                }
            }
            return isSaved;
        }

        public async Task<List<SubValveDTO>> GetSubValvesByTankAndMainValve(string tankId, string mainValve, string globalSchema, string con)
        {
            List<SubValveDTO> subValvesDTOlist = new List<SubValveDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "SELECT vchsubvalve FROM tabnewsubvalvelist WHERE vchtankid = '" + tankId + "' and vchmainvalve = '" + mainValve + "';";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            SubValveDTO subValvesDTO = new SubValveDTO();
                            subValvesDTO.vchsubvalve = dr["vchsubvalve"]  == DBNull.Value ? "" : Convert.ToString(dr["vchsubvalve"]);
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


        public async Task<List<FruitsDTO>> GetUOMByFruit(string productType, string ModuleName, string globalSchema, string con)
        {
            List<FruitsDTO> fruitssDTOlist = new List<FruitsDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "select vchtypeoffruits,vchuom,coalesce(gst_percentage,0)gst_percentage from tabfruitsmst  where  vchtypeoffruits='" + productType + "' ;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            FruitsDTO fruitsDTO = new FruitsDTO();
                            fruitsDTO.vchtypeoffruits = dr["vchtypeoffruits"] == DBNull.Value ? "" : Convert.ToString(dr["vchtypeoffruits"]);
                            fruitsDTO.vchuom = dr["vchuom"] == DBNull.Value ? "" : Convert.ToString(dr["vchuom"]);
                            fruitsDTO.gst_percentage = dr["gst_percentage"] == DBNull.Value ? "" : Convert.ToString(dr["gst_percentage"]);
                            fruitssDTOlist.Add(fruitsDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return fruitssDTOlist;
        }

        public async Task<List<AreaValvesDTO>> GetAreaValves(string productType, string globalSchema, string con)
        {
            List<AreaValvesDTO> fruitssDTOlist = new List<AreaValvesDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "select distinct vchtankid ||'-'|| vchmainvalve ||'-'|| vchsubvalve  as vchsubvalve,0 as recordid from tabplantation  where vchfruittype like '" + productType + "%' order by vchsubvalve;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            AreaValvesDTO fruitsDTO = new AreaValvesDTO();
                            fruitsDTO.vchsubvalve = dr["vchsubvalve"] == DBNull.Value ? "" : Convert.ToString(dr["vchsubvalve"]);
                            fruitssDTOlist.Add(fruitsDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return fruitssDTOlist;
        }

        public async Task<List<AreaValvesDTO>> GetRows(string productType, string valve, string globalSchema, string con)
        {
            List<AreaValvesDTO> fruitssDTOlist = new List<AreaValvesDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "select distinct vchlines from tabplantation where vchfruittype = '" + productType + "' and (vchtankid ||'-'|| vchmainvalve ||'-'|| vchsubvalve)='" + valve + "';";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            AreaValvesDTO fruitsDTO = new AreaValvesDTO();
                            fruitsDTO.vchlines = dr["vchlines"] == DBNull.Value ? "" : Convert.ToString(dr["vchlines"]);
                            fruitssDTOlist.Add(fruitsDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return fruitssDTOlist;
        }


        public bool SaveProductionDetails(ProductionDTO productionDTO, string globalSchema, string connectionstring)
        {
            bool isSaved = false;
            string strproductionsave = string.Empty;
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
                


                for (int i = 0; i < productionDTO.rowscount; i++)
                    {
                    strNextID = "SELECT 'HP' || (COALESCE(MAX(SUBSTRING(vchproductionno FROM 3)::int), 0) + 1) AS next_vchproductionno FROM TABHORTICULTUREPRODUCTION;";

                    strNextID = Convert.ToString(NPGSqlHelper.ExecuteScalar(con, CommandType.Text, strNextID));
                    DateTime today = DateTime.Now;
                    string yearLastTwoDigits = today.ToString("yy");
                    strNextID = strNextID + "/" + yearLastTwoDigits + "";

                    strproductionsave = strproductionsave + "INSERT INTO TABHORTICULTUREPRODUCTION(vchproductionno,datproductiondate,vchfruittype,numquantity,numdamagequantity,numnetquantity,vchuom,vchgarden,vchremarks,statusid,createdby,createddate,vchlines) VALUES('" + strNextID + "',CURRENT_TIMESTAMP,'" + ManageQuote(productionDTO.vchfruittype[i]) + "'," + productionDTO.numquantity[i] + "," + productionDTO.numdamagequantity[i] + "," + productionDTO.numnetquantity[i] + ",'" + ManageQuote(productionDTO.vchuom[i]) + "','" + ManageQuote(productionDTO.vchgarden[i]) + "','" + ManageQuote(productionDTO.vchremarks) + "','" + productionDTO.statusid + "','" + productionDTO.createdby + "',CURRENT_TIMESTAMP,'" + ManageQuote(productionDTO.vchlines[i]) + "');";

                }
                NPGSqlHelper.ExecuteNonQuery(trans, CommandType.Text, strproductionsave);
                trans.Commit();
                isSaved = true;
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


        

    }
}
