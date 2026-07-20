using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Easychit_Infrastructure.ChangeDetails
{
    public class BranchNamesDTO
    {
        public string Branch_name { get; set; }
        public string branch_code { get; set; }
        public Int32 company_configuration_id { get; set; }
        public Int32 tbl_mst_branch_configuration_id { get; set; }
    }

    public class companyNamesDTO
    {
        public string company_name { get; set; }
        public string company_code { get; set; }
        public Int32 tbl_mst_chit_company_configuration_id { get; set; }
    }

    public class GroupCodeDTO
    {
        public string Group_Code { get; set; }
        public string chitgroup_id { get; set; }
    }

    public class TicketDTO
    {
        public int ticketno { get; set; }
    }

    public class MobileNoChangeDto
    {
        public int contact_id { get; set; }
        public Int64 newMobileNo { get; set; }
        public string ptypeofoperation { get; set; }
    }

    public class AddressChangeDto
    {
        public int contact_id { get; set; }
        public string NewAddress { get; set; }
        public Int64 NewPincode { get; set; }
        public string NewArea { get; set; }
        public string NewCity { get; set; }
        public string ptypeofoperation { get; set; }
    }

    public class NameChangeDto
    {
        public int contact_id { get; set; }
        public string NewName { get; set; }
        public string NewSurname { get; set; }
        public string NewMailingName { get; set; }
        public string ptypeofoperation { get; set; }
    }

    public class NameUpdateDTO
    {
        public int Ticket_No { get; set; }
        public int ContactId { get; set; }
        public string OldName { get; set; }
        public string chit_status { get; set; }
        public string OldSurname { get; set; }
        public string OldMailingName { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public int Pincode { get; set; }
    }

    public class ReportsofUpdatesDTO
    {
        public string LoginId { get; set; }
        public DateTime LoginDate { get; set; }
        public string ChangeType { get; set; }
        public string OldData { get; set; }
        public string NewData { get; set; }
        public string Reason { get; set; }
    }

    public class ChangeTypes
    {
        public string ChangeType { get; set; }
    }

    public class AgentCodeDto
    {
        public string referralCode { get; set; }
        public string AgentUniqueCode { get; set; }
        public string AgentName { get; set; }
        public long BusinessEntityContactNo { get; set; }
        public string BranchName { get; set; }
    }

    public class UpdateAgentBranchDto
    {
        public string ReferralCode { get; set; }
        public Int64 newBranchId { get; set; }
        public string ptypeofoperation { get; set; }

    }
    public class BranchNameDTO
    {
        public Int64 branchconfigurationid { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
    }

    public class ChequeDetailsDTO
    {
        public string payment_number { get; set; }
        public string reference_number { get; set; }
        public string clear_status { get; set; }
        public DateTime PayDate { get; set; }
        public string PaidTo { get; set; }
        public ulong Amount { get; set; }
        public DateTime? ClearDate { get; set; }
    }

    public class ChangeAuditDto
    {
        public string SchemaName { get; set; }
        public string LoginName { get; set; }
        public DateTime LoginTime { get; set; }
        public object OldData { get; set; }
        public object NewData { get; set; }
        public string Reason { get; set; }
        public string ChangeType { get; set; }
    }

    public class auctionschedule
    {
        public string auction_date { get; set; }
        public string auction_time { get; set; }
    }

    public class bidlosspermission
    {
        public Int64 ticketno { get; set; }
        public Int64 auction_number { get; set; }
    }

    public class vacantdto
    {
        public string branch_code { get; set; }
        public bool is_vacant_full_receipt_mandatory { get; set; }
    }

    public class multiplecontactdto
    {
        public string contact_reference_id { get; set; }
        public string business_entity_contactno { get; set; }
        public Int64 max_chits_per_contact { get; set; }
    }

    public class subsciberdto
    {
        public string subscriber_name { get; set; }
        public Int32 ticketno { get; set; }
        public ulong branch_id { get; set; }
        public string branch_code { get; set; }
        public string groupcode { get; set; }
        public string chit_status { get; set; }
        public Int64 total_count { get; set; }
    }

    public class chequesreturndto
    {
        public Int32 tbl_mst_chit_company_configuration_ID { get; set; }
        public Int64 chequereturn_charges_amount { get; set; }
    }

    public class dateschangedto
    {
        public int no_of_auctions { get; set; }
        public string? pso_number { get; set; }
        public DateTime? commencement_date { get; set; }
        public DateTime? termination_date { get; set; }
        public string? commencement_certificate_no { get; set; }
        public string groupcode { get; set; }
    }

    public class firstmemoapproveddto
    {
        public Int64 ticketno { get; set; }
        public string? groupcode { get; set; }
        public DateTime? approved_date { get; set; }
        public string? approved_file_name { get; set; }
    }

    public class sjvdto
    {
        public string transacion_no { get; set; }
        public string transaction_type { get; set; }
    }

    public class generalcancelreceiptdto
    {
        public string general_receipt_number { get; set; }
        public string groupcode { get; set; }
        public Int64 ticketno { get; set; }
        public Int64 bank_entries_id { get; set; }
        public Int64 contact_id { get; set; }
        public Int64 interbranch_id { get; set; }
        public Int64 chitgroup_id { get; set; }
        public Int64 comman_receipt_number { get; set; }
        public string caoname { get; set; }
        public string reference_number { get; set; }
        public string deposited_reference_no { get; set; }
        public string contact_name { get; set; }
        public decimal total_paid_amount { get; set; }
        public decimal ledger_amount { get; set; }
        public string receipt_date { get; set; }
        public string receipt_number { get; set; }
        public string transaction_no { get; set; }
        public Int64 tbl_trans_payment_voucher_id { get; set; }
        public Int64 tbl_trans_payment_voucher_details_id { get; set; }
        public Int64 tbl_trans_total_transactions_id { get; set; }
        public string payment_number { get; set; }
        public string payment_date { get; set; }
        public string account_type { get; set; }
        public decimal creditamount { get; set; }
        public decimal debitamount { get; set; }
    }

    public class ReceiptVoucherTransactionDTO
    {
        public long Tbl_Trans_Payment_Voucher_Details_Id { get; set; }
        public long Payment_Voucher_Id { get; set; }
        public long Detail_Debit_Account_Id { get; set; }
        public long Detail_Contact_Id { get; set; }
        public long Contact_Id { get; set; }
        public decimal Ledger_Amount { get; set; }
        public string payment_number { get; set; }
        public string contact_name { get; set; }
    }

    public class chequechangepsdto
    {
        public string branch_code { get; set; }
        public string groupcode { get; set; }
        public string chitgroup_status { get; set; }
        public string cheque_number { get; set; }
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public Int64 ticketno { get; set; }
        public Int64 tbl_trans_ps_cheques_id { get; set; }
    }
    public class MvCancelDTO
    {
        public string payment_number { get; set; }
        public DateTime? payment_date { get; set; }
        public decimal total_paid_amount { get; set; }
        public long tbl_trans_payment_voucher_id { get; set; }

        public long tbl_trans_payment_voucher_details_id { get; set; }
        public long contact_id { get; set; }
        public decimal ledger_amount { get; set; }
        public string contact_name { get; set; }
    }


    public class BusinessRefDTO
    {
        public int ContactId { get; set; }
        public int SubscriberId { get; set; }
        public string SubscriberName { get; set; }
        public string EmployeeName { get; set; }
        public long BusinessReferenceId { get; set; }
    }

    public class BusinessRefUpdateDTO
    {
        public long Chitgroup_id { get; set; }
        public int Ticketno { get; set; }
        public long SubscriberId { get; set; }
        public long OldBusinessReferenceId { get; set; }
        public string NewBusinessReferenceId { get; set; }
        public string Ptypeofoperation { get; set; }
    }
    public class PaymentReferenceDTO
    {
        public string payment_number { get; set; }
        public string reference_number { get; set; }
        public string payment_date { get; set; }
        public string clear_date { get; set; }
        public decimal paid_amount { get; set; }
        public string paid_to { get; set; }
        public long? tbl_trans_bank_entries_id { get; set; }
        public string transaction_no { get; set; }
        public long? tbl_trans_bank_entries_details_id { get; set; }
    }

    public class pannumberchnagedto
    {
        public string pan_number { get; set; }
        public string contact_name { get; set; }
        public string document_reference_no { get; set; }
        public long tbl_mst_contact_documents_id { get; set; }
        public long tbl_mst_contact_id { get; set; }
        public long document_proofs_id { get; set; }
        public long tbl_trans_chit_advance_id { get; set; }
        public long branch_id { get; set; }
        public string branch_code { get; set; }
        public string contact_reference_id { get; set; }
    }

    public class PaymentAdjustmentDTO
    {
        public string subscriber_name { get; set; }
        public decimal adjustment_amount { get; set; }
        public long tbl_trans_bpo_cheques_information_details_id { get; set; }
        public string payment_adjustment_type { get; set; }
        public long tbl_mst_contact_id { get; set; }
        public long tbl_mst_subscriber_id { get; set; }
        public long other_adjustment_type_id { get; set; }
        public string cheque_number { get; set; }
        public long tbl_trans_chit_payment_adjustments_id { get; set; }
    }

    public class reactiondatedto
    {
        public string subscriber_name { get; set; }
        public string receipt_number { get; set; }
        public string bidjv_transaction_no { get; set; }
        public string dividend_transaction_number { get; set; }
        public string reauction_date { get; set; }
        public string bidjv_transaction_date { get; set; }
        public string transaction_date { get; set; }
        public string dividend_date { get; set; }
        public Int64 from_ticketno { get; set; }
        public Int64 to_ticketno { get; set; }
        public string transaction_no { get; set; }
        public Int64 tbl_mst_contact_id { get; set; }
        public Int64 ticketno { get; set; }

    }


    public class guarantornamechangedto
    {
        public Int64 tbl_mst_contact_id { get; set; }
        public string contact_name { get; set; }
        public string contact_surname { get; set; }
        public string contact_mailing_name { get; set; }
        public string subscriber_name { get; set; }
        public string guarantor_name { get; set; }
        public string guarantor_namemvo { get; set; }

    }
    public class GuarantorNameChangeSaveDto
    {
        public long ContactId { get; set; }
        public long ChitgroupId { get; set; }
        public int Ticketno { get; set; }
        public string BranchSchema { get; set; } = string.Empty;
        public string NewName { get; set; } = string.Empty;
        public string NewSurname { get; set; } = string.Empty;
        public string NewMailingName { get; set; } = string.Empty;
    }

    public class GuarantorMvoDTO
    {
        public long TblMstGuarantorMvoId { get; set; }
        public long ContactId { get; set; }
        public string GuarantorName { get; set; }
        public string GuarantorSurname { get; set; }
        public string GuarantorMailingName { get; set; }
    }

    public class MvoAllotmentDTO
    {
        public long TblTransMvoAllotmentId { get; set; }
        public long TblMstGuarantorMvoId { get; set; }
    }

    public class MvoGuarantorDeleteRequestDTO
    {
        public string Branchschema { get; set; }
        public long Chitgroupid { get; set; }
        public int Ticketno { get; set; }
        public List<long> ContactIds { get; set; }
        public List<long> GuarantorMvoIds { get; set; }
    }

    public class GuarantorDTO
    {
        public long TblMstGuarantorId { get; set; }
        public long ContactId { get; set; }
        public string GuarantorName { get; set; }
    }

    public class GuarantorIncomeDTO
    {
        public long TblMstGuarantorIncomeId { get; set; }
        public long GuarantorId { get; set; }
    }

    public class BpoGuarantorDeleteRequestDTO
    {
        public string Branchschema { get; set; }
        public long Chitgroupid { get; set; }
        public int Ticketno { get; set; }
        public List<long> ContactIds { get; set; }
        public List<long> GuarantorIds { get; set; }
    }


    public class GuarantorDTO1
    {
        public long GuarantorId { get; set; }
        public long contact_id { get; set; }
        public string GuarantorName { get; set; }
        public string v_reference_id { get; set; }

    }
    public class bpoGuarantorDTO
    {
        public long GuarantorId { get; set; }
        public string GuarantorName { get; set; }
        public Int64 contact_id { get; set; }

    }

    public class finalsettlement
    {
        public string surety_name { get; set; }

        public List<FirstMemoDTO> FirstMemo { get; set; } = new List<FirstMemoDTO>();

        public List<FinalMemoDTO> FinalMemo { get; set; } = new List<FinalMemoDTO>();
    }

    public class FirstMemoDTO
    {
        public long? authorized_by { get; set; }
        public long? approved_by { get; set; }
        public DateTime? authorized_date { get; set; }
        public DateTime? approved_date { get; set; }
        public string file_name { get; set; }
        public string approved_file_name { get; set; }
    }

    public class FinalMemoDTO
    {
        public long? authorized_by { get; set; }
        public long? approved_by { get; set; }
        public DateTime? authorized_date { get; set; }
        public DateTime? approved_date { get; set; }
        public string file_name { get; set; }
        public string approved_file_name { get; set; }
    }


    public class chitcanceldto
    {
        public string NPS_GROUPCODE { get; set; }
    }


     public class suretynotshwingdto
    {
        public string subscriber_name { get; set; }
        public Int64 tbl_trans_mvo_surety_id { get; set; }
    }


     public class RECEIVEDDOCUMENTDTO
    {
        public string subscriber_name { get; set; }
        public string filled_surety_file { get; set; }
        public string svofilledverfieddocument { get; set; }
    }


     public class centralofficechitsdto
    {
        public string contact_mailing_name { get; set; }
        public string subscriber_name { get; set; }
        public string bank_branch_name { get; set; }
        public string chit_receipt_number { get; set; }
        public DateTime chit_receipt_date { get; set; }
        public string bank_account_number { get; set; }
        public Int64 contact_id { get; set; }
    }

    public class UpdateAdvanceInterestBankDTO
    {
        public long ContactId { get; set; }
        public long ChitGroupId { get; set; }
        public long TicketNo { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public long BranchId { get; set; }
        public long AdjustedBranchId { get; set; }
        public long AdjustedChitGroupId { get; set; }
        public long AdjustedTicketNo { get; set; }
        // public long UserId { get; set; }
    }

}