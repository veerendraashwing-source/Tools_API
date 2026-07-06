
////using Easychit_Infrastructure.Easy_Chit_Tools;
////using HelperManager;
////using Npgsql;
////using System;
////using System.Collections.Generic;
////using System.Text.Json;
////using System.Text;
////using Easychit_Repository.Interfaces.Easy_Chit_Tools;
////using Easychit_Infrastructure.Settings.Users;
////using Easychit_Repository.Interfaces.Security;
////using Easychit_Infrastructure.ChangeDetails;

////namespace Easychit_Repository.DataAccess.Easy_Chit_Tools
////{
////    public class EasyChitToolsDAL : CommonDAL, IEasyChitTools
////    {

////        NpgsqlConnection con = new NpgsqlConnection(NPGSqlHelper.SQLConnString);

////        #region Branches


////        public List<BranchNamesDTO> GetBranchNames(string globalschema, string Conn)
////        {
////            List<BranchNamesDTO> productionlist = new List<BranchNamesDTO>();
////            string Query = string.Empty;

////            try
////            {

////                // Query = "select distinct branch_name,branch_code from " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration where branch_name like '%CAO%' and branch_type ='CAO';";

////                Query = "select distinct branch_name, branch_code " +
////                        "from " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration " +
////                        "where branch_name like '%CAO%' and branch_type ='CAO';";

////                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
////                {
////                    while (dr.Read())
////                    {
////                        BranchNamesDTO productionDTO = new BranchNamesDTO
////                        {
////                            Branch_name = Convert.ToString(dr["branch_name"]),
////                            branch_code = Convert.ToString(dr["branch_code"])
////                        };
////                        productionlist.Add(productionDTO);
////                    }
////                }
////            }
////            catch (Exception ex)
////            {
////                throw ex;
////            }
////            return productionlist;
////        }

////        public List<GroupCodeDTO> GetGroupcodes(string branchschema, string Conn)
////        {
////            List<GroupCodeDTO> productionlist = new List<GroupCodeDTO>();
////            string Query = string.Empty;

////            try
////            {
////                // Query = "select distinct branch_code from " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration where branch_name like '%CAO%' and branch_type ='CAO';";

////                Query = "select distinct groupcode, chitgroup_id " +
////                        "from " + AddDoubleQuotes(branchschema) + ".tbl_mst_subscriber x, " +
////                        AddDoubleQuotes(branchschema) + ".tbl_mst_chitgroup y " +
////                        "where chitgroup_status in('Registered','Commenced') " +
////                        "and x.chitgroup_id = y.tbl_mst_chitgroup_id;";

////                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
////                {
////                    while (dr.Read())
////                    {
////                        GroupCodeDTO productionDTO = new GroupCodeDTO
////                        {
////                            Group_Code = dr["groupcode"] != DBNull.Value ? Convert.ToString(dr["groupcode"]) : string.Empty,
////                            chitgroup_id = dr["chitgroup_id"] != DBNull.Value ? Convert.ToString(dr["chitgroup_id"]) : string.Empty
////                        };
////                        productionlist.Add(productionDTO);
////                    }
////                }
////            }
////            catch (Exception ex)
////            {
////                throw ex;
////            }

////            return productionlist;
////        }


////        public List<TicketDTO> GetTickets(string schema, string Conn, string groupcode)
////        {
////            List<TicketDTO> productionlist = new List<TicketDTO>();
////            string Query = string.Empty;

////            try
////            {
////                Query = "select ticketno " +
////                        "from " + AddDoubleQuotes(schema) + ".tbl_mst_subscriber x, " +
////                        AddDoubleQuotes(schema) + ".tbl_mst_chitgroup y " +
////                        "where chitgroup_status in('Registered','Commenced') " +
////                        "and x.chitgroup_id = y.tbl_mst_chitgroup_id " +
////                        "and groupcode='" + groupcode + "';";

////                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
////                {
////                    while (dr.Read())
////                    {
////                        TicketDTO productionDTO = new TicketDTO
////                        {
////                            ticketno = Convert.ToInt32(dr["ticketno"])
////                        };
////                        productionlist.Add(productionDTO);
////                    }
////                }
////            }
////            catch (Exception ex)
////            {
////                throw ex;
////            }

////            return productionlist;
////        }

////        #endregion

////        #region


////        public NameUpdateDTO GetOldNameByTicketNo(string globalschema, string schema, string Conn, int ticketNo, int chitgroupid)
////        {
////            NameUpdateDTO dto = new NameUpdateDTO();
////            string Query = string.Empty;

////            try
////            {
////                Query = "select contact_name, contact_surname, contact_mailing_name, t1.contact_id, c.business_entity_contactno, t1.address1,t1.area,t1.city_name,t1.pincode " +
////                        "from " + AddDoubleQuotes(schema) + ".tbl_mst_subscriber x, " +
////                        AddDoubleQuotes(schema) + ".tbl_mst_chitgroup y, " +
////                        AddDoubleQuotes(globalschema) + ".tbl_mst_contact c, " +
////                        AddDoubleQuotes(globalschema) + ".tbl_mst_contact_address_details t1 " +
////                        "where chitgroup_status in('Registered','Commenced') and t1.isprimary='true' and t1.status='true' " +
////                        "and x.chitgroup_id = y.tbl_mst_chitgroup_id " +
////                        "and x.contact_id = c.tbl_mst_contact_id " +
////                        "and t1.contact_id = c.tbl_mst_contact_id " +
////                        "and chitgroup_id = " + chitgroupid + " " +
////                        "and ticketno = " + ticketNo + " " +
////                        "order by groupcode, ticketno;";

////                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
////                {
////                    if (dr.Read())
////                    {
////                        dto.OldName = Convert.ToString(dr["contact_name"]);
////                        dto.OldSurname = Convert.ToString(dr["contact_surname"]);
////                        dto.OldMailingName = Convert.ToString(dr["contact_mailing_name"]);
////                        dto.ContactId = Convert.ToInt32(dr["contact_id"]);
////                        dto.MobileNo = Convert.ToString(dr["business_entity_contactno"]);
////                        dto.Address = Convert.ToString(dr["address1"]);
////                        dto.Area = Convert.ToString(dr["area"]);
////                        dto.City = Convert.ToString(dr["city_name"]);
////                        dto.Pincode = Convert.ToInt32(dr["pincode"]);


////                    }
////                }
////            }
////            catch (Exception ex)
////            {
////                throw ex;
////            }

////            return dto;
////        }


////        public void UpdateNameByTicketNo(string globalschema, string Conn, List<Interfaces.Easy_Chit_Tools.NameChangeDto> dtoNames, string branchschema)
////        {
////            string Query = string.Empty;
////            try
////            {
////                foreach (var item in dtoNames)
////                {
////                    //var oldJson = JsonSerializer.Serialize(item.oldNameJson);
////                    //var newJson = JsonSerializer.Serialize(item.newNameJson);
////                    Query = "Update " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact SET contact_name = '" + item.NewName + "',contact_surname = '" + item.NewSurname + "',contact_mailing_name ='" + item.NewMailingName + "'WHERe tbl_mst_contact_id = " + item.contact_id + ";update " + AddDoubleQuotes(branchschema) + ".tbl_mst_subscriber set subscriber_name='" + item.NewMailingName + "' where contact_id=" + item.contact_id + ";";
////                    NPGSqlHelper.ExecuteNonQuery(Conn, System.Data.CommandType.Text, Query);
////                }


////            }
////            catch (Exception ex)
////            {
////                throw ex;
////            }
////        }


////        public void UpdateNameByTicketNo1(string globalschema, string Conn, Interfaces.Easy_Chit_Tools.NameChangeDto dtonames)
////        {
////            string Query = string.Empty;
////            try
////            {
////                Query = "update " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact " +
////                        "set contact_name='" + dtonames.NewName + "', " +
////                        "contact_surname='" + dtonames.NewSurname + "', " +
////                        "contact_mailing_name='" + dtonames.NewMailingName + "'";

////                NPGSqlHelper.ExecuteNonQuery(Conn, System.Data.CommandType.Text, Query);
////            }
////            catch (Exception ex)
////            {
////                throw ex;
////            }
////        }


////        public void InsertGridDetails(string Conn, string globalschema, GridDTO griddto)
////        {
////            string query = string.Empty;
////            try
////            {
////                string oldDataJson = griddto.OldData.GetRawText();
////                string newDataJson = griddto.NewData.GetRawText();
////                oldDataJson = oldDataJson.Replace("'", "''");
////                newDataJson = newDataJson.Replace("'", "''");
////                string reason = (griddto.Reason ?? "").Replace("'", "''");
////                string changetype = (griddto.vchtype ?? "").Replace("'", "''");
////                string schemaName = (griddto.SchemaName ?? "").Replace("'", "''");
////                query = "insert into " + AddDoubleQuotes(globalschema) + ".tbl_mst_change_details(schema_name,login_id,change_date,olddata,newdata,reason,vchtype) values('" + schemaName + "','" + griddto.LoginId + "',current_timestamp,'" + oldDataJson + "','" + newDataJson + "','" + reason + "','" + changetype + "')";
////                NPGSqlHelper.ExecuteNonQuery(Conn, System.Data.CommandType.Text, query);
////            }
////            catch (Exception ex)
////            {
////                throw ex;
////            }
////        }

////        #endregion
////        public List<ReferralDetailsDto> Agentcode(string globalschema, string Con)
////        {
////            List<ReferralDetailsDto> lstrefdetails = new List<ReferralDetailsDto>();
////            string query = string.Empty;
////            try
////            {

////                query = "select referral_code from " + AddDoubleQuotes(globalschema) + ".tbl_mst_referral";


////                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Con, System.Data.CommandType.Text, query))
////                {
////                    while (dr.Read())
////                    {
////                        ReferralDetailsDto refdetails = new ReferralDetailsDto();
////                        refdetails.referralCode = Convert.ToString(dr["referral_code"]);

////                        lstrefdetails.Add(refdetails);
////                    }
////                }
////                return lstrefdetails;
////            }
////            catch (Exception ex)
////            {
////                throw ex;
////            }
////        }

////        public void UpdateNameByAgentcode(string globalschema, string Conn, Interfaces.Easy_Chit_Tools.NameChangeDto dtonames)
////        {
////            string Query = string.Empty;
////            try
////            {
////                Query = "update " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact " +
////                        "set contact_name='" + dtonames.NewName + "', " +
////                        "contact_surname='" + dtonames.NewSurname + "', " +
////                        "contact_mailing_name='" + dtonames.NewMailingName + "'";

////                NPGSqlHelper.ExecuteNonQuery(Conn, System.Data.CommandType.Text, Query);
////            }
////            catch (Exception ex)
////            {
////                throw ex;
////            }
////        }

////        //public void InsertGridDetails(string Conn, string globalschema, GridDTO griddto)
////        //{
////        //    string query = string.Empty;
////        //    try
////        //    {
////        //        query = "INSERT INTO " + AddDoubleQuotes(globalschema) + ".tbl_mst_NameChange " +
////        //                "VALUES('" + griddto.SchemaName + "', '" + griddto.LoginId + "', current_timestamp, '" +
////        //                griddto.OldName + "', '" + griddto.NewName + "', '" + griddto.Reason + "');";

////        //        NPGSqlHelper.ExecuteNonQuery(Conn, System.Data.CommandType.Text, query);
////        //    }
////        //    catch (Exception ex)
////        //    {
////        //        throw ex;
////        //    }
////        //}
////        public List<ReportsofUpdatesDTO> GetUpdateReports(string globalschema, string Con, DateTime fromdate, DateTime todate, string changetype)
////        {
////            List<ReportsofUpdatesDTO> lstupdatereports = new List<ReportsofUpdatesDTO>();
////            string Query = string.Empty;
////            try
////            {
////                DateTime toDateExclusive = todate.Date.AddDays(1);
////                Query = "SELECT login_id,change_date,olddata,newdata,reason,vchtype FROM " + AddDoubleQuotes(globalschema) + ".tbl_mst_change_details WHERE change_date >= '" + fromdate.Date.ToString("yyyy-MM-dd") + "' AND change_date < '" + toDateExclusive.ToString("yyyy-MM-dd") + "' and vchtype='" + changetype + "' ";
////                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Con, System.Data.CommandType.Text, Query))
////                {
////                    while (dr.Read())
////                    {
////                        ReportsofUpdatesDTO updatereports = new ReportsofUpdatesDTO();
////                        updatereports.LoginId = Convert.ToString(dr["login_id"]);
////                        updatereports.LoginDate = Convert.ToDateTime(dr["change_date"]);
////                        updatereports.OldData = Convert.ToString(dr["olddata"]);
////                        updatereports.NewData = Convert.ToString(dr["newdata"]);
////                        updatereports.ChangeType = Convert.ToString(dr["vchtype"]);
////                        updatereports.Reason = Convert.ToString(dr["reason"]);

////                        lstupdatereports.Add(updatereports);
////                    }
////                }
////                return lstupdatereports;
////            }
////            catch (Exception ex)
////            {
////                throw ex;
////            }
////        }
////        public List<ChangeTypes> GetChangeTypes(string globalschema, string Con)
////        {

////            List<ChangeTypes> lstct = new List<ChangeTypes>();
////            string Query = string.Empty;
////            try
////            {

////                Query = "SELECT distinct vchtype FROM " + AddDoubleQuotes(globalschema) + ".tbl_mst_change_details;";

////                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Con, System.Data.CommandType.Text, Query))
////                {
////                    while (dr.Read())
////                    {
////                        ChangeTypes ct = new ChangeTypes();
////                        ct.ChangeType = Convert.ToString(dr["vchtype"]);
////                        lstct.Add(ct);
////                    }
////                }
////            }
////            catch (Exception ex)
////            {
////                throw ex;
////            }
////            return lstct;
////        }
////        public void UpdateMoblieNoByContact(string globalschema, string Conn, string contactid, Interfaces.Easy_Chit_Tools.NameChangeDto dto)
////        {
////            string Query = string.Empty;
////            try
////            {
////                Query = "UPDATE " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact  SET business_entity_contactno ='" + dto.NewMobileNo + "' WHERE  tbl_mst_contact_id=" + contactid + ";";

////                NPGSqlHelper.ExecuteNonQuery(Conn, System.Data.CommandType.Text, Query);
////            }
////            catch (Exception ex)
////            {
////                throw ex;
////            }
////        }
////        public void UpdateAddressByContact(string globalschema, string Conn, string contactid, Interfaces.Easy_Chit_Tools.NameChangeDto dto)
////        {
////            string Query = string.Empty;
////            try
////            {
////                Query = "UPDATE " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact_address_details SET ADDRESS1='" + dto.NewAddress + "',AREA='" + dto.NewArea + "',CITY_NAME='" + dto.NewCity + "',PINCODE=" + dto.NewPincode + " WHERE contact_id=" + contactid + ";";

////                NPGSqlHelper.ExecuteNonQuery(Conn, System.Data.CommandType.Text, Query);
////            }
////            catch (Exception ex)
////            {
////                throw ex;
////            }
////        }
////        public List<ReferralDetailsDto> GetReferralDetails(string globalschema, string connectionString, string referralCode)
////        {
////            List<ReferralDetailsDto> lstrefdetails = new List<ReferralDetailsDto>();
////            string Query = string.Empty;
////            try
////            {
////                Query = "select t1.agent_unique_code,t2.contact_mailing_name,t2.business_entity_contactno,t3.branch_name   from " + AddDoubleQuotes(globalschema) + ".tbl_mst_referral t1  join " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact t2 on t2.tbl_mst_contact_id = t1.contact_id join " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration t3 on t3.tbl_mst_branch_configuration_id = t1.agent_branch_id where t1.referral_code = '" + referralCode + "'; ";
////                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(connectionString, System.Data.CommandType.Text, Query))
////                {
////                    while (dr.Read())
////                    {
////                        ReferralDetailsDto refdetails = new ReferralDetailsDto();
////                        refdetails.AgentUniqueCode = Convert.ToString(dr["agent_unique_code"]);
////                        refdetails.AgentName = Convert.ToString(dr["contact_mailing_name"]);
////                        refdetails.BusinessEntityContactNo = Convert.ToInt64(dr["business_entity_contactno"]);
////                        refdetails.BranchName = Convert.ToString(dr["branch_name"]);
////                        lstrefdetails.Add(refdetails);
////                    }
////                }
////                return lstrefdetails;
////            }
////            catch (Exception ex)
////            {
////                throw ex;
////            }
////        }
////        public List<BranchNameDTO> GetBranchName(string globalschema, string Con)

////        {
////            List<BranchNameDTO> productionlist = new List<BranchNameDTO>();
////            string Query = string.Empty;
////            try
////            {

////                Query = "select distinct branch_name ,branch_code from " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration;";

////                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Con, System.Data.CommandType.Text, Query))
////                {
////                    while (dr.Read())
////                    {
////                        BranchNameDTO productionDTO = new BranchNameDTO();
////                        productionDTO.BranchName = Convert.ToString(dr["branch_name"]);
////                        productionDTO.BranchCode = Convert.ToString(dr["branch_code"]);

////                        productionlist.Add(productionDTO);
////                    }
////                }
////            }
////            catch (Exception ex)
////            {
////                throw ex;
////            }

////            return productionlist;
////        }
////        public List<ChequeDetailsDTO> GetChequeDetails(string branchschema, string Con, string checkreferno)
////        {
////            List<ChequeDetailsDTO> checkdetailslist = new List<ChequeDetailsDTO>();
////            string query = string.Empty;
////            try
////            {
////                query = "select payment_number,payment_date,paid_to,paid_amount,clear_date from " + AddDoubleQuotes(branchschema) + ".tbl_trans_payment_reference where reference_number='" + checkreferno + "'";
////                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Con, System.Data.CommandType.Text, query))
////                {
////                    while (dr.Read())
////                    {
////                        ChequeDetailsDTO checkDTO = new ChequeDetailsDTO();
////                        checkDTO.PaymentNo = Convert.ToString(dr["payment_number"]);
////                        checkDTO.PayDate = Convert.ToDateTime(dr["payment_date"]);
////                        checkDTO.PaidTo = Convert.ToString(dr["paid_to"]);
////                        checkDTO.Amount = Convert.ToUInt64(dr["paid_amount"]);
////                        checkDTO.ClearDate = dr["clear_date"] == DBNull.Value ? (DateTime?)null : (DateTime)dr["clear_date"];


////                        checkdetailslist.Add(checkDTO);
////                    }
////                }
////            }
////            catch (Exception ex)
////            {
////                throw ex;
////            }
////            return checkdetailslist;
////        }
////        public void ClearDateandStatus(string branchschema, string Con, string checkreferno, string paymentno)
////        {
////            string Query = string.Empty;

////            try
////            {
////                Query = "update " + AddDoubleQuotes(branchschema) + ".tbl_trans_payment_reference set clear_date=null,clear_status='N' where reference_number='" + checkreferno + "' and payment_number='" + paymentno + "'";

////                NPGSqlHelper.ExecuteNonQuery(Con, System.Data.CommandType.Text, Query);
////            }
////            catch (Exception ex)
////            {
////                throw ex;
////            }
////        }
////        public void SaveData(string globalschema, string Con, ChangeAuditDto audit)
////        {
////            string Query = string.Empty;
////            try
////            {
////                string oldDataJson = audit.OldData.GetRawText();
////                string newDataJson = audit.NewData.GetRawText();
////                oldDataJson = oldDataJson.Replace("'", "''");
////                newDataJson = newDataJson.Replace("'", "''");
////                string reason = (audit.Reason ?? "").Replace("'", "''");
////                string changetype = (audit.ChangeType ?? "").Replace("'", "''");
////                string schemaName = (audit.SchemaName ?? "").Replace("'", "''");
////                Query = "insert into " + AddDoubleQuotes(globalschema) + ".tbl_mst_change_details(schema_name,login_id,change_date,olddata,newdata,reason,vchtype) values('" + schemaName + "','" + audit.LoginName + "',current_timestamp,'" + oldDataJson + "','" + newDataJson + "','" + reason + "','" + changetype + "')";
////                NPGSqlHelper.ExecuteNonQuery(Con, System.Data.CommandType.Text, Query);
////            }
////            catch (Exception ex)
////            {
////                throw ex;
////            }

////        }


////        public void UpdateAgentBranch(
////           string schema,
////           string connectionString,
////           UpdateAgentBranchDto dto)
////        {
////            using var con = new NpgsqlConnection(connectionString);
////            con.Open();

////            using var tx = con.BeginTransaction();

////            try
////            {
////                // 1️⃣ Get old branch id
////                int oldBranchId;
////                int newBranchId;

////                string getOldBranchQuery = $@"
////                    SELECT agent_branch_id
////                    FROM ""{schema}"".tbl_mst_referral
////                    WHERE referral_code = @referralCode";

////                using (var cmd = new NpgsqlCommand(getOldBranchQuery, con))
////                {
////                    cmd.Parameters.AddWithValue("@referralCode", dto.ReferralCode);
////                    oldBranchId = Convert.ToInt32(cmd.ExecuteScalar());
////                }


////                string getNewBranchQuery = $@"
////                    SELECT branch_id
////                    FROM ""{schema}"".tbl_mst_branch_configuration
////                    WHERE branch_code = @branchCode";

////                using (var cmd = new NpgsqlCommand(getNewBranchQuery, con))
////                {
////                    cmd.Parameters.AddWithValue("@branchCode", dto.NewBranchCode);
////                    newBranchId = Convert.ToInt32(cmd.ExecuteScalar());
////                }


////                string updateReferralQuery = $@"
////                    UPDATE ""{schema}"".tbl_mst_referral
////                    SET agent_branch_id = @newBranchId
////                    WHERE referral_code = @referralCode";

////                using (var cmd = new NpgsqlCommand(updateReferralQuery, con))
////                {
////                    cmd.Parameters.AddWithValue("@newBranchId", newBranchId);
////                    cmd.Parameters.AddWithValue("@referralCode", dto.ReferralCode);
////                    cmd.ExecuteNonQuery();
////                }


////                string auditQuery = $@"
////                    INSERT INTO ""{schema}"".tbl_mst_change_details
////                    (schema_name, login_id, change_date, olddata, newdata, reason, vchtype)
////                    VALUES
////                    (@schema, 'SYSTEM', CURRENT_TIMESTAMP,
////                     jsonb_build_object('oldBranchId', @oldBranchId),
////                     jsonb_build_object('newBranchId', @newBranchId),
////                     @reason,
////                     'BRANCHCHANGE')";

////                using (var cmd = new NpgsqlCommand(auditQuery, con))
////                {
////                    cmd.Parameters.AddWithValue("@schema", schema);
////                    cmd.Parameters.AddWithValue("@oldBranchId", oldBranchId);
////                    cmd.Parameters.AddWithValue("@newBranchId", newBranchId);
////                    cmd.Parameters.AddWithValue("@reason", dto.Reason);
////                    cmd.ExecuteNonQuery();
////                }

////                tx.Commit();
////            }
////            catch
////            {
////                tx.Rollback();
////                throw;
////            }
////        }

////        public void UpdateNameByTicketNo(string globalschema, string Conn, Easychit_Infrastructure.Easy_Chit_Tools.NameChangeDto dtoNames, string branchschema)
////        {
////            throw new NotImplementedException();
////        }

////        public void UpdateMoblieNoByContact(string globalschema, string Conn, string contactid, Easychit_Infrastructure.Easy_Chit_Tools.NameChangeDto dto)
////        {
////            throw new NotImplementedException();
////        }

////        public void UpdateAddressByContact(string globalschema, string Conn, string contactid, Easychit_Infrastructure.Easy_Chit_Tools.NameChangeDto dto)
////        {
////            throw new NotImplementedException();
////        }

////        public void UpdateNameByTicketNo(string globalSchema, string con, List<Easychit_Infrastructure.Easy_Chit_Tools.NameChangeDto> dtoNames, string branchschema)
////        {
////            throw new NotImplementedException();
////        }

////        public void UpdateNameByTicketNo(string globalSchema, string con, List<Interfaces.Easy_Chit_Tools.NameChangeDto> dtoNames, Interfaces.Easy_Chit_Tools.NameChangeDto dtonames)
////        {
////            throw new NotImplementedException();
////        }

////        public void UpdateNameByTicketNo(string globalSchema, string con, Interfaces.Easy_Chit_Tools.NameChangeDto dtoName, string branchschema)
////        {
////            throw new NotImplementedException();
////        }

////        public bool SaveNameChange(string globalSchema, string con, Easychit_Infrastructure.Easy_Chit_Tools.NameChangeSaveDTO dto)
////        {
////            throw new NotImplementedException();
////        }

////        public void UpdateAddressByContact(string globalSchema, string con, string contactid, ChangeDTO.NameChangeDto dto)
////        {
////            throw new NotImplementedException();
////        }



////        //public List<TicketDTO> GetTickets(string schema, string Conn, string groupcode)
////        //{
////        //    throw new NotImplementedException();
////        //}

////        //public void InsertGridDetails(string Conn, string globalschema, GridDTO griddto)
////        //{
////        //    throw new NotImplementedException();
////        //}

////        //public void UpdateNameByTicketNo(string globalschema, string Conn, List<NameChangeDto> dtoNames)
////        //{
////        //    throw new NotImplementedException();
////        //}

////        //public void UpdateNameByTicketNo1(string globalschema, string Conn, NameChangeDto dtonames)
////        //{
////        //    throw new NotImplementedException();
////        //}
////    }








////    //    public NameUpdateDTO GetOldNameByTicketNo(string globalschema, string schema, string Conn, int ticketNo, int chitgroupid)
////    //    {
////    //        NameUpdateDTO dto = new NameUpdateDTO();

////    //        string Query = string.Empty;

////    //        try
////    //        {
////    //            Query = "select contact_name,contact_surname,contact_mailing_name,contact_id from " + AddDoubleQuotes(schema) + ".tbl_mst_subscriber x," + AddDoubleQuotes(schema) + ".tbl_mst_chitgroup y," + AddDoubleQuotes(globalschema) + ".tbl_mst_contact c where chitgroup_status in('Registered', 'Commenced') and x.chitgroup_id = y.tbl_mst_chitgroup_id and x.contact_id = c.tbl_mst_contact_id and chitgroup_id=" + chitgroupid + " and ticketno= " + ticketNo + "  order by groupcode,ticketno";

////    //            using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
////    //            {
////    //                if (dr.Read())
////    //                {
////    //                    //dto.Ticket_No = ticketNo;
////    //                    dto.OldName = Convert.ToString(dr["contact_name"]);
////    //                    dto.OldSurname = Convert.ToString(dr["contact_surname"]);
////    //                    dto.OldMailingName = Convert.ToString(dr["contact_mailing_name"]);
////    //                    dto.ContactId = Convert.ToInt32(dr["contact_id"]);
////    //                }
////    //            }
////    //        }
////    //        catch (Exception ex)
////    //        {
////    //            throw ex;
////    //        }

////    //        return dto;
////    //    }

////    //    public void UpdateNameByTicketNo(string globalschema, string Conn, NewNameDTO dtonames, int contactid)
////    //    {
////    //        string Query = string.Empty;
////    //        //NameUpdateDTO dto = new NameUpdateDTO();

////    //        try
////    //        {
////    //            Query = "update " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact set contact_name='" + dtonames.NewName + "',contact_surname='" + dtonames.NewSurname + "', contact_mailing_name='" + dtonames.NewMailingName + "' where tbl_mst_contact_id=" + contactid + "";

////    //            NPGSqlHelper.ExecuteNonQuery(Conn, System.Data.CommandType.Text, Query);
////    //        }
////    //        catch (Exception ex)
////    //        {
////    //            throw ex;
////    //        }

////    //    }
////    //    public NameUpdateDTO GetOldNameByTicketNo(string globalschema, string schema, string Conn, int ticketNo, int chitgroupid)
////    //    {
////    //        NameUpdateDTO dto = new NameUpdateDTO();

////    //        string Query = string.Empty;

////    //        try
////    //        {
////    //            Query = "select contact_name,contact_surname,contact_mailing_name,contact_id from " + AddDoubleQuotes(schema) + ".tbl_mst_subscriber x," + AddDoubleQuotes(schema) + ".tbl_mst_chitgroup y," + AddDoubleQuotes(globalschema) + ".tbl_mst_contact c where chitgroup_status in('Registered', 'Commenced') and x.chitgroup_id = y.tbl_mst_chitgroup_id and x.contact_id = c.tbl_mst_contact_id and chitgroup_id=" + chitgroupid + " and ticketno= " + ticketNo + "  order by groupcode,ticketno";


////    //            //  select contact_name, contact_surname, contact_mailing_name, t1.contact_id,c.business_entity_contactno,t1.address1 from "KKR".tbl_mst_subscriber x,"KKR".tbl_mst_chitgroup y,"GLOBAL".tbl_mst_contact c,"GLOBAL".tbl_mst_contact_address_details t1 where chitgroup_status in('Registered', 'Commenced') and x.chitgroup_id = y.tbl_mst_chitgroup_id and x.contact_id = c.tbl_mst_contact_id and t1.contact_id = c.tbl_mst_contact_id and chitgroup_id = 1092 and ticketno = 2  order by groupcode,ticketno




////    //            using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
////    //            {
////    //                if (dr.Read())
////    //                {
////    //                    //dto.Ticket_No = ticketNo;
////    //                    dto.OldName = Convert.ToString(dr["contact_name"]);
////    //                    dto.OldSurname = Convert.ToString(dr["contact_surname"]);
////    //                    dto.OldMailingName = Convert.ToString(dr["contact_mailing_name"]);
////    //                    dto.ContactId = Convert.ToInt32(dr["contact_id"]);
////    //                }
////    //            }
////    //        }
////    //        catch (Exception ex)
////    //        {
////    //            throw ex;
////    //        }

////    //        return dto;
////    //    }

////    //    public void UpdateNameByTicketNo(string globalschema, string Conn, NewNameDTO dtonames, int contactid)
////    //    {
////    //        string Query = string.Empty;
////    //        //NameUpdateDTO dto = new NameUpdateDTO();

////    //        try
////    //        {
////    //            Query = "update " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact set contact_name='" + dtonames.NewName + "',contact_surname='" + dtonames.NewSurname + "', contact_mailing_name='" + dtonames.NewMailingName + "' where tbl_mst_contact_id=" + contactid + "";

////    //            NPGSqlHelper.ExecuteNonQuery(Conn, System.Data.CommandType.Text, Query);
////    //        }
////    //        catch (Exception ex)
////    //        {
////    //            throw ex;
////    //        }

////    //    }
////    //    public void InsertGridDetails(string Conn, string globalschema, GridDTO griddto)
////    //    {
////    //        string query = string.Empty;
////    //        try
////    //        {
////    //            query = "INSERT INTO " + AddDoubleQuotes(globalschema) + ".tbl_mst_NameChange VALUES( " + griddto.Id + ",'" + griddto.SchemaName + "','" + griddto.LoginId + "',current_timestamp,'" + griddto.OldName + "','" + griddto.NewName + "','" + griddto.Reason + "'); ";
////    //            NPGSqlHelper.ExecuteNonQuery(Conn, System.Data.CommandType.Text, query);
////    //        }
////    //        catch (Exception Ex)
////    //        {
////    //            throw Ex;
////    //        }
////    //    }
////   // public void InsertGridDetails(string Conn, string globalschema, GridDTO griddto)
////    //    {
////    //        string query = string.Empty;
////    //        try
////    //        {
////    //            query = "INSERT INTO " + AddDoubleQuotes(globalschema) + ".tbl_mst_NameChange VALUES( " + griddto.Id + ",'" + griddto.SchemaName + "','" + griddto.LoginId + "',current_timestamp,'" + griddto.OldName + "','" + griddto.NewName + "','" + griddto.Reason + "'); ";
////    //            NPGSqlHelper.ExecuteNonQuery(Conn, System.Data.CommandType.Text, query);
////    //        }
////    //        catch (Exception Ex)
////    //        {
////    //            throw Ex;
////    //        }
////    //    }
////    //public void InsertGridDetails(string Conn, string globalschema, GridDTO griddto)
////    //    {
////    //        string query = string.Empty;
////    //        try
////    //        {
////    //            query = "INSERT INTO " + AddDoubleQuotes(globalschema) + ".tbl_mst_NameChange VALUES( " + griddto.Id + ",'" + griddto.SchemaName + "','" + griddto.LoginId + "',current_timestamp,'" + griddto.OldName + "','" + griddto.NewName + "','" + griddto.Reason + "'); ";
////    //            NPGSqlHelper.ExecuteNonQuery(Conn, System.Data.CommandType.Text, query);
////    //        }
////    //        catch (Exception Ex)
////    //        {
////    //            throw Ex;
////    //        }
////    //    }






////}
//using Easychit_Infrastructure.Easy_Chit_Tools;
//using HelperManager;
//using Npgsql;
//using System;
//using System.Collections.Generic;
//using System.Text.Json;
//using System.Text;
//using Easychit_Repository.Interfaces.Easy_Chit_Tools;
//using Easychit_Infrastructure.Settings.Users;
//using Easychit_Repository.Interfaces.Security;

//namespace Easychit_Repository.DataAccess.Easy_Chit_Tools
//{
//    public class EasyChitToolsDAL : CommonDAL, IEasyChitTools
//    {

//        NpgsqlConnection con = new NpgsqlConnection(NPGSqlHelper.SQLConnString);

//        #region Branches


//        public List<BranchNamesDTO> GetBranchNames(string globalschema, string Conn)
//        {
//            List<BranchNamesDTO> productionlist = new List<BranchNamesDTO>();
//            string Query = string.Empty;

//            try
//            {

//                // Query = "select distinct branch_name,branch_code from " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration where branch_name like '%CAO%' and branch_type ='CAO';";

//                Query = "select distinct branch_name, branch_code " +
//                        "from " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration " +
//                        "where branch_name like '%CAO%' and branch_type ='CAO';";

//                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
//                {
//                    while (dr.Read())
//                    {
//                        BranchNamesDTO productionDTO = new BranchNamesDTO
//                        {
//                            Branch_name = Convert.ToString(dr["branch_name"]),
//                            branch_code = Convert.ToString(dr["branch_code"])
//                        };
//                        productionlist.Add(productionDTO);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            return productionlist;
//        }

//        public List<GroupCodeDTO> GetGroupcodes(string branchschema, string Conn)
//        {
//            List<GroupCodeDTO> productionlist = new List<GroupCodeDTO>();
//            string Query = string.Empty;

//            try
//            {
//                // Query = "select distinct branch_code from " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration where branch_name like '%CAO%' and branch_type ='CAO';";

//                Query = "select distinct groupcode, chitgroup_id " +
//                        "from " + AddDoubleQuotes(branchschema) + ".tbl_mst_subscriber x, " +
//                        AddDoubleQuotes(branchschema) + ".tbl_mst_chitgroup y " +
//                        "where chitgroup_status in('Registered','Commenced') " +
//                        "and x.chitgroup_id = y.tbl_mst_chitgroup_id;";

//                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
//                {
//                    while (dr.Read())
//                    {
//                        GroupCodeDTO productionDTO = new GroupCodeDTO
//                        {
//                            Group_Code = dr["groupcode"] != DBNull.Value ? Convert.ToString(dr["groupcode"]) : string.Empty,
//                            chitgroup_id = dr["chitgroup_id"] != DBNull.Value ? Convert.ToString(dr["chitgroup_id"]) : string.Empty
//                        };
//                        productionlist.Add(productionDTO);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }

//            return productionlist;
//        }


//        public List<TicketDTO> GetTickets(string schema, string Conn, string groupcode)
//        {
//            List<TicketDTO> productionlist = new List<TicketDTO>();
//            string Query = string.Empty;

//            try
//            {
//                Query = "select ticketno " +
//                        "from " + AddDoubleQuotes(schema) + ".tbl_mst_subscriber x, " +
//                        AddDoubleQuotes(schema) + ".tbl_mst_chitgroup y " +
//                        "where chitgroup_status in('Registered','Commenced') " +
//                        "and x.chitgroup_id = y.tbl_mst_chitgroup_id " +
//                        "and groupcode='" + groupcode + "';";

//                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
//                {
//                    while (dr.Read())
//                    {
//                        TicketDTO productionDTO = new TicketDTO
//                        {
//                            ticketno = Convert.ToInt32(dr["ticketno"])
//                        };
//                        productionlist.Add(productionDTO);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }

//            return productionlist;
//        }

//        #endregion

//        #region


//        public NameUpdateDTO GetOldNameByTicketNo(string globalschema, string schema, string Conn, int ticketNo, int chitgroupid)
//        {
//            NameUpdateDTO dto = new NameUpdateDTO();
//            string Query = string.Empty;

//            try
//            {
//                Query = "select contact_name, contact_surname, contact_mailing_name, t1.contact_id, c.business_entity_contactno, t1.address1,t1.area,t1.city_name,t1.pincode " +
//                        "from " + AddDoubleQuotes(schema) + ".tbl_mst_subscriber x, " +
//                        AddDoubleQuotes(schema) + ".tbl_mst_chitgroup y, " +
//                        AddDoubleQuotes(globalschema) + ".tbl_mst_contact c, " +
//                        AddDoubleQuotes(globalschema) + ".tbl_mst_contact_address_details t1 " +
//                        "where chitgroup_status in('Registered','Commenced') and t1.isprimary='true' and t1.status='true' " +
//                        "and x.chitgroup_id = y.tbl_mst_chitgroup_id " +
//                        "and x.contact_id = c.tbl_mst_contact_id " +
//                        "and t1.contact_id = c.tbl_mst_contact_id " +
//                        "and chitgroup_id = " + chitgroupid + " " +
//                        "and ticketno = " + ticketNo + " " +
//                        "order by groupcode, ticketno;";

//                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
//                {
//                    if (dr.Read())
//                    {
//                        dto.OldName = Convert.ToString(dr["contact_name"]);
//                        dto.OldSurname = Convert.ToString(dr["contact_surname"]);
//                        dto.OldMailingName = Convert.ToString(dr["contact_mailing_name"]);
//                        dto.ContactId = Convert.ToInt32(dr["contact_id"]);
//                        dto.MobileNo = Convert.ToString(dr["business_entity_contactno"]);
//                        dto.Address = Convert.ToString(dr["address1"]);
//                        dto.Area = Convert.ToString(dr["area"]);
//                        dto.City = Convert.ToString(dr["city_name"]);
//                        dto.Pincode = Convert.ToInt32(dr["pincode"]);


//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }

//            return dto;
//        }


//        //public void UpdateNameByTicketNo(string globalschema, string Conn, List<NameChangeDto> dtoNames, string branchschema)
//        //{
//        //    string Query = string.Empty;
//        //    try
//        //    {
//        //        foreach (var item in dtoNames)
//        //        {
//        //            //var oldJson = JsonSerializer.Serialize(item.oldNameJson);
//        //            //var newJson = JsonSerializer.Serialize(item.newNameJson);
//        //            Query = "Update " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact SET contact_name = '" + item.NewName + "',contact_surname = '" + item.NewSurname + "',contact_mailing_name ='" + item.NewMailingName + "'WHERe tbl_mst_contact_id = " + item.contact_id + ";update " + AddDoubleQuotes(branchschema) + ".tbl_mst_subscriber set subscriber_name='" + item.NewMailingName + "' where contact_id=" + item.contact_id + ";";
//        //            NPGSqlHelper.ExecuteNonQuery(Conn, System.Data.CommandType.Text, Query);
//        //        }


//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        throw ex;
//        //    }
//        //}


//        //public void UpdateNameByTicketNo1(string globalschema, string Conn, NameChangeDto dtonames)
//        //{
//        //    string Query = string.Empty;
//        //    try
//        //    {
//        //        Query = "update " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact " +
//        //                "set contact_name='" + dtonames.NewName + "', " +
//        //                "contact_surname='" + dtonames.NewSurname + "', " +
//        //                "contact_mailing_name='" + dtonames.NewMailingName + "'";

//        //        NPGSqlHelper.ExecuteNonQuery(Conn, System.Data.CommandType.Text, Query);
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        throw ex;
//        //    }
//        //}


//        public void InsertGridDetails(string Conn, string globalschema, GridDTO griddto)
//        {
//            string query = string.Empty;
//            try
//            {
//                string oldDataJson = griddto.OldData.GetRawText();
//                string newDataJson = griddto.NewData.GetRawText();
//                oldDataJson = oldDataJson.Replace("'", "''");
//                newDataJson = newDataJson.Replace("'", "''");
//                string reason = (griddto.Reason ?? "").Replace("'", "''");
//                string changetype = (griddto.vchtype ?? "").Replace("'", "''");
//                string schemaName = (griddto.SchemaName ?? "").Replace("'", "''");
//                query = "insert into " + AddDoubleQuotes(globalschema) + ".tbl_mst_change_details(schema_name,login_id,change_date,olddata,newdata,reason,vchtype) values('" + schemaName + "','" + griddto.LoginId + "',current_timestamp,'" + oldDataJson + "','" + newDataJson + "','" + reason + "','" + changetype + "')";
//                NPGSqlHelper.ExecuteNonQuery(Conn, System.Data.CommandType.Text, query);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        #endregion
//        public List<ReferralDetailsDto> Agentcode(string globalschema, string Con)
//        {
//            List<ReferralDetailsDto> lstrefdetails = new List<ReferralDetailsDto>();
//            string query = string.Empty;
//            try
//            {

//                query = "select referral_code from " + AddDoubleQuotes(globalschema) + ".tbl_mst_referral";


//                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Con, System.Data.CommandType.Text, query))
//                {
//                    while (dr.Read())
//                    {
//                        ReferralDetailsDto refdetails = new ReferralDetailsDto();
//                        refdetails.referralCode = Convert.ToString(dr["referral_code"]);

//                        lstrefdetails.Add(refdetails);
//                    }
//                }
//                return lstrefdetails;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        //public void UpdateNameByAgentcode(string globalschema, string Conn, NameChangeDto dtonames)
//        //{
//        //    string Query = string.Empty;
//        //    try
//        //    {
//        //        Query = "update " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact " +
//        //                "set contact_name='" + dtonames.NewName + "', " +
//        //                "contact_surname='" + dtonames.NewSurname + "', " +
//        //                "contact_mailing_name='" + dtonames.NewMailingName + "'";

//        //        NPGSqlHelper.ExecuteNonQuery(Conn, System.Data.CommandType.Text, Query);
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        throw ex;
//        //    }
//        //}

//        //public void InsertGridDetails(string Conn, string globalschema, GridDTO griddto)
//        //{
//        //    string query = string.Empty;
//        //    try
//        //    {
//        //        query = "INSERT INTO " + AddDoubleQuotes(globalschema) + ".tbl_mst_NameChange " +
//        //                "VALUES('" + griddto.SchemaName + "', '" + griddto.LoginId + "', current_timestamp, '" +
//        //                griddto.OldName + "', '" + griddto.NewName + "', '" + griddto.Reason + "');";

//        //        NPGSqlHelper.ExecuteNonQuery(Conn, System.Data.CommandType.Text, query);
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        throw ex;
//        //    }
//        //}
//        public List<ReportsofUpdatesDTO> GetUpdateReports(string globalschema, string Con, DateTime fromdate, DateTime todate, string changetype)
//        {
//            List<ReportsofUpdatesDTO> lstupdatereports = new List<ReportsofUpdatesDTO>();
//            string Query = string.Empty;
//            try
//            {
//                DateTime toDateExclusive = todate.Date.AddDays(1);
//                Query = "SELECT login_id,change_date,olddata,newdata,reason,vchtype FROM " + AddDoubleQuotes(globalschema) + ".tbl_mst_change_details WHERE change_date >= '" + fromdate.Date.ToString("yyyy-MM-dd") + "' AND change_date < '" + toDateExclusive.ToString("yyyy-MM-dd") + "' and vchtype='" + changetype + "' ";
//                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Con, System.Data.CommandType.Text, Query))
//                {
//                    while (dr.Read())
//                    {
//                        ReportsofUpdatesDTO updatereports = new ReportsofUpdatesDTO();
//                        updatereports.LoginId = Convert.ToString(dr["login_id"]);
//                        updatereports.LoginDate = Convert.ToDateTime(dr["change_date"]);
//                        updatereports.OldData = Convert.ToString(dr["olddata"]);
//                        updatereports.NewData = Convert.ToString(dr["newdata"]);
//                        updatereports.ChangeType = Convert.ToString(dr["vchtype"]);
//                        updatereports.Reason = Convert.ToString(dr["reason"]);

//                        lstupdatereports.Add(updatereports);
//                    }
//                }
//                return lstupdatereports;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        public List<ChangeTypes> GetChangeTypes(string globalschema, string Con)
//        {

//            List<ChangeTypes> lstct = new List<ChangeTypes>();
//            string Query = string.Empty;
//            try
//            {

//                Query = "SELECT distinct vchtype FROM " + AddDoubleQuotes(globalschema) + ".tbl_mst_change_details;";

//                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Con, System.Data.CommandType.Text, Query))
//                {
//                    while (dr.Read())
//                    {
//                        ChangeTypes ct = new ChangeTypes();
//                        ct.ChangeType = Convert.ToString(dr["vchtype"]);
//                        lstct.Add(ct);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            return lstct;
//        }
//        //public void UpdateMoblieNoByContact(string globalschema, string Conn, string contactid, NameChangeDto dto)
//        //{
//        //    string Query = string.Empty;
//        //    try
//        //    {
//        //        Query = "UPDATE " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact  SET business_entity_contactno ='" + dto.NewMobileNo + "' WHERE  tbl_mst_contact_id=" + contactid + ";";

//        //        NPGSqlHelper.ExecuteNonQuery(Conn, System.Data.CommandType.Text, Query);
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        throw ex;
//        //    }
//        //}
//        //public void UpdateAddressByContact(string globalschema, string Conn, string contactid, NameChangeDto dto)
//        //{
//        //    string Query = string.Empty;
//        //    try
//        //    {
//        //        Query = "UPDATE " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact_address_details SET ADDRESS1='" + dto.NewAddress + "',AREA='" + dto.NewArea + "',CITY_NAME='" + dto.NewCity + "',PINCODE=" + dto.NewPincode + " WHERE contact_id=" + contactid + ";";

//        //        NPGSqlHelper.ExecuteNonQuery(Conn, System.Data.CommandType.Text, Query);
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        throw ex;
//        //    }
//        //}
//        public List<ReferralDetailsDto> GetReferralDetails(string globalschema, string connectionString, string referralCode)
//        {
//            List<ReferralDetailsDto> lstrefdetails = new List<ReferralDetailsDto>();
//            string Query = string.Empty;
//            try
//            {
//                Query = "select t1.agent_unique_code,t2.contact_mailing_name,t2.business_entity_contactno,t3.branch_name   from " + AddDoubleQuotes(globalschema) + ".tbl_mst_referral t1  join " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact t2 on t2.tbl_mst_contact_id = t1.contact_id join " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration t3 on t3.tbl_mst_branch_configuration_id = t1.agent_branch_id where t1.referral_code = '" + referralCode + "'; ";
//                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(connectionString, System.Data.CommandType.Text, Query))
//                {
//                    while (dr.Read())
//                    {
//                        ReferralDetailsDto refdetails = new ReferralDetailsDto();
//                        refdetails.AgentUniqueCode = Convert.ToString(dr["agent_unique_code"]);
//                        refdetails.AgentName = Convert.ToString(dr["contact_mailing_name"]);
//                        refdetails.BusinessEntityContactNo = Convert.ToInt64(dr["business_entity_contactno"]);
//                        refdetails.BranchName = Convert.ToString(dr["branch_name"]);
//                        lstrefdetails.Add(refdetails);
//                    }
//                }
//                return lstrefdetails;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        public List<BranchNameDTO> GetBranchName(string globalschema, string Con)

//        {
//            List<BranchNameDTO> productionlist = new List<BranchNameDTO>();
//            string Query = string.Empty;
//            try
//            {

//                Query = "select distinct branch_name ,branch_code from " + AddDoubleQuotes(globalschema) + ".tbl_mst_branch_configuration;";

//                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Con, System.Data.CommandType.Text, Query))
//                {
//                    while (dr.Read())
//                    {
//                        BranchNameDTO productionDTO = new BranchNameDTO();
//                        productionDTO.BranchName = Convert.ToString(dr["branch_name"]);
//                        productionDTO.BranchCode = Convert.ToString(dr["branch_code"]);

//                        productionlist.Add(productionDTO);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }

//            return productionlist;
//        }
//        public List<ChequeDetailsDTO> GetChequeDetails(string branchschema, string Con, string checkreferno)
//        {
//            List<ChequeDetailsDTO> checkdetailslist = new List<ChequeDetailsDTO>();
//            string query = string.Empty;
//            try
//            {
//                query = "select payment_number,payment_date,paid_to,paid_amount,clear_date from " + AddDoubleQuotes(branchschema) + ".tbl_trans_payment_reference where reference_number='" + checkreferno + "'";
//                using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Con, System.Data.CommandType.Text, query))
//                {
//                    while (dr.Read())
//                    {
//                        ChequeDetailsDTO checkDTO = new ChequeDetailsDTO();
//                        checkDTO.PaymentNo = Convert.ToString(dr["payment_number"]);
//                        checkDTO.PayDate = Convert.ToDateTime(dr["payment_date"]);
//                        checkDTO.PaidTo = Convert.ToString(dr["paid_to"]);
//                        checkDTO.Amount = Convert.ToUInt64(dr["paid_amount"]);
//                        checkDTO.ClearDate = dr["clear_date"] == DBNull.Value ? (DateTime?)null : (DateTime)dr["clear_date"];


//                        checkdetailslist.Add(checkDTO);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            return checkdetailslist;
//        }
//        public void ClearDateandStatus(string branchschema, string Con, string checkreferno, string paymentno)
//        {
//            string Query = string.Empty;

//            try
//            {
//                Query = "update " + AddDoubleQuotes(branchschema) + ".tbl_trans_payment_reference set clear_date=null,clear_status='N' where reference_number='" + checkreferno + "' and payment_number='" + paymentno + "'";

//                NPGSqlHelper.ExecuteNonQuery(Con, System.Data.CommandType.Text, Query);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        public void SaveData(string globalschema, string Con, ChangeAuditDto audit)
//        {
//            string Query = string.Empty;
//            try
//            {
//                string oldDataJson = audit.OldData.GetRawText();
//                string newDataJson = audit.NewData.GetRawText();
//                oldDataJson = oldDataJson.Replace("'", "''");
//                newDataJson = newDataJson.Replace("'", "''");
//                string reason = (audit.Reason ?? "").Replace("'", "''");
//                string changetype = (audit.ChangeType ?? "").Replace("'", "''");
//                string schemaName = (audit.SchemaName ?? "").Replace("'", "''");
//                Query = "insert into " + AddDoubleQuotes(globalschema) + ".tbl_mst_change_details(schema_name,login_id,change_date,olddata,newdata,reason,vchtype) values('" + schemaName + "','" + audit.LoginName + "',current_timestamp,'" + oldDataJson + "','" + newDataJson + "','" + reason + "','" + changetype + "')";
//                NPGSqlHelper.ExecuteNonQuery(Con, System.Data.CommandType.Text, Query);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }

//        }


//        public void UpdateAgentBranch(
//           string schema,
//           string connectionString,
//           UpdateAgentBranchDto dto)
//        {
//            using var con = new NpgsqlConnection(connectionString);
//            con.Open();

//            using var tx = con.BeginTransaction();

//            try
//            {
//                // 1️⃣ Get old branch id
//                int oldBranchId;
//                int newBranchId;

//                string getOldBranchQuery = $@"
//                    SELECT agent_branch_id
//                    FROM ""{schema}"".tbl_mst_referral
//                    WHERE referral_code = @referralCode";

//                using (var cmd = new NpgsqlCommand(getOldBranchQuery, con))
//                {
//                    cmd.Parameters.AddWithValue("@referralCode", dto.ReferralCode);
//                    oldBranchId = Convert.ToInt32(cmd.ExecuteScalar());
//                }


//                string getNewBranchQuery = $@"
//                    SELECT branch_id
//                    FROM ""{schema}"".tbl_mst_branch_configuration
//                    WHERE branch_code = @branchCode";

//                using (var cmd = new NpgsqlCommand(getNewBranchQuery, con))
//                {
//                    cmd.Parameters.AddWithValue("@branchCode", dto.NewBranchCode);
//                    newBranchId = Convert.ToInt32(cmd.ExecuteScalar());
//                }


//                string updateReferralQuery = $@"
//                    UPDATE ""{schema}"".tbl_mst_referral
//                    SET agent_branch_id = @newBranchId
//                    WHERE referral_code = @referralCode";

//                using (var cmd = new NpgsqlCommand(updateReferralQuery, con))
//                {
//                    cmd.Parameters.AddWithValue("@newBranchId", newBranchId);
//                    cmd.Parameters.AddWithValue("@referralCode", dto.ReferralCode);
//                    cmd.ExecuteNonQuery();
//                }


//                string auditQuery = $@"
//                    INSERT INTO ""{schema}"".tbl_mst_change_details
//                    (schema_name, login_id, change_date, olddata, newdata, reason, vchtype)
//                    VALUES
//                    (@schema, 'SYSTEM', CURRENT_TIMESTAMP,
//                     jsonb_build_object('oldBranchId', @oldBranchId),
//                     jsonb_build_object('newBranchId', @newBranchId),
//                     @reason,
//                     'BRANCHCHANGE')";

//                using (var cmd = new NpgsqlCommand(auditQuery, con))
//                {
//                    cmd.Parameters.AddWithValue("@schema", schema);
//                    cmd.Parameters.AddWithValue("@oldBranchId", oldBranchId);
//                    cmd.Parameters.AddWithValue("@newBranchId", newBranchId);
//                    cmd.Parameters.AddWithValue("@reason", dto.Reason);
//                    cmd.ExecuteNonQuery();
//                }

//                tx.Commit();
//            }
//            catch
//            {
//                tx.Rollback();
//                throw;
//            }
//        }



//        //public List<TicketDTO> GetTickets(string schema, string Conn, string groupcode)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //public void InsertGridDetails(string Conn, string globalschema, GridDTO griddto)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //public void UpdateNameByTicketNo(string globalschema, string Conn, List<NameChangeDto> dtoNames)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //public void UpdateNameByTicketNo1(string globalschema, string Conn, NameChangeDto dtonames)
//        //{
//        //    throw new NotImplementedException();
//        //}
//    }








//    //    public NameUpdateDTO GetOldNameByTicketNo(string globalschema, string schema, string Conn, int ticketNo, int chitgroupid)
//    //    {
//    //        NameUpdateDTO dto = new NameUpdateDTO();

//    //        string Query = string.Empty;

//    //        try
//    //        {
//    //            Query = "select contact_name,contact_surname,contact_mailing_name,contact_id from " + AddDoubleQuotes(schema) + ".tbl_mst_subscriber x," + AddDoubleQuotes(schema) + ".tbl_mst_chitgroup y," + AddDoubleQuotes(globalschema) + ".tbl_mst_contact c where chitgroup_status in('Registered', 'Commenced') and x.chitgroup_id = y.tbl_mst_chitgroup_id and x.contact_id = c.tbl_mst_contact_id and chitgroup_id=" + chitgroupid + " and ticketno= " + ticketNo + "  order by groupcode,ticketno";

//    //            using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
//    //            {
//    //                if (dr.Read())
//    //                {
//    //                    //dto.Ticket_No = ticketNo;
//    //                    dto.OldName = Convert.ToString(dr["contact_name"]);
//    //                    dto.OldSurname = Convert.ToString(dr["contact_surname"]);
//    //                    dto.OldMailingName = Convert.ToString(dr["contact_mailing_name"]);
//    //                    dto.ContactId = Convert.ToInt32(dr["contact_id"]);
//    //                }
//    //            }
//    //        }
//    //        catch (Exception ex)
//    //        {
//    //            throw ex;
//    //        }

//    //        return dto;
//    //    }

//    //    public void UpdateNameByTicketNo(string globalschema, string Conn, NewNameDTO dtonames, int contactid)
//    //    {
//    //        string Query = string.Empty;
//    //        //NameUpdateDTO dto = new NameUpdateDTO();

//    //        try
//    //        {
//    //            Query = "update " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact set contact_name='" + dtonames.NewName + "',contact_surname='" + dtonames.NewSurname + "', contact_mailing_name='" + dtonames.NewMailingName + "' where tbl_mst_contact_id=" + contactid + "";

//    //            NPGSqlHelper.ExecuteNonQuery(Conn, System.Data.CommandType.Text, Query);
//    //        }
//    //        catch (Exception ex)
//    //        {
//    //            throw ex;
//    //        }

//    //    }
//    //    public NameUpdateDTO GetOldNameByTicketNo(string globalschema, string schema, string Conn, int ticketNo, int chitgroupid)
//    //    {
//    //        NameUpdateDTO dto = new NameUpdateDTO();

//    //        string Query = string.Empty;

//    //        try
//    //        {
//    //            Query = "select contact_name,contact_surname,contact_mailing_name,contact_id from " + AddDoubleQuotes(schema) + ".tbl_mst_subscriber x," + AddDoubleQuotes(schema) + ".tbl_mst_chitgroup y," + AddDoubleQuotes(globalschema) + ".tbl_mst_contact c where chitgroup_status in('Registered', 'Commenced') and x.chitgroup_id = y.tbl_mst_chitgroup_id and x.contact_id = c.tbl_mst_contact_id and chitgroup_id=" + chitgroupid + " and ticketno= " + ticketNo + "  order by groupcode,ticketno";


//    //            //  select contact_name, contact_surname, contact_mailing_name, t1.contact_id,c.business_entity_contactno,t1.address1 from "KKR".tbl_mst_subscriber x,"KKR".tbl_mst_chitgroup y,"GLOBAL".tbl_mst_contact c,"GLOBAL".tbl_mst_contact_address_details t1 where chitgroup_status in('Registered', 'Commenced') and x.chitgroup_id = y.tbl_mst_chitgroup_id and x.contact_id = c.tbl_mst_contact_id and t1.contact_id = c.tbl_mst_contact_id and chitgroup_id = 1092 and ticketno = 2  order by groupcode,ticketno




//    //            using (NpgsqlDataReader dr = NPGSqlHelper.ExecuteReader(Conn, System.Data.CommandType.Text, Query))
//    //            {
//    //                if (dr.Read())
//    //                {
//    //                    //dto.Ticket_No = ticketNo;
//    //                    dto.OldName = Convert.ToString(dr["contact_name"]);
//    //                    dto.OldSurname = Convert.ToString(dr["contact_surname"]);
//    //                    dto.OldMailingName = Convert.ToString(dr["contact_mailing_name"]);
//    //                    dto.ContactId = Convert.ToInt32(dr["contact_id"]);
//    //                }
//    //            }
//    //        }
//    //        catch (Exception ex)
//    //        {
//    //            throw ex;
//    //        }

//    //        return dto;
//    //    }

//    //    public void UpdateNameByTicketNo(string globalschema, string Conn, NewNameDTO dtonames, int contactid)
//    //    {
//    //        string Query = string.Empty;
//    //        //NameUpdateDTO dto = new NameUpdateDTO();

//    //        try
//    //        {
//    //            Query = "update " + AddDoubleQuotes(globalschema) + ".tbl_mst_contact set contact_name='" + dtonames.NewName + "',contact_surname='" + dtonames.NewSurname + "', contact_mailing_name='" + dtonames.NewMailingName + "' where tbl_mst_contact_id=" + contactid + "";

//    //            NPGSqlHelper.ExecuteNonQuery(Conn, System.Data.CommandType.Text, Query);
//    //        }
//    //        catch (Exception ex)
//    //        {
//    //            throw ex;
//    //        }

//    //    }
//    //    public void InsertGridDetails(string Conn, string globalschema, GridDTO griddto)
//    //    {
//    //        string query = string.Empty;
//    //        try
//    //        {
//    //            query = "INSERT INTO " + AddDoubleQuotes(globalschema) + ".tbl_mst_NameChange VALUES( " + griddto.Id + ",'" + griddto.SchemaName + "','" + griddto.LoginId + "',current_timestamp,'" + griddto.OldName + "','" + griddto.NewName + "','" + griddto.Reason + "'); ";
//    //            NPGSqlHelper.ExecuteNonQuery(Conn, System.Data.CommandType.Text, query);
//    //        }
//    //        catch (Exception Ex)
//    //        {
//    //            throw Ex;
//    //        }
//    //    }




//}






