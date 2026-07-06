//using System;
//using System.Text.Json;

//namespace Easychit_Infrastructure.Easy_Chit_Tools
//{

//    public class BranchNamesDTO
//    {
//        public string Branch_name { get; set; }
//        public string branch_code { get; set; }
//    }


//    public class GroupDTO
//    {
//        public string branch_code { get; set; }
//    }


//    public class GroupCodeDTO
//    {
//        public string Group_Code { get; set; }
//        public string chitgroup_id { get; set; }
//    }


//    public class TicketDTO
//    {
//        public int ticketno { get; set; }
//    }


//    public class NameUpdateDTO
//    {
//        public int Ticket_No { get; set; }
//        public int ContactId { get; set; }

//        public string OldName { get; set; }
//        public string OldSurname { get; set; }
//        public string OldMailingName { get; set; }

//        public string MobileNo { get; set; }
//        public string Address { get; set; }
//        public string Area { get; set; }
//        public string City { get; set; }
//        public int Pincode { get; set; }
//    }



//    public class NewNameJsonDto
//    {
//        public string newName { get; set; }
//        public string newSurname { get; set; }
//    }

//    public class OldNameDto
//    {
//        public string oldName { get; set; }
//        public string oldSurname { get; set; }
//    }


//    public class GridDTO
//    {
//        public string SchemaName { get; set; }
//        public string LoginId { get; set; }
//        public DateTime Date { get; set; }

//        public JsonElement OldData { get; set; }
//        public JsonElement NewData { get; set; }
//        public string Reason { get; set; }
//        public string vchtype { get; set; }
//    }

//    public class AgentBranchIDChangeDTO
//    {
//        public string referral_code { get; set; }
//        public string AGENT_UNIQUE_CODE { get; set; }
//        public string AGENT_Name { get; set; }

//        public long MobileNo { get; set; }
//        public string Branch_name { get; set; }
//        public string Reason { get; set; }
//        public string vchtype { get; set; }

//        public string New_Branch_name { get; set; }

//    }
//    public class ReportsofUpdatesDTO
//    {
//        public string LoginId { get; set; }
//        public DateTime LoginDate { get; set; }
//        public string ChangeType { get; set; }
//        public string OldData { get; set; }
//        public string NewData { get; set; }
//        public string Reason { get; set; }
//    }
//    public class ChangeTypes
//    {
//        public string ChangeType { get; set; }
//    }
//    public class AgentCodeDTO
//    {
//        public string ReferralCode { get; set; }
//    }
//    public class ReferralDetailsDto
//    {
//        public string referralCode { get; set; }
//        public string AgentUniqueCode { get; set; }
//        public string AgentName { get; set; }
//        public long BusinessEntityContactNo { get; set; }
//        public string BranchName { get; set; }
//    }

//    public class UpdateAgentBranchDto
//    {
//        public string ReferralCode { get; set; }
//        public string NewBranchCode { get; set; }
//        public string Reason { get; set; }
//        public object LoginName { get; set; }
//    }
//    public class BranchNameDTO
//    {
//        public string BranchName { get; set; }
//        public string BranchCode { get; set; }
//    }
//    public class ChequeDetailsDTO
//    {
//        public string PaymentNo { get; set; }
//        public DateTime PayDate { get; set; }
//        public string PaidTo { get; set; }
//        public ulong Amount { get; set; }
//        public DateTime? ClearDate { get; set; }
//    }
//    public class ChangeAuditDto
//    {
//        public string SchemaName { get; set; }
//        public string LoginName { get; set; }
//        public DateTime LoginTime { get; set; }
//        public JsonElement OldData { get; set; }
//        public JsonElement NewData { get; set; }
//        public string Reason { get; set; }
//        public string ChangeType { get; set; }
//    }

//    public class NameChangeDto
//    {
//        public string contact_id;

//        public int TicketNo { get; set; }
//        public int ContactId { get; set; }
//        public string OldName { get; set; }
//        public string OldSurname { get; set; }
//        public string NewName { get; set; }
//        public string NewSurname { get; set; }
//        public string MobileNo { get; set; }
//        public string Address { get; set; }
//        public string Reason { get; set; }
//        public string BranchSchema { get; set; }
//        public object NewMailingName { get; set; }
//        public object NewCity { get; set; }
//        public object NewAddress { get; set; }
//        public object NewMobileNo { get; set; }
//        public object NewPincode { get; set; }
//        public object NewArea { get; set; }
//    }

//    public class NameChangeSaveDTO
//    {
//        public int TicketNo { get; set; }
//        public int ContactId { get; set; }
//        public string OldName { get; set; }
//        public string NewName { get; set; }
//        public string Reason { get; set; }
//        public string ChangeType { get; set; }
//        public int Contact_Id { get; set; }
//    }
//}




//using System;
//using System.Text.Json;

//namespace Easychit_Infrastructure.Easy_Chit_Tools
//{

//    public class BranchNamesDTO
//    {
//        public string Branch_name { get; set; }
//        public string branch_code { get; set; }
//    }


//    public class GroupDTO
//    {
//        public string branch_code { get; set; }
//    }


//    public class GroupCodeDTO
//    {
//        public string Group_Code { get; set; }
//        public string chitgroup_id { get; set; }
//    }


//    public class TicketDTO
//    {
//        public int ticketno { get; set; }
//    }


//    public class NameUpdateDTO
//    {
//        public int Ticket_No { get; set; }
//        public int ContactId { get; set; }

//        public string OldName { get; set; }
//        public string OldSurname { get; set; }
//        public string OldMailingName { get; set; }

//        public string MobileNo { get; set; }
//        public string Address { get; set; }
//        public string Area { get; set; }
//        public string City { get; set; }
//        public int Pincode { get; set; }
//    }


//    public class NameChangeDto
//    {
//        public int contact_id { get; set; }
//        public string NewName { get; set; }
//        public string Name { get; set; }
//        public string NewSurname { get; set; }
//        public string group_Code { get; set; }
//        public string NewMailingName { get; set; }

//        public OldNameDto oldNameJson { get; set; }
//        public NewNameJsonDto newNameJson { get; set; }

//        public long NewMobileNo { get; set; }
//        public string NewAddress { get; set; }
//        public string NewArea { get; set; }
//        public string NewCity { get; set; }
//        public int NewPincode { get; set; }
//        public string changeType { get; set; }
//        public object dateOfChange { get; set; }

//        public string reason { get; set; }
//    }
//    public class NewNameJsonDto
//    {
//        public string newName { get; set; }
//        public string newSurname { get; set; }
//    }

//    public class OldNameDto
//    {
//        public string oldName { get; set; }
//        public string oldSurname { get; set; }
//    }


//    public class GridDTO
//    {
//        public string SchemaName { get; set; }
//        public string LoginId { get; set; }
//        public DateTime Date { get; set; }

//        public JsonElement OldData { get; set; }
//        public JsonElement NewData { get; set; }
//        public string Reason { get; set; }
//        public string vchtype { get; set; }
//    }

//    public class AgentBranchIDChangeDTO
//    {
//        public string referral_code { get; set; }
//        public string AGENT_UNIQUE_CODE { get; set; }
//        public string AGENT_Name { get; set; }

//        public long MobileNo { get; set; }
//        public string Branch_name { get; set; }
//        public string Reason { get; set; }
//        public string vchtype { get; set; }

//        public string New_Branch_name { get; set; }

//    }
//    public class ReportsofUpdatesDTO
//    {
//        public string LoginId { get; set; }
//        public DateTime LoginDate { get; set; }
//        public string ChangeType { get; set; }
//        public string OldData { get; set; }
//        public string NewData { get; set; }
//        public string Reason { get; set; }
//    }
//    public class ChangeTypes
//    {
//        public string ChangeType { get; set; }
//    }
//    public class AgentCodeDTO
//    {
//        public string ReferralCode { get; set; }
//    }
//    public class ReferralDetailsDto
//    {
//        public string referralCode { get; set; }
//        public string AgentUniqueCode { get; set; }
//        public string AgentName { get; set; }
//        public long BusinessEntityContactNo { get; set; }
//        public string BranchName { get; set; }
//    }

//    public class UpdateAgentBranchDto
//    {
//        public string ReferralCode { get; set; }
//        public string NewBranchCode { get; set; }
//        public string Reason { get; set; }
//    }
//    public class BranchNameDTO
//    {
//        public string BranchName { get; set; }
//        public string BranchCode { get; set; }
//    }
//    public class ChequeDetailsDTO
//    {
//        public string PaymentNo { get; set; }
//        public DateTime PayDate { get; set; }
//        public string PaidTo { get; set; }
//        public ulong Amount { get; set; }
//        public DateTime? ClearDate { get; set; }
//    }
//    public class ChangeAuditDto
//    {
//        public string SchemaName { get; set; }
//        public string LoginName { get; set; }
//        public DateTime LoginTime { get; set; }
//        public JsonElement OldData { get; set; }
//        public JsonElement NewData { get; set; }
//        public string Reason { get; set; }
//        public string ChangeType { get; set; }
//    }



//}




