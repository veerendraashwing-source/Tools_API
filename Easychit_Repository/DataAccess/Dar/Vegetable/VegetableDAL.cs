using System;
using System.Collections.Generic;
using System.Text;
using Easychit_Infrastructure.Dar.Vegetable;
using Easychit_Repository.Interfaces.Dar.Vegetable;
using System.Threading.Tasks;
using HelperManager;
using Npgsql;
using System.Data;

namespace Easychit_Repository.DataAccess.Dar.Vegetable
{
    public class VegetableDAL : CommonDAL, IVegetable
    {
        NpgsqlTransaction trans = null;
        NpgsqlConnection con = new NpgsqlConnection(NPGSqlHelper.SQLConnString);
        NpgsqlDataReader npgdr = null;
        string GNextID = "";
        public async Task<List<VegetableProductionDTO>> GetCollectionInchargeNames(string globalSchema, string con)
        {
            List<VegetableProductionDTO> CollectionInchargeDTOlist = new List<VegetableProductionDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "select vchinchargeid ,vchinchargename  from tabtrprodbookincharge where  statusid=1 order by vchinchargename ";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            VegetableProductionDTO productionDTO = new VegetableProductionDTO();
                            productionDTO.vchinchargeid = dr["vchinchargeid"] == DBNull.Value ? "" : Convert.ToString(dr["vchinchargeid"]);
                            productionDTO.vchinchargename = dr["vchinchargename"] == DBNull.Value ? "" : Convert.ToString(dr["vchinchargename"]);
                            CollectionInchargeDTOlist.Add(productionDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return CollectionInchargeDTOlist;
        }


        public async Task<List<VegetableProductionDTO>> GettypeOfVegetable(string globalSchema, string con)
        {
            List<VegetableProductionDTO> typeOfVegetableDTOlist = new List<VegetableProductionDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "select veg_name,veg_id from tabvegetablestypes where vchstatus='Y' order by veg_name asc";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            VegetableProductionDTO typeOfVegetableDTO = new VegetableProductionDTO();
                            typeOfVegetableDTO.veg_name = dr["veg_name"] == DBNull.Value ? "" : Convert.ToString(dr["veg_name"]);
                            typeOfVegetableDTO.vchinchargename = dr["veg_name"] == DBNull.Value ? "" : Convert.ToString(dr["veg_name"]);
                            typeOfVegetableDTOlist.Add(typeOfVegetableDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return typeOfVegetableDTOlist;
        }


        public async Task<List<VegetableProductionDTO>> GetBookNobasedonCollectionIncharge(string CollectionInchargeName, string globalSchema, string con)
        {
            List<VegetableProductionDTO> bookNoDTOlist = new List<VegetableProductionDTO>();
            string Query = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    Query = "select distinct ti.bookno||'-'||ti.bookid  as bookno,ti.bookid  from tabtrprodbookissue ti,tabtrprodbookleafs tl where ti.bookno = tl.bookno and ti.vchcollectionincharge = '" + CollectionInchargeName + "' and datbookreturndate is null order by  bookno ";


                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            VegetableProductionDTO bookNoDTO = new VegetableProductionDTO();
                            bookNoDTO.bookno = dr["bookno"] == DBNull.Value ? "" : Convert.ToString(dr["bookno"]);
                            bookNoDTO.bookid = dr["bookid"] == DBNull.Value ? 0 : Convert.ToInt64(dr["bookid"]);

                            bookNoDTOlist.Add(bookNoDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return bookNoDTOlist;
        }




        public async Task<List<VegetableProductionDTO>> GetUomByVegName(string VegName, string ModuleName, string globalSchema, string con)
        {
            List<VegetableProductionDTO> uomDTOlist = new List<VegetableProductionDTO>();
            string Query = string.Empty;

            await Task.Run(() =>
            {
                try
                {
                    if (ModuleName == "HORTICULTURE")
                    {
                        //StrUom = "select vchtypeoffruits,vchuom, from tabfruitsmst  where  vchtypeoffruits='" + Product + "' ;";
                        Query = "select vchtypeoffruits,vchuom,coalesce(gst_percentage,0)gst_percentage from tabfruitsmst  where  vchtypeoffruits='" + VegName + "' ;";
                    }
                    if (ModuleName == "VEGETABLE")
                    {
                        Query = "select veg_name,vchuom,0 as gst_percentage  from tabvegetablestypes where veg_name='" + VegName + "' ;";
                    }
                    if (ModuleName == "FLORICULTURE")
                    {
                        Query = "select flower_name,vchuom,0 as gst_percentage  from tabflowersmst where flower_name='" + VegName + "' ;";
                    }
                    if (ModuleName == "POULTRY")
                    {
                        Query = "select poultry_name,vchuom,0 as gst_percentage  from tabpoultrytypes where poultry_name='" + VegName + "' ;";
                    }


                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            VegetableProductionDTO uomDTO = new VegetableProductionDTO();
                            uomDTO.veg_name = dr["veg_name"] == DBNull.Value ? "" : Convert.ToString(dr["veg_name"]);
                            uomDTO.vchuom = dr["vchuom"] == DBNull.Value ? "" : Convert.ToString(dr["vchuom"]);

                            uomDTOlist.Add(uomDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return uomDTOlist;
        }

        public async Task<List<VegetableProductionDTO>> GetBlockName(string VegName, string globalSchema, string con)
        {
            List<VegetableProductionDTO> BlockNameDTOlist = new List<VegetableProductionDTO>();
            string Query = string.Empty;

            await Task.Run(() =>
            {
                try
                {

                    Query = "select distinct VCHBLOCKNAME from tabflowersvacantreplacementdetails where vchflowername = '" + VegName + "' and vchstatus in('PROPOSED BED', 'NEW BED')  and datvacantdate is null ORDER BY VCHBLOCKNAME; ";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            VegetableProductionDTO BlockNameDTO = new VegetableProductionDTO();
                            BlockNameDTO.vnhblockname = dr["VCHBLOCKNAME"] == DBNull.Value ? "" : Convert.ToString(dr["VCHBLOCKNAME"]);

                            BlockNameDTOlist.Add(BlockNameDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return BlockNameDTOlist;
        }





        public async Task<List<VegetableProductionDTO>> GetBedNo(string VegName, string BlockName, string globalSchema, string con)
        {
            List<VegetableProductionDTO> BedNoDTOlist = new List<VegetableProductionDTO>();
            string Query = string.Empty;

            await Task.Run(() =>
            {
                try
                {

                    Query = "select distinct VCHBLOCKNAME,vchbedno from tabvegvacantreplacementdetails where vchvegname ='" + VegName + "' and vchstatus ='NEW BED' and  VCHBLOCKNAME='" + BlockName.Split('-')[0] + "' and datvacantdate is null  union all  select distinct VCHBLOCKNAME,vchbedno from tabvegvacantreplacementdetails where vchvegname ='" + VegName + "' and vchstatus ='PROPOSED BED' and  VCHBLOCKNAME='" + BlockName.Split('-')[0] + "'  and datvacantdate is null ORDER BY VCHBLOCKNAME;";

                    using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(con, CommandType.Text, Query))
                    {
                        while (dr.Read())
                        {
                            VegetableProductionDTO BedNoDTO = new VegetableProductionDTO();
                            BedNoDTO.vnhblockname = dr["VCHBLOCKNAME"] == DBNull.Value ? "" : Convert.ToString(dr["VCHBLOCKNAME"]);

                            BedNoDTO.vchbedno = dr["vchbedno"] == DBNull.Value ? "" : Convert.ToString(dr["vchbedno"]);


                            BedNoDTOlist.Add(BedNoDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return BedNoDTOlist;
        }



        public bool SaveProductionDetails(List<VegetableProductionDTO> productionDTO, string globalSchema, string connectionstring)
        {
            bool isSaved = false;
            string Productionsave = string.Empty;
            string strcheck = string.Empty;
            string strNextID = string.Empty;
            string strProductionNo = string.Empty;
            string strVegReturnInsert = string.Empty;


            try
            {

                con = new NpgsqlConnection(connectionstring);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                foreach (var item in productionDTO)
                {
                    string strVegReturnDate = FormatDate(Convert.ToString(item.datproductiondate));


                    strNextID = GenerateNextID("TABVEGETABLEPRODUCTION", "vchproductionno", 2, strVegReturnDate, "datproductiondate", "VP", connectionstring);


                    strNextID = "VP" + strNextID;
                    strProductionNo = strNextID;


                    strcheck = "select count(*) from tabtrprodbookleafs  where bookno='" + item.bookno + "' and numtrno=" + item.numtrno + " and vchstatus <>'UNUSED';";

                    int cnt = Convert.ToInt32(NPGSqlHelper.ExecuteScalar(con, CommandType.Text, strcheck));

                    if (cnt == 0)
                    {
                        strVegReturnInsert = "";
                    }

                    NPGSqlHelper.ExecuteNonQuery(trans, CommandType.Text, strVegReturnInsert);
                    trans.Commit();
                    isSaved = true;


                }


                int Count = Convert.ToInt32(NPGSqlHelper.ExecuteScalar(con, CommandType.Text, strcheck));
                if (Count == 0)
                {
                    string strtanksave = "";

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












        public string GenerateNextID(string strtablename, string strcolname, int prefix, string strdate, string strColumnDate, string strPrefix, string connectionstring)
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