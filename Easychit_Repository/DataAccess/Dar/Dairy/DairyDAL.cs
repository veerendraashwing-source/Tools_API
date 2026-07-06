using System;
using System.Collections.Generic;
using System.Text;
using HelperManager;
using Npgsql;
using System.Data;
using Easychit_Infrastructure.Dar.Dairy;
using Easychit_Repository.Interfaces.Dar.Dairy;
using System.Threading.Tasks;


namespace Easychit_Repository.DataAccess.Dar.Dairy
{
    public class DairyDAL : CommonDAL, IDairy
    {
        NpgsqlTransaction trans = null;
        NpgsqlConnection con = new NpgsqlConnection(NPGSqlHelper.SQLConnString);
        public async Task<List<ProductionDTO>> GetTagNo(string globalSchema, string con)
        {
            List<ProductionDTO> productionlist = new List<ProductionDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "select 'DUNG_DUNG' as vchcattle union all select vchcattle from tabtagcattlemaster where vchstatus = 'Y' order by 1; ";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            ProductionDTO productionDTO = new ProductionDTO();
                            productionDTO.vchcattle = Convert.ToString(dr["vchcattle"]);
                            productionlist.Add(productionDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return productionlist;
        }


        public async Task<List<ProductionDTO>> GetMilkType(string globalSchema, string con, string tag)
        {
            List<ProductionDTO> productionlist = new List<ProductionDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {


                    if (tag == "DUNG")
                    {
                        Query = "select Milk_name,Milk_id from tabmilktypemst where status=1 and  Milk_name like '%" + tag + "%' ;";
                    }
                    else
                    {

                        Query = "select Milk_name,Milk_id from tabmilktypemst where status=1 and milk_id<>'DUNG' and Milk_name like '%" + tag + "%' ;";
                    }

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            ProductionDTO productionDTO = new ProductionDTO();
                            productionDTO.milk_id = Convert.ToString(dr["Milk_id"]);
                            productionDTO.milk_name = Convert.ToString(dr["Milk_name"]);
                            productionlist.Add(productionDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return productionlist;
        }

        public async Task<List<DamageReasonDTO>> GetDamageReason(string globalSchema, string con)
        {
            List<DamageReasonDTO> reasonslist = new List<DamageReasonDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "select distinct reason_description  from tabdairy_milk_damage_reasons_mst order by reason_description;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            DamageReasonDTO reasonslistDTO = new DamageReasonDTO();
                            reasonslistDTO.reason_description = Convert.ToString(dr["reason_description"]);
                            reasonslist.Add(reasonslistDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return reasonslist;
        }

        public string GenerateNextID(string strtablename, string strcolname, int prefix, string strdate, string strColumnDate, string strPrefix)
        {

            string GNextID = string.Empty; 

            try
            {

                string Query = "select * from  GENERATENEXTID('" + strtablename + "','" + strcolname + "'," + prefix + ",'" + strdate + "','" + strColumnDate + "','" + strPrefix + "')";

                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con.ConnectionString, CommandType.Text, Query))
                {
                    if (dr.Read())
                    {
                        GNextID = dr[0].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return GNextID;
        }


        public bool SaveProductionDetails(List<SaveProductionDTO> SaveProductionDTO, string globalSchema, string connectionstring)
        {
            bool isSaved = false;
            string strSave = string.Empty;
            string strProductionDuplicate = string.Empty;
            string strProductId = string.Empty;
            string strNextID = string.Empty;
            string strdcduplicate = string.Empty;
            string strtagno = string.Empty;
            string strDairyReturnInsert = string.Empty;


            try
            {
                con = new NpgsqlConnection(connectionstring);
                for (int j = 0; j < SaveProductionDTO.Count; j++)
                {


                    string productionDateStr = SaveProductionDTO[j].datproductiondate?.ToString() ?? "";

                    strNextID = GenerateNextID(
                        "TABDAIRYPRODUCTION",
                        "VCHPRODUCTIONNO",
                        2,
                        productionDateStr,   
                        "DATPRODUCTIONDATE",
                        "DP"
                    )?.ToString();         

                    strNextID = "DP" + strNextID;

                    strdcduplicate = "select  count(*) from tabdairyproduction where datproductiondate='" + SaveProductionDTO[j].datproductiondate + "' and vchmilkedat='" + ManageQuote(SaveProductionDTO[j].vchmilkedat) + "' and vchtagnumber='" + ManageQuote(SaveProductionDTO[j].vchtagnumber) + "'";



                    int Count = Convert.ToInt32(NPGSqlHelper.ExecuteScalar(con, CommandType.Text, strdcduplicate));

                    if (Count > 0)
                    {
                        strtagno = "select vchtagnumber from tabdairyproduction where datproductiondate='" + SaveProductionDTO[j].datproductiondate + "' and vchmilkedat='" + ManageQuote(SaveProductionDTO[j].vchmilkedat) + "' and vchtagnumber='" + ManageQuote(SaveProductionDTO[j].vchtagnumber) + "'";
                        using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(connectionstring, CommandType.Text, strtagno))

                        {
                            while (dr.Read())
                            {
                                strtagno = SaveProductionDTO[j].vchtagnumber;
                                return false;
                            }
                        }
                    }
                    else
                    {
                        strDairyReturnInsert = "INSERT INTO TABDAIRYPRODUCTION(VCHPRODUCTIONNO, DATPRODUCTIONDATE, VCHCATTLE, VCHMILKEDAT, NUMQUANTITY, NUMFEEDTOCALF, VCHREMARKS, STATUSID, CREATEDBY, CREATEDDATE, vchtagnumber, vchmilktype, numdamagemilkquantity, numnetmilkquantity,reason_of_damage) ";
                        strDairyReturnInsert += "VALUES('" + strNextID + "', '" + SaveProductionDTO[j].datproductiondate + "', '"
                            + ManageQuote(SaveProductionDTO[j].vchcattle) + "', '" + ManageQuote(SaveProductionDTO[j].vchmilkedat) + "', "
                            + SaveProductionDTO[j].numquanity + ", '" + SaveProductionDTO[j].numfeedtocalf + "', "
                            + (string.IsNullOrWhiteSpace(SaveProductionDTO[j].vchremarks) ? "NULL" : "'" + ManageQuote(SaveProductionDTO[j].vchremarks) + "'") + ", "
                            + SaveProductionDTO[j].statusid + ", " + SaveProductionDTO[j].createdby + ", CURRENT_TIMESTAMP, '"
                            + ManageQuote(SaveProductionDTO[j].vchtagnumber) + "', '" + ManageQuote(SaveProductionDTO[j].vchmilktype) + "', "
                            + SaveProductionDTO[j].numdamagemilkquantity + ", " + SaveProductionDTO[j].numnetmilkquantity + ",'" + SaveProductionDTO[j].Damage_Reason + "')";

                        NPGSqlHelper.ExecuteNonQuery(connectionstring, CommandType.Text, strDairyReturnInsert);

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
                    con.Close();
                }
            }

            return isSaved;
        }
    }
}
