using System;
using System.Collections.Generic;
using System.Text;
using Easychit_Infrastructure.ChangeDetails;
using System.Threading.Tasks;

namespace Easychit_Repository.Interfaces.ChangeDetails
{
    public interface Ichange
    {
        List<BranchNamesDTO> GetBranchNames(string globalschema, string Conn);

        List<companyNamesDTO> GetcompanyNames(string globalschema, string Conn);

        List<GroupCodeDTO> GetGroupcodes(string branchschema, string Conn);

        List<TicketDTO> GetTickets(string schema, string con, string groupcode);

        NameUpdateDTO GetOldNameByTicketNo(string globalschema, string schema, string Conn, int ticketNo, int chitgroupid);

        bool UpdateNameByTicketNo(string globalschema, string Con, NameChangeDto dtoNames, string branchschema);

        List<ChangeTypes> GetChangeTypes(string globalschema, string Con);

        bool UpdateMoblieNoByContact(string globalschema, string Con, MobileNoChangeDto dto);

        bool UpdateAddressByContact(string globalschema, string Con, AddressChangeDto dto);

        List<AgentCodeDto> Agentcode(string globalschema, string Con);

        List<AgentCodeDto> GetReferralDetails(string schema, string connectionString, string referralCode);

        List<BranchNameDTO> GetBranchName(string globalschema, string Con);

        bool UpdateAgentBranch(string globalschema, string Con, UpdateAgentBranchDto dto);



        void SaveData(string globalschema, string Con, ChangeAuditDto audit);

        public List<ReportsofUpdatesDTO> GetUpdateReports(string globalschema, string Con, DateTime fromdate, DateTime todate, string changetype);


        List<auctionschedule> GetAuctionschedules(string Con, string branchschema, Int64 chitgroupid);

        void updatescheduledateandtime(string branchschema, string Con, string newauctiondate, string newauctiontime, Int64 chitgroupid);

        List<bidlosspermission> Getauctionnumber(string Con, string branchschema, Int64 chitgroupid);

        void updatebidlosspermission(string branchschema, string Con, string auction_number, string ticketno, Int64 chitgroupid);

        List<vacantdto> getvacantstatus(string Con, string globalschema);

        void updatevacantstatus(string globalschema, string Con, string branch_code);

        List<multiplecontactdto> getmaxchitcount(string Con, string globalschema, string? referenceid, string? contactno);

        void updatemultiplecontact(string globalschema, string Con, string referenceid, Int64 totalcount);

        List<subsciberdto> getsubscribername(string Con, string globalschema, string branchschema, Int32 ticketno, string groupcode, string branch_code);

        List<subsciberdto> getsubscribercount(string Con, string globalschema, Int32 ticketno, string groupcode);

        void deletelegalcell(string globalschema, string Con, string groupcode, Int32 ticketno);

        List<chequesreturndto> getchequesreturncharges(string Con, string globalschema, string company_code);

        void updatechequesreturncharges(string globalschema, string Con, Int32 tbl_mst_chit_company_configuration_ID, Int32 chequereturn_charges_amount);

        List<dateschangedto> GetDatesChangeDetails(string branchschema, string Con, string groupcode);

        void UpdateDatesChangeDetails(string branchschema, string Con, dateschangedto dto);


        List<firstmemoapproveddto> getapprovedgroupcodes(string globalschema, string Conn, string branchschema, string branchcode);

        List<firstmemoapproveddto> getapproveddetails(string globalschema, string Conn, string groupcode, int ticketno);

        bool RemoveFirstMemo(string globalschema, string groupCode, int ticketNo, string connectionString, out string message);


        List<TicketDTO> GetTickets1(string globalschema, string Conn, string groupcode, string branchcode, string branchschema);

        List<sjvdto> getsjvno(string globalschema, string Conn, string branchschema);

        void updatesjvno(string branchschema, string Con, string transaction_no);

        List<generalcancelreceiptdto> getgeneralreceiptnumbers(string globalschema, string Conn, string branchschema, string branchcode);

        List<generalcancelreceiptdto> getgeneralreceiptdetails(string globalschema, string Conn, string branchschema, string general_receipt_number, long interbranch_id);

        List<generalcancelreceiptdto> getgeneralreceiptamount(string Conn, string branchschema, string deposited_reference_no);

        List<ReceiptVoucherTransactionDTO> GetReceiptVoucherTransactions(string branchSchema, string connectionString, string generalReceiptNumber, long contactId, string globalschema);

        public string ReverseReceipt(string caoschema, string globalschema, string branchschema, string groupcode, long ticketno, long contactid, string generalreceiptnumber, long commonreceiptnumber, string paymentnumber, string paymentdate, long paymentvoucherid, long paymentvoucherdetailsid, string Conn);

        List<generalcancelreceiptdto> getchequegeneralnumbers(string globalschema, string Conn, string branchschema, string branchcode);

        List<generalcancelreceiptdto> getchequegeneralnumbersdetails(string globalschema, string Conn, string branchschema, string general_receipt_number);

        List<generalcancelreceiptdto> GetchequeliveDetails(string branchschema, string globalschema, string general_receipt_number, string reference_number, long interbranch_id, string Conn);

        string ReverseChequeDeposit(string globalschema, string branchschema, string caoschema, long chitgroupid, long ticketno, long contactid, string trimnumber, string referencenumber, string receiptnumber, long bankentriesid, string transactionno, string Conn);

        List<chequechangepsdto> GetcommencedGroupcodes(string branchschema, string Conn, string globalschema, string branch_code);

        List<chequechangepsdto> GetcomencedgroupcodeTickets(string branchschema, string Conn, string groupcode);

        List<chequechangepsdto> Getcomencedchequenumberdetails(string globalschema, string branchschema, string Conn, string groupcode, string branch_code, long tickeno);

        string DeletePSCheques(string branchschema, string chequenumbers, string chequeids, string Conn);

        List<MvCancelDTO> GetMvNumbers(string connectionString, string branchschema, string globalschema, string branchcode);
        List<MvCancelDTO> GetMvdetails(string connectionString, string branchschema, string paymentnumber);
        List<MvCancelDTO> GetMvnumberdetails(string connectionString, string branchschema, string paymentnumber, string globalschema, long paymentvoucherid);
        string ReverseMvVoucher(
            string connectionString,
            string branchschema,
            string globalschema,
            string payment_number,
            DateTime payment_date,
            long paymentvoucherid,
            long contactid,
            long paymentvoucherdetailsid
        );


        BusinessRefDTO GetBusinessRefByTicket(string globalschema, string schema, string Conn, int ticketNo, int chitgroupid);

        bool UpdateBusinessRef(string schema, string Con, BusinessRefUpdateDTO dto);


        List<ChequeDetailsDTO> GetChequenumbers(string branchschema, string Con, string globalschema);


        List<PaymentReferenceDTO> GetchequeDetails(string branchschema, string paymentnumber, string referencenumber, string Conn, string clearstatus);

        string ClearDateandStatus(string branchschema, string paymentnumber, string referencenumber, string transactionno, long bankentriesid, string Conn, string clearstatus);

        List<pannumberchnagedto> Getpannumbers(string connectionString, string globalschema);

        List<pannumberchnagedto> GetContactDocumentDetails(
string connectionString,
string panNumber, string globalschema);

        string UpdatePanNumber(
            string connectionString,
            string branchschema,
            string oldPanNumber,
            string newPanNumber,
            string contactReferenceId,
            long contactId,
            string documentIds,
            string globalschema);

        List<PaymentAdjustmentDTO> GetPaymentAdjustmentDetails(
string connectionString,
string branchschema,
string globalschema,
string groupcode,
int ticketno);

        List<PaymentAdjustmentDTO> GetSubscriberDetails(
        string connectionString,
        string branchschema,
        string globalschema,
        string groupcode,
        int ticketno);

        string UpdateAdjustmentDetails(
        string connectionString,
        string branchschema,
        long adjustmentChitGroupId,
        long adjustmentTicketNo,
        long adjustmentContactId,
        string bpoChequeInformationIds,
        string bpoChequeInformationDetailIds,
        decimal adjustmentAmount,
        string chitPaymentAdjustmentIds);

        string InsertSubscriberIncome(
        string connectionString,
        string branchschema,
        long subscriberId,
        int branch_id);

        List<reactiondatedto> GetreauctionSubscriberDetails(
            string connectionString,
            string branchschema,
            string globalschema,
            string groupcode,
            int ticketno, string branch_code);


        List<reactiondatedto> GetreauctionDetails(
        string connectionString,
        string branchschema,
        int from_ticketno, int to_ticketno);

        List<reactiondatedto> Getreauctiondivdenddetails(
        string connectionString,
        string branchschema, string transaction_no);

        List<guarantornamechangedto> GetnamechangeDetails(
        string connectionString, string branchschema, string globalschema, string groupcode,
        int ticketno, string branch_code);

        // bool SaveGuarantorNameChange(string connectionString, string branchschema, GuarantorNameChangeSaveDto dto);

        string UpdateSubscriberGuarantorName(string branchschema, string globalschema, long chitgroupid, long ticketno, long contactid, string contactname, string contactmailingname, string Conn, string contactsurname);


        string UpdateReauctionTransactionDates(string branchschema, long chitgroupid, long toticketno, long ticketno, string reauctiondate, string oldtransactiondate, string createddate, string subscriberjvno, string dividendtransactionnos, string bidjvtransactionnos, string Conn);


        // List<GuarantorMvoDTO> GetGuarantorMvoDetails(string globalschema, string schema, string Conn, long chitgroupid, int ticketNo);

        // List<MvoAllotmentDTO> GetMvoAllotmentDetails(string schema, string Conn, List<long> guarantorMvoIds);

        // bool DeleteMvoGuarantorRecords(string schema, string Conn, MvoGuarantorDeleteRequestDTO request);
        // List<GuarantorDTO> GetGuarantorDetails(string schema, string Conn, long chitgroupid, int ticketNo);

        // List<GuarantorIncomeDTO> GetGuarantorIncomeDetails(string schema, string Conn, List<long> guarantorIds);

        // bool DeleteBpoGuarantorRecords(string schema, string Conn, BpoGuarantorDeleteRequestDTO request);


        
        List<GuarantorDTO1> GetGuarantormvoDetails(string branchSchema, string globalSchema, string branchCode, string groupCode, long ticketNo, string connectionString);

        List<GuarantorDTO1> GetMVOReferenceIds(string branchSchema, long guarantorIds, string connectionString);

        string DeleteGuarantorDetails(string branchSchema, long chitGroupId, long ticketNo, string contactIds, string guarantorMVOIds, string connectionString);


         List<bpoGuarantorDTO> GetGuarantorbpoDetails(string branchSchema, string globalSchema, string branchCode, string groupCode, long ticketNo, string connectionString);

        string DeletepdoGuarantorDetails(string branchSchema, long chitGroupId, long ticketNo, string contactIds, string guarantorMVOIds, string connectionString);

        
        List<finalsettlement> Getfinalsetlement(string branchSchema, string globalSchema, string groupCode, long ticketNo, string connectionString);

        // List<finalsettlement> Getfinalsetlementdetails(string surety_name, string globalSchema, string groupCode, long ticketNo, string connectionString);

        List<finalsettlement> Getfinalsetlementdetails(string surety_name, string globalSchema, string groupCode, long ticketNo, string connectionString);


        string DeleteFinalMemoDetails(string branchSchema, string globalSchema, long chitGroupId, long ticketNo, string groupCode, string Conn);

        List<chitcanceldto> Getchitpledgecancel(string branchSchema, string globalSchema, string groupCode, long ticketNo, string connectionString);

        string DeleteChitPledgeAndSecurityLienDetails(string branchSchema, string globalSchema, string psGroupCode, long psTicketNo, string npsGroupCode, string Conn);



        List<suretynotshwingdto> Getsuretyname(string branchSchema, string globalSchema, string branchCode, string groupCode, long ticketNo, string connectionString);

        List<suretynotshwingdto> Getsuretynamedetails(string globalSchema, string groupCode, long ticketNo, string connectionString);

         string DeleteMvoSvoSuretyDetails(string branchSchema, string globalSchema, string groupCode, long ticketNo, long mvoSuretyId, long chitGroupId, string Conn);


         List<RECEIVEDDOCUMENTDTO> GetRECEIVEDDOCUMENT(string branchSchema,string globalSchema, string connectionString, string groupCode, Int64 ticketNo);


        string DeleteKgmsMvoSvoDetails(string branchSchema, string globalSchema, string groupCode, long ticketNo, bool fileDownloadStatus, string Conn);

         List<centralofficechitsdto> Getcentralofficechitsdetails( string connectionString, string branchschema, string globalschema, string groupcode, Int64 ticketno, string branch_code);


        List<BranchNamesDTO> GetcoBranchNames(string globalschema, string Conn);

        string UpdateAdvanceInterestPaymentBank(string branchSchema, UpdateAdvanceInterestBankDTO obj, string Conn);

    }
}