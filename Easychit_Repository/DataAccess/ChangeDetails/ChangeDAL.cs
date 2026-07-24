//using Easychit_Infrastructure.Easy_Chit_Tools;
using Easychit_Infrastructure.ChangeDetails;
using HelperManager;
using Npgsql;
using System;
using System.Data;
using System.Collections.Generic;
using System.Text.Json;
using System.Text;
using Easychit_Repository.Interfaces.ChangeDetails;
using System.Diagnostics;


namespace Easychit_Repository.DataAccess.ChangeDetails
{
    public class ChangeDAL : CommonDAL, Ichange
    {
        NpgsqlConnection con = new NpgsqlConnection(NPGSqlHelper.SQLConnString);


        public List<BranchNamesDTO> GetBranchNames(string globalschema, string Conn)
        {
            List<BranchNamesDTO> productionlist = new List<BranchNamesDTO>();
            string Query = string.Empty;

            Stopwatch sw = new Stopwatch();

            try
            {
                Query = "SELECT DISTINCT branch_name,tbl_mst_branch_configuration_id, branch_code, company_configuration_id FROM "
                    + AddDoubleQuotes(globalschema)
                    + ".tbl_mst_branch_configuration WHERE branch_name LIKE '%CAO%' AND branch_type='CAO';";

                sw.Start();

                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, CommandType.Text, Query))
                {
                    sw.Stop();
                    Console.WriteLine("ExecuteReader Time : " + sw.ElapsedMilliseconds + " ms");

                    sw.Restart();

                    while (dr.Read())
                    {
                        BranchNamesDTO productionDTO = new BranchNamesDTO
                        {
                            Branch_name = Convert.ToString(dr["branch_name"]),
                            branch_code = Convert.ToString(dr["branch_code"]),
                            company_configuration_id = Convert.ToInt32(dr["company_configuration_id"]),
                            tbl_mst_branch_configuration_id = Convert.ToInt32(dr["tbl_mst_branch_configuration_id"])
                        };

                        productionlist.Add(productionDTO);
                    }

                    sw.Stop();
                    Console.WriteLine("Reading Data Time : " + sw.ElapsedMilliseconds + " ms");
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return productionlist;
        }

        public List<companyNamesDTO> GetcompanyNames(string globalschema, string Conn)
        {
            List<companyNamesDTO> productionlist = new List<companyNamesDTO>();
            string Query = string.Empty;
            try
            {
                Query = "select tbl_mst_chit_company_configuration_id,company_name,company_code from " + AddDoubleQuotes(globalschema) + ".tbl_mst_chit_company_configuration";

                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
                {
                    while (dr.Read())
                    {
                        companyNamesDTO productionDTO = new companyNamesDTO
                        {
                            company_name = Convert.ToString(dr["company_name"]),
                            company_code = Convert.ToString(dr["company_code"]),
                            tbl_mst_chit_company_configuration_id = Convert.ToInt32(dr["tbl_mst_chit_company_configuration_id"])
                        };
                        productionlist.Add(productionDTO);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productionlist;
        }

        public List<GroupCodeDTO> GetGroupcodes(string branchschema, string Conn)
        {
            List<GroupCodeDTO> productionlist = new List<GroupCodeDTO>();
            string Query = string.Empty;
            try
            {
                Query = "select distinct groupcode, chitgroup_id from " + AddDoubleQuotes(branchschema) + ".tbl_mst_subscriber x, " + AddDoubleQuotes(branchschema) + ".tbl_mst_chitgroup y where chitgroup_status in('Registered','Commenced','Completed') and x.chitgroup_id = y.tbl_mst_chitgroup_id;";

                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
                {
                    while (dr.Read())
                    {
                        GroupCodeDTO productionDTO = new GroupCodeDTO
                        {
                            Group_Code = dr["groupcode"] != DBNull.Value ? Convert.ToString(dr["groupcode"]) : string.Empty,
                            chitgroup_id = dr["chitgroup_id"] != DBNull.Value ? Convert.ToString(dr["chitgroup_id"]) : string.Empty
                        };
                        productionlist.Add(productionDTO);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productionlist;
        }

        public List<TicketDTO> GetTickets(string schema, string Conn, string groupcode)
        {
            List<TicketDTO> productionlist = new List<TicketDTO>();
            string Query = string.Empty;
            try
            {
                Query = "select ticketno from " + AddDoubleQuotes(schema) + ".tbl_mst_subscriber x, " + AddDoubleQuotes(schema) + ".tbl_mst_chitgroup y where chitgroup_status in('Registered','Commenced') and x.chitgroup_id = y.tbl_mst_chitgroup_id and groupcode='" + groupcode + "' and x.chit_status='P' order by ticketno asc;";

                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
                {
                    while (dr.Read())
                    {
                        TicketDTO productionDTO = new TicketDTO
                        {
                            ticketno = Convert.ToInt32(dr["ticketno"])
                        };
                        productionlist.Add(productionDTO);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return productionlist;
        }

        public NameUpdateDTO GetOldNameByTicketNo(string globalschema, string schema, string Conn, int ticketNo, int chitgroupid)
        {
            NameUpdateDTO dto = new NameUpdateDTO();
            string Query = string.Empty;
            try
            {
                //Query = "select contact_name, contact_surname, contact_mailing_name, t1.contact_id, c.business_entity_contactno, t1.address1, t1.area, t1.city_name, t1.pincode,chit_status from " + AddDoubleQuotes(globalschema) + ".vw_mst_subscriber x, " + AddDoubleQuotes(schema) + ".tbl_mst_chitgroup y, " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact c, " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact_address_details t1 where chitgroup_status in('Registered','Commenced') and t1.isprimary='true' and t1.status='true' and x.chitgroup_id = y.tbl_mst_chitgroup_id and x.contact_id = c.tbl_mst_contact_id and t1.contact_id = c.tbl_mst_contact_id and chitgroup_id = " + chitgroupid + " and ticketno = " + ticketNo + " order by groupcode, ticketno;";
                Query = "SELECT c.contact_name, c.contact_surname, c.contact_mailing_name, t1.contact_id, c.business_entity_contactno, t1.address1, t1.area, t1.city_name, t1.pincode, x.chit_status FROM " + AddDoubleQuotes(globalschema) + ".vw_mst_subscriber x INNER JOIN " + AddDoubleQuotes(schema) + ".tbl_mst_chitgroup y ON x.chitgroup_id = y.tbl_mst_chitgroup_id INNER JOIN " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact c ON x.contact_id = c.tbl_mst_contact_id INNER JOIN " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact_address_details t1 ON t1.contact_id = c.tbl_mst_contact_id INNER JOIN " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration bc ON bc.tbl_mst_branch_configuration_id = x.branch_id WHERE y.chitgroup_status IN ('Registered','Completed','Commenced') AND t1.isprimary = TRUE AND t1.status = TRUE AND x.chitgroup_id = " + chitgroupid + " AND x.ticketno = " + ticketNo + " AND bc.branch_code = '" + schema + "' ORDER BY x.groupcode, x.ticketno;";
                Console.WriteLine(Query);
                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
                {
                    if (dr.Read())
                    {
                        dto.OldName = Convert.ToString(dr["contact_name"]);
                        dto.chit_status = Convert.ToString(dr["chit_status"]);
                        dto.OldSurname = Convert.ToString(dr["contact_surname"]);
                        dto.OldMailingName = Convert.ToString(dr["contact_mailing_name"]);
                        dto.ContactId = Convert.ToInt32(dr["contact_id"]);
                        dto.MobileNo = Convert.ToString(dr["business_entity_contactno"]);
                        dto.Address = Convert.ToString(dr["address1"]);
                        dto.Area = Convert.ToString(dr["area"]);
                        dto.City = Convert.ToString(dr["city_name"]);
                        dto.Pincode = Convert.ToInt32(dr["pincode"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dto;
        }

        public bool UpdateNameByTicketNo(string globalschema, string Con, NameChangeDto dtoNames, string branchschema)
        {
            bool isSaved = false;
            StringBuilder sbinsert = new StringBuilder();
            NpgsqlTransaction trans = null;
            int i;
            con = new NpgsqlConnection(Con);
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            trans = con.BeginTransaction();
            try
            {
                if (dtoNames.ptypeofoperation.Trim().ToUpper() == "UPDATE")
                {
                    sbinsert.Append("Update " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact SET contact_name = '" + dtoNames.NewName + "',contact_surname = '" + dtoNames.NewSurname + "',contact_mailing_name ='" + dtoNames.NewMailingName + "' where tbl_mst_contact_id = " + dtoNames.contact_id + ";update " + AddDoubleQuotes(branchschema) + ".tbl_mst_subscriber set subscriber_name='" + dtoNames.NewMailingName + "' where contact_id=" + dtoNames.contact_id + ";");
                }
                if (!string.IsNullOrEmpty(sbinsert.ToString()))
                {
                    NPGSqlHelper.ExecuteNonQuery(trans, CommandType.Text, sbinsert.ToString());
                }
                trans.Commit();
                isSaved = true;
            }
            catch (Exception Ex)
            {
                throw Ex;
                trans.Rollback();
                isSaved = false;
            }
            return isSaved;
        }

        public bool UpdateMoblieNoByContact(string globalschema, string Con, MobileNoChangeDto dto)
        {
            bool isSaved = false;
            StringBuilder sbinsert = new StringBuilder();
            NpgsqlTransaction trans = null;
            int i;
            con = new NpgsqlConnection(Con);
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            trans = con.BeginTransaction();
            try
            {
                if (dto.ptypeofoperation.Trim().ToUpper() == "UPDATE")
                {
                    sbinsert.Append("UPDATE " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact  SET business_entity_contactno ='" + dto.newMobileNo + "' WHERE  tbl_mst_contact_id=" + dto.contact_id + ";");
                }
                if (!string.IsNullOrEmpty(sbinsert.ToString()))
                {
                    NPGSqlHelper.ExecuteNonQuery(trans, CommandType.Text, sbinsert.ToString());
                }
                trans.Commit();
                isSaved = true;
            }
            catch (Exception Ex)
            {
                throw Ex;
                trans.Rollback();
                isSaved = false;
            }
            return isSaved;
        }

        public bool UpdateAddressByContact(string globalschema, string Con, AddressChangeDto dto)
        {
            bool isSaved = false;
            StringBuilder sbinsert = new StringBuilder();
            NpgsqlTransaction trans = null;
            int i;
            con = new NpgsqlConnection(Con);
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            trans = con.BeginTransaction();
            try
            {
                if (dto.ptypeofoperation.Trim().ToUpper() == "UPDATE")
                {
                    sbinsert.Append("UPDATE " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact_address_details SET ADDRESS1='" + dto.NewAddress + "',AREA='" + dto.NewArea + "',CITY_NAME='" + dto.NewCity + "',PINCODE=" + dto.NewPincode + " WHERE contact_id=" + dto.contact_id + ";");
                }
                if (!string.IsNullOrEmpty(sbinsert.ToString()))
                {
                    NPGSqlHelper.ExecuteNonQuery(trans, CommandType.Text, sbinsert.ToString());
                }
                trans.Commit();
                isSaved = true;
            }
            catch (Exception Ex)
            {
                throw Ex;
                trans.Rollback();
                isSaved = false;
            }
            return isSaved;
        }

        public List<AgentCodeDto> Agentcode(string globalschema, string Con)
        {
            List<AgentCodeDto> lstrefdetails = new List<AgentCodeDto>();
            string query = string.Empty;
            try
            {
                query = "select referral_code from " + AddDoubleQuotes(globalschema) + ".tbl_mst_referral";

                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Con, System.Data.CommandType.Text, query))
                {
                    while (dr.Read())
                    {
                        AgentCodeDto refdetails = new AgentCodeDto();
                        refdetails.referralCode = Convert.ToString(dr["referral_code"]);
                        lstrefdetails.Add(refdetails);
                    }
                }
                return lstrefdetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AgentCodeDto> GetReferralDetails(string globalschema, string connectionString, string referralCode)
        {
            List<AgentCodeDto> lstrefdetails = new List<AgentCodeDto>();
            string Query = string.Empty;
            try
            {
                Query = "select t1.agent_unique_code,t2.contact_mailing_name,t2.business_entity_contactno,t3.branch_name   from " + AddDoubleQuotes(globalschema) + ".tbl_mst_referral t1  join " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact t2 on t2.tbl_mst_contact_id = t1.contact_id join " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration t3 on t3.tbl_mst_branch_configuration_id = t1.agent_branch_id where t1.referral_code = '" + referralCode + "'; ";
                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(connectionString, System.Data.CommandType.Text, Query))
                {
                    while (dr.Read())
                    {
                        AgentCodeDto refdetails = new AgentCodeDto();
                        refdetails.AgentUniqueCode = Convert.ToString(dr["agent_unique_code"]);
                        refdetails.AgentName = Convert.ToString(dr["contact_mailing_name"]);
                        refdetails.BusinessEntityContactNo = Convert.ToInt64(dr["business_entity_contactno"]);
                        refdetails.BranchName = Convert.ToString(dr["branch_name"]);
                        lstrefdetails.Add(refdetails);
                    }
                }
                return lstrefdetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BranchNameDTO> GetBranchName(string globalschema, string Con)
        {
            List<BranchNameDTO> productionlist = new List<BranchNameDTO>();
            string Query = string.Empty;
            try
            {
                Query = "select distinct tbl_mst_branch_configuration_id,branch_name ,branch_code from " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration;";

                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Con, System.Data.CommandType.Text, Query))
                {
                    while (dr.Read())
                    {
                        BranchNameDTO productionDTO = new BranchNameDTO();
                        productionDTO.branchconfigurationid = Convert.ToInt64(dr["tbl_mst_branch_configuration_id"]);
                        productionDTO.BranchName = Convert.ToString(dr["branch_name"]);
                        productionDTO.BranchCode = Convert.ToString(dr["branch_code"]);
                        productionlist.Add(productionDTO);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productionlist;
        }

        public bool UpdateAgentBranch(string globalschema, string Con, UpdateAgentBranchDto dto)
        {
            bool isSaved = false;
            StringBuilder sbinsert = new StringBuilder();
            NpgsqlTransaction trans = null;
            int i;
            con = new NpgsqlConnection(Con);
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            trans = con.BeginTransaction();
            try
            {
                if (dto.ptypeofoperation.Trim().ToUpper() == "UPDATE")
                {
                    sbinsert.Append("UPDATE " + AddDoubleQuotes(globalschema) + ".tbl_mst_referral " + "SET agent_branch_id = " + dto.newBranchId + " " + "WHERE referral_code = '" + dto.ReferralCode + "';");
                }
                if (!string.IsNullOrEmpty(sbinsert.ToString()))
                {
                    NPGSqlHelper.ExecuteNonQuery(trans, CommandType.Text, sbinsert.ToString());
                }
                trans.Commit();
                isSaved = true;
            }
            catch (Exception Ex)
            {
                throw Ex;
                trans.Rollback();
                isSaved = false;
            }
            return isSaved;
        }


        // public List<ChequeDetailsDTO> GetChequeDetails(string branchschema, string Con, string checkreferno)
        // {
        //     List<ChequeDetailsDTO> checkdetailslist = new List<ChequeDetailsDTO>();
        //     string query = string.Empty;
        //     try
        //     {
        //         query = "select payment_number,payment_date,paid_to,paid_amount,clear_date from " + AddDoubleQuotes(branchschema) + ".tbl_trans_payment_reference where reference_number='" + checkreferno + "' and clear_status='P' AND clear_date is not null";

        //         using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Con, System.Data.CommandType.Text, query))
        //         {
        //             while (dr.Read())
        //             {
        //                 ChequeDetailsDTO checkDTO = new ChequeDetailsDTO();
        //                 checkDTO.PaymentNo = Convert.ToString(dr["payment_number"]);
        //                 checkDTO.PayDate = Convert.ToDateTime(dr["payment_date"]);
        //                 checkDTO.PaidTo = Convert.ToString(dr["paid_to"]);
        //                 checkDTO.Amount = Convert.ToUInt64(dr["paid_amount"]);
        //                 checkDTO.ClearDate = dr["clear_date"] == DBNull.Value ? (DateTime?)null : (DateTime)dr["clear_date"];
        //                 checkdetailslist.Add(checkDTO);
        //             }
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         throw ex;
        //     }
        //     return checkdetailslist;
        // }



        public void SaveData(string globalschema, string Con, ChangeAuditDto audit)
        {
            string Query = string.Empty;
            try
            {
                string oldDataJson = JsonSerializer.Serialize(audit.OldData);
                string newDataJson = JsonSerializer.Serialize(audit.NewData);
                oldDataJson = oldDataJson.Replace("'", "''");
                newDataJson = newDataJson.Replace("'", "''");
                string reason = (audit.Reason ?? "").Replace("'", "''");
                string changetype = (audit.ChangeType ?? "").Replace("'", "''");
                string schemaName = (audit.SchemaName ?? "").Replace("'", "''");
                //  Query = "insert into " + AddDoubleQuotes(globalschema) + ".tbl_mst_change_details(schema_name,login_id,change_date,olddata,newdata,reason,vchtype) values('" + audit.SchemaName + "','" + audit.LoginName + "',current_timestamp,'" + oldDataJson + "','" + newDataJson + "','" + reason + "','" + changetype + "')";


                string loginName = (audit.LoginName ?? "").Replace("'", "''");

                Query = "INSERT INTO " + AddDoubleQuotes(globalschema) + ".tbl_mst_change_details(schema_name, login_id, change_date, olddata, newdata, reason, vchtype) VALUES('" + schemaName + "', '" + loginName + "', CURRENT_TIMESTAMP, '" + oldDataJson + "'::jsonb, '" + newDataJson + "'::jsonb, '" + reason + "', '" + changetype + "')";
                //  Query = 
                Console.WriteLine(Query);
                NPGSqlHelper.ExecuteNonQuery(Con, System.Data.CommandType.Text, Query);
            }
            catch
            {
                throw;
            }
        }


        public List<ChangeTypes> GetChangeTypes(string globalschema, string Con)
        {
            List<ChangeTypes> lstct = new List<ChangeTypes>();
            string Query = string.Empty;
            try
            {
                Query = "SELECT distinct vchtype FROM " + AddDoubleQuotes(globalschema) + ".tbl_mst_change_details;";

                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Con, System.Data.CommandType.Text, Query))
                {
                    while (dr.Read())
                    {
                        ChangeTypes ct = new ChangeTypes();
                        ct.ChangeType = Convert.ToString(dr["vchtype"]);
                        lstct.Add(ct);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstct;
        }

        public List<ReportsofUpdatesDTO> GetUpdateReports(string globalschema, string Con, DateTime fromdate, DateTime todate, string changetype)
        {
            List<ReportsofUpdatesDTO> lstupdatereports = new List<ReportsofUpdatesDTO>();
            string Query = string.Empty;
            try
            {
                DateTime toDateExclusive = todate.Date.AddDays(1);
                Query = "SELECT login_id,change_date,olddata,newdata,reason,vchtype FROM " + AddDoubleQuotes(globalschema) + ".tbl_mst_change_details WHERE change_date >= '" + fromdate.Date.ToString("yyyy-MM-dd") + "' AND change_date < '" + toDateExclusive.ToString("yyyy-MM-dd") + "' and vchtype='" + changetype + "' ";

                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Con, System.Data.CommandType.Text, Query))
                {
                    while (dr.Read())
                    {
                        ReportsofUpdatesDTO updatereports = new ReportsofUpdatesDTO();
                        updatereports.LoginId = Convert.ToString(dr["login_id"]);
                        updatereports.LoginDate = Convert.ToDateTime(dr["change_date"]);
                        updatereports.OldData = Convert.ToString(dr["olddata"]);
                        updatereports.NewData = Convert.ToString(dr["newdata"]);
                        updatereports.ChangeType = Convert.ToString(dr["vchtype"]);
                        updatereports.Reason = Convert.ToString(dr["reason"]);
                        lstupdatereports.Add(updatereports);
                    }
                }
                return lstupdatereports;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region auction schudule
        public List<auctionschedule> GetAuctionschedules(string Con, string branchschema, Int64 chitgroupid)
        {
            List<auctionschedule> schedulelist = new List<auctionschedule>();
            string Query = string.Empty;
            try
            {
                Query = "select b.auction_date,b.auction_time from " + AddDoubleQuotes(branchschema) + ".tbl_mst_chitgroup a inner join " + AddDoubleQuotes(branchschema) + ".tbl_trans_auction_schedule b on a.tbl_mst_chitgroup_id = b.chitgroup_id and b.auction_status= false and b.chitgroup_id=" + chitgroupid + "";

                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Con, System.Data.CommandType.Text, Query))
                {
                    while (dr.Read())
                    {
                        auctionschedule schedule = new auctionschedule();
                        schedule.auction_date = Convert.ToString(dr["auction_date"]);
                        schedule.auction_time = Convert.ToString(dr["auction_time"]);
                        schedulelist.Add(schedule);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return schedulelist;
        }

        #endregion auction schudule
        public void updatescheduledateandtime(string branchschema, string Con, string newauctiondate, string newauctiontime, Int64 chitgroupid)
        {
            string Query = string.Empty;
            try
            {
                Query = "update " + AddDoubleQuotes(branchschema) + ".tbl_trans_auction_schedule set auction_date='" + newauctiondate + "',auction_time='" + newauctiontime + "' where chitgroup_id=" + chitgroupid + " and auction_status= false";

                NPGSqlHelper.ExecuteNonQuery(Con, System.Data.CommandType.Text, Query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region bidlosspermission
        public List<bidlosspermission> Getauctionnumber(string Con, string branchschema, Int64 chitgroupid)
        {
            List<bidlosspermission> bidpermissionlist = new List<bidlosspermission>();
            string Query = string.Empty;
            try
            {
                Query = "select b.auction_number,b.ticketno from " + AddDoubleQuotes(branchschema) + ".tbl_mst_chitgroup a inner join " + AddDoubleQuotes(branchschema) + ".tbl_trans_auction_pslist b on a.tbl_mst_chitgroup_id = b.chitgroup_id and b.bidloss_allow_status= false and b.chitgroup_id=" + chitgroupid + " order by b.auction_number";

                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Con, System.Data.CommandType.Text, Query))
                {
                    while (dr.Read())
                    {
                        bidlosspermission bidloss = new bidlosspermission();
                        bidloss.auction_number = Convert.ToInt64(dr["auction_number"]);
                        bidloss.ticketno = Convert.ToInt64(dr["ticketno"]);
                        bidpermissionlist.Add(bidloss);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bidpermissionlist;
        }

        #endregion bidlosspermission
        public void updatebidlosspermission(string branchschema, string Con, string auction_number, string ticketno, Int64 chitgroupid)
        {
            string Query = string.Empty;
            try
            {
                Query = "update " + AddDoubleQuotes(branchschema) + ".tbl_trans_auction_pslist set bidloss_allow_status=true where auction_number = " + auction_number + " and chitgroup_id = " + chitgroupid + " and ticketno = " + ticketno + " and bidloss_allow_status=false";

                NPGSqlHelper.ExecuteNonQuery(Con, System.Data.CommandType.Text, Query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region getvacantstatus
        public List<vacantdto> getvacantstatus(string Con, string globalschema)
        {
            List<vacantdto> vacantdtoList = new List<vacantdto>();
            string Query = string.Empty;
            try
            {
                Query = "select branch_code,is_vacant_full_receipt_mandatory from " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration where branch_name like '%CAO%' and branch_type ='CAO';";

                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Con, System.Data.CommandType.Text, Query))
                {
                    while (dr.Read())
                    {
                        vacantdto vacant = new vacantdto();
                        vacant.branch_code = Convert.ToString(dr["branch_code"]);
                        vacant.is_vacant_full_receipt_mandatory =
                    dr["is_vacant_full_receipt_mandatory"] == DBNull.Value
                    ? false
                    : Convert.ToBoolean(dr["is_vacant_full_receipt_mandatory"]);
                        vacantdtoList.Add(vacant);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vacantdtoList;
        }

        #endregion getvacantstatus
        public void updatevacantstatus(string globalschema, string Con, string branch_code)
        {
            string Query = string.Empty;
            try
            {
                Query = "update " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration set is_vacant_full_receipt_mandatory=false where branch_code='" + branch_code + "'";

                NPGSqlHelper.ExecuteNonQuery(Con, System.Data.CommandType.Text, Query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region getmaxchitcount
        public List<multiplecontactdto> getmaxchitcount(string Con, string globalschema, string? referenceid, string? contactno)
        {
            List<multiplecontactdto> multiplecontacttoList = new List<multiplecontactdto>();
            string Query = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(referenceid))
                {
                    Query = "select contact_reference_id,business_entity_contactno,max_chits_per_contact from " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact WHERE business_entity_contactno='" + contactno + "'";
                }
                else
                {
                    Query = "select contact_reference_id,business_entity_contactno,max_chits_per_contact from " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact WHERE contact_reference_id='" + referenceid + "'";
                }

                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Con, System.Data.CommandType.Text, Query))
                {
                    while (dr.Read())
                    {
                        multiplecontactdto multiplecontact = new multiplecontactdto();
                        multiplecontact.contact_reference_id = Convert.ToString(dr["contact_reference_id"]);
                        multiplecontact.max_chits_per_contact = Convert.ToInt64(dr["max_chits_per_contact"]);
                        multiplecontact.business_entity_contactno = Convert.ToString(dr["business_entity_contactno"]);
                        multiplecontacttoList.Add(multiplecontact);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return multiplecontacttoList;
        }

        #endregion getmaxchitcount
        public void updatemultiplecontact(string globalschema, string Con, string referenceid, Int64 totalcount)
        {
            string Query = string.Empty;
            try
            {
                Query = "update " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact set max_chits_per_contact=" + totalcount + " where contact_reference_id='" + referenceid + "'";

                NPGSqlHelper.ExecuteNonQuery(Con, System.Data.CommandType.Text, Query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region getsubscribername
        public List<subsciberdto> getsubscribername(string Con, string globalschema, string branchschema, Int32 ticketno, string groupcode, string branch_code)
        {
            List<subsciberdto> subscibertoList = new List<subsciberdto>();
            string Query = string.Empty;
            try
            {
                Query = "SELECT c.subscriber_name,c.ticketno,c.branch_id,b.branch_code,a.groupcode,c.chit_status FROM " + AddDoubleQuotes(branchschema) + ".tbl_mst_chitgroup a INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_mst_subscriber c ON a.tbl_mst_chitgroup_id = c.chitgroup_id INNER JOIN " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration b ON a.branch_id = b.tbl_mst_branch_configuration_id WHERE c.ticketno = " + ticketno + " AND a.groupcode = '" + groupcode + "' AND b.branch_code = '" + branch_code + "' and c.chit_status='P';";

                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Con, System.Data.CommandType.Text, Query))
                {
                    while (dr.Read())
                    {
                        subsciberdto subscriber = new subsciberdto();
                        subscriber.branch_code = Convert.ToString(dr["branch_code"]);
                        subscriber.subscriber_name = Convert.ToString(dr["subscriber_name"]);
                        subscriber.ticketno = Convert.ToInt32(dr["ticketno"]);
                        subscriber.branch_id = Convert.ToUInt64(dr["branch_id"]);
                        subscriber.groupcode = Convert.ToString(dr["groupcode"]);
                        subscriber.chit_status = Convert.ToString(dr["chit_status"]);
                        subscibertoList.Add(subscriber);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return subscibertoList;
        }

        public List<subsciberdto> getsubscribercount(string Con, string globalschema, Int32 ticketno, string groupcode)
        {
            List<subsciberdto> subscibertoList = new List<subsciberdto>();
            string Query = string.Empty;
            try
            {
                Query = "SELECT COUNT(*) AS total_count FROM " + AddDoubleQuotes(globalschema) + ".tbl_mst_lc_chits WHERE ticketno = " + ticketno + " AND groupcode = '" + groupcode + "' AND vchstatus = 'f';";

                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Con, System.Data.CommandType.Text, Query))
                {
                    while (dr.Read())
                    {
                        subsciberdto subscriber = new subsciberdto();
                        subscriber.total_count = Convert.ToInt32(dr["total_count"]);
                        subscibertoList.Add(subscriber);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return subscibertoList;
        }

        #endregion getsubscribername
        public void deletelegalcell(string globalschema, string Con, string groupcode, Int32 ticketno)
        {
            string Query = string.Empty;
            try
            {
                Query = "delete from " + AddDoubleQuotes(globalschema) + ".tbl_mst_lc_chits where groupcode='" + groupcode + "' and ticketno=" + ticketno + "  AND VCHSTATUS='t';";

                NPGSqlHelper.ExecuteNonQuery(Con, System.Data.CommandType.Text, Query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<chequesreturndto> getchequesreturncharges(string Con, string globalschema, string company_code)
        {
            List<chequesreturndto> chequesreturndtoList = new List<chequesreturndto>();
            string Query = string.Empty;
            try
            {
                Query = "select chequereturn_charges_amount,tbl_mst_chit_company_configuration_ID from " + AddDoubleQuotes(globalschema) + ".tbl_mst_chit_company_configuration WHERE  company_code='" + company_code + "'";

                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Con, System.Data.CommandType.Text, Query))
                {
                    while (dr.Read())
                    {
                        chequesreturndto chequereturn = new chequesreturndto();
                        chequereturn.chequereturn_charges_amount = Convert.ToInt64(dr["chequereturn_charges_amount"]);
                        chequereturn.tbl_mst_chit_company_configuration_ID = Convert.ToInt32(dr["tbl_mst_chit_company_configuration_ID"]);
                        chequesreturndtoList.Add(chequereturn);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return chequesreturndtoList;
        }

        public void updatechequesreturncharges(string globalschema, string Con, Int32 tbl_mst_chit_company_configuration_ID, Int32 chequereturn_charges_amount)
        {
            string Query = string.Empty;
            try
            {
                if (chequereturn_charges_amount == 0)
                {
                    Query = "update " + AddDoubleQuotes(globalschema) + ".tbl_mst_chit_company_configuration set chequereturn_charges_amount=250 where tbl_mst_chit_company_configuration_id=" + tbl_mst_chit_company_configuration_ID + ";";
                }
                else
                {
                    Query = "update " + AddDoubleQuotes(globalschema) + ".tbl_mst_chit_company_configuration set chequereturn_charges_amount=0 where tbl_mst_chit_company_configuration_id=" + tbl_mst_chit_company_configuration_ID + ";";
                }

                NPGSqlHelper.ExecuteNonQuery(Con, System.Data.CommandType.Text, Query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<dateschangedto> GetDatesChangeDetails(string branchschema, string Con, string groupcode)
        {
            List<dateschangedto> list = new List<dateschangedto>();
            string Query = "SELECT no_of_auctions, pso_number, commencement_date, termination_date, commencement_certificate_no, groupcode FROM " + AddDoubleQuotes(branchschema) + ".tbl_mst_chitgroup WHERE groupcode = '" + groupcode + "';";

            using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Con, CommandType.Text, Query))
            {
                while (dr.Read())
                {
                    list.Add(new dateschangedto
                    {
                        no_of_auctions = dr["no_of_auctions"] == DBNull.Value ? 0 : Convert.ToInt32(dr["no_of_auctions"]),
                        pso_number = Convert.ToString(dr["pso_number"]),
                        commencement_date = dr["commencement_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["commencement_date"]),
                        termination_date = dr["termination_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["termination_date"]),
                        commencement_certificate_no = Convert.ToString(dr["commencement_certificate_no"]),
                        groupcode = Convert.ToString(dr["groupcode"])
                    });
                }
            }
            return list;
        }

        public void UpdateDatesChangeDetails(string branchschema, string Con, dateschangedto dto)
        {
            StringBuilder query = new StringBuilder();
            List<string> updates = new List<string>();

            if (dto.commencement_date.HasValue)
                updates.Add("commencement_date = '" + dto.commencement_date.Value.ToString("yyyy-MM-dd") + "'");

            if (dto.termination_date.HasValue)
                updates.Add("termination_date = '" + dto.termination_date.Value.ToString("yyyy-MM-dd") + "'");

            if (!string.IsNullOrEmpty(dto.commencement_certificate_no))
                updates.Add("commencement_certificate_no = '" + dto.commencement_certificate_no + "'");

            if (!string.IsNullOrEmpty(dto.pso_number))
                updates.Add("pso_number = '" + dto.pso_number + "'");

            if (updates.Count == 0)
                throw new Exception("No fields to update");

            query.Append("UPDATE " + AddDoubleQuotes(branchschema) + ".tbl_mst_chitgroup SET ");
            query.Append(string.Join(",", updates));
            query.Append(" WHERE groupcode = '" + dto.groupcode + "'");

            NPGSqlHelper.ExecuteNonQuery(Con, CommandType.Text, query.ToString());
        }

        public List<firstmemoapproveddto> getapprovedgroupcodes(string globalschema, string Conn, string branchschema, string branchcode)
        {
            List<firstmemoapproveddto> groupc = new List<firstmemoapproveddto>();
            string Query = "SELECT DISTINCT b.groupcode FROM " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration a INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_mst_chitgroup b ON a.tbl_mst_branch_configuration_id = b.branch_id INNER JOIN " + AddDoubleQuotes(globalschema) + ".tbl_trans_svo_surety_details c ON b.tbl_mst_chitgroup_id = c.chitgroup_id WHERE a.branch_code = '" + branchcode + "' AND c.FIRST_MEMO_STATUS = 'A';";

            using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
            {
                while (dr.Read())
                {
                    firstmemoapproveddto gc = new firstmemoapproveddto
                    {
                        groupcode = dr["groupcode"] == DBNull.Value ? null : Convert.ToString(dr["groupcode"]),
                    };

                    groupc.Add(gc);
                }
            }
            return groupc;
        }

        public List<firstmemoapproveddto> getapproveddetails(string globalschema, string Conn, string groupcode, int ticketno)
        {
            List<firstmemoapproveddto> approvedlist = new List<firstmemoapproveddto>();
            string Query = string.Empty;
            try
            {
                Query = "select approved_date,approved_file_name from " + AddDoubleQuotes(globalschema) + ".tbl_mst_first_memo where groupcode = '" + groupcode + "' and ticketno = " + ticketno + "";

                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
                {
                    while (dr.Read())
                    {
                        firstmemoapproveddto approved = new firstmemoapproveddto
                        {
                            approved_date = dr["approved_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["approved_date"]),
                            approved_file_name = dr["approved_file_name"] == DBNull.Value ? (string)null : Convert.ToString(dr["approved_file_name"])
                        };
                        approvedlist.Add(approved);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return approvedlist;
        }

        public bool RemoveFirstMemo(string globalschema, string groupCode, int ticketNo, string connectionString, out string message)
        {
            bool isSuccess = false;
            message = string.Empty;
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                using (NpgsqlTransaction trans = con.BeginTransaction())
                {
                    try
                    {
                        string updateQuery = @"UPDATE " + AddDoubleQuotes(globalschema) + ".tbl_trans_svo_surety_details SET first_memo_status = NULL WHERE groupcode = '" + groupCode + "' AND ticketno =" + ticketNo + "";

                        using (NpgsqlCommand cmdUpdate = new NpgsqlCommand(updateQuery, con, trans))
                        {
                            cmdUpdate.ExecuteNonQuery();
                        }
                        string deleteQuery = @"DELETE FROM " + AddDoubleQuotes(globalschema) + ".tbl_mst_first_memo WHERE groupcode = '" + groupCode + "' AND ticketno =" + ticketNo + "";

                        using (NpgsqlCommand cmdDelete = new NpgsqlCommand(deleteQuery, con, trans))
                        {
                            cmdDelete.ExecuteNonQuery();
                        }
                        trans.Commit();
                        isSuccess = true;
                        message = "First memo removed successfully";
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        message = ex.Message;
                        isSuccess = false;
                    }
                }
            }
            return isSuccess;
        }

        public List<TicketDTO> GetTickets1(string globalschema, string Conn, string groupcode, string branchcode, string branchschema)
        {
            List<TicketDTO> productionlist = new List<TicketDTO>();
            string Query = string.Empty;
            try
            {
                Query = "SELECT distinct c.ticketno,c.FIRST_MEMO_STATUS FROM " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration a INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_mst_chitgroup b ON a.tbl_mst_branch_configuration_id = b.branch_id INNER JOIN " + AddDoubleQuotes(globalschema) + ".tbl_trans_svo_surety_details c ON b.tbl_mst_chitgroup_id = c.chitgroup_id  WHERE a.branch_code = '" + branchcode + "' AND c.FIRST_MEMO_STATUS = 'A' and c.groupcode='" + groupcode + "';";

                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
                {
                    while (dr.Read())
                    {
                        TicketDTO productionDTO = new TicketDTO
                        {
                            ticketno = Convert.ToInt32(dr["ticketno"])
                        };
                        productionlist.Add(productionDTO);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productionlist;
        }

        public List<sjvdto> getsjvno(string globalschema, string Conn, string branchschema)
        {
            List<sjvdto> productionlist = new List<sjvdto>();
            string Query = string.Empty;
            try
            {
                Query = "select distinct transaction_no,transaction_type from " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration a inner join " + AddDoubleQuotes(branchschema) + ".tbl_trans_subscriber_jv b on a.tbl_mst_branch_configuration_id = b.branch_id where  transaction_type='A'";

                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
                {
                    while (dr.Read())
                    {
                        sjvdto productionDTO = new sjvdto
                        {
                            transacion_no = Convert.ToString(dr["transaction_no"]),
                            transaction_type = Convert.ToString(dr["transaction_type"])
                        };
                        productionlist.Add(productionDTO);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productionlist;
        }

        public void updatesjvno(string branchschema, string Con, string transaction_no)
        {
            string Query = string.Empty;
            try
            {
                Query = "UPDATE " + AddDoubleQuotes(branchschema) + ".tbl_trans_subscriber_jv SET TRANSACTION_TYPE='M' where transaction_NO='" + transaction_no + "' AND TRANSACTION_TYPE='A';";

                NPGSqlHelper.ExecuteNonQuery(Con, System.Data.CommandType.Text, Query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<generalcancelreceiptdto> getgeneralreceiptnumbers(string globalschema, string Conn, string branchschema, string branchcode)
        {
            List<generalcancelreceiptdto> receiptlist = new List<generalcancelreceiptdto>();
            string Query = string.Empty;
            try
            {
                Query = "select general_receipt_number,interbranch_id from " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration a inner join " + AddDoubleQuotes(branchschema) + ".tbl_trans_interbranch_receipt b on a.tbl_mst_branch_configuration_id = b.branch_id where a.branch_code='" + branchcode + "' and b.deposited_status='Y' and b.deposited_reference_no is not null";

                Console.WriteLine(Query);
                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
                {
                    while (dr.Read())
                    {
                        generalcancelreceiptdto receipts = new generalcancelreceiptdto
                        {
                            interbranch_id = Convert.ToInt64(dr["interbranch_id"]),
                            general_receipt_number = Convert.ToString(dr["general_receipt_number"])
                        };
                        receiptlist.Add(receipts);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return receiptlist;
        }

        public List<generalcancelreceiptdto> getgeneralreceiptdetails(string globalschema, string Conn, string branchschema, string general_receipt_number, long interbranch_id)
        {
            List<generalcancelreceiptdto> receiptlist = new List<generalcancelreceiptdto>();

            try
            {
                string caoschema = "";

                string caoschemaQuery = "SELECT DISTINCT a.branch_code FROM " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration a INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_trans_interbranch_receipt b ON a.tbl_mst_branch_configuration_id=b.interbranch_id WHERE a.tbl_mst_branch_configuration_id=" + interbranch_id + ";";

                using (NpgsqlConnection con = new NpgsqlConnection(Conn))
                {
                    con.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand(caoschemaQuery, con))
                    {
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            caoschema = Convert.ToString(result);
                        }
                    }
                }

                if (string.IsNullOrEmpty(caoschema))
                {
                    return receiptlist;
                }

                string Query = "SELECT a.general_receipt_number,b.groupcode,a.ticketno,a.contact_id,a.interbranch_id,b.branch_code,a.deposited_reference_no,c.contact_name,b.receipt_date,b.receipt_number,a.comman_receipt_number FROM " + AddDoubleQuotes(branchschema) + ".tbl_trans_interbranch_receipt a INNER JOIN " + AddDoubleQuotes(caoschema) + ".tbl_trans_trim_data_details b ON a.interbranch_id=b.tbl_trans_trim_data_details_id INNER JOIN " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact c ON a.contact_id=c.tbl_mst_contact_id WHERE a.general_receipt_number='" + general_receipt_number + "';";

                Console.WriteLine(Query);

                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, CommandType.Text, Query))
                {
                    while (dr.Read())
                    {
                        generalcancelreceiptdto receipts = new generalcancelreceiptdto();

                        receipts.caoname = caoschema;
                        receipts.general_receipt_number = Convert.ToString(dr["general_receipt_number"]);
                        receipts.groupcode = Convert.ToString(dr["groupcode"]);
                        receipts.ticketno = Convert.ToInt64(dr["ticketno"]);
                        receipts.comman_receipt_number = Convert.ToInt64(dr["comman_receipt_number"]);
                        receipts.contact_id = Convert.ToInt64(dr["contact_id"]);
                        receipts.interbranch_id = Convert.ToInt64(dr["interbranch_id"]);
                        receipts.deposited_reference_no = Convert.ToString(dr["deposited_reference_no"]);
                        receipts.contact_name = Convert.ToString(dr["contact_name"]);
                        receipts.receipt_date = Convert.ToString(dr["receipt_date"]);
                        receipts.receipt_number = Convert.ToString(dr["receipt_number"]);

                        receiptlist.Add(receipts);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return receiptlist;
        }

        public List<generalcancelreceiptdto> getgeneralreceiptamount(string Conn, string branchschema, string deposited_reference_no)
        {
            List<generalcancelreceiptdto> receiptlist = new List<generalcancelreceiptdto>();
            string Query = string.Empty;
            try
            {
                Query = "SELECT pv.tbl_trans_payment_voucher_id,pv.payment_number,pv.payment_date,pv.total_paid_amount,pvd.tbl_trans_payment_voucher_details_id,pvd.contact_id,pvd.ledger_amount,ttt.tbl_trans_total_transactions_id,ttt.transaction_no,ttt.creditamount,ttt.debitamount FROM " + AddDoubleQuotes(branchschema) + ".tbl_trans_payment_voucher pv LEFT JOIN " + AddDoubleQuotes(branchschema) + ".tbl_trans_payment_voucher_details pvd ON pvd.payment_voucher_id=pv.tbl_trans_payment_voucher_id LEFT JOIN " + AddDoubleQuotes(branchschema) + ".tbl_trans_total_transactions ttt ON ttt.transaction_no=pv.payment_number WHERE pv.payment_number='" + deposited_reference_no + "';";

                Console.WriteLine(Query);
                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
                {
                    while (dr.Read())
                    {
                        generalcancelreceiptdto receipts = new generalcancelreceiptdto
                        {
                            tbl_trans_payment_voucher_id = dr["tbl_trans_payment_voucher_id"] == DBNull.Value ? 0 : Convert.ToInt64(dr["tbl_trans_payment_voucher_id"]),
                            payment_number = dr["payment_number"]?.ToString() ?? "",
                            payment_date = Convert.ToString(dr["payment_date"]),
                            total_paid_amount = dr["total_paid_amount"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["total_paid_amount"]),
                            tbl_trans_payment_voucher_details_id = dr["tbl_trans_payment_voucher_details_id"] == DBNull.Value ? 0 : Convert.ToInt64(dr["tbl_trans_payment_voucher_details_id"]),
                            contact_id = dr["contact_id"] == DBNull.Value ? 0 : Convert.ToInt64(dr["contact_id"]),
                            ledger_amount = dr["ledger_amount"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ledger_amount"]),
                            tbl_trans_total_transactions_id = dr["tbl_trans_total_transactions_id"] == DBNull.Value ? 0 : Convert.ToInt64(dr["tbl_trans_total_transactions_id"]),
                            transaction_no = dr["transaction_no"]?.ToString() ?? "",
                            creditamount = dr["creditamount"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["creditamount"]),
                            debitamount = dr["debitamount"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["debitamount"])
                        };
                        receiptlist.Add(receipts);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return receiptlist;
        }

        public string ReverseReceipt(string caoschema, string globalschema, string branchschema, string groupcode, long ticketno, long contactid, string generalreceiptnumber, long commonreceiptnumber, string paymentnumber, string paymentdate, long paymentvoucherid, long paymentvoucherdetailsid, string Conn)
        {
            string message = "";
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(Conn);
                con.Open();
                StringBuilder sb = new StringBuilder();

                string query1 = "DELETE FROM " + AddDoubleQuotes(caoschema) + ".tbl_trans_trim_data_details WHERE trim_number='" + generalreceiptnumber + "' AND groupcode='" + groupcode + "' AND ticketno=" + ticketno + " AND contact_id=" + contactid + ";";

                Console.WriteLine("Query 1:");
                Console.WriteLine(query1);
                sb.Append(query1);

                string query2 = "UPDATE " + AddDoubleQuotes(branchschema) + ".tbl_trans_interbranch_receipt SET deposited_status='N' WHERE general_receipt_number='" + generalreceiptnumber + "' AND comman_receipt_number=" + commonreceiptnumber + ";";

                Console.WriteLine("Query 2:");
                Console.WriteLine(query2);
                sb.Append(query2);

                string query3 = "UPDATE " + AddDoubleQuotes(branchschema) + ".tbl_trans_payment_voucher SET total_paid_amount=0 WHERE payment_number='" + paymentnumber + "' AND payment_date='" + paymentdate + "';";

                Console.WriteLine("Query 3:");
                Console.WriteLine(query3);
                sb.Append(query3);

                string query4 = "UPDATE " + AddDoubleQuotes(branchschema) + ".tbl_trans_payment_voucher_details SET ledger_amount=0 WHERE payment_voucher_id=" + paymentvoucherid + " AND contact_id=" + contactid + " AND tbl_trans_payment_voucher_details_id=" + paymentvoucherdetailsid + ";";

                Console.WriteLine("Query 4:");
                Console.WriteLine(query4);
                sb.Append(query4);

                string query5 = "DELETE FROM " + AddDoubleQuotes(branchschema) + ".tbl_trans_total_transactions WHERE transaction_no='" + paymentnumber + "';";

                Console.WriteLine("Query 5:");
                Console.WriteLine(query5);
                sb.Append(query5);

                string query6 = "SELECT " + AddDoubleQuotes(globalschema) + ".fntotaltransactions('" + globalschema + "','" + branchschema + "','" + paymentnumber + "','PAYMENT VOUCHER');";

                Console.WriteLine("Query 6:");
                Console.WriteLine(query6);
                sb.Append(query6);


                string query7 = "SELECT " + AddDoubleQuotes(globalschema) + ".accountsupdate('" + branchschema + "');";

                Console.WriteLine("Query 7:");
                Console.WriteLine(query7);
                sb.Append(query7);




                NpgsqlCommand cmd = new NpgsqlCommand(sb.ToString(), con);
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
                message = "Success";
                con.Close();
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }

        public List<ReceiptVoucherTransactionDTO> GetReceiptVoucherTransactions(string branchSchema, string connectionString, string generalReceiptNumber, long contactId, string globalschema)
        {
            List<ReceiptVoucherTransactionDTO> result = new List<ReceiptVoucherTransactionDTO>();
            string query = "SELECT ir.contact_id,pvd.tbl_trans_payment_voucher_details_id,pvd.payment_voucher_id,pvd.debit_account_id AS detail_debit_account_id,pv.payment_number,c.contact_name,pvd.contact_id AS detail_contact_id,pvd.ledger_amount FROM " + AddDoubleQuotes(branchSchema) + ".tbl_trans_interbranch_receipt ir LEFT JOIN " + AddDoubleQuotes(branchSchema) + ".tbl_trans_payment_voucher pv ON pv.payment_number = ir.deposited_reference_no LEFT JOIN " + AddDoubleQuotes(branchSchema) + ".tbl_trans_payment_voucher_details pvd ON pvd.payment_voucher_id = pv.tbl_trans_payment_voucher_id LEFT JOIN " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact c ON c.tbl_mst_contact_id = pvd.contact_id WHERE ir.general_receipt_number='" + generalReceiptNumber + "' AND pvd.contact_id=" + contactId + "";

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                {
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ReceiptVoucherTransactionDTO obj = new ReceiptVoucherTransactionDTO();
                            obj.Tbl_Trans_Payment_Voucher_Details_Id = dr["tbl_trans_payment_voucher_details_id"] == DBNull.Value ? 0 : Convert.ToInt64(dr["tbl_trans_payment_voucher_details_id"]);
                            obj.Payment_Voucher_Id = dr["payment_voucher_id"] == DBNull.Value ? 0 : Convert.ToInt64(dr["payment_voucher_id"]);
                            obj.Detail_Debit_Account_Id = dr["detail_debit_account_id"] == DBNull.Value ? 0 : Convert.ToInt64(dr["detail_debit_account_id"]);
                            obj.Detail_Contact_Id = dr["detail_contact_id"] == DBNull.Value ? 0 : Convert.ToInt64(dr["detail_contact_id"]);
                            obj.Contact_Id = dr["contact_id"] == DBNull.Value ? 0 : Convert.ToInt64(dr["contact_id"]);
                            obj.Ledger_Amount = dr["ledger_amount"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ledger_amount"]);
                            obj.payment_number = dr["payment_number"] == DBNull.Value ? "" : dr["payment_number"].ToString();
                            obj.contact_name = dr["contact_name"] == DBNull.Value ? "" : dr["contact_name"].ToString();
                            result.Add(obj);
                        }
                    }
                }
            }
            return result;
        }

        public List<generalcancelreceiptdto> getchequegeneralnumbers(string globalschema, string Conn, string branchschema, string branchcode)
        {
            List<generalcancelreceiptdto> receiptlist = new List<generalcancelreceiptdto>();
            string Query = string.Empty;
            try
            {
                Query = "SELECT b.interbranch_id,b.general_receipt_number,b.comman_receipt_number,c.receipt_number,c.reference_number FROM " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration a INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_trans_interbranch_receipt b ON a.tbl_mst_branch_configuration_id=b.branch_id INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_trans_receipt_reference c ON c.receipt_number=b.comman_receipt_number::text WHERE a.branch_code='" + branchcode + "' AND c.deposit_status='P' AND c.clear_status='N' AND b.general_receipt_number LIKE 'RCQ%';";

                Console.WriteLine(Query);
                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
                {
                    while (dr.Read())
                    {
                        generalcancelreceiptdto receipts = new generalcancelreceiptdto
                        {
                            general_receipt_number = Convert.ToString(dr["general_receipt_number"]),
                        };
                        receiptlist.Add(receipts);
                    }
                    ;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return receiptlist;
        }

        public List<generalcancelreceiptdto> getchequegeneralnumbersdetails(string globalschema, string Conn, string branchschema, string general_receipt_number)
        {
            List<generalcancelreceiptdto> receiptlist = new List<generalcancelreceiptdto>();
            string Query = string.Empty;
            try
            {
                Query = "SELECT b.interbranch_id,b.comman_receipt_number,c.receipt_number,c.reference_number FROM " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration a INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_trans_interbranch_receipt b ON a.tbl_mst_branch_configuration_id=b.branch_id INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_trans_receipt_reference c ON c.receipt_number=b.comman_receipt_number::text WHERE b.general_receipt_number='" + general_receipt_number + "';";

                Console.WriteLine(Query);
                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
                {
                    while (dr.Read())
                    {
                        generalcancelreceiptdto receipts = new generalcancelreceiptdto
                        {
                            comman_receipt_number = Convert.ToInt64(dr["comman_receipt_number"]),
                            receipt_number = Convert.ToString(dr["receipt_number"]),
                            reference_number = Convert.ToString(dr["reference_number"]),
                            interbranch_id = Convert.ToInt64(dr["interbranch_id"])
                        };
                        receiptlist.Add(receipts);
                    }
                    ;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return receiptlist;
        }

        public List<generalcancelreceiptdto> GetchequeliveDetails(string branchschema, string globalschema, string general_receipt_number, string reference_number, long interbranch_id, string Conn)
        {
            List<generalcancelreceiptdto> lst = new List<generalcancelreceiptdto>();
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(Conn))
                {
                    con.Open();
                    string caoschema = "";
                    caoschema = "SELECT DISTINCT a.branch_code FROM " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration a INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_trans_interbranch_receipt b ON a.tbl_mst_branch_configuration_id=b.interbranch_id WHERE a.tbl_mst_branch_configuration_id=" + interbranch_id + ";";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(caoschema, con))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            caoschema = Convert.ToString(result);
                        }
                    }

                    string qry = "SELECT b.general_receipt_number,b.interbranch_id,b.chitgroup_id,e.groupcode,f.contact_name,b.contact_id,b.ticketno,b.comman_receipt_number,d.receipt_date,d.receipt_number,g.transaction_no,g.reference_receipt_no,g.referencenumber,h.account_type,h.bank_entries_id,MAX(h.ledger_amount) AS ledger_amount FROM " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration a INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_trans_interbranch_receipt b ON a.tbl_mst_branch_configuration_id=b.branch_id INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_trans_receipt_reference c ON c.receipt_number=b.comman_receipt_number::text INNER JOIN " + AddDoubleQuotes(caoschema) + ".tbl_trans_trim_data_details d ON c.reference_number=d.cheque_number INNER JOIN " + AddDoubleQuotes(caoschema) + ".tbl_mst_chitgroup e ON d.chitgroup_id=e.tbl_mst_chitgroup_id INNER JOIN " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact f ON f.tbl_mst_contact_id=b.contact_id INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_trans_bank_entries g ON c.reference_number=g.referencenumber AND c.receipt_number=g.reference_receipt_no INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_trans_bank_entries_details h ON g.tbl_trans_bank_entries_id=h.bank_entries_id WHERE b.general_receipt_number='" + general_receipt_number + "' AND c.reference_number='" + reference_number + "' and d.trim_number='" + general_receipt_number + "' AND d.receipt_number IS NULL AND d.receipt_date IS NULL GROUP BY b.general_receipt_number,b.interbranch_id,b.chitgroup_id,e.groupcode,f. contact_name,b.contact_id,b.ticketno,b.comman_receipt_number,d.receipt_date,d.receipt_number,g.transaction_no,g.reference_receipt_no,g.referencenumber,h.account_type,h.bank_entries_id";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(qry, con))
                    {
                        using (NpgsqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                generalcancelreceiptdto obj = new generalcancelreceiptdto();
                                obj.general_receipt_number = Convert.ToString(dr["general_receipt_number"]);
                                obj.interbranch_id = Convert.ToInt64(dr["interbranch_id"]);
                                obj.chitgroup_id = Convert.ToInt64(dr["chitgroup_id"]);
                                obj.bank_entries_id = Convert.ToInt64(dr["bank_entries_id"]);
                                obj.groupcode = Convert.ToString(dr["groupcode"]);
                                obj.account_type = Convert.ToString(dr["account_type"]);
                                obj.contact_name = Convert.ToString(dr["contact_name"]);
                                obj.contact_id = Convert.ToInt64(dr["contact_id"]);
                                obj.ticketno = Convert.ToInt64(dr["ticketno"]);
                                obj.comman_receipt_number = Convert.ToInt64(dr["comman_receipt_number"]);
                                obj.transaction_no = Convert.ToString(dr["transaction_no"]);
                                obj.deposited_reference_no = Convert.ToString(dr["referencenumber"]);
                                obj.ledger_amount = Convert.ToDecimal(dr["ledger_amount"]);
                                obj.receipt_date = Convert.ToString(dr["receipt_date"]);
                                obj.receipt_number = Convert.ToString(dr["receipt_number"]);
                                obj.caoname = caoschema;
                                lst.Add(obj);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return lst;
        }

        public string ReverseChequeDeposit(string globalschema, string branchschema, string caoschema, long chitgroupid, long ticketno, long contactid, string trimnumber, string referencenumber, string receiptnumber, long bankentriesid, string transactionno, string Conn)
        {
            string message = "";
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(Conn);
                con.Open();
                StringBuilder sb = new StringBuilder();
                string query1 = "DELETE FROM " + AddDoubleQuotes(caoschema) + ".tbl_trans_trim_data_details WHERE chitgroup_id=" + chitgroupid + " AND ticketno=" + ticketno + " AND trim_number='" + trimnumber + "' AND contact_id=" + contactid + ";";

                Console.WriteLine("Query 1:");
                Console.WriteLine(query1);
                sb.Append(query1);

                string query2 = "UPDATE " + AddDoubleQuotes(branchschema) + ".tbl_trans_receipt_reference SET deposited_date=NULL, deposit_status='N' WHERE reference_number='" + referencenumber + "' AND receipt_number='" + receiptnumber + "';";

                Console.WriteLine("Query 2:");
                Console.WriteLine(query2);
                sb.Append(query2);

                string query3 = "DELETE FROM " + AddDoubleQuotes(branchschema) + ".tbl_trans_bank_entries_details WHERE bank_entries_id=" + bankentriesid + ";";

                Console.WriteLine("Query 3:");
                Console.WriteLine(query3);
                sb.Append(query3);

                string query4 = "DELETE FROM " + AddDoubleQuotes(branchschema) + ".tbl_trans_bank_entries WHERE referencenumber='" + referencenumber + "' AND transaction_no='" + transactionno + "';";

                Console.WriteLine("Query 4:");
                Console.WriteLine(query4);
                sb.Append(query4);

                string query5 = "DELETE FROM " + AddDoubleQuotes(branchschema) + ".tbl_trans_total_transactions WHERE transaction_no='" + transactionno + "';";

                Console.WriteLine("Query 5:");
                Console.WriteLine(query5);
                sb.Append(query5);
                NpgsqlCommand cmd = new NpgsqlCommand(sb.ToString(), con);
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
                message = "Success";
                con.Close();
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }

        public List<chequechangepsdto> GetcommencedGroupcodes(string branchschema, string Conn, string globalschema, string branch_code)
        {
            List<chequechangepsdto> productionlist = new List<chequechangepsdto>();
            string Query = string.Empty;
            try
            {
                Query = "SELECT a.branch_code, b.groupcode, b.chitgroup_status FROM " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration a INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_mst_chitgroup b ON a.tbl_mst_branch_configuration_id = b.branch_id WHERE a.branch_code='" + branch_code + "' AND b.chitgroup_status IN ('Commenced','Completed');";

                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, CommandType.Text, Query))
                {
                    while (dr.Read())
                    {
                        chequechangepsdto productionDTO = new chequechangepsdto
                        {
                            branch_code = Convert.ToString(dr["branch_code"]),
                            groupcode = Convert.ToString(dr["groupcode"]),
                            chitgroup_status = Convert.ToString(dr["chitgroup_status"])
                        };

                        productionlist.Add(productionDTO);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productionlist;
        }

        public List<chequechangepsdto> GetcomencedgroupcodeTickets(string branchschema, string Conn, string groupcode)
        {
            List<chequechangepsdto> productionlist = new List<chequechangepsdto>();
            string Query = string.Empty;
            try
            {
                Query = "SELECT DISTINCT b.ticketno, a.groupcode FROM " + AddDoubleQuotes(branchschema) + ".tbl_mst_chitgroup a INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_trans_ps_cheques b ON a.tbl_mst_chitgroup_id = b.chitgroup_id WHERE a.groupcode='" + groupcode + "';";

                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
                {
                    while (dr.Read())
                    {
                        chequechangepsdto productionDTO = new chequechangepsdto
                        {
                            groupcode = Convert.ToString(dr["groupcode"]),
                            ticketno = Convert.ToInt32(dr["ticketno"])
                        };
                        productionlist.Add(productionDTO);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productionlist;
        }

        public List<chequechangepsdto> Getcomencedchequenumberdetails(string globalschema, string branchschema, string Conn, string groupcode, string branch_code, long tickeno)
        {
            List<chequechangepsdto> productionlist = new List<chequechangepsdto>();
            string Query = string.Empty;
            try
            {
                Query = "SELECT c.tbl_trans_ps_cheques_id, c.cheque_number, c.bank_name, c.branch_name, c.ticketno FROM " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration a INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_mst_chitgroup b ON a.tbl_mst_branch_configuration_id = b.branch_id INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_trans_ps_cheques c ON b.tbl_mst_chitgroup_id = c.chitgroup_id WHERE a.branch_code='" + branch_code + "' AND b.groupcode='" + groupcode + "' AND c.ticketno=" + tickeno + ";";

                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
                {
                    while (dr.Read())
                    {
                        chequechangepsdto productionDTO = new chequechangepsdto
                        {
                            tbl_trans_ps_cheques_id = Convert.ToInt64(dr["tbl_trans_ps_cheques_id"]),
                            cheque_number = Convert.ToString(dr["cheque_number"]),
                            bank_name = Convert.ToString(dr["bank_name"]),
                            branch_name = Convert.ToString(dr["branch_name"]),
                            ticketno = Convert.ToInt64(dr["ticketno"])
                        };
                        productionlist.Add(productionDTO);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return productionlist;
        }

        public string DeletePSCheques(string branchschema, string chequenumbers, string chequeids, string Conn)
        {
            string message = "";
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(Conn);
                con.Open();
                StringBuilder sb = new StringBuilder();
                string[] chequeArray = chequenumbers.Split(',');
                string formattedChequeNumbers = "'" + string.Join("','", chequeArray) + "'";
                string query1 = "DELETE FROM " + AddDoubleQuotes(branchschema) + ".tbl_trans_ps_cheques WHERE cheque_number IN (" + formattedChequeNumbers + ") AND tbl_trans_ps_cheques_id IN (" + chequeids + ");";

                Console.WriteLine("Query 1:");
                Console.WriteLine(query1);
                sb.Append(query1);

                NpgsqlCommand cmd = new NpgsqlCommand(sb.ToString(), con);
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
                message = "Success";
                con.Close();
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }

        public List<MvCancelDTO> GetMvNumbers(string connectionString, string branchschema, string globalschema, string branchcode)
        {
            List<MvCancelDTO> list = new List<MvCancelDTO>();
            try
            {
                string query = "select payment_number from " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration a inner join " + AddDoubleQuotes(branchschema) + ".tbl_trans_payment_voucher b on a.tbl_mst_branch_configuration_id = b. branch_id where a.branch_code ='" + branchcode + "' and b.total_paid_amount>0";

                Console.WriteLine(query);
                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(
                           connectionString, CommandType.Text, query))
                {
                    while (dr.Read())
                    {
                        list.Add(new MvCancelDTO
                        {
                            // tbl_trans_payment_voucher_id = dr["tbl_trans_payment_voucher_id"] == DBNull.Value
                            // ? 0 : Convert.ToInt64(dr["tbl_trans_payment_voucher_id"]),
                            payment_number = dr["payment_number"]?.ToString() ?? "",
                            // payment_date = dr["payment_date"] == DBNull.Value
                            // ? (DateTime?)null : Convert.ToDateTime(dr["payment_date"]),
                            // total_paid_amount = dr["total_paid_amount"] == DBNull.Value
                            // ? 0 : Convert.ToDecimal(dr["total_paid_amount"]),
                        });
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            return list;
        }

        public List<MvCancelDTO> GetMvdetails(string connectionString, string branchschema, string paymentnumber)
        {
            List<MvCancelDTO> list = new List<MvCancelDTO>();
            try
            {
                string query = "select tbl_trans_payment_voucher_id,total_paid_amount,payment_date,payment_number from " + AddDoubleQuotes(branchschema) + ".tbl_trans_payment_voucher where payment_number='" + paymentnumber + "'";

                Console.WriteLine(query);
                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(
                           connectionString, CommandType.Text, query))
                {
                    while (dr.Read())
                    {
                        list.Add(new MvCancelDTO
                        {
                            tbl_trans_payment_voucher_id = dr["tbl_trans_payment_voucher_id"] == DBNull.Value ? 0 : Convert.ToInt64(dr["tbl_trans_payment_voucher_id"]),
                            payment_number = dr["payment_number"]?.ToString() ?? "",
                            payment_date = dr["payment_date"] == DBNull.Value
                                 ? (DateTime?)null : Convert.ToDateTime(dr["payment_date"]),
                            total_paid_amount = dr["total_paid_amount"] == DBNull.Value
                                 ? 0 : Convert.ToDecimal(dr["total_paid_amount"]),
                        });
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            return list;
        }

        public List<MvCancelDTO> GetMvnumberdetails(string connectionString, string branchschema, string paymentnumber, string globalschema, long paymentvoucherid)
        {
            List<MvCancelDTO> list = new List<MvCancelDTO>();
            try
            {
                string query = "select a.payment_number,b.tbl_trans_payment_voucher_details_id,b.contact_id,b.ledger_amount,c.contact_name from " + AddDoubleQuotes(branchschema) + ".tbl_trans_payment_voucher a inner join " + AddDoubleQuotes(branchschema) + ".tbl_trans_payment_voucher_details b on b.payment_voucher_id=a.tbl_trans_payment_voucher_id inner join " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact c on c.tbl_mst_contact_id=b.contact_id where payment_voucher_id=" + paymentvoucherid + " and payment_number='" + paymentnumber + "'";

                Console.WriteLine(query);
                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(
                           connectionString, CommandType.Text, query))
                {
                    while (dr.Read())
                    {
                        list.Add(new MvCancelDTO
                        {
                            tbl_trans_payment_voucher_details_id = dr["tbl_trans_payment_voucher_details_id"] == DBNull.Value ? 0 : Convert.ToInt64(dr["tbl_trans_payment_voucher_details_id"]),
                            contact_id = dr["contact_id"] == DBNull.Value ? 0 : Convert.ToInt64(dr["contact_id"]),
                            payment_number = dr["payment_number"]?.ToString() ?? "",
                            contact_name = dr["contact_name"]?.ToString() ?? "",
                            ledger_amount = dr["ledger_amount"] == DBNull.Value
                                 ? 0 : Convert.ToDecimal(dr["ledger_amount"]),

                        });
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            return list;
        }




        // public string ReverseMvVoucher(
        //     string connectionString,
        //     string branchschema,
        //     string globalschema,
        //     string payment_number,
        //     string payment_date,
        //     long paymentvoucherid,
        //     long contactid,
        //     long paymentvoucherdetailsid)
        // {
        //     string message = "";
        //     try
        //     {
        //         using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
        //         {
        //             con.Open();
        //             StringBuilder sb = new StringBuilder();

        //             string q1 = "UPDATE " + AddDoubleQuotes(branchschema) + ".tbl_trans_payment_voucher SET total_paid_amount=0 WHERE payment_number='" + payment_number + "' AND payment_date='" + payment_date + "';";
        //             Console.WriteLine("Q1: " + q1);
        //             sb.Append(q1);

        //             string q2 = "UPDATE " + AddDoubleQuotes(branchschema) + ".tbl_trans_payment_voucher_details SET ledger_amount=0 WHERE payment_voucher_id=" + paymentvoucherid + " AND contact_id=" + contactid + " AND tbl_trans_payment_voucher_details_id=" + paymentvoucherdetailsid + ";";
        //             Console.WriteLine("Q2: " + q2);
        //             sb.Append(q2);

        //             // string q3 = "UPDATE " + AddDoubleQuotes(branchschema) +
        //             //             ".tbl_trans_total_transactions SET debitamount=0 " +
        //             //             "WHERE transaction_no='" + payment_number + "' " +
        //             //             "AND debitamount=" + debit_amount +
        //             //             " AND tbl_trans_total_transactions_id=" + debit_transaction_id + ";";
        //             string q3 = "update " + AddDoubleQuotes(branchschema) + ".tbl_trans_total_transactions set debitamount=0 where transaction_no='" + payment_number + "' and debitamount > 0";
        //             Console.WriteLine("Q3: " + q3);
        //             sb.Append(q3);

        //             string q4 = "update " + AddDoubleQuotes(branchschema) + ".tbl_trans_total_transactions set creditamount=0 where transaction_no='" + payment_number + "' and creditamount > 0";
        //             // string q4 = "UPDATE " + AddDoubleQuotes(branchschema) +
        //             //             ".tbl_trans_total_transactions SET creditamount=0 " +
        //             //             "WHERE transaction_no='" + payment_number + "' " +
        //             //             "AND creditamount=" + credit_amount +
        //             //             " AND tbl_trans_total_transactions_id=" + credit_transaction_id + ";";
        //             Console.WriteLine("Q4: " + q4);
        //             sb.Append(q4);

        //             using (NpgsqlCommand cmd = new NpgsqlCommand(sb.ToString(), con))
        //             {
        //                 cmd.CommandTimeout = 0;
        //                 cmd.ExecuteNonQuery();
        //             }

        //             message = "Success";
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         message = ex.Message;
        //     }
        //     return message;
        // }

        public string ReverseMvVoucher(
   string connectionString,
   string branchschema,
   string globalschema,
   string payment_number,
   DateTime payment_date,
   long paymentvoucherid,
   long contactid,
   long paymentvoucherdetailsid)
        {
            string message = "";

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();

                    using (var tran = con.BeginTransaction())
                    {
                        try
                        {
                            // Q1
                            string q1 = @"
                        UPDATE " + AddDoubleQuotes(branchschema) + @".tbl_trans_payment_voucher 
                        SET total_paid_amount = 0 
                        WHERE payment_number = @payment_number 
                        AND payment_date = @payment_date";

                            using (var cmd1 = new NpgsqlCommand(q1, con, tran))
                            {
                                cmd1.Parameters.AddWithValue("@payment_number", payment_number);
                                cmd1.Parameters.AddWithValue("@payment_date", payment_date);
                                cmd1.ExecuteNonQuery();
                            }

                            // Q2
                            string q2 = @"
                        UPDATE " + AddDoubleQuotes(branchschema) + @".tbl_trans_payment_voucher_details 
                        SET ledger_amount = 0 
                        WHERE payment_voucher_id = @paymentvoucherid 
                        AND contact_id = @contactid 
                        AND tbl_trans_payment_voucher_details_id = @paymentvoucherdetailsid";

                            using (var cmd2 = new NpgsqlCommand(q2, con, tran))
                            {
                                cmd2.Parameters.AddWithValue("@paymentvoucherid", paymentvoucherid);
                                cmd2.Parameters.AddWithValue("@contactid", contactid);
                                cmd2.Parameters.AddWithValue("@paymentvoucherdetailsid", paymentvoucherdetailsid);
                                cmd2.ExecuteNonQuery();
                            }

                            // Q3
                            string q3 = @"
                        UPDATE " + AddDoubleQuotes(branchschema) + @".tbl_trans_total_transactions 
                        SET debitamount = 0 
                        WHERE transaction_no = @payment_number 
                        AND debitamount > 0";

                            using (var cmd3 = new NpgsqlCommand(q3, con, tran))
                            {
                                cmd3.Parameters.AddWithValue("@payment_number", payment_number);
                                cmd3.ExecuteNonQuery();
                            }

                            // Q4
                            string q4 = @"
                        UPDATE " + AddDoubleQuotes(branchschema) + @".tbl_trans_total_transactions 
                        SET creditamount = 0 
                        WHERE transaction_no = @payment_number 
                        AND creditamount > 0";

                            using (var cmd4 = new NpgsqlCommand(q4, con, tran))
                            {
                                cmd4.Parameters.AddWithValue("@payment_number", payment_number);
                                cmd4.ExecuteNonQuery();
                            }

                            tran.Commit();
                            message = "Success";
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            message = ex.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return message;
        }

        public BusinessRefDTO GetBusinessRefByTicket(string globalschema, string schema, string Conn, int ticketNo, int chitgroupid)
        {
            BusinessRefDTO dto = new BusinessRefDTO();
            string Query = string.Empty;
            try
            {
                Query = "SELECT b.subscriber_id, b.business_reference_id, b.business_reference_name, " +
                        "c.contact_mailing_name, c.tbl_mst_contact_id " +
                        "FROM " + AddDoubleQuotes(schema) + ".tbl_mst_business_reference b, " +
                        AddDoubleQuotes(globalschema) + ".tbl_mst_contact c " +
                        "WHERE b.subscriber_id = c.tbl_mst_contact_id " +
                        "AND b.chitgroup_id = " + chitgroupid +
                        " AND b.ticketno = " + ticketNo +
                        " AND b.status = true " +
                        "ORDER BY b.tbl_mst_business_reference_id LIMIT 1;";

                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
                {
                    if (dr.Read())
                    {
                        dto.ContactId = Convert.ToInt32(dr["tbl_mst_contact_id"]);
                        dto.SubscriberId = Convert.ToInt32(dr["subscriber_id"]);
                        dto.SubscriberName = Convert.ToString(dr["contact_mailing_name"]);
                        dto.EmployeeName = Convert.ToString(dr["business_reference_name"]);
                        dto.BusinessReferenceId = Convert.ToInt64(dr["business_reference_id"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dto;
        }


        public bool UpdateBusinessRef(string schema, string Con, BusinessRefUpdateDTO dto)
        {
            bool isSaved = false;
            StringBuilder sbinsert = new StringBuilder();
            NpgsqlTransaction trans = null;
            con = new NpgsqlConnection(Con);
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            trans = con.BeginTransaction();
            try
            {
                if (dto.Ptypeofoperation.Trim().ToUpper() == "UPDATE")
                {
                    sbinsert.Append("UPDATE " + AddDoubleQuotes(schema) + ".tbl_mst_business_reference SET business_reference_id = " + dto.NewBusinessReferenceId + " WHERE chitgroup_id = " + dto.Chitgroup_id + " AND ticketno = " + dto.Ticketno + " AND business_reference_id = " + dto.OldBusinessReferenceId + " AND subscriber_id = " + dto.SubscriberId + ";");
                }

                if (!string.IsNullOrEmpty(sbinsert.ToString()))
                {
                    NPGSqlHelper.ExecuteNonQuery(trans, CommandType.Text, sbinsert.ToString());
                }
                trans.Commit();
                isSaved = true;
            }
            catch (Exception Ex)
            {
                trans.Rollback();
                isSaved = false;
                throw Ex;
            }
            return isSaved;
        }


        public List<ChequeDetailsDTO> GetChequenumbers(string branchschema, string Con, string globalschema)
        {
            List<ChequeDetailsDTO> checkdetailslist = new List<ChequeDetailsDTO>();
            string query = string.Empty;
            try
            {
                query = "SELECT DISTINCT payment_number, reference_number, clear_status FROM " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration a INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_trans_payment_reference b ON b.branch_id = a.tbl_mst_branch_configuration_id WHERE clear_date IS NOT NULL AND clear_status IN ('P','C');";

                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Con, System.Data.CommandType.Text, query))
                {
                    while (dr.Read())
                    {
                        ChequeDetailsDTO checkDTO = new ChequeDetailsDTO();
                        checkDTO.payment_number = Convert.ToString(dr["payment_number"]);
                        checkDTO.reference_number = Convert.ToString(dr["reference_number"]);
                        checkDTO.clear_status = Convert.ToString(dr["clear_status"]);
                        checkdetailslist.Add(checkDTO);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return checkdetailslist;
        }

        public List<PaymentReferenceDTO> GetchequeDetails(string branchschema, string Conn, string paymentnumber, string referencenumber, string clearstatus)
        {
            List<PaymentReferenceDTO> lst = new List<PaymentReferenceDTO>();

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(Conn))
                {
                    con.Open();


                    string query = "";

                    if (clearstatus == "P")
                    {
                        query = "SELECT payment_number,reference_number,payment_date,clear_date,paid_amount,paid_to " +
                                "FROM " + AddDoubleQuotes(branchschema) + ".tbl_trans_payment_reference " +
                                "WHERE payment_number='" + paymentnumber + "' " +
                                "AND reference_number='" + referencenumber + "' " +
                                "AND clear_status='P'";
                    }
                    else if (clearstatus == "C")
                    {
                        query = "SELECT a.payment_number,a.reference_number,a.payment_date,a.clear_date,a.paid_amount,a.paid_to," +
                                "b.tbl_trans_bank_entries_id,b.transaction_no,c.tbl_trans_bank_entries_details_id " +
                                "FROM " + AddDoubleQuotes(branchschema) + ".tbl_trans_payment_reference a " +
                                "INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_trans_bank_entries b " +
                                "ON b.referencenumber=a.reference_number " +
                                "INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_trans_bank_entries_details c " +
                                "ON c.bank_entries_id=b.tbl_trans_bank_entries_id " +
                                "WHERE a.payment_number='" + paymentnumber + "' " +
                                "AND a.reference_number='" + referencenumber + "' " +
                                "AND a.clear_status='C'";
                    }

                    if (!string.IsNullOrEmpty(query))
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                        {
                            // cmd.Parameters.AddWithValue("@paymentnumber", paymentnumber);
                            // cmd.Parameters.AddWithValue("@referencenumber", referencenumber);

                            using (NpgsqlDataReader dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    PaymentReferenceDTO obj = new PaymentReferenceDTO();

                                    obj.payment_number = dr["payment_number"].ToString();
                                    obj.reference_number = dr["reference_number"].ToString();
                                    obj.payment_date = dr["payment_date"].ToString();
                                    obj.clear_date = dr["clear_date"].ToString();
                                    obj.paid_amount = Convert.ToDecimal(dr["paid_amount"]);
                                    obj.paid_to = dr["paid_to"].ToString();

                                    if (dr.GetSchemaTable().Columns.Contains("tbl_trans_bank_entries_id") && dr["tbl_trans_bank_entries_id"] != DBNull.Value)
                                    {
                                        obj.tbl_trans_bank_entries_id = Convert.ToInt64(dr["tbl_trans_bank_entries_id"]);
                                    }

                                    if (dr.GetSchemaTable().Columns.Contains("transaction_no") && dr["transaction_no"] != DBNull.Value)
                                    {
                                        obj.transaction_no = dr["transaction_no"].ToString();
                                    }

                                    if (dr.GetSchemaTable().Columns.Contains("tbl_trans_bank_entries_details_id") && dr["tbl_trans_bank_entries_details_id"] != DBNull.Value)
                                    {
                                        obj.tbl_trans_bank_entries_details_id = Convert.ToInt64(dr["tbl_trans_bank_entries_details_id"]);
                                    }

                                    lst.Add(obj);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lst;
        }

        public string ClearDateandStatus(string branchschema, string paymentnumber, string referencenumber, string transactionno, long bankentriesid, string Conn, string clearstatus)
        {
            string message = "";

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(Conn))
                {
                    con.Open();

                    NpgsqlTransaction trans = con.BeginTransaction();

                    try
                    {
                        string query = "";
                        if (clearstatus == "P")
                        {
                            query = "UPDATE " + AddDoubleQuotes(branchschema) + ".tbl_trans_payment_reference " +
                                          "SET clear_status='N', clear_date=NULL " +
                                          "WHERE clear_status='P' AND clear_date IS NOT NULL " +
                                          "AND payment_number='" + paymentnumber + "'";

                            using (NpgsqlCommand cmd = new NpgsqlCommand(query, con, trans))
                            {
                                // cmd.Parameters.AddWithValue("@paymentnumber", paymentnumber);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        if (clearstatus == "C")
                        {
                            query = "DELETE FROM " + AddDoubleQuotes(branchschema) + ".tbl_trans_bank_entries_details " +
                                    "WHERE bank_entries_id=" + bankentriesid + "";

                            using (NpgsqlCommand cmd = new NpgsqlCommand(query, con, trans))
                            {
                                // cmd.Parameters.AddWithValue("@bankentriesid", bankentriesid);
                                cmd.ExecuteNonQuery();
                            }

                            query = "DELETE FROM " + AddDoubleQuotes(branchschema) + ".tbl_trans_bank_entries " +
                                    "WHERE referencenumber='" + referencenumber + "' AND transaction_no='" + transactionno + "'";

                            using (NpgsqlCommand cmd = new NpgsqlCommand(query, con, trans))
                            {
                                // cmd.Parameters.AddWithValue("@referencenumber", referencenumber);
                                // cmd.Parameters.AddWithValue("@transactionno", transactionno);
                                cmd.ExecuteNonQuery();
                            }

                            query = "DELETE FROM " + AddDoubleQuotes(branchschema) + ".tbl_trans_total_transactions " +
                                    "WHERE transaction_no='" + transactionno + "'";

                            using (NpgsqlCommand cmd = new NpgsqlCommand(query, con, trans))
                            {
                                // cmd.Parameters.AddWithValue("@transactionno", transactionno);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        trans.Commit();
                        message = "Success";
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        message = ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return message;
        }

        public List<pannumberchnagedto> Getpannumbers(string connectionString, string globalschema)
        {
            List<pannumberchnagedto> list = new List<pannumberchnagedto>();
            try
            {
                string query = "SELECT DISTINCT pan_number FROM " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact WHERE NULLIF(TRIM(pan_number), '') IS NOT NULL;";

                Console.WriteLine(query);
                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(
                           connectionString, CommandType.Text, query))
                {
                    while (dr.Read())
                    {
                        list.Add(new pannumberchnagedto
                        {
                            pan_number = dr["pan_number"]?.ToString() ?? "",
                        });
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            return list;
        }

        public List<pannumberchnagedto> GetContactDocumentDetails(
string connectionString,
string panNumber, string globalschema)
        {
            List<pannumberchnagedto> lst = new List<pannumberchnagedto>();

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                string qry = @"SELECT a.tbl_mst_contact_id,a.contact_reference_id,a.contact_name, b.document_reference_no, b.tbl_mst_contact_documents_id, b.document_proofs_id, c.tbl_trans_chit_advance_id, c.branch_id, d.branch_code FROM " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact a INNER JOIN " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact_documents b ON b.contact_id = a.tbl_mst_contact_id AND b.document_reference_no = a.pan_number INNER JOIN " + AddDoubleQuotes(globalschema) + ".vw_trans_chit_advance c ON c.contact_id = b.contact_id INNER JOIN " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration d ON d.tbl_mst_branch_configuration_id = c.branch_id WHERE a.pan_number = '" + panNumber + "'";


                using (NpgsqlCommand cmd = new NpgsqlCommand(qry, con))
                {
                    // cmd.Parameters.AddWithValue("@panNumber", panNumber);

                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lst.Add(new pannumberchnagedto
                            {
                                contact_name = dr["contact_name"]?.ToString(),
                                contact_reference_id = dr["contact_reference_id"]?.ToString(),
                                document_reference_no = dr["document_reference_no"]?.ToString(),
                                tbl_mst_contact_documents_id = Convert.ToInt64(dr["tbl_mst_contact_documents_id"]),
                                tbl_mst_contact_id = Convert.ToInt64(dr["tbl_mst_contact_id"]),
                                document_proofs_id = Convert.ToInt64(dr["document_proofs_id"]),
                                tbl_trans_chit_advance_id = Convert.ToInt64(dr["tbl_trans_chit_advance_id"]),
                                branch_id = Convert.ToInt64(dr["branch_id"]),
                                branch_code = dr["branch_code"]?.ToString()
                            });
                        }
                    }
                }
            }

            return lst;
        }

        public string UpdatePanNumber(
    string connectionString,
    string branchschema,
    string oldPanNumber,
    string newPanNumber,
    string contactReferenceIds,
    long contactIds,
    string documentIds,
    string globalschema)
        {
            string message = "";

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();

                    using (var tran = con.BeginTransaction())
                    {
                        try
                        {
                            StringBuilder sb = new StringBuilder();


                            contactReferenceIds = "'" + contactReferenceIds.Replace(",", "','") + "'";
                            string qry1 = "UPDATE " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact SET pan_number='" + newPanNumber + "' WHERE contact_reference_id IN (" + contactReferenceIds + ") AND pan_number='" + oldPanNumber + "';";

                            string qry2 = "UPDATE " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact_documents SET document_reference_no='" + newPanNumber + "' WHERE contact_id IN (" + contactIds + ") AND tbl_mst_contact_documents_id IN (" + documentIds + ");";

                            string qry3 = "UPDATE " + AddDoubleQuotes(branchschema) + ".tbl_trans_chit_advance SET pan_number='" + newPanNumber + "' WHERE contact_id IN (" + contactIds + ");";

                            Console.WriteLine("Query1 : " + qry1);
                            Console.WriteLine("Query2 : " + qry2);
                            Console.WriteLine("Query3 : " + qry3);

                            sb.Append(qry1);
                            sb.Append(qry2);
                            sb.Append(qry3);
                            using (NpgsqlCommand cmd = new NpgsqlCommand(sb.ToString(), con, tran))
                            {
                                cmd.CommandTimeout = 0;
                                cmd.ExecuteNonQuery();
                            }

                            tran.Commit();
                            message = "Updated Successfully";
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            message = ex.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return message;
        }


        public List<PaymentAdjustmentDTO> GetPaymentAdjustmentDetails(
    string connectionString,
    string branchschema,
    string globalschema,
    string groupcode,
    int ticketno)
        {
            List<PaymentAdjustmentDTO> lst = new List<PaymentAdjustmentDTO>();

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                string qry = "SELECT b.subscriber_name, f.adjustment_amount, f.tbl_trans_bpo_cheques_information_details_id, f.payment_adjustment_type, f.other_adjustment_type_id, f.cheque_number, h.tbl_trans_chit_payment_adjustments_id FROM " + AddDoubleQuotes(branchschema) + ".tbl_mst_chitgroup a INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_mst_subscriber b ON b.chitgroup_id=a.tbl_mst_chitgroup_id INNER JOIN " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact c ON c.tbl_mst_contact_id=b.contact_id INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_mst_bpo_inward d ON d.groupcode=a.groupcode AND d.ticketno=b.ticketno AND d.contact_id=c.tbl_mst_contact_id INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_trans_bpo_cheques_information e ON e.bpo_reference_id=d.bpo_reference_id INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_trans_bpo_cheques_information_details f ON f.bpo_cheques_information_id=e.tbl_trans_bpo_cheques_information_id AND f.adjustment_ticketno=b.ticketno INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_trans_chit_payment g ON g.chitgroup_id=a.tbl_mst_chitgroup_id AND g.ticketno=b.ticketno AND g.transaction_amount=f.adjustment_amount INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_trans_chit_payment_adjustments h ON h.chit_payment_id=g.tbl_trans_chit_payment_id AND h.adjustment_ticketno=b.ticketno AND h.adjustment_contact_id=b.contact_id AND h.adjustment_amount=f.adjustment_amount WHERE a.groupcode='" + groupcode + "' AND b.ticketno=" + ticketno + " AND f.other_adjustment_type_id IS NOT NULL";

                Console.WriteLine(qry);

                using (NpgsqlCommand cmd = new NpgsqlCommand(qry, con))
                {
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            PaymentAdjustmentDTO obj = new PaymentAdjustmentDTO();

                            obj.subscriber_name = dr["subscriber_name"].ToString();
                            obj.adjustment_amount = Convert.ToDecimal(dr["adjustment_amount"]);
                            obj.tbl_trans_bpo_cheques_information_details_id = Convert.ToInt64(dr["tbl_trans_bpo_cheques_information_details_id"]);
                            obj.payment_adjustment_type = dr["payment_adjustment_type"].ToString();
                            obj.other_adjustment_type_id = Convert.ToInt64(dr["other_adjustment_type_id"]);
                            obj.cheque_number = dr["cheque_number"].ToString();
                            obj.tbl_trans_chit_payment_adjustments_id = Convert.ToInt64(dr["tbl_trans_chit_payment_adjustments_id"]);

                            lst.Add(obj);
                        }
                    }
                }
            }

            return lst;
        }

        //     public List<PaymentAdjustmentDTO> GetSubscriberDetails(
        // string connectionString,
        // string branchschema,
        // string globalschema,
        // string groupcode,
        // int ticketno)
        //     {
        //         List<PaymentAdjustmentDTO> lst = new List<PaymentAdjustmentDTO>();

        //         using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
        //         {
        //             con.Open();

        //             string qry = "SELECT b.tbl_mst_subscriber_id,b.subscriber_name,c.tbl_mst_contact_id FROM " + AddDoubleQuotes(branchschema) + ".tbl_mst_chitgroup a INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_mst_subscriber b ON b.chitgroup_id=a.tbl_mst_chitgroup_id INNER JOIN " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact c ON c.tbl_mst_contact_id=b.contact_id WHERE a.groupcode='" + groupcode + "' AND b.ticketno=" + ticketno;

        //             Console.WriteLine(qry);

        //             using (NpgsqlCommand cmd = new NpgsqlCommand(qry, con))
        //             {
        //                 using (NpgsqlDataReader dr = cmd.ExecuteReader())
        //                 {
        //                     while (dr.Read())
        //                     {
        //                         PaymentAdjustmentDTO obj = new PaymentAdjustmentDTO();

        //                         obj.subscriber_name = dr["subscriber_name"].ToString();
        //                         obj.tbl_mst_subscriber_id = Convert.ToInt64(dr["tbl_mst_subscriber_id"]);
        //                         obj.tbl_mst_contact_id = Convert.ToInt64(dr["tbl_mst_contact_id"]);


        //                         lst.Add(obj);
        //                     }
        //                 }
        //             }
        //         }

        //         return lst;
        //     }

        public List<PaymentAdjustmentDTO> GetSubscriberDetails(
        string connectionString,
        string branchschema,
        string globalschema,
        string groupcode,
        int ticketno)
        {
            Console.WriteLine("1. Method Entered");

            List<PaymentAdjustmentDTO> lst = new List<PaymentAdjustmentDTO>();

            try
            {
                // using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    Console.WriteLine("2. Connection Object Created");

                    con.Open();
                    Console.WriteLine("3. Database Connection Opened Successfully");

                    string qry = "SELECT b.tbl_mst_subscriber_id,b.subscriber_name,c.tbl_mst_contact_id " +
                                 "FROM " + AddDoubleQuotes(branchschema) + ".tbl_mst_chitgroup a " +
                                 "INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_mst_subscriber b " +
                                 "ON b.chitgroup_id=a.tbl_mst_chitgroup_id " +
                                 "INNER JOIN " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact c " +
                                 "ON c.tbl_mst_contact_id=b.contact_id " +
                                 "WHERE a.groupcode='" + groupcode + "' " +
                                 "AND b.ticketno=" + ticketno;

                    Console.WriteLine("4. Query Created");
                    Console.WriteLine(qry);

                    using (NpgsqlCommand cmd = new NpgsqlCommand(qry, con))
                    {
                        cmd.CommandTimeout = 120;

                        Console.WriteLine("5. Command Created");

                        using (NpgsqlDataReader dr = cmd.ExecuteReader())
                        {
                            Console.WriteLine("6. ExecuteReader Executed Successfully");

                            while (dr.Read())
                            {
                                Console.WriteLine("7. Reading Record");

                                PaymentAdjustmentDTO obj = new PaymentAdjustmentDTO();

                                obj.subscriber_name = dr["subscriber_name"].ToString();
                                obj.tbl_mst_subscriber_id = Convert.ToInt64(dr["tbl_mst_subscriber_id"]);
                                obj.tbl_mst_contact_id = Convert.ToInt64(dr["tbl_mst_contact_id"]);

                                lst.Add(obj);
                            }

                            Console.WriteLine("8. All Records Read");
                        }

                        Console.WriteLine("9. Reader Closed");
                    }

                    Console.WriteLine("10. Command Completed");
                }

                Console.WriteLine("11. Connection Closed");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR OCCURRED");
                Console.WriteLine("Message: " + ex.Message);
                Console.WriteLine("StackTrace: " + ex.StackTrace);

                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }

                throw;
            }

            Console.WriteLine("12. Returning Data");

            return lst;
        }

        public string UpdateAdjustmentDetails(
    string connectionString,
    string branchschema,
    long adjustmentChitGroupId,
    long adjustmentTicketNo,
    long adjustmentContactId,
    string bpoChequeInformationIds,
    string bpoChequeInformationDetailIds,
    decimal adjustmentAmount,
    string chitPaymentAdjustmentIds)
        {
            string message = "";

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();

                    using (var tran = con.BeginTransaction())
                    {
                        try
                        {
                            StringBuilder sb = new StringBuilder();

                            string qry1 = "UPDATE " + AddDoubleQuotes(branchschema) + ".tbl_trans_bpo_cheques_information_details SET adjustment_chitgroup_id=" + adjustmentChitGroupId + ", adjustment_ticketno=" + adjustmentTicketNo + ", adjustment_contact_id=" + adjustmentContactId + " WHERE bpo_cheques_information_id IN (" + bpoChequeInformationIds + ") AND adjustment_ticketno=" + adjustmentTicketNo + " AND tbl_trans_bpo_cheques_information_details_id IN (" + bpoChequeInformationDetailIds + ");";

                            string qry2 = "UPDATE " + AddDoubleQuotes(branchschema) + ".tbl_trans_chit_payment_adjustments SET adjustment_contact_id=" + adjustmentContactId + " WHERE adjustment_amount=" + adjustmentAmount + " AND adjustment_ticketno=" + adjustmentTicketNo + " AND tbl_trans_chit_payment_adjustments_id IN (" + chitPaymentAdjustmentIds + ");";

                            Console.WriteLine(qry1);
                            Console.WriteLine(qry2);

                            sb.Append(qry1);
                            sb.Append(qry2);

                            using (NpgsqlCommand cmd = new NpgsqlCommand(sb.ToString(), con, tran))
                            {
                                cmd.CommandTimeout = 0;
                                cmd.ExecuteNonQuery();
                            }

                            tran.Commit();
                            message = "Updated Successfully";
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            message = ex.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return message;
        }


        public string InsertSubscriberIncome(
    string connectionString,
    string branchschema,
    long subscriberId,
    int branch_id)
        {
            string message = "";

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();

                    string qry = "";

                    qry = "insert into  " + AddDoubleQuotes(branchschema) + ".tbl_mst_subscriber_income( branch_id, subscriber_id, designation_id, employement_type, basic_salary_amount, hire_date, allowance_amount, gross_amount, deductions_amount, net_amount, reporting_to, current_company_experience, total_work_experience, name_of_organization, enterprise_type_id, office_phone_number, date_of_establishment, date_of_commencement, gst_number, cin_number, din_number, trade_license_number, status, employee_id, type_of_employee, retirement_date, share_in_business_amount, investment_amount, net_worth_of_business_amount, annual_income_amount) values (" + branch_id + "," + subscriberId + ",null,null,0.00,null,0.00,0.00,0.00,0.00,null,null,null,null,null,null,null,null,null,null,null,null,'t',null,null,null,null,null,null,null)";

                    NpgsqlCommand cmd = new NpgsqlCommand(qry, con);
                    cmd.ExecuteNonQuery();

                    message = "Subscriber Income Inserted Successfully";
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return message;
        }

        public List<reactiondatedto> GetreauctionSubscriberDetails(
        string connectionString,
        string branchschema,
        string globalschema,
        string groupcode,
        int ticketno, string branch_code)
        {

            List<reactiondatedto> lst = new List<reactiondatedto>();

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {

                    con.Open();

                    string qry = "select c.subscriber_name,e.reauction_date,e.from_ticketno,e.to_ticketno,f.transaction_no,d.tbl_mst_contact_id,c.ticketno from " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration a inner join " + AddDoubleQuotes(branchschema) + ".tbl_mst_chitgroup b on b.branch_id=a.tbl_mst_branch_configuration_id inner join " + AddDoubleQuotes(branchschema) + ".tbl_mst_subscriber c on c.chitgroup_id=b.tbl_mst_chitgroup_id and c.branch_id=a.tbl_mst_branch_configuration_id inner join " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact d on d.tbl_mst_contact_id=c.contact_id inner join " + AddDoubleQuotes(branchschema) + ".tbl_trans_reauction_entry e on e.chitgroup_id=c.chitgroup_id and e.to_ticketno=c.ticketno inner join " + AddDoubleQuotes(branchschema) + ".tbl_trans_subscriber_jv f on e.tbl_trans_subscriber_jv_id=f.tbl_trans_subscriber_jv_id where a.branch_code='" + branch_code + "' and b.groupcode='" + groupcode + "' and c.ticketno=" + ticketno + "";


                    Console.WriteLine(qry);

                    using (NpgsqlCommand cmd = new NpgsqlCommand(qry, con))
                    {
                        using (NpgsqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {

                                reactiondatedto obj = new reactiondatedto();
                                obj.subscriber_name = dr["subscriber_name"].ToString();
                                obj.reauction_date = dr["reauction_date"].ToString();
                                obj.transaction_no = dr["transaction_no"].ToString();
                                obj.from_ticketno = Convert.ToInt64(dr["from_ticketno"]);
                                obj.to_ticketno = Convert.ToInt64(dr["to_ticketno"]);
                                obj.tbl_mst_contact_id = Convert.ToInt64(dr["tbl_mst_contact_id"]);
                                obj.ticketno = Convert.ToInt64(dr["ticketno"]);


                                lst.Add(obj);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return lst;
        }




        public List<reactiondatedto> GetreauctionDetails(
        string connectionString,
        string branchschema,
        int from_ticketno, int to_ticketno)
        {

            List<reactiondatedto> lst = new List<reactiondatedto>();

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {

                    con.Open();

                    string qry = "select distinct b.bidjv_transaction_no,a.reauction_date,b.bidjv_transaction_date,d.transaction_date,d.receipt_number from " + AddDoubleQuotes(branchschema) + ".tbl_trans_reauction_entry a inner join " + AddDoubleQuotes(branchschema) + ".tbl_trans_bidjv b on b.chitgroup_id = a.chitgroup_id AND b.ticketno IN (a.from_ticketno, a.to_ticketno) inner join " + AddDoubleQuotes(branchschema) + ".tbl_trans_bidjv_details c on c.bidjv_id=b.tbl_trans_bidjv_id inner join " + AddDoubleQuotes(branchschema) + ".tbl_trans_gst_receipts d on d.receipt_number=b.bidjv_transaction_no where from_ticketno=" + from_ticketno + " and to_ticketno=" + to_ticketno + "";

                    Console.WriteLine(qry);

                    using (NpgsqlCommand cmd = new NpgsqlCommand(qry, con))
                    {
                        using (NpgsqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {

                                reactiondatedto obj = new reactiondatedto();
                                obj.bidjv_transaction_no = dr["bidjv_transaction_no"].ToString();
                                obj.receipt_number = dr["receipt_number"].ToString();
                                obj.reauction_date = dr["reauction_date"].ToString();
                                obj.bidjv_transaction_date = dr["bidjv_transaction_date"].ToString();
                                obj.transaction_date = dr["transaction_date"].ToString();
                                lst.Add(obj);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return lst;
        }

        public List<reactiondatedto> Getreauctiondivdenddetails(
        string connectionString,
        string branchschema, string transaction_no)
        {

            List<reactiondatedto> lst = new List<reactiondatedto>();

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {

                    con.Open();

                    string qry = "SELECT distinct dividend_transaction_number, dividend_date FROM " + AddDoubleQuotes(branchschema) + ".tbl_trans_subscriber_jv a INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_trans_subscriber_dividend b ON b.dividend_reference_number = a.transaction_no WHERE a.transaction_no = '" + transaction_no + "';";

                    Console.WriteLine(qry);

                    using (NpgsqlCommand cmd = new NpgsqlCommand(qry, con))
                    {
                        using (NpgsqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {

                                reactiondatedto obj = new reactiondatedto();
                                obj.dividend_transaction_number = dr["dividend_transaction_number"].ToString();
                                obj.dividend_date = dr["dividend_date"].ToString();
                                lst.Add(obj);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return lst;
        }


        public string UpdateReauctionTransactionDates(string branchschema, long chitgroupid, long toticketno, long ticketno, string reauctiondate, string oldtransactiondate, string createddate, string subscriberjvno, string dividendtransactionnos, string bidjvtransactionnos, string Conn)
        {
            string message = "";
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(Conn))
                {
                    con.Open();

                    StringBuilder sb = new StringBuilder();

                    string query1 = "UPDATE " + AddDoubleQuotes(branchschema) +
                                    ".tbl_trans_reauction_entry SET reauction_date='" + reauctiondate +
                                    "' WHERE chitgroup_id=" + chitgroupid +
                                    " AND to_ticketno=" + toticketno + ";";

                    Console.WriteLine(query1);
                    sb.Append(query1);

                    string query2 = "UPDATE " + AddDoubleQuotes(branchschema) +
                                    ".tbl_trans_subscriber_jv SET transaction_date='" + reauctiondate +
                                    "' WHERE transaction_no='" + subscriberjvno +
                                    "' AND transaction_date='" + oldtransactiondate + "';";

                    Console.WriteLine(query2);
                    sb.Append(query2);

                    string query3 = "UPDATE " + AddDoubleQuotes(branchschema) +
                                    ".tbl_trans_total_transactions SET transaction_date='" + reauctiondate +
                                    "', created_date='" + createddate +
                                    "' WHERE transaction_no='" + subscriberjvno +
                                    "' AND transaction_date='" + oldtransactiondate + "';";

                    Console.WriteLine(query3);
                    sb.Append(query3);

                    string query4 = "UPDATE " + AddDoubleQuotes(branchschema) +
                                    ".tbl_trans_subscriber_dividend SET dividend_date='" + reauctiondate +
                                    "' WHERE chitgroup_id=" + chitgroupid +
                                    " AND ticketno=" + ticketno +
                                    " AND dividend_date='" + oldtransactiondate +
                                    "' AND dividend_transaction_number IN(" + dividendtransactionnos + ");";

                    Console.WriteLine(query4);
                    sb.Append(query4);

                    string query5 = "UPDATE " + AddDoubleQuotes(branchschema) +
                                    ".tbl_trans_auction_pslist SET last_reauction_date='" + reauctiondate +
                                    "' WHERE chitgroup_id=" + chitgroupid +
                                    " AND ticketno=" + toticketno + ";";

                    Console.WriteLine(query5);
                    sb.Append(query5);

                    string query6 = "UPDATE " + AddDoubleQuotes(branchschema) +
                                    ".tbl_trans_gst_receipts SET transaction_date='" + reauctiondate +
                                    "' WHERE receipt_number IN(" + bidjvtransactionnos + ");";

                    Console.WriteLine(query6);
                    sb.Append(query6);

                    string query7 = "UPDATE " + AddDoubleQuotes(branchschema) +
                                    ".tbl_trans_bidjv SET bidjv_transaction_date='" + reauctiondate +
                                    "' WHERE chitgroup_id=" + chitgroupid +
                                    " AND ticketno IN(" + ticketno + "," + toticketno + ")" +
                                    " AND bidjv_transaction_no IN(" + bidjvtransactionnos + ");";

                    Console.WriteLine(query7);
                    sb.Append(query7);

                    string query8 = "UPDATE " + AddDoubleQuotes(branchschema) +
                                    ".tbl_trans_total_transactions SET transaction_date='" + reauctiondate +
                                    "', created_date='" + createddate +
                                    "' WHERE transaction_no IN(" + dividendtransactionnos + ")" +
                                    " AND transaction_date='" + oldtransactiondate + "';";

                    Console.WriteLine(query8);
                    sb.Append(query8);

                    string query9 = "UPDATE " + AddDoubleQuotes(branchschema) +
                                    ".tbl_trans_total_transactions SET transaction_date='" + reauctiondate +
                                    "', created_date='" + createddate +
                                    "' WHERE transaction_no IN(" + bidjvtransactionnos + ")" +
                                    " AND transaction_date='" + oldtransactiondate + "';";

                    Console.WriteLine(query9);
                    sb.Append(query9);

                    NpgsqlCommand cmd = new NpgsqlCommand(sb.ToString(), con);
                    cmd.CommandTimeout = 0;
                    cmd.ExecuteNonQuery();

                    message = "Success";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return message;
        }

        public List<guarantornamechangedto> GetnamechangeDetails(
       string connectionString,
       string branchschema,
       string globalschema,
       string groupcode,
       int ticketno, string branch_code)
        {

            List<guarantornamechangedto> lst = new List<guarantornamechangedto>();

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {

                    con.Open();

                    string qry = "select d.tbl_mst_contact_id,d.contact_name,d.contact_surname,d.contact_mailing_name,c.subscriber_name,e.guarantor_name,f.guarantor_name from " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration a inner join " + AddDoubleQuotes(branchschema) + ".tbl_mst_chitgroup b on b.branch_id=a.tbl_mst_branch_configuration_id inner join " + AddDoubleQuotes(branchschema) + ".tbl_mst_subscriber c on c.chitgroup_id=b.tbl_mst_chitgroup_id and c.branch_id=a.tbl_mst_branch_configuration_id inner join " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact d on d.tbl_mst_contact_id=c.contact_id inner join " + AddDoubleQuotes(branchschema) + ".tbl_mst_guarantor_mvo e on e.branch_id=a.tbl_mst_branch_configuration_id and e.chitgroup_id=b.tbl_mst_chitgroup_id and e.ticketno=c.ticketno and e.contact_id=d.tbl_mst_contact_id inner join " + AddDoubleQuotes(branchschema) + ".tbl_mst_guarantor f on f.contact_id=d.tbl_mst_contact_id where a.branch_code='" + branch_code + "' and b.groupcode='" + groupcode + "' and c.ticketno=" + ticketno + "";


                    Console.WriteLine(qry);

                    using (NpgsqlCommand cmd = new NpgsqlCommand(qry, con))
                    {
                        using (NpgsqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {

                                guarantornamechangedto obj = new guarantornamechangedto();
                                obj.subscriber_name = dr["subscriber_name"].ToString();
                                obj.contact_name = dr["contact_name"].ToString();
                                obj.contact_surname = dr["contact_surname"].ToString();
                                obj.contact_mailing_name = dr["contact_mailing_name"].ToString();
                                obj.guarantor_namemvo = dr["guarantor_name"].ToString();
                                obj.guarantor_name = dr["guarantor_name"].ToString();
                                obj.tbl_mst_contact_id = Convert.ToInt64(dr["tbl_mst_contact_id"]);

                                lst.Add(obj);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return lst;
        }

        public string UpdateSubscriberGuarantorName(string branchschema, string globalschema, long chitgroupid, long ticketno, long contactid, string contactname, string contactmailingname, string Conn, string contactsurname)
        {
            string message = "";
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(Conn))
                {
                    con.Open();

                    StringBuilder sb = new StringBuilder();

                    string query1 = "UPDATE " + AddDoubleQuotes(branchschema) + ".tbl_mst_guarantor_mvo " +
                                    "SET guarantor_name='" + contactmailingname + "' " +
                                    "WHERE chitgroup_id=" + chitgroupid +
                                    " AND ticketno=" + ticketno +
                                    " AND contact_id=" + contactid + ";";

                    Console.WriteLine(query1);
                    sb.Append(query1);

                    string query2 = "UPDATE " + AddDoubleQuotes(branchschema) + ".tbl_mst_guarantor " +
                                    "SET guarantor_name='" + contactmailingname + "' " +
                                    "WHERE chitgroup_id=" + chitgroupid +
                                    " AND ticketno=" + ticketno +
                                    " AND contact_id=" + contactid + ";";

                    Console.WriteLine(query2);
                    sb.Append(query2);

                    string query3 = "UPDATE " + AddDoubleQuotes(branchschema) + ".tbl_mst_subscriber " +
                                    "SET subscriber_name='" + contactmailingname + "' " +
                                    "WHERE chitgroup_id=" + chitgroupid +
                                    " AND ticketno=" + ticketno +
                                    " AND contact_id=" + contactid + ";";

                    Console.WriteLine(query3);
                    sb.Append(query3);

                    string query4 = "UPDATE " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact SET contact_name='" + contactname + "',contact_surname='" + contactsurname + "', contact_mailing_name='" + contactmailingname + "' WHERE tbl_mst_contact_id=" + contactid + ";";

                    Console.WriteLine(query4);
                    sb.Append(query4);

                    NpgsqlCommand cmd = new NpgsqlCommand(sb.ToString(), con);
                    cmd.CommandTimeout = 0;
                    cmd.ExecuteNonQuery();

                    message = "Success";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return message;
        }

        // public List<GuarantorMvoDTO> GetGuarantorMvoDetails(string globalschema, string schema, string Conn, long chitgroupid, int ticketNo)
        // {
        //     List<GuarantorMvoDTO> list = new List<GuarantorMvoDTO>();
        //     string Query = string.Empty;
        //     try
        //     {
        //         Query = "SELECT g.tbl_mst_guarantor_mvo_id, g.contact_id, c.contact_name, c.contact_surname, c.contact_mailing_name FROM "
        //               + AddDoubleQuotes(schema) + ".tbl_mst_guarantor_mvo g INNER JOIN "
        //               + AddDoubleQuotes(globalschema) + ".tbl_mst_contact c ON g.contact_id = c.tbl_mst_contact_id "
        //               + "WHERE g.chitgroup_id = " + chitgroupid + " AND g.ticketno = " + ticketNo + ";";

        //         Console.WriteLine(Query);

        //         using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
        //         {
        //             while (dr.Read())
        //             {
        //                 GuarantorMvoDTO dto = new GuarantorMvoDTO();
        //                 dto.TblMstGuarantorMvoId = Convert.ToInt64(dr["tbl_mst_guarantor_mvo_id"]);
        //                 dto.ContactId = Convert.ToInt64(dr["contact_id"]);
        //                 dto.GuarantorName = Convert.ToString(dr["contact_name"]);
        //                 dto.GuarantorSurname = Convert.ToString(dr["contact_surname"]);
        //                 dto.GuarantorMailingName = Convert.ToString(dr["contact_mailing_name"]);
        //                 list.Add(dto);
        //             }
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         throw ex;
        //     }
        //     return list;
        // }

        // public List<MvoAllotmentDTO> GetMvoAllotmentDetails(string schema, string Conn, List<long> guarantorMvoIds)
        // {
        //     List<MvoAllotmentDTO> list = new List<MvoAllotmentDTO>();
        //     string Query = string.Empty;
        //     try
        //     {
        //         if (guarantorMvoIds == null || guarantorMvoIds.Count == 0)
        //             return list;

        //         string idList = string.Join(",", guarantorMvoIds);

        //         Query = "SELECT tbl_trans_mvo_allotment_id, tbl_mst_guarantor_mvo_id FROM "
        //               + AddDoubleQuotes(schema) + ".tbl_trans_mvo_allotment "
        //               + "WHERE tbl_mst_guarantor_mvo_id IN (" + idList + ");";

        //         Console.WriteLine(Query);

        //         using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
        //         {
        //             while (dr.Read())
        //             {
        //                 MvoAllotmentDTO dto = new MvoAllotmentDTO();
        //                 dto.TblTransMvoAllotmentId = Convert.ToInt64(dr["tbl_trans_mvo_allotment_id"]);
        //                 dto.TblMstGuarantorMvoId = Convert.ToInt64(dr["tbl_mst_guarantor_mvo_id"]);
        //                 list.Add(dto);
        //             }
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         throw ex;
        //     }
        //     return list;
        // }

        // public bool DeleteMvoGuarantorRecords(string schema, string Conn, MvoGuarantorDeleteRequestDTO request)
        // {
        //     string Query = string.Empty;
        //     try
        //     {
        //         if (request.GuarantorMvoIds == null || request.GuarantorMvoIds.Count == 0)
        //             return false;

        //         string contactIdList = string.Join(",", request.ContactIds);
        //         string guarantorIdList = string.Join(",", request.GuarantorMvoIds);

        //         // 1. Delete from tbl_mst_guarantor_mvo
        //         Query = "DELETE FROM " + AddDoubleQuotes(schema) + ".tbl_mst_guarantor_mvo "
        //               + "WHERE chitgroup_id = " + request.Chitgroupid
        //               + " AND ticketno = " + request.Ticketno
        //               + " AND contact_id IN (" + contactIdList + ");";

        //         Console.WriteLine(Query);
        //         NPGSqlHelper.ExecuteNonQuery(Conn, System.Data.CommandType.Text, Query);

        //         // 2. Delete from tbl_trans_mvo_allotment
        //         Query = "DELETE FROM " + AddDoubleQuotes(schema) + ".tbl_trans_mvo_allotment "
        //               + "WHERE tbl_mst_guarantor_mvo_id IN (" + guarantorIdList + ");";

        //         Console.WriteLine(Query);
        //         NPGSqlHelper.ExecuteNonQuery(Conn, System.Data.CommandType.Text, Query);

        //         return true;
        //     }
        //     catch (Exception ex)
        //     {
        //         throw ex;
        //     }
        // }

        public List<GuarantorDTO> GetGuarantorDetails(string schema, string Conn, long chitgroupid, int ticketNo)
        {
            List<GuarantorDTO> list = new List<GuarantorDTO>();
            string Query = string.Empty;
            try
            {
                Query = "SELECT tbl_mst_guarantor_id, contact_id, guarantor_name FROM "
                      + AddDoubleQuotes(schema) + ".tbl_mst_guarantor "
                      + "WHERE chitgroup_id = " + chitgroupid + " AND ticketno = " + ticketNo + ";";

                Console.WriteLine(Query);

                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
                {
                    while (dr.Read())
                    {
                        GuarantorDTO dto = new GuarantorDTO();
                        dto.TblMstGuarantorId = Convert.ToInt64(dr["tbl_mst_guarantor_id"]);
                        dto.ContactId = Convert.ToInt64(dr["contact_id"]);
                        dto.GuarantorName = Convert.ToString(dr["guarantor_name"]);
                        list.Add(dto);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }

        public List<GuarantorIncomeDTO> GetGuarantorIncomeDetails(string schema, string Conn, List<long> guarantorIds)
        {
            List<GuarantorIncomeDTO> list = new List<GuarantorIncomeDTO>();
            string Query = string.Empty;
            try
            {
                if (guarantorIds == null || guarantorIds.Count == 0)
                    return list;

                string idList = string.Join(",", guarantorIds);

                Query = "SELECT tbl_mst_guarantor_income_id, guarantor_id FROM "
                      + AddDoubleQuotes(schema) + ".tbl_mst_guarantor_income "
                      + "WHERE guarantor_id IN (" + idList + ");";

                Console.WriteLine(Query);

                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
                {
                    while (dr.Read())
                    {
                        GuarantorIncomeDTO dto = new GuarantorIncomeDTO();
                        dto.TblMstGuarantorIncomeId = Convert.ToInt64(dr["tbl_mst_guarantor_income_id"]);
                        dto.GuarantorId = Convert.ToInt64(dr["guarantor_id"]);
                        list.Add(dto);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }

        public bool DeleteBpoGuarantorRecords(string schema, string Conn, BpoGuarantorDeleteRequestDTO request)
        {
            string Query = string.Empty;
            try
            {
                if (request.GuarantorIds == null || request.GuarantorIds.Count == 0)
                    return false;

                string contactIdList = string.Join(",", request.ContactIds);
                string guarantorIdList = string.Join(",", request.GuarantorIds);

                // 1. Delete from tbl_mst_guarantor
                Query = "DELETE FROM " + AddDoubleQuotes(schema) + ".tbl_mst_guarantor "
                      + "WHERE chitgroup_id = " + request.Chitgroupid
                      + " AND ticketno = " + request.Ticketno
                      + " AND contact_id IN (" + contactIdList + ");";

                Console.WriteLine(Query);
                NPGSqlHelper.ExecuteNonQuery(Conn, System.Data.CommandType.Text, Query);

                // 2. Delete from tbl_mst_guarantor_income
                Query = "DELETE FROM " + AddDoubleQuotes(schema) + ".tbl_mst_guarantor_income "
                      + "WHERE guarantor_id IN (" + guarantorIdList + ");";

                Console.WriteLine(Query);
                NPGSqlHelper.ExecuteNonQuery(Conn, System.Data.CommandType.Text, Query);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<GuarantorDTO1> GetGuarantormvoDetails(string branchSchema, string globalSchema, string branchCode, string groupCode, long ticketNo, string connectionString)
        {
            List<GuarantorDTO1> list = new List<GuarantorDTO1>();

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();

                    string qry = "SELECT e.contact_id,e.guarantor_name, e.tbl_mst_guarantor_mvo_id FROM " + AddDoubleQuotes(globalSchema) + ".tbl_mst_branch_configuration a INNER JOIN " + AddDoubleQuotes(branchSchema) + ".tbl_mst_chitgroup b ON b.branch_id = a.tbl_mst_branch_configuration_id INNER JOIN " + AddDoubleQuotes(branchSchema) + ".tbl_mst_subscriber c ON c.chitgroup_id = b.tbl_mst_chitgroup_id AND c.branch_id = a.tbl_mst_branch_configuration_id INNER JOIN " + AddDoubleQuotes(globalSchema) + ".tbl_mst_contact d ON d.tbl_mst_contact_id = c.contact_id INNER JOIN " + AddDoubleQuotes(branchSchema) + ".tbl_mst_guarantor_mvo e ON e.branch_id = a.tbl_mst_branch_configuration_id AND e.chitgroup_id = b.tbl_mst_chitgroup_id AND e.ticketno = c.ticketno AND e.contact_id = d.tbl_mst_contact_id WHERE a.branch_code = '" + branchCode + "' AND b.groupcode = '" + groupCode + "' AND c.ticketno = " + ticketNo + ";";




                    using (NpgsqlCommand cmd = new NpgsqlCommand(qry, con))
                    {


                        using (NpgsqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                list.Add(new GuarantorDTO1
                                {
                                    GuarantorName = dr["guarantor_name"].ToString(),
                                    GuarantorId = Convert.ToInt64(dr["tbl_mst_guarantor_mvo_id"]),
                                    contact_id = Convert.ToInt64(dr["contact_id"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }

        public List<GuarantorDTO1> GetMVOReferenceIds(string branchSchema, long guarantorIds, string connectionString)
        {
            List<GuarantorDTO1> list = new List<GuarantorDTO1>();

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();

                    //string qry = "SELECT b.v_reference_id FROM " + AddDoubleQuotes(branchSchema) + ".tbl_mst_guarantor_mvo WHERE tbl_mst_guarantor_mvo_id = ANY(" + string.Join(",", guarantorIds) + ") AND v_reference_id IS NOT NULL;";
                    string qry = "select b.v_reference_id from " + AddDoubleQuotes(branchSchema) + ".tbl_mst_guarantor_mvo a inner join " + AddDoubleQuotes(branchSchema) + ".tbl_trans_mvo_allotment b on b.tbl_mst_guarantor_mvo_id=a.tbl_mst_guarantor_mvo_id where a.tbl_mst_guarantor_mvo_id in(" + guarantorIds + ") AND v_reference_id IS NOT NULL;";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(qry, con))
                    {

                        using (NpgsqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                if (dr["v_reference_id"] != DBNull.Value)
                                {
                                    list.Add(new GuarantorDTO1
                                    {
                                        v_reference_id = Convert.ToString(dr["v_reference_id"])
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }


        public string DeleteGuarantorDetails(string branchSchema, long chitGroupId, long ticketNo, string contactIds, string guarantorMVOIds, string connectionString)
        {
            string message = "";

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();

                    using (NpgsqlTransaction trans = con.BeginTransaction())
                    {
                        try
                        {
                            StringBuilder sb = new StringBuilder();

                            sb.Append("DELETE FROM " + AddDoubleQuotes(branchSchema) + ".tbl_mst_guarantor_mvo WHERE chitgroup_id=" + chitGroupId + " AND ticketno=" + ticketNo + " AND contact_id IN(" + contactIds + ");");

                            using (NpgsqlCommand cmd = new NpgsqlCommand(sb.ToString(), con, trans))
                            {
                                cmd.ExecuteNonQuery();
                            }

                            // sb.Clear();

                            sb.Append("DELETE FROM " + AddDoubleQuotes(branchSchema) + ".tbl_trans_mvo_allotment WHERE tbl_mst_guarantor_mvo_id IN(" + guarantorMVOIds + ");");

                            using (NpgsqlCommand cmd = new NpgsqlCommand(sb.ToString(), con, trans))
                            {
                                cmd.ExecuteNonQuery();
                            }

                            trans.Commit();
                            message = "Deleted Successfully";
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return message;
        }



        public List<bpoGuarantorDTO> GetGuarantorbpoDetails(string branchSchema, string globalSchema, string branchCode, string groupCode, long ticketNo, string connectionString)
        {
            List<bpoGuarantorDTO> list = new List<bpoGuarantorDTO>();

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();

                    //string qry = "SELECT e.guarantor_name, e.tbl_mst_guarantor_mvo_id FROM " + AddDoubleQuotes(globalSchema) + ".tbl_mst_branch_configuration a INNER JOIN " + AddDoubleQuotes(branchSchema) + ".tbl_mst_chitgroup b ON b.branch_id = a.tbl_mst_branch_configuration_id INNER JOIN " + AddDoubleQuotes(branchSchema) + ".tbl_mst_subscriber c ON c.chitgroup_id = b.tbl_mst_chitgroup_id AND c.branch_id = a.tbl_mst_branch_configuration_id INNER JOIN " + AddDoubleQuotes(globalSchema) + ".tbl_mst_contact d ON d.tbl_mst_contact_id = c.contact_id INNER JOIN " + AddDoubleQuotes(branchSchema) + ".tbl_mst_guarantor_mvo e ON e.branch_id = a.tbl_mst_branch_configuration_id AND e.chitgroup_id = b.tbl_mst_chitgroup_id AND e.ticketno = c.ticketno AND e.contact_id = d.tbl_mst_contact_id WHERE a.branch_code = '" + branchCode + "' AND b.groupcode = '" + groupCode + "' AND c.ticketno = " + ticketNo + ";";
                    string qry = "SELECT e.guarantor_name, e.tbl_mst_guarantor_id, e.contact_id FROM " + AddDoubleQuotes(globalSchema) + ".tbl_mst_branch_configuration a INNER JOIN " + AddDoubleQuotes(branchSchema) + ".tbl_mst_chitgroup b ON b.branch_id = a.tbl_mst_branch_configuration_id INNER JOIN " + AddDoubleQuotes(branchSchema) + ".tbl_mst_subscriber c ON c.chitgroup_id = b.tbl_mst_chitgroup_id AND c.branch_id = a.tbl_mst_branch_configuration_id INNER JOIN " + AddDoubleQuotes(branchSchema) + ".tbl_mst_guarantor e ON e.branch_id = a.tbl_mst_branch_configuration_id AND e.chitgroup_id = b.tbl_mst_chitgroup_id AND e.ticketno = c.ticketno INNER JOIN " + AddDoubleQuotes(globalSchema) + ".tbl_mst_contact d ON d.tbl_mst_contact_id = e.contact_id INNER JOIN " + AddDoubleQuotes(branchSchema) + ".tbl_mst_guarantor_income f ON f.guarantor_id = e.tbl_mst_guarantor_id WHERE a.branch_code = '" + branchCode + "' AND b.groupcode = '" + groupCode + "' AND c.ticketno = " + ticketNo + ";";




                    using (NpgsqlCommand cmd = new NpgsqlCommand(qry, con))
                    {


                        using (NpgsqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                list.Add(new bpoGuarantorDTO
                                {
                                    GuarantorName = dr["guarantor_name"].ToString(),
                                    GuarantorId = Convert.ToInt64(dr["tbl_mst_guarantor_id"]),
                                    contact_id = Convert.ToInt64(dr["contact_id"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }


        public string DeletepdoGuarantorDetails(string branchSchema, long chitGroupId, long ticketNo, string contactIds, string guarantorMVOIds, string connectionString)
        {
            string message = "";

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();

                    using (NpgsqlTransaction trans = con.BeginTransaction())
                    {
                        try
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append("DELETE FROM " + AddDoubleQuotes(branchSchema) + ".tbl_mst_guarantor_income WHERE guarantor_id IN(" + guarantorMVOIds + ");");

                            using (NpgsqlCommand cmd = new NpgsqlCommand(sb.ToString(), con, trans))
                            {
                                cmd.ExecuteNonQuery();
                            }

                            sb.Clear();

                            sb.Append("delete from " + AddDoubleQuotes(branchSchema) + ".tbl_mst_guarantor WHERE chitgroup_id=" + chitGroupId + " AND ticketno=" + ticketNo + " AND contact_id IN(" + contactIds + ");");


                            using (NpgsqlCommand cmd = new NpgsqlCommand(sb.ToString(), con, trans))
                            {
                                cmd.ExecuteNonQuery();
                            }

                            trans.Commit();
                            message = "Deleted Successfully";
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return message;
        }


        public List<finalsettlement> Getfinalsetlement(string branchSchema, string globalSchema, string groupCode, long ticketNo, string connectionString)
        {
            List<finalsettlement> list = new List<finalsettlement>();

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();


                    string qry = "SELECT e.surety_name FROM " + AddDoubleQuotes(branchSchema) + ".tbl_mst_branch_outward a LEFT JOIN " + AddDoubleQuotes(branchSchema) + ".tbl_mst_bpo_inward b ON b.branch_outward_id = a.tbl_mst_branch_outward_id LEFT JOIN " + AddDoubleQuotes(branchSchema) + ".tbl_mst_bpo_outward c ON c.bpo_inward_id = b.tbl_mst_bpo_inward_id INNER JOIN " + AddDoubleQuotes(globalSchema) + ".tbl_trans_svo_surety_details d ON d.groupcode = a.groupcode AND d.ticketno = a.ticketno INNER JOIN " + AddDoubleQuotes(branchSchema) + ".tbl_trans_prized_subscriber_surety e ON e.chitgroup_id = d.chitgroup_id AND e.ticketno = d.ticketno WHERE a.groupcode = '" + groupCode + "' AND a.ticketno = " + ticketNo + " AND ((a.branch_outward_status = 'R' AND c.bpo_outward_status = 'R') OR (a.branch_outward_status = 'N'));";




                    using (NpgsqlCommand cmd = new NpgsqlCommand(qry, con))
                    {


                        using (NpgsqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                list.Add(new finalsettlement
                                {
                                    surety_name = dr["surety_name"].ToString(),
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }

        public List<finalsettlement> Getfinalsetlementdetails(string surety_name, string globalSchema, string groupCode, long ticketNo, string connectionString)
        {
            List<finalsettlement> list = new List<finalsettlement>();

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();

                    finalsettlement obj = new finalsettlement();
                    obj.surety_name = surety_name;

                    bool hasGuarantor = false;

                    string[] suretyList = surety_name.Split(',');

                    foreach (string item in suretyList)
                    {
                        if (item.Trim().Equals("GUARANTOR", StringComparison.OrdinalIgnoreCase))
                        {
                            hasGuarantor = true;
                            break;
                        }
                    }

                    //================ FIRST MEMO ===================
                    if (hasGuarantor)
                    {
                        string firstMemoQry = "SELECT authorized_by,approved_by,authorized_date,approved_date,file_name,approved_file_name FROM " + AddDoubleQuotes(globalSchema) + ".tbl_mst_first_memo WHERE groupcode='" + groupCode + "' AND ticketno=" + ticketNo;

                        using (NpgsqlCommand cmd = new NpgsqlCommand(firstMemoQry, con))
                        {
                            using (NpgsqlDataReader dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    obj.FirstMemo.Add(new FirstMemoDTO
                                    {
                                        authorized_by = dr["authorized_by"] == DBNull.Value ? (long?)null : Convert.ToInt64(dr["authorized_by"]),
                                        approved_by = dr["approved_by"] == DBNull.Value ? (long?)null : Convert.ToInt64(dr["approved_by"]),
                                        authorized_date = dr["authorized_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["authorized_date"]),
                                        approved_date = dr["approved_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["approved_date"]),
                                        file_name = dr["file_name"].ToString(),
                                        approved_file_name = dr["approved_file_name"].ToString()
                                    });
                                }
                            }
                        }
                    }

                    //================ FINAL MEMO ===================
                    string finalMemoQry = "SELECT authorized_by,approved_by,authorized_date,approved_date,file_name,approved_file_name FROM " + AddDoubleQuotes(globalSchema) + ".tbl_mst_final_memo WHERE groupcode='" + groupCode + "' AND ticketno=" + ticketNo;

                    using (NpgsqlCommand cmd = new NpgsqlCommand(finalMemoQry, con))
                    {
                        using (NpgsqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                obj.FinalMemo.Add(new FinalMemoDTO
                                {
                                    authorized_by = dr["authorized_by"] == DBNull.Value ? (long?)null : Convert.ToInt64(dr["authorized_by"]),
                                    approved_by = dr["approved_by"] == DBNull.Value ? (long?)null : Convert.ToInt64(dr["approved_by"]),
                                    authorized_date = dr["authorized_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["authorized_date"]),
                                    approved_date = dr["approved_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["approved_date"]),
                                    file_name = dr["file_name"].ToString(),
                                    approved_file_name = dr["approved_file_name"].ToString()
                                });
                            }
                        }
                    }

                    list.Add(obj);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }

        public string DeleteFinalMemoDetails(string branchSchema, string globalSchema, long chitGroupId, long ticketNo, string groupCode, string Conn)
        {
            string message = "";

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(Conn))
                {
                    con.Open();

                    StringBuilder sb = new StringBuilder();

                    string query1 = "DELETE FROM " + AddDoubleQuotes(globalSchema) + ".tbl_mst_final_memo WHERE groupcode='" + groupCode + "' AND ticketno=" + ticketNo + ";";

                    Console.WriteLine(query1);
                    sb.Append(query1);

                    string query2 = "DELETE FROM " + AddDoubleQuotes(branchSchema) + ".tbl_trans_prized_subscriber_surety WHERE chitgroup_id=" + chitGroupId + " AND ticketno=" + ticketNo + ";";

                    Console.WriteLine(query2);
                    sb.Append(query2);

                    string query3 = "UPDATE " + AddDoubleQuotes(globalSchema) + ".tbl_trans_svo_surety_details SET handover_date=NULL, final_memo_written_by=NULL, document_approved_date=NULL, remarks=NULL, documentapprovalstatus=NULL, bpo_transfer_date=NULL, first_memo_status=NULL, final_memo_status=NULL WHERE groupcode='" + groupCode + "' AND ticketno=" + ticketNo + ";";

                    Console.WriteLine(query3);
                    sb.Append(query3);

                    NpgsqlCommand cmd = new NpgsqlCommand(sb.ToString(), con);
                    cmd.CommandTimeout = 0;
                    cmd.ExecuteNonQuery();

                    message = "Success";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return message;
        }

        public List<chitcanceldto> Getchitpledgecancel(string branchSchema, string globalSchema, string groupCode, long ticketNo, string connectionString)
        {
            List<chitcanceldto> list = new List<chitcanceldto>();

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();


                    string qry = "select distinct a.NPS_GROUPCODE from " + AddDoubleQuotes(globalSchema) + ".tbl_trans_chit_pledge a left join " + AddDoubleQuotes(branchSchema) + ".tbl_trans_chit_pledge b on b.ps_ticketno=a.ps_ticketno and b.ps_groupcode=a.ps_groupcode where a.ps_groupcode='" + groupCode + "' and a.ps_ticketno=" + ticketNo + " ";




                    using (NpgsqlCommand cmd = new NpgsqlCommand(qry, con))
                    {


                        using (NpgsqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                list.Add(new chitcanceldto
                                {
                                    NPS_GROUPCODE = dr["NPS_GROUPCODE"].ToString(),
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }

        public string DeleteChitPledgeAndSecurityLienDetails(string branchSchema, string globalSchema, string psGroupCode, long psTicketNo, string npsGroupCode, string Conn)
        {
            string message = "";

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(Conn))
                {
                    con.Open();

                    StringBuilder sb = new StringBuilder();

                    // Delete from GLOBAL Chit Pledge
                    string query1 = "DELETE FROM " + AddDoubleQuotes(globalSchema) + ".tbl_trans_chit_pledge WHERE ps_groupcode='" + psGroupCode + "' AND ps_ticketno=" + psTicketNo + " AND nps_groupcode='" + npsGroupCode + "';";

                    Console.WriteLine(query1);
                    sb.Append(query1);

                    // Delete from Branch Chit Pledge
                    string query2 = "DELETE FROM " + AddDoubleQuotes(branchSchema) + ".tbl_trans_chit_pledge WHERE ps_groupcode='" + psGroupCode + "' AND ps_ticketno=" + psTicketNo + " AND nps_groupcode='" + npsGroupCode + "';";

                    Console.WriteLine(query2);
                    sb.Append(query2);

                    // Delete from Branch Security Lien
                    string query3 = "DELETE FROM " + AddDoubleQuotes(branchSchema) + ".tbl_trans_security_lien WHERE ps_groupcode='" + psGroupCode + "' AND ps_ticketno=" + psTicketNo + ";";

                    Console.WriteLine(query3);
                    sb.Append(query3);

                    // Delete from GLOBAL Security Lien
                    string query4 = "DELETE FROM " + AddDoubleQuotes(globalSchema) + ".tbl_trans_security_lien WHERE ps_groupcode='" + psGroupCode + "' AND ps_ticketno=" + psTicketNo + ";";

                    Console.WriteLine(query4);
                    sb.Append(query4);

                    NpgsqlCommand cmd = new NpgsqlCommand(sb.ToString(), con);
                    cmd.CommandTimeout = 0;
                    cmd.ExecuteNonQuery();

                    message = "Success";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return message;
        }

        public List<suretynotshwingdto> Getsuretyname(string branchSchema, string globalSchema, string branchCode, string groupCode, long ticketNo, string connectionString)
        {
            List<suretynotshwingdto> list = new List<suretynotshwingdto>();

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();

                    string qry = "SELECT c.subscriber_name FROM " + AddDoubleQuotes(globalSchema) + ".tbl_mst_branch_configuration a INNER JOIN " + AddDoubleQuotes(branchSchema) + ".tbl_mst_chitgroup b ON b.branch_id=a.tbl_mst_branch_configuration_id INNER JOIN " + AddDoubleQuotes(branchSchema) + ".tbl_mst_subscriber c ON c.branch_id=a.tbl_mst_branch_configuration_id AND c.chitgroup_id=b.tbl_mst_chitgroup_id INNER JOIN " + AddDoubleQuotes(branchSchema) + ".tbl_trans_reauction_entry d ON d.chitgroup_id=b.tbl_mst_chitgroup_id AND d.branch_id=a.tbl_mst_branch_configuration_id AND d.from_ticketno=c.ticketno WHERE a.branch_code = '" + branchCode + "' AND b.groupcode = '" + groupCode + "' AND c.ticketno = " + ticketNo + ";";




                    using (NpgsqlCommand cmd = new NpgsqlCommand(qry, con))
                    {


                        using (NpgsqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                list.Add(new suretynotshwingdto
                                {
                                    subscriber_name = dr["subscriber_name"].ToString(),
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }
        public List<suretynotshwingdto> Getsuretynamedetails(string globalSchema, string groupCode, long ticketNo, string connectionString)
        {
            List<suretynotshwingdto> list = new List<suretynotshwingdto>();

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();

                    string qry = "SELECT DISTINCT a.tbl_trans_mvo_surety_id FROM " + AddDoubleQuotes(globalSchema) + ".tbl_trans_mvo_surety a INNER JOIN " + AddDoubleQuotes(globalSchema) + ".tbl_trans_mvo_surety_details b ON b.tbl_trans_mvo_surety_id=a.tbl_trans_mvo_surety_id WHERE a.groupcode='" + groupCode + "' AND a.ticketno=" + ticketNo + ";";




                    using (NpgsqlCommand cmd = new NpgsqlCommand(qry, con))
                    {


                        using (NpgsqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                list.Add(new suretynotshwingdto
                                {
                                    tbl_trans_mvo_surety_id = Convert.ToInt64(dr["tbl_trans_mvo_surety_id"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }


        public string DeleteMvoSvoSuretyDetails(string branchSchema, string globalSchema, string groupCode, long ticketNo, long mvoSuretyId, long chitGroupId, string Conn)
        {
            string message = "";

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(Conn))
                {
                    con.Open();

                    StringBuilder sb = new StringBuilder();

                    // Delete from GLOBAL MVO Surety


                    // Delete from GLOBAL MVO Surety Details
                    string query1 = "DELETE FROM " + AddDoubleQuotes(globalSchema) + ".tbl_trans_mvo_surety_details WHERE tbl_trans_mvo_surety_id=" + mvoSuretyId + ";";
                    Console.WriteLine(query1);
                    sb.Append(query1);

                    // Delete from GLOBAL SVO Surety Details
                    string query2 = "DELETE FROM " + AddDoubleQuotes(globalSchema) + ".tbl_trans_svo_surety_details WHERE groupcode='" + groupCode + "' AND ticketno=" + ticketNo + ";";
                    Console.WriteLine(query2);
                    sb.Append(query2);

                    // Delete from GLOBAL First Memo
                    string query3 = "DELETE FROM " + AddDoubleQuotes(globalSchema) + ".tbl_mst_first_memo WHERE groupcode='" + groupCode + "' AND ticketno=" + ticketNo + ";";
                    Console.WriteLine(query3);
                    sb.Append(query3);

                    // Delete from Branch Guarantor MVO
                    string query4 = "DELETE FROM " + AddDoubleQuotes(branchSchema) + ".tbl_mst_guarantor_mvo WHERE chitgroup_id=" + chitGroupId + " AND ticketno=" + ticketNo + ";";
                    Console.WriteLine(query4);
                    sb.Append(query4);

                    // Delete from Branch KGMS Outward
                    string query5 = "DELETE FROM " + AddDoubleQuotes(branchSchema) + ".tbl_mst_kgms_outward WHERE groupcode='" + groupCode + "' AND ticketno=" + ticketNo + ";";
                    Console.WriteLine(query5);
                    sb.Append(query5);

                    // Delete from Branch MVO Inward
                    string query6 = "DELETE FROM " + AddDoubleQuotes(branchSchema) + ".tbl_mst_mvo_inward WHERE groupcode='" + groupCode + "' AND ticketno=" + ticketNo + ";";
                    Console.WriteLine(query6);
                    sb.Append(query6);

                    // Delete from Branch SVO Inward
                    string query7 = "DELETE FROM " + AddDoubleQuotes(branchSchema) + ".tbl_mst_svo_inward WHERE groupcode='" + groupCode + "' AND ticketno=" + ticketNo + ";";
                    Console.WriteLine(query7);
                    sb.Append(query7);

                    string query8 = "DELETE FROM " + AddDoubleQuotes(globalSchema) + ".tbl_trans_mvo_surety WHERE groupcode='" + groupCode + "' AND ticketno=" + ticketNo + ";";
                    Console.WriteLine(query8);
                    sb.Append(query8);



                    NpgsqlCommand cmd = new NpgsqlCommand(sb.ToString(), con);
                    cmd.CommandTimeout = 0;
                    cmd.ExecuteNonQuery();

                    message = "Success";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return message;
        }

        public List<RECEIVEDDOCUMENTDTO> GetRECEIVEDDOCUMENT(string branchSchema, string globalSchema, string connectionString, string groupCode, Int64 ticketNo)
        {
            List<RECEIVEDDOCUMENTDTO> list = new List<RECEIVEDDOCUMENTDTO>();

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();

                    string qry = "SELECT e.subscriber_name,d.filled_surety_file,d.svofilledverfieddocument FROM " + AddDoubleQuotes(branchSchema) + ".tbl_mst_kgms_outward a LEFT JOIN " + AddDoubleQuotes(branchSchema) + ".tbl_mst_mvo_inward b ON b.mvo_outward_id = a.tbl_mst_kgms_outward_id LEFT JOIN " + AddDoubleQuotes(branchSchema) + ".tbl_mst_Svo_inward c ON c.svo_outward_id = a.tbl_mst_kgms_outward_id INNER JOIN " + AddDoubleQuotes(globalSchema) + ".tbl_trans_svo_surety_details d ON d.groupcode = a.groupcode AND d.ticketno = a.ticketno INNER JOIN " + AddDoubleQuotes(branchSchema) + ".tbl_mst_subscriber e ON e.chitgroup_id = d.chitgroup_id AND e.ticketno = d.ticketno WHERE a.groupcode = '" + groupCode + "' AND a.ticketno = " + ticketNo + ";";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(qry, con))
                    {

                        using (NpgsqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {

                                list.Add(new RECEIVEDDOCUMENTDTO
                                {
                                    subscriber_name = Convert.ToString(dr["subscriber_name"]),
                                    filled_surety_file = Convert.ToString(dr["filled_surety_file"]),
                                    svofilledverfieddocument = Convert.ToString(dr["svofilledverfieddocument"])
                                });

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }



        public string DeleteKgmsMvoSvoDetails(string branchSchema, string globalSchema, string groupCode, long ticketNo, bool fileDownloadStatus, string Conn)
        {
            string message = "";

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(Conn))
                {
                    con.Open();

                    StringBuilder sb = new StringBuilder();

                    if (fileDownloadStatus)
                    {
                        string query1 = "UPDATE " + AddDoubleQuotes(globalSchema) + ".tbl_trans_svo_surety_details SET subscriberreceiveddate=NULL, subscribersubmitteddate=NULL, svo_kgms_receivedstatus=NULL, filedownloadstatus=NULL WHERE groupcode='" + groupCode + "' AND ticketno=" + ticketNo + ";";
                        Console.WriteLine(query1);
                        sb.Append(query1);
                    }
                    else
                    {
                        string query2 = "UPDATE " + AddDoubleQuotes(globalSchema) + ".tbl_trans_svo_surety_details SET subscriberreceiveddate=NULL, subscribersubmitteddate=NULL, svo_kgms_receivedstatus=NULL WHERE groupcode='" + groupCode + "' AND ticketno=" + ticketNo + ";";
                        Console.WriteLine(query2);
                        sb.Append(query2);
                    }

                    string query3 = "DELETE FROM " + AddDoubleQuotes(branchSchema) + ".tbl_mst_kgms_outward WHERE groupcode='" + groupCode + "' AND ticketno=" + ticketNo + ";";
                    Console.WriteLine(query3);
                    sb.Append(query3);

                    string query4 = "DELETE FROM " + AddDoubleQuotes(branchSchema) + ".tbl_mst_mvo_inward WHERE groupcode='" + groupCode + "' AND ticketno=" + ticketNo + ";";
                    Console.WriteLine(query4);
                    sb.Append(query4);

                    string query5 = "DELETE FROM " + AddDoubleQuotes(branchSchema) + ".tbl_mst_svo_inward WHERE groupcode='" + groupCode + "' AND ticketno=" + ticketNo + ";";
                    Console.WriteLine(query5);
                    sb.Append(query5);

                    NpgsqlCommand cmd = new NpgsqlCommand(sb.ToString(), con);
                    cmd.CommandTimeout = 0;
                    cmd.ExecuteNonQuery();

                    message = "Success";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return message;
        }

         public List<centralofficechitsdto> Getcentralofficechitsdetails(string connectionString, string branchschema, string globalschema, string groupcode, Int64 ticketno, string branch_code)
        {

            List<centralofficechitsdto> lst = new List<centralofficechitsdto>();

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {

                    con.Open();

                    string qry = "SELECT d.contact_mailing_name,c.contact_id, c.subscriber_name, e.bank_branch_name, f.chit_receipt_number, f.chit_receipt_date, g.bank_account_number FROM " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration a INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_mst_chitgroup b ON b.branch_id = a.tbl_mst_branch_configuration_id INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_mst_subscriber c ON c.branch_id = a.tbl_mst_branch_configuration_id AND c.chitgroup_id = b.tbl_mst_chitgroup_id INNER JOIN " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact d ON d.tbl_mst_contact_id = c.contact_id INNER JOIN " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact_bank e ON e.contact_id = c.contact_id INNER JOIN " + AddDoubleQuotes(branchschema) + ".tbl_trans_chit_advance f ON f.branch_id = a.tbl_mst_branch_configuration_id AND f.chitgroup_id = b.tbl_mst_chitgroup_id AND f.ticketno = c.ticketno AND f.contact_id = d.tbl_mst_contact_id LEFT JOIN " + AddDoubleQuotes(branchschema) + ".tbl_trans_chit_advance_interest_payment_bank g ON g.branch_id = a.tbl_mst_branch_configuration_id AND g.chitgroup_id = b.tbl_mst_chitgroup_id AND g.ticketno = c.ticketno AND g.contact_id = d.tbl_mst_contact_id LEFT JOIN " + AddDoubleQuotes(branchschema) + ".tbl_trans_chit_advance_interest_adjustments h ON h.branch_id = a.tbl_mst_branch_configuration_id AND h.chitgroup_id = b.tbl_mst_chitgroup_id AND h.ticketno = c.ticketno AND h.contact_id = d.tbl_mst_contact_id WHERE a.branch_code='" + branch_code + "' and b.groupcode='" + groupcode + "' and c.ticketno=" + ticketno + " AND e.isprimary = 'true' AND e.status = 'true';";


                    Console.WriteLine(qry);

                    using (NpgsqlCommand cmd = new NpgsqlCommand(qry, con))
                    {
                        using (NpgsqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {

                                centralofficechitsdto obj = new centralofficechitsdto();

                                obj.subscriber_name = dr["subscriber_name"].ToString();
                                obj.bank_branch_name = dr["bank_branch_name"].ToString();
                                obj.chit_receipt_number = dr["chit_receipt_number"].ToString();
                                obj.chit_receipt_date = Convert.ToDateTime(dr["chit_receipt_date"]);
                                obj.bank_account_number = dr["bank_account_number"].ToString();
                                obj.contact_mailing_name = dr["contact_mailing_name"].ToString();
                                obj.contact_id = Convert.ToInt64(dr["contact_id"]);

                                lst.Add(obj);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return lst;
        }



        public List<BranchNamesDTO> GetcoBranchNames(string globalschema, string Conn)
        {
            List<BranchNamesDTO> productionlist = new List<BranchNamesDTO>();
            string Query = string.Empty;


            try
            {
                Query = "SELECT DISTINCT branch_name, tbl_mst_branch_configuration_id, branch_code, branch_type, company_configuration_id FROM " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration WHERE UNIQUE_branch_name LIKE '%CA-CO%' AND branch_type = 'CAO';";


                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, CommandType.Text, Query))
                {


                    while (dr.Read())
                    {
                        BranchNamesDTO productionDTO = new BranchNamesDTO
                        {
                            Branch_name = Convert.ToString(dr["branch_name"]),
                            branch_code = Convert.ToString(dr["branch_code"]),
                            company_configuration_id = Convert.ToInt32(dr["company_configuration_id"]),
                            tbl_mst_branch_configuration_id = Convert.ToInt32(dr["tbl_mst_branch_configuration_id"])
                        };

                        productionlist.Add(productionDTO);
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return productionlist;
        }


        public string UpdateAdvanceInterestPaymentBank(string branchSchema, UpdateAdvanceInterestBankDTO obj, string Conn)
        {
            string message = "";

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(Conn))
                {
                    con.Open();

                    StringBuilder sb = new StringBuilder();

                    string query1 = "UPDATE " + AddDoubleQuotes(branchSchema) + ".tbl_trans_chit_advance_interest_payment_bank SET isprimary='false', status='false' WHERE contact_id=" + obj.ContactId + " AND chitgroup_id=" + obj.ChitGroupId + " AND ticketno=" + obj.TicketNo + ";";
                    Console.WriteLine(query1);
                    sb.Append(query1);

                    string query2 = "INSERT INTO " + AddDoubleQuotes(branchSchema) + ".tbl_trans_chit_advance_interest_adjustments (adjustment_date, branch_id, chitgroup_id, ticketno, contact_id, adjusted_branch_id, adjusted_chitgroup_id, adjusted_ticketno, adjusted_contact_id, user_id, status) VALUES ('" + obj.AdjustmentDate.ToString("yyyy-MM-dd") + "', " + obj.BranchId + ", " + obj.ChitGroupId + ", " + obj.TicketNo + ", " + obj.ContactId + ", " + obj.AdjustedBranchId + ", " + obj.AdjustedChitGroupId + ", " + obj.AdjustedTicketNo + ", " + obj.ContactId + ",1, 'true');";
                    Console.WriteLine(query2);
                    sb.Append(query2);

                    NpgsqlCommand cmd = new NpgsqlCommand(sb.ToString(), con);
                    cmd.CommandTimeout = 0;
                    cmd.ExecuteNonQuery();

                    message = "Success";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return message;
        }


    }
}