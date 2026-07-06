using System;
using System.Collections.Generic;
using System.Text;
using Easychit_Infrastructure.Dar.Plant_Protection;
using Easychit_Repository.Interfaces.Dar.Plant_Protection;
using System.Threading.Tasks;
using HelperManager;
using Npgsql;
using System.Data;

namespace Easychit_Repository.DataAccess.Dar.Plant_Protection
{
    public class PlantProtectionDAL : CommonDAL, IPlantProtection
    {
        NpgsqlTransaction trans = null;
        NpgsqlConnection con = new NpgsqlConnection(NPGSqlHelper.SQLConnString);
        NpgsqlDataReader npgdr = null;
        string GNextID = "";

        public async Task<List<DevisionNamesDTO>> GetPlationationDivisionNames(string globalSchema, string con)
        {
            List<DevisionNamesDTO> devisionsDTOlist = new List<DevisionNamesDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "select distinct vchmoduleid,vchmodulename FROM tabmodulemaster where vchmoduleid in('M2','M3','M5','M4') order by vchmodulename;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            DevisionNamesDTO devisionsDTO = new DevisionNamesDTO();
                            devisionsDTO.vchmoduleid = dr["vchmoduleid"] == DBNull.Value ? "" : Convert.ToString(dr["vchmoduleid"]);
                            devisionsDTO.vchmodulename = dr["vchmodulename"] == DBNull.Value ? "" : Convert.ToString(dr["vchmodulename"]);
                            devisionsDTOlist.Add(devisionsDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return devisionsDTOlist;
        }

        public async Task<List<RecomendationValveDTO>> GetRecomendationValve(string division, string globalSchema, string con)
        {
            List<RecomendationValveDTO> recomendationValvesDTOlist = new List<RecomendationValveDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    if(division == "HORTICULTURE")
                    {
                        Query = "select distinct vchtankid||'-'||vchmainvalve||'-'||vchsubvalve as valve from tabplantation where statusid=1 order by valve;";
                    }
                    else if (division == "VEGETABLE")
                    {
                        Query = " select distinct vchblockname as valve from tabvegvacantreplacementdetails  where vchstatus='NEW BED' order by vchblockname;";
                    }
                    else if (division == "FLORICULTURE")
                    {
                        Query = "select vchblockname as valve  from tabflowersvacantreplacementdetails where statusid=1 order by vchblockname;";
                    }
                    else if (division == "NURSERY")
                    {
                        Query = "select distinct '0' as valve from tabnurserymst where intstatus=1 order by valve;";
                    }
                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            RecomendationValveDTO recomendationValvesDTO = new RecomendationValveDTO();
                            recomendationValvesDTO.valve = dr["valve"] == DBNull.Value ? "" : Convert.ToString(dr["valve"]);
                            recomendationValvesDTOlist.Add(recomendationValvesDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return recomendationValvesDTOlist;
        }

        public async Task<List<RecomendationValveDTO>> GetRecomendationCrop(string division, string valvename, string globalSchema, string con)
        {
            List<RecomendationValveDTO> recomendationValvesDTOlist = new List<RecomendationValveDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    if (division == "HORTICULTURE")
                    {
                        Query = "(select 'ALL' as cropname ) union all select distinct vchfruittype  as cropname from tabplantation where vchtankid||'-'||vchmainvalve||'-'||vchsubvalve='" + valvename + "' and statusid=1 order by cropname;";
                    }
                    else if (division == "VEGETABLE")
                    {
                        Query = "(select 'ALL' as cropname ) union all select distinct vchvegname as cropname  from tabvegvacantreplacementdetails where statusid=1 and vchstatus='NEW BED' AND  vchblockname='" + valvename + "'  order by cropname;";
                    }
                    else if (division == "FLORICULTURE")
                    {
                        Query = "(select 'ALL' as cropname ) union all select distinct vchflowername as cropname  from tabflowersvacantreplacementdetails where statusid=1 and vchstatus='NEW BED' and vchblockname='" + valvename + "'  order by cropname;";
                    }
                    else if (division == "NURSERY")
                    {
                        Query = "(select 'ALL' as cropname ) union all select distinct vchplantname as cropname from tabnurserymst where intstatus=1 order by cropname;";

                    }

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            RecomendationValveDTO recomendationValvesDTO = new RecomendationValveDTO();
                            recomendationValvesDTO.cropname = dr["cropname"] == DBNull.Value ? "" : Convert.ToString(dr["cropname"]);
                            recomendationValvesDTOlist.Add(recomendationValvesDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return recomendationValvesDTOlist;
        }

        public async Task<List<DevisionNamesDTO>> GetDivisionNames(string globalSchema, string con)
        {
            List<DevisionNamesDTO> devisionsDTOlist = new List<DevisionNamesDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "select vchmoduleid,vchmodulename from tabmodulemaster where statusid=1;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            DevisionNamesDTO devisionsDTO = new DevisionNamesDTO();
                            devisionsDTO.vchmoduleid = dr["vchmoduleid"] == DBNull.Value ? "" : Convert.ToString(dr["vchmoduleid"]);
                            devisionsDTO.vchmodulename = dr["vchmodulename"] == DBNull.Value ? "" : Convert.ToString(dr["vchmodulename"]);
                            devisionsDTOlist.Add(devisionsDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return devisionsDTOlist;
        }

        public async Task<List<CategoryTypeDTO>> GetCatergoryNames(string division,string globalSchema, string con)
        {
            List<CategoryTypeDTO> categoryNamesDTOList = new List<CategoryTypeDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "select distinct vchcategoryid, vchcategoryname from tabstockmaster where  vchpurchasetype='" + division + "' and (vchproducttype in ( 'VERMICOMPOST','JEEVAMRUTHAM','DUNG') OR (vchproducttype <> 'Vermicompost' AND vchquantity > 0)) order by vchcategoryname;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            CategoryTypeDTO categoryNamesDTO = new CategoryTypeDTO();
                            categoryNamesDTO.vchcategoryid = dr["vchcategoryid"] == DBNull.Value ? "" : Convert.ToString(dr["vchcategoryid"]);
                            categoryNamesDTO.vchcategoryname = dr["vchcategoryname"] == DBNull.Value ? "" : Convert.ToString(dr["vchcategoryname"]);
                            categoryNamesDTOList.Add(categoryNamesDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return categoryNamesDTOList;
        }

        public async Task<List<StockCompanyNamesDTO>> GetStockCompanynames(string division,string category, string globalSchema, string con)
        {
            List<StockCompanyNamesDTO> stockCompanyNamesDTOList = new List<StockCompanyNamesDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "select distinct vchprodcompanyname,vchprodcompanyid from tabstockmaster where vchpurchasetype='" + division + "' and vchcategoryname='" + category + "' and  statusid=1   order by vchprodcompanyname;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            StockCompanyNamesDTO stockCompanyNamesDTO = new StockCompanyNamesDTO();
                            stockCompanyNamesDTO.vchprodcompanyid = dr["vchprodcompanyid"] == DBNull.Value ? "" : Convert.ToString(dr["vchprodcompanyid"]);
                            stockCompanyNamesDTO.vchprodcompanyname = dr["vchprodcompanyname"] == DBNull.Value ? "" : Convert.ToString(dr["vchprodcompanyname"]);
                            stockCompanyNamesDTOList.Add(stockCompanyNamesDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return stockCompanyNamesDTOList;
        }

        public async Task<List<StockCompanyNamesDTO>> GetProductByCategoryAndCompnay_Missue(string division, string category,string prodCompName, string globalSchema, string con)
        {
            List<StockCompanyNamesDTO> stockCompanyNamesDTOList = new List<StockCompanyNamesDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "select distinct vchproducttype as vchproductname from tabstockmaster where vchpurchasetype='" + division + "'  and vchcategoryname='" + category + "' and vchprodcompanyname='" + prodCompName + "'  and statusid=1 order by vchproductname;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            StockCompanyNamesDTO stockCompanyNamesDTO = new StockCompanyNamesDTO();
                            stockCompanyNamesDTO.vchproductname = dr["vchproductname"] == DBNull.Value ? "" : Convert.ToString(dr["vchproductname"]);
                            stockCompanyNamesDTOList.Add(stockCompanyNamesDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return stockCompanyNamesDTOList;
        }

        public async Task<string> GetUOMName(string ferti, string globalSchema, string con)
        {
            string Query = string.Empty;
            string uom = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "select vchuom from tabproductmaster where vchproductname='" + ferti + "' order by vchproductname;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            uom = dr["vchuom"] == DBNull.Value ? "" : Convert.ToString(dr["vchuom"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return uom;
        }

        public async Task<string> GetRATE(string ferti,string fromdate, string globalSchema, string con)
        {
            string Query = string.Empty;
            string rate = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "select coalesce(rate1,0) as openingrate from fnmaterialclosingstockratefifo(cast('" + fromdate + "' as date)) where materialid1='" + ferti + "';";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            rate = dr["openingrate"] == DBNull.Value ? "" : Convert.ToString(dr["openingrate"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return rate;
        }

        public async Task<List<EmployeeNamesDTO>> GetEmployeeNames(string gender,string globalSchema, string con)
        {
            List<EmployeeNamesDTO> employeeNamesDTOList = new List<EmployeeNamesDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    if(gender == "Male")
                    {
                        Query = "select vchemployeeid1 as vchemployeeid,employeename1 as employeename from public.fn_daily_activity_employees() where gender1='M' order by employeename1;";
                    }
                    else if(gender == "Female")
                    {
                        Query = "select vchemployeeid1 as vchemployeeid,employeename1 as employeename from public.fn_daily_activity_employees() where gender1='F' order by employeename1;";
                    }

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            EmployeeNamesDTO employeeNamesDTO = new EmployeeNamesDTO();
                            employeeNamesDTO.vchemployeeid = dr["vchemployeeid"] == DBNull.Value ? "" : Convert.ToString(dr["vchemployeeid"]);
                            employeeNamesDTO.employeename = dr["employeename"] == DBNull.Value ? "" : Convert.ToString(dr["employeename"]);
                            employeeNamesDTOList.Add(employeeNamesDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return employeeNamesDTOList;
        }

        public async Task<List<GeneralItemsDTO>> GetGeneralItems(string globalSchema, string con)
        {
            List<GeneralItemsDTO> generalItemsDTOList = new List<GeneralItemsDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "select  itemname from tabdargenralitems;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            GeneralItemsDTO generalItemsDTO = new GeneralItemsDTO();
                            generalItemsDTO.itemname = dr["itemname"] == DBNull.Value ? "" : Convert.ToString(dr["itemname"]);
                            generalItemsDTOList.Add(generalItemsDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return generalItemsDTOList;
        }

        public async Task<string> GetvchUOM(string general,string globalSchema, string con)
        {
            string Query = string.Empty;
            string uom = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "select  vchuom from tabdargenralitems where itemname='" + general + "' order by itemname;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            uom = dr["vchuom"] == DBNull.Value ? "" : Convert.ToString(dr["vchuom"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return uom;
        }


        public bool SavePlantDailyActivity(DailyActivityDTO dailyActivityDTO, string globalSchema, string connectionstring)
        {
            long strdetails = 0;
            bool isSaved = false;
            string strNextID = string.Empty;
            string strDairyReturnInsert = string.Empty;
            string strDairyReturnInsert1 = string.Empty;
            string strDispatchno = string.Empty;
            decimal grid_total = 0.00M;
            try
            {
                con = new NpgsqlConnection(connectionstring);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                trans = con.BeginTransaction();
                string strplantationdate = dailyActivityDTO.Date.ToString("yyyy-MM-dd");
                strNextID = GenerateNextID("tabdar_dailyactivitymst", "vchdano", 2, strplantationdate, "datdate", "DA", connectionstring);
                strNextID = "DA" + strNextID;

                strDispatchno = strNextID;

                strDairyReturnInsert = "INSERT INTO tabdar_dailyactivitymst(vchdano, datdate, vchmodulename, vchvalve,vchcropname,vchwork, total_amount,statusid, createdby, createddate) VALUES('" + strDispatchno + "','" + FormatDate(strplantationdate) + "','" + ManageQuote(dailyActivityDTO.vchmodulename) + "','" + ManageQuote(dailyActivityDTO.vchvalve) + "','" + dailyActivityDTO.vchcropname + "','" + ManageQuote(dailyActivityDTO.vchwork) + "',0.00,1," + dailyActivityDTO.createdby + ",CURRENT_TIMESTAMP) returning recordid;";
                strdetails = Convert.ToInt64(NPGSqlHelper.ExecuteScalar(trans, CommandType.Text, strDairyReturnInsert));

                for (int j = 0; j < dailyActivityDTO.lst_dailyactivity.Count; j++)
                {
                    decimal quantity, rate, amount = 0;
                    quantity = decimal.TryParse(dailyActivityDTO.lst_dailyactivity[j].quantity, out var q) && q != 0 ? q : 0;
                    rate = decimal.TryParse(dailyActivityDTO.lst_dailyactivity[j].rate, out var r) && r != 0 ? r : 0;
                    amount = decimal.TryParse(dailyActivityDTO.lst_dailyactivity[j].amount, out var a) && a != 0 ? a : 0;

                    strDairyReturnInsert1 = "INSERT INTO tabdar_dailyactivitymst_details(detailsid,vchdano, datdate,vchstarttime,vchendtime,totalhrs,vchpurchasetype, vchcategoryname,vchprodcompanyname,vchproducttype, vchuom, quantity, rate,amount,createdby,statusid,createddate)";
                    strDairyReturnInsert1 = strDairyReturnInsert1 + "VALUES(" + strdetails + ",'" + strDispatchno + "','" + FormatDate(strplantationdate) + "','" + dailyActivityDTO.lst_dailyactivity[j].vchstarttime + "','" + dailyActivityDTO.lst_dailyactivity[j].vchendtime + "','" + dailyActivityDTO.lst_dailyactivity[j].totalhrs + "','" + dailyActivityDTO.lst_dailyactivity[j].vchpurchasetype + "','" + dailyActivityDTO.lst_dailyactivity[j].vchcategoryname + "','" + dailyActivityDTO.lst_dailyactivity[j].vchprodcompanyname + "','" + dailyActivityDTO.lst_dailyactivity[j].vchproducttype + "','" + dailyActivityDTO.lst_dailyactivity[j].vchuom + "'," + quantity + "," + rate + "," + amount + "," + dailyActivityDTO.createdby + ",1,CURRENT_TIMESTAMP);";
                    NPGSqlHelper.ExecuteNonQuery(trans, CommandType.Text, strDairyReturnInsert1);

                    grid_total = grid_total + Convert.ToDecimal(dailyActivityDTO.lst_dailyactivity[j].amount);

                }

                strDairyReturnInsert1 = "update  tabdar_dailyactivitymst set total_amount=" + grid_total + " where vchdano='" + strNextID + "';";

                NPGSqlHelper.ExecuteNonQuery(trans, CommandType.Text, strDairyReturnInsert1);


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
                    con.Close();
                    con.Dispose();
                    trans.Dispose();
                }
            }

            return isSaved;
        }

        public async Task<List<MachineNamesDTO>> GetMachineNames(string globalSchema, string con)
        {
            List<MachineNamesDTO> machineNamesDTOList = new List<MachineNamesDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "select roboname from tabrobomachine_mst;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            MachineNamesDTO machineNamesDTO = new MachineNamesDTO();
                            machineNamesDTO.roboname = dr["roboname"] == DBNull.Value ? "" : Convert.ToString(dr["roboname"]);
                            machineNamesDTOList.Add(machineNamesDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return machineNamesDTOList;
        }

        public async Task<List<DivisionNamesDTO>> GetDivisions(string globalSchema, string con)
        {
            List<DivisionNamesDTO> divisionNamesDTOList = new List<DivisionNamesDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "select distinct vchmoduleid,vchmodulename FROM tabmodulemaster where vchmoduleid in('M2','M3','M5','M4') order by vchmodulename;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            DivisionNamesDTO divisionNamesDTO = new DivisionNamesDTO();
                            divisionNamesDTO.vchmoduleid = dr["vchmoduleid"] == DBNull.Value ? "" : Convert.ToString(dr["vchmoduleid"]);
                            divisionNamesDTO.vchmodulename = dr["vchmodulename"] == DBNull.Value ? "" : Convert.ToString(dr["vchmodulename"]);
                            divisionNamesDTOList.Add(divisionNamesDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return divisionNamesDTOList;
        }

        public async Task<List<ValveNamesDTO>> GetRoboValves(string globalSchema, string con)
        {
            List<ValveNamesDTO> valveNamesDTOList = new List<ValveNamesDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "select distinct valve from tabrobovalves_crops order by valve;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            ValveNamesDTO valveNamesDTO = new ValveNamesDTO();
                            valveNamesDTO.valve = dr["valve"] == DBNull.Value ? "" : Convert.ToString(dr["valve"]);
                            valveNamesDTOList.Add(valveNamesDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return valveNamesDTOList;
        }

        public async Task<List<CropNamesDTO>> GetRoboCrops(string valve,string globalSchema, string con)
        {
            List<CropNamesDTO> cropNamesDTOList = new List<CropNamesDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "select distinct crop from tabrobovalves_crops where valve='" + valve + "' order by crop;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            CropNamesDTO cropNamesDTO = new CropNamesDTO();
                            cropNamesDTO.crop = dr["crop"] == DBNull.Value ? "" : Convert.ToString(dr["crop"]);
                            cropNamesDTOList.Add(cropNamesDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return cropNamesDTOList;
        }


        public bool SaveRoboEntry(List<RoboEntryDTO> roboEntryDTOList, string globalSchema, string connectionstring)
        {
            bool isSaved = false;
            string strquery = string.Empty;
            try
            {
                con = new NpgsqlConnection(connectionstring);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                trans = con.BeginTransaction();
                for(int k = 0; k < roboEntryDTOList.Count; k++)
                {
                    strquery = "INSERT INTO tabroboentry (datdate, machinename, division, valve, crop, starttime, endtime, remarks, createddate) " +
                  "VALUES ('" + roboEntryDTOList[k].Date + "', '" + roboEntryDTOList[k].MachineName + "', '" +
                  roboEntryDTOList[k].Division + "', '" + roboEntryDTOList[k].Valve + "', '" +
                  roboEntryDTOList[k].Crop + "', '" + roboEntryDTOList[k].StartTime + "', '" +
                  roboEntryDTOList[k].EndTime + "', '" + roboEntryDTOList[k].Remarks + "', current_timestamp);";

                    NPGSqlHelper.ExecuteNonQuery(trans, CommandType.Text, strquery);
                }
                
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
                    con.Close();
                    con.Dispose();
                    trans.Dispose();
                }
            }

            return isSaved;
        }

















        public string GenerateNextID(string strtablename, string strcolname, int prefix, string strdate, string strColumnDate, string strPrefix,string connectionstring)
        {

            GNextID = "";
            try
            {
                string query = "select public.GENERATENEXTID('" + strtablename + "','" + strcolname + "','" + prefix + "','" + strdate + "','" + strColumnDate + "','" + strPrefix + "');";
                using (npgdr = NPGSqlHelper.ExecuteReader(connectionstring, CommandType.Text, query, null))
                {
                    while (npgdr.Read())
                    {
                        GNextID = npgdr[0].ToString();
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GNextID;
        }

    }
}
