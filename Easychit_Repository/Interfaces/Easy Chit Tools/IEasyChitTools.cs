////using System;
////using System.Collections.Generic;
////using Easychit_Infrastructure.Easy_Chit_Tools; // Only use Infrastructure DTOs
////using System.Threading.Tasks;
////using Easychit_Infrastructure.ChangeDetails;

////namespace Easychit_Repository.Interfaces.Easy_Chit_Tools
////{
////    public interface IEasyChitTools
////    {
////        // Branch related methods
////        List<BranchNamesDTO> GetBranchNames(string globalschema, string Conn);
////        List<GroupCodeDTO> GetGroupcodes(string branchschema, string Conn);
////        List<TicketDTO> GetTickets(string schema, string con, string groupcode);

////        // Name update related methods
////        NameUpdateDTO GetOldNameByTicketNo(string globalschema, string schema, string Conn, int ticketNo, int chitgroupid);
////        void UpdateNameByTicketNo(string globalschema, string Conn, Easychit_Infrastructure.Easy_Chit_Tools.NameChangeDto dtoNames, string branchschema);


////        // Grid / audit
////        void InsertGridDetails(string Conn, string globalschema, GridDTO griddto);
////        List<ReportsofUpdatesDTO> GetUpdateReports(string globalschema, string Con, DateTime fromdate, DateTime todate, string changetype);
////        List<ChangeTypes> GetChangeTypes(string globalschema, string Con);
////        void UpdateMoblieNoByContact(string globalschema, string Conn, string contactid, Easychit_Infrastructure.Easy_Chit_Tools.NameChangeDto dto);
////        void UpdateAddressByContact(string globalschema, string Conn, string contactid, Easychit_Infrastructure.Easy_Chit_Tools.NameChangeDto dto);

////        // Referral / agent / branch methods
////        List<ReferralDetailsDto> Agentcode(string globalschema, string Con);
////        List<ReferralDetailsDto> GetReferralDetails(string schema, string connectionString, string referralCode);
////        void UpdateAgentBranch(string schema, string connectionString, UpdateAgentBranchDto dto);
////        List<BranchNameDTO> GetBranchName(string globalschema, string Con);

////        // Cheque / payment methods
////        List<ChequeDetailsDTO> GetChequeDetails(string branchschema, string Con, string checkreferno);
////        void ClearDateandStatus(string branchschema, string Con, string checkreferno, string paymentno);

////        // Audit
////        void SaveData(string globalschema, string Con, ChangeAuditDto audit);
////       // void UpdateNameByTicketNo(string globalSchema, string con, List<Easychit_Infrastructure.Easy_Chit_Tools.NameChangeDto > dtoNames, string branchschema);
////       // void UpdateNameByTicketNo(string globalSchema, string con, List<NameChangeDto> dtoNames, NameChangeDto dtonames);
////        void UpdateAddressByContact(string globalSchema, string con, string contactid, NameChangeDto dto);
////        void UpdateMoblieNoByContact(string globalschema, string Conn, string contactid, Interfaces.Easy_Chit_Tools.NameChangeDto dto);
////        void UpdateNameByTicketNo(string globalSchema, string con, List<NameChangeDto> dtoNames, NameChangeDto dtonames);
////       // void UpdateNameByTicketNo(string globalSchema, string con, List<NameChangeDto> dtoNames, NameChangeDto dtonames);
////      // void UpdateNameByTicketNo(string globalSchema, string con, List<NameChangeDto> dtoNames, NameChangeDto dtonames);
////     // void UpdateMoblieNoByContact(string globalSchema, string con, string contactid, ChangeDTO.NameChangeDto dto);
////    //   void UpdateNameByTicketNo(string globalSchema, string con, List<ChangeDTO.NameChangeDto> dtoNames, string branchschema);
////        void UpdateNameByTicketNo1(string globalSchema, string con, NameChangeDto dtonames);
////       // void UpdateMoblieNoByContact(string globalSchema, string con, string contactid, ChangeDTO.NameChangeDto dto);
////        void UpdateNameByTicketNo(string globalSchema, string con, NameChangeDto dtoName, string branchschema);
////        void UpdateNameByTicketNo(string globalSchema, string con, List<NameChangeDto> dtoNames, string branchschema);
////        bool SaveNameChange(string globalSchema, string con, NameChangeSaveDTO dto);
////        void UpdateAddressByContact(string globalSchema, string con, string contactid, ChangeDTO.NameChangeDto dto);
////    }
////}

//using System;
//using System.Collections.Generic;
//using System.Text;
//using Easychit_Infrastructure.Easy_Chit_Tools;
//using System.Threading.Tasks;

//namespace Easychit_Repository.Interfaces.Easy_Chit_Tools
//{
//    public interface IEasyChitTools
//    {

//        List<BranchNamesDTO> GetBranchNames(string globalschema, string Conn);


//        List<GroupCodeDTO> GetGroupcodes(string branchschema, string Conn);


//        List<TicketDTO> GetTickets(string schema, string con, string groupcode);


//        NameUpdateDTO GetOldNameByTicketNo(string globalschema, string schema, string Conn, int ticketNo, int chitgroupid);


//        //void UpdateNameByTicketNo(string globalschema, string Conn, List<NameChangeDto> dtoNames, string branchschema);


//        //void UpdateNameByTicketNo1(string globalschema, string Conn, NameChangeDto dtonames);


//        void InsertGridDetails(string Conn, string globalschema, GridDTO griddto);

//        List<ReportsofUpdatesDTO> GetUpdateReports(string globalschema, string Con, DateTime fromdate, DateTime todate, string changetype);
//        List<ChangeTypes> GetChangeTypes(string globalschema, string Con);
//        //void UpdateMoblieNoByContact(string globalschema, string Conn, string contactid, NameChangeDto dto);

//       // void UpdateAddressByContact(string globalschema, string Conn, string contactid, NameChangeDto dto);

//        //void InsertGridDetails(string Conn,string globalschema,GridDTO griddto);
//        List<ReferralDetailsDto> Agentcode(string globalschema, string Con);
//        List<ReferralDetailsDto> GetReferralDetails(string schema, string connectionString, string referralCode);
//        void UpdateAgentBranch(string schema, string connectionString, UpdateAgentBranchDto dto);
//        List<BranchNameDTO> GetBranchName(string globalschema, string Con);
//        List<ChequeDetailsDTO> GetChequeDetails(string branchschema, string Con, string checkreferno);
//        void ClearDateandStatus(string branchschema, string Con, string checkreferno, string paymentno);
//        void SaveData(string globalschema, string Con, ChangeAuditDto audit);
//    }
//}



