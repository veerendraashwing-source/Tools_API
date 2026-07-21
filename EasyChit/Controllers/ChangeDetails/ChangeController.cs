using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Easychit_Infrastructure.ChangeDetails;
using Microsoft.AspNetCore.Cors;
using Easychit_Repository.DataAccess.ChangeDetails;
using Easychit_Repository.Interfaces.ChangeDetails;

namespace Easychit_Api.Controllers.ChangeDetails
{
    [Route("api/[controller]")]
    [ApiController]

    public class ChangeController : ControllerBase
    {
        string Con = string.Empty;
        string GlobalSchema = string.Empty;
        string Schema = string.Empty;
        string db = string.Empty;
        string Url = string.Empty;
        static IConfiguration _iconfiguration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<ChangeController> _logger;
        Ichange _easychittools = new ChangeDAL();

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChangeController(
            IConfiguration iconfiguration,
            IWebHostEnvironment hostingEnvironment,
            ILogger<ChangeController> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _iconfiguration = iconfiguration;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
            db = _httpContextAccessor.HttpContext.Request.Headers["database"].ToString();
            Con = _iconfiguration.GetConnectionString("Connection1");
            GlobalSchema = _iconfiguration.GetSection("ConnectionStrings").GetSection("GlobalSchemeName").Value;
            Schema = _iconfiguration.GetSection("ConnectionStrings").GetSection("SchemeName").Value;
            Url = _iconfiguration.GetSection("ApplicationUrl").GetSection("URL").Value;
        }

        #region Branch & Group APIs

        [HttpGet]
        [Route("GetBranchNames")]
        public IActionResult GetBranchNames()
        {
            List<BranchNamesDTO> branchList = new List<BranchNamesDTO>();
            try
            {
                branchList = _easychittools.GetBranchNames(GlobalSchema, Con);
                return Ok(branchList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetcompanyNames")]
        public IActionResult GetcompanyNames()
        {
            List<companyNamesDTO> companyList = new List<companyNamesDTO>();
            try
            {
                companyList = _easychittools.GetcompanyNames(GlobalSchema, Con);
                return Ok(companyList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetGroupcodes")]
        public IActionResult GetGroupcodes(string branchschema)
        {
            List<GroupCodeDTO> groupList = new List<GroupCodeDTO>();
            try
            {
                groupList = _easychittools.GetGroupcodes(branchschema, Con);
                return Ok(groupList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetTickets")]
        public IActionResult GetTickets(string schema, string groupcode)
        {
            List<TicketDTO> ticketList = new List<TicketDTO>();
            try
            {
                ticketList = _easychittools.GetTickets(schema, Con, groupcode);
                return Ok(ticketList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetOldNameByTicketNo")]
        public IActionResult GetOldNameByTicketNo(string Schema, int ticketNo, int chitgroupid)
        {
            try
            {
                var result = _easychittools.GetOldNameByTicketNo(GlobalSchema, Schema, Con, ticketNo, chitgroupid);

                if (result == null || string.IsNullOrEmpty(result.OldName))
                    return StatusCode(StatusCodes.Status204NoContent);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        #endregion
        [HttpPost]
        [Route("UpdateMoblieNoByContact")]
        public bool UpdateMoblieNoByContact(MobileNoChangeDto dto)
        {
            bool Issaved;
            try
            {
                Issaved = _easychittools.UpdateMoblieNoByContact(GlobalSchema, Con, dto);
            }
            catch (Exception)
            {
                throw;
            }
            return Issaved;
        }

        [HttpPost]
        [Route("Updatenamebyticketno")]
        public bool updatenamebyticketno(NameChangeDto dtoNames, string branchschema)
        {
            bool Issaved;
            try
            {
                Issaved = _easychittools.UpdateNameByTicketNo(GlobalSchema, Con, dtoNames, branchschema);

            }
            catch (Exception)
            {
                throw;
            }
            return Issaved;

        }

        [HttpPost]
        [Route("UpdateAddressByContact")]
        public bool UpdateAddressByContact(AddressChangeDto dto)
        {
            bool Issaved;
            try
            {
                Issaved = _easychittools.UpdateAddressByContact(GlobalSchema, Con, dto);
            }
            catch (Exception)
            {
                throw;
            }
            return Issaved;
        }

        [HttpGet]
        [Route("GetReferralDetails")]
        public IActionResult GetReferralDetails(string referralCode)

        {
            List<AgentCodeDto> lstref = new List<AgentCodeDto>();
            try
            {
                lstref = _easychittools.GetReferralDetails(GlobalSchema, Con, referralCode);
                return lstref != null ? Ok(lstref) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
        }

        [HttpGet]
        [Route("Agentcode")]
        public IActionResult Agentcode()
        {
            List<AgentCodeDto> lstref = new List<AgentCodeDto>();
            try
            {
                lstref = _easychittools.Agentcode(GlobalSchema, Con);
                return lstref != null ? Ok(lstref) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
        }

        [HttpGet]
        [Route("GetChequeBranchName")]
        public IActionResult GetChequeBranchName()
        {
            List<BranchNameDTO> branchlist = new List<BranchNameDTO>();
            try
            {
                branchlist = _easychittools.GetBranchName(GlobalSchema, Con);

                return branchlist != null ? Ok(branchlist) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
        }

        [HttpPost]
        [Route("UpdateAgentBranch")]
        public bool UpdateAgentBranch(UpdateAgentBranchDto dtoNames)
        {
            bool Issaved;
            try
            {
                Issaved = _easychittools.UpdateAgentBranch(GlobalSchema, Con, dtoNames);

            }
            catch (Exception)
            {
                throw;
            }
            return Issaved;

        }

        [HttpPost]
        [Route("SaveData")]
        public IActionResult SaveData([FromBody] List<ChangeAuditDto> audits)
        {
            try
            {
                foreach (var audit in audits)
                {
                    _easychittools.SaveData(GlobalSchema, Con, audit);
                }

                return Ok(new { Message = "Name updated successfully" });
            }
            // catch (Exception)
            // {
            //     return StatusCode(StatusCodes.Status500InternalServerError);
            // }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = ex.Message,
                    InnerException = ex.InnerException?.Message
                });
            }
        }

        [HttpGet]
        [Route("GetChangeTypes")]
        public IActionResult GetChangeTypes()
        {
            List<ChangeTypes> lstct = new List<ChangeTypes>();
            try
            {
                lstct = _easychittools.GetChangeTypes(GlobalSchema, Con);
                return lstct != null ? Ok(lstct) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
        }

        [HttpGet]
        [Route("GetUpdateReports")]
        public IActionResult GetUpdateReports(DateTime fromdate, DateTime todate, string changetype)
        {
            List<ReportsofUpdatesDTO> lstupdatereports = new List<ReportsofUpdatesDTO>();
            try
            {
                lstupdatereports = _easychittools.GetUpdateReports(GlobalSchema, Con, fromdate, todate, changetype);

                return lstupdatereports != null ? Ok(lstupdatereports) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
        }

        [HttpGet("get-auction-schedules")]

        public IActionResult GetAuctionSchedules(string branchschema, Int64 chitgroupid)
        {
            List<auctionschedule> schedulelist = new List<auctionschedule>();
            try
            {
                schedulelist = _easychittools.GetAuctionschedules(Con, branchschema, chitgroupid);

                return Ok(schedulelist);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("update-auction-schedule")]
        public IActionResult UpdateAuctionSchedule(string branchschema, string newauctiondate, string newauctiontime, Int64 chitgroupid)
        {
            try
            {
                _easychittools.updatescheduledateandtime(branchschema, Con, newauctiondate, newauctiontime, chitgroupid);
                return Ok(new { Message = "Auction schedule updated successfully" });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("get-auction-numbers")]
        public IActionResult getauctionnumber(string branchschema, Int64 chitgroupid)
        {
            List<bidlosspermission> bidlosslist = new List<bidlosspermission>();
            try
            {
                bidlosslist = _easychittools.Getauctionnumber(Con, branchschema, chitgroupid);

                return Ok(bidlosslist);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("update-bidloss_permission")]
        public IActionResult Updatebidlosspermission(string branchschema, string auction_number, string ticketno, Int64 chitgroupid)
        {
            try
            {
                _easychittools.updatebidlosspermission(branchschema, Con, auction_number, ticketno, chitgroupid);
                return Ok(new { Message = "Auction schedule updated successfully" });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getvacantstatus")]
        public IActionResult getvacantstatus()
        {
            List<vacantdto> vacanlist = new List<vacantdto>();
            try
            {
                vacanlist = _easychittools.getvacantstatus(Con, GlobalSchema);
                return Ok(vacanlist);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("updatevacantstatus")]
        public IActionResult updatevacantstatus(string Branch_code)
        {
            try
            {
                _easychittools.updatevacantstatus(GlobalSchema, Con, Branch_code);
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(new { Message = "Vacant status updated successfully" });
        }

        [HttpGet("getmaxchitcount")]
        public IActionResult getmaxchitcount(string? referenceid, string? contactno)
        {
            List<multiplecontactdto> multiplecontactlist = new List<multiplecontactdto>();
            try
            {
                multiplecontactlist = _easychittools.getmaxchitcount(Con, GlobalSchema, referenceid, contactno);
                return Ok(multiplecontactlist);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("updatemultiplecontact")]
        public IActionResult updatemultiplecontact(string referenceid, Int64 totalcount)
        {
            try
            {
                _easychittools.updatemultiplecontact(GlobalSchema, Con, referenceid, totalcount);
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(new { Message = "Multiple contact updated successfully" });
        }

        [HttpGet("getsubscribername")]
        public IActionResult getsubscribername(string branchschema, Int32 ticketno, string groupcode, string branch_code)
        {
            List<subsciberdto> subscibertoList = new List<subsciberdto>();
            try
            {
                subscibertoList = _easychittools.getsubscribername(Con, GlobalSchema, branchschema, ticketno, groupcode, branch_code);
                return Ok(subscibertoList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getsubscribercount")]
        public IActionResult getsubscribercount(Int32 ticketno, string groupcode)
        {
            List<subsciberdto> subscibertoList = new List<subsciberdto>();
            try
            {
                subscibertoList = _easychittools.getsubscribercount(Con, GlobalSchema, ticketno, groupcode);
                return Ok(subscibertoList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("deletelegalcell")]
        public IActionResult deletelegalcell(string groupcode, Int32 ticketno)
        {
            try
            {
                _easychittools.deletelegalcell(GlobalSchema, Con, groupcode, ticketno);
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(new { Message = "Legal cell deleted successfully" });
        }

        [HttpGet("getchequesreturncharges")]
        public IActionResult getchequesreturncharges(string company_code)
        {
            List<chequesreturndto> chequesreturndtoList = new List<chequesreturndto>();
            try
            {
                chequesreturndtoList = _easychittools.getchequesreturncharges(Con, GlobalSchema, company_code);
                return Ok(chequesreturndtoList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("updatechequesreturncharges")]
        public IActionResult updatechequesreturncharges(Int32 tbl_mst_chit_company_configuration_ID, Int32 chequereturn_charges_amount)
        {
            try
            {
                _easychittools.updatechequesreturncharges(GlobalSchema, Con, tbl_mst_chit_company_configuration_ID, chequereturn_charges_amount);
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(new { Message = "Cheque return charges updated successfully" });
        }

        [HttpGet("get-dates-change")]
        public IActionResult GetDatesChange(string branchschema, string groupcode)
        {
            try
            {
                var result = _easychittools.GetDatesChangeDetails(branchschema, Con, groupcode);
                if (result == null || result.Count == 0)
                    return NoContent();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("update-dates-change")]
        public IActionResult UpdateDatesChange(string branchschema, [FromBody] dateschangedto dto)
        {
            try
            {
                if (dto == null || string.IsNullOrEmpty(dto.groupcode))
                    return BadRequest("Invalid data");

                _easychittools.UpdateDatesChangeDetails(branchschema, Con, dto);

                return Ok(new { Message = "Dates updated successfully" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("getapprovedgroupcodes")]
        public IActionResult getapprovedgroupcodes(string branchschema, string branchcode)
        {
            List<firstmemoapproveddto> branchList = new List<firstmemoapproveddto>();
            try
            {
                branchList = _easychittools.getapprovedgroupcodes(GlobalSchema, Con, branchschema, branchcode);
                return Ok(branchList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("getapproveddetails")]
        public IActionResult getapproveddetails(string groupcode, int ticketno)
        {
            List<firstmemoapproveddto> branchList = new List<firstmemoapproveddto>();
            try
            {
                branchList = _easychittools.getapproveddetails(GlobalSchema, Con, groupcode, ticketno);
                return Ok(branchList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("RemoveFirstMemo")]
        public IActionResult RemoveFirstMemo(string groupcode, int ticketno)
        {
            try
            {
                string message = string.Empty;

                bool result = _easychittools.RemoveFirstMemo(GlobalSchema, groupcode, ticketno, Con, out message);

                if (result)
                {
                    return Ok(new
                    {
                        success = true,
                        message = message
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = message
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("GetTickets1")]
        public IActionResult GetTickets1(string groupcode, string branchcode, string branchschema)
        {
            List<TicketDTO> ticketList = new List<TicketDTO>();
            try
            {
                ticketList = _easychittools.GetTickets1(GlobalSchema, Con, groupcode, branchcode, branchschema);
                return Ok(ticketList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("getsjvno")]
        public IActionResult getsjvno(string branchschema)
        {
            List<sjvdto> sjvnolist = new List<sjvdto>();
            try
            {
                sjvnolist = _easychittools.getsjvno(GlobalSchema, Con, branchschema);
                return Ok(sjvnolist);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("updatesjvno")]
        public IActionResult updatesjvno(string branchschema, string transaction_no)
        {
            try
            {
                _easychittools.updatesjvno(branchschema, Con, transaction_no);
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(new { Message = "sjv no updated successfully" });
        }

        [HttpGet]
        [Route("getgeneralreceiptnumbers")]
        public IActionResult getgeneralreceiptnumbers(string branchschema, string branchcode)
        {
            List<generalcancelreceiptdto> receiptList = new List<generalcancelreceiptdto>();
            try
            {
                receiptList = _easychittools.getgeneralreceiptnumbers(GlobalSchema, Con, branchschema, branchcode);
                return Ok(receiptList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getgeneralreceiptdetails")]
        public IActionResult getgeneralreceiptdetails(string branchschema, string general_receipt_number, long interbranch_id)
        {
            List<generalcancelreceiptdto> receiptList = new List<generalcancelreceiptdto>();
            try
            {
                receiptList = _easychittools.getgeneralreceiptdetails(GlobalSchema, Con, branchschema, general_receipt_number, interbranch_id);
                return Ok(receiptList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getgeneralreceiptamount")]
        public IActionResult getgeneralreceiptamount(string branchschema, string deposited_reference_no)
        {
            List<generalcancelreceiptdto> receiptList = new List<generalcancelreceiptdto>();
            try
            {
                receiptList = _easychittools.getgeneralreceiptamount(Con, branchschema, deposited_reference_no);
                return Ok(receiptList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("ReverseReceipt")]
        public IActionResult ReverseReceipt(string caoschema, string branchschema, string groupcode, long ticketno, long contactid, string generalreceiptnumber, long commonreceiptnumber, string paymentnumber, string paymentdate, long paymentvoucherid, long paymentvoucherdetailsid)
        {
            try
            {
                string result = _easychittools.ReverseReceipt(caoschema, GlobalSchema, branchschema, groupcode, ticketno, contactid, generalreceiptnumber, commonreceiptnumber, paymentnumber, paymentdate, paymentvoucherid, paymentvoucherdetailsid,

                    Con);

                return Ok(new
                {
                    message = "Success",
                    result = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("GetReceiptVoucherTransactions")]
        public IActionResult GetReceiptVoucherTransactions(string branchSchema, string generalReceiptNumber, long contactId)
        {
            List<ReceiptVoucherTransactionDTO> receiptList = new List<ReceiptVoucherTransactionDTO>();
            try
            {
                receiptList = _easychittools.GetReceiptVoucherTransactions(branchSchema, Con, generalReceiptNumber, contactId, GlobalSchema);

                return Ok(receiptList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getchequegeneralnumbers")]
        public IActionResult getchequegeneralnumbers(string branchschema, string branchcode)
        {
            List<generalcancelreceiptdto> receiptList = new List<generalcancelreceiptdto>();
            try
            {
                receiptList = _easychittools.getchequegeneralnumbers(GlobalSchema, Con, branchschema, branchcode);
                return Ok(receiptList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getchequegeneralnumbersdetails")]
        public IActionResult getchequegeneralnumbersdetails(string branchschema, string general_receipt_number)
        {
            List<generalcancelreceiptdto> receiptList = new List<generalcancelreceiptdto>();
            try
            {
                receiptList = _easychittools.getchequegeneralnumbersdetails(GlobalSchema, Con, branchschema, general_receipt_number);
                return Ok(receiptList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetchequeliveDetails")]
        public IActionResult GetchequeliveDetails(string general_receipt_number, string branchschema, long interbranch_id, string reference_number)
        {
            List<generalcancelreceiptdto> receiptList = new List<generalcancelreceiptdto>();
            try
            {
                receiptList = _easychittools.GetchequeliveDetails(branchschema, GlobalSchema, general_receipt_number, reference_number, interbranch_id, Con);
                return Ok(receiptList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("ReverseChequeDeposit")]
        public IActionResult ReverseChequeDeposit(string caoschema, string branchschema, long chitgroupid, long ticketno, long contactid, string trimnumber, string referencenumber, string receiptnumber, long bankentriesid, string transactionno)
        {
            string result = _easychittools.ReverseChequeDeposit(GlobalSchema, branchschema, caoschema, chitgroupid, ticketno, contactid, trimnumber, referencenumber, receiptnumber, bankentriesid, transactionno, Con);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetcommencedGroupcodes")]
        public IActionResult GetcommencedGroupcodes(string branchschema, string branch_code)
        {
            List<chequechangepsdto> groupList = new List<chequechangepsdto>();
            try
            {
                groupList = _easychittools.GetcommencedGroupcodes(branchschema, Con, GlobalSchema, branch_code);
                return Ok(groupList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetcomencedgroupcodeTickets")]
        public IActionResult GetcomencedgroupcodeTickets(string branchschema, string groupcode)
        {
            List<chequechangepsdto> ticketList = new List<chequechangepsdto>();
            try
            {
                ticketList = _easychittools.GetcomencedgroupcodeTickets(branchschema, Con, groupcode);
                return Ok(ticketList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("Getcomencedchequenumberdetails")]
        public IActionResult Getcomencedchequenumberdetails(string branchschema, string groupcode, string branch_code, long tickeno)
        {
            List<chequechangepsdto> deatilslist = new List<chequechangepsdto>();
            try
            {
                deatilslist = _easychittools.Getcomencedchequenumberdetails(GlobalSchema, branchschema, Con, groupcode, branch_code, tickeno);
                return Ok(deatilslist);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("DeletePSCheques")]
        public IActionResult DeletePSCheques(string branchschema, string chequenumbers, string chequeids)
        {
            try
            {
                string result = _easychittools.DeletePSCheques(branchschema, chequenumbers, chequeids, Con);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("GetMvNumbers")]
        public IActionResult GetMvNumbers(string branchschema, string branchcode)
        {
            try
            {
                var list = _easychittools.GetMvNumbers(Con, branchschema, GlobalSchema, branchcode);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetMvdetails")]
        public IActionResult GetMvdetails(string branchschema, string branchcode, string paymentnumber)
        {
            try
            {
                var list = _easychittools.GetMvdetails(Con, branchschema, paymentnumber);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetMvnumberdetails")]
        public IActionResult GetMvnumberdetails(string branchschema, string branchcode, string paymentnumber, long paymentvoucherid)
        {
            try
            {
                var list = _easychittools.GetMvnumberdetails(Con, branchschema, paymentnumber, GlobalSchema, paymentvoucherid);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        [HttpPost]
        [Route("ReverseMvVoucher")]
        public IActionResult ReverseMvVoucher(
            string branchschema,
            string payment_number,
            DateTime payment_date,
            long paymentvoucherid,
            long contactid,
            long paymentvoucherdetailsid)
        {
            try
            {
                string result = _easychittools.ReverseMvVoucher(
                    Con,
                    branchschema,
                    GlobalSchema,
                    payment_number,
                    payment_date,
                    paymentvoucherid,
                    contactid,
                    paymentvoucherdetailsid
                );

                if (result == "Success")
                    return Ok(new { success = true, message = "MV voucher reversed successfully." });
                else
                    return BadRequest(new { success = false, message = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        [Route("GetBusinessRefByTicket")]
        public IActionResult GetBusinessRefByTicket(string Schema, int ticketNo, int chitgroupid)
        {
            try
            {
                var result = _easychittools.GetBusinessRefByTicket(GlobalSchema, Schema, Con, ticketNo, chitgroupid);

                if (result == null || result.ContactId == 0)
                    return StatusCode(StatusCodes.Status204NoContent);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdateBusinessRef")]
        public bool UpdateBusinessRef(BusinessRefUpdateDTO dto, string branchschema)
        {
            bool Issaved;
            try
            {
                Issaved = _easychittools.UpdateBusinessRef(branchschema, Con, dto);
            }
            catch (Exception)
            {
                throw;
            }
            return Issaved;
        }


        [HttpGet]
        [Route("GetChequenumbers")]
        public IActionResult GetChequenumbers(string branchschema)
        {
            List<ChequeDetailsDTO> checkdetails = new List<ChequeDetailsDTO>();
            try
            {
                checkdetails = _easychittools.GetChequenumbers(branchschema, Con, GlobalSchema);

                return checkdetails != null ? Ok(checkdetails) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
        }
        [HttpGet]
        [Route("GetchequeDetails")]
        public IActionResult GetchequeDetails(string branchschema, string paymentnumber, string referencenumber, string clearstatus)
        {
            List<PaymentReferenceDTO> checkdetails = new List<PaymentReferenceDTO>();
            try
            {
                checkdetails = _easychittools.GetchequeDetails(branchschema, Con, paymentnumber, referencenumber, clearstatus);

                return checkdetails != null ? Ok(checkdetails) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
        }

        [HttpPost]
        [Route("ClearDateandStatus")]
        public IActionResult ClearDateandStatus(string branchschema, string paymentnumber, string referencenumber, string transactionno, long bankentriesid, string clearstatus)
        {

            try
            {
                _easychittools.ClearDateandStatus(branchschema, paymentnumber, referencenumber, transactionno, bankentriesid, Con, clearstatus);
                return Ok(new { Message = "Name updated successfully" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet]
        [Route("Getpannumbers")]
        public IActionResult Getpannumbers()
        {
            try
            {
                var list = _easychittools.Getpannumbers(Con, GlobalSchema);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet]
        [Route("GetContactDocumentDetails")]
        public IActionResult GetContactDocumentDetails(string panNumber)
        {
            try
            {
                var list = _easychittools.GetContactDocumentDetails(Con, panNumber, GlobalSchema);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost]
        [Route("UpdatePanNumber")]
        public IActionResult UpdatePanNumber(
    string branchschema,
    string oldPanNumber,
    string newPanNumber,
    string contactReferenceId,
    long contactId,
    string documentIds)
        {
            try
            {
                string result = _easychittools.UpdatePanNumber(
                    Con,
                    branchschema,
                    oldPanNumber,
                    newPanNumber,
                    contactReferenceId,
                    contactId,
                    documentIds,
                    GlobalSchema);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("GetPaymentAdjustmentDetails")]
        public IActionResult GetPaymentAdjustmentDetails(
    string branchschema,
    string groupcode,
    int ticketno)
        {
            try
            {
                var result = _easychittools.GetPaymentAdjustmentDetails(
                    Con,
                    branchschema,
                    GlobalSchema,
                    groupcode,
                    ticketno);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetSubscriberDetails")]
        public IActionResult GetSubscriberDetails(
    string branchschema,
    string groupcode,
    int ticketno)
        {
            try
            {
                Console.WriteLine(1234567);
                var result = _easychittools.GetSubscriberDetails(
                    Con,
                    branchschema,
                    GlobalSchema,
                    groupcode,
                    ticketno);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //     [HttpPost]
        //     [Route("UpdateAdjustmentDetails")]
        //     public IActionResult UpdateAdjustmentDetails(
        // string branchschema,
        // long adjustmentChitGroupId,
        // long adjustmentTicketNo,
        // long adjustmentContactId,
        // string bpoChequeInformationIds,
        // string bpoChequeInformationDetailIds,
        // decimal adjustmentAmount,
        // string chitPaymentAdjustmentIds)
        //     {
        //         try
        //         {
        //             var result = _easychittools.UpdateAdjustmentDetails(
        //                 Con,
        //                 branchschema,
        //                 adjustmentChitGroupId,
        //                 adjustmentTicketNo,
        //                 adjustmentContactId,
        //                 bpoChequeInformationIds,
        //                 bpoChequeInformationDetailIds,
        //                 adjustmentAmount,
        //                 chitPaymentAdjustmentIds);

        //             return Ok(result);
        //         }
        //         catch (Exception ex)
        //         {
        //             return BadRequest(ex.Message);
        //         }
        //     }

        [HttpPost]
        [Route("UpdateAdjustmentDetails")]
        public IActionResult UpdateAdjustmentDetails(
        string branchschema,
        long adjustmentChitGroupId,
        long adjustmentTicketNo,
        long adjustmentContactId,
        string bpoChequeInformationIds,
        string bpoChequeInformationDetailIds,
        decimal adjustmentAmount,
        string chitPaymentAdjustmentIds)
        {
            try
            {
                var result = _easychittools.UpdateAdjustmentDetails(
                    Con,
                    branchschema,
                    adjustmentChitGroupId,
                    adjustmentTicketNo,
                    adjustmentContactId,
                    bpoChequeInformationIds,
                    bpoChequeInformationDetailIds,
                    adjustmentAmount,
                    chitPaymentAdjustmentIds);

                return Ok(new
                {
                    success = true,
                    message = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost("InsertSubscriberIncome")]
        public IActionResult InsertSubscriberIncome(string branchschema, long subscriberId, int branch_id)
        {
            try
            {
                _easychittools.InsertSubscriberIncome(Con, branchschema, subscriberId, branch_id);
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(new { Message = "sjv no updated successfully" });
        }

        [HttpGet]
        [Route("GetreauctionSubscriberDetails")]
        public IActionResult GetreauctionSubscriberDetails(
    string branchschema,
    string groupcode,
    int ticketno, string branch_code)
        {
            try
            {
                var result = _easychittools.GetreauctionSubscriberDetails(
                    Con,
                    branchschema,
                    GlobalSchema,
                    groupcode,
                    ticketno, branch_code);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("GetreauctionDetails")]
        public IActionResult GetreauctionDetails(
    string branchschema,
    int from_ticketno, int to_ticketno)
        {
            try
            {
                var result = _easychittools.GetreauctionDetails(
                    Con,
                    branchschema,
                    from_ticketno,
                    to_ticketno);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("Getreauctiondivdenddetails")]
        public IActionResult Getreauctiondivdenddetails(
    string branchschema,
    string transaction_no)
        {
            try
            {
                var result = _easychittools.Getreauctiondivdenddetails(
                    Con,
                    branchschema, transaction_no);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("UpdateReauctionTransactionDates")]
        public IActionResult UpdateReauctionTransactionDates(string branchschema, long chitgroupid, long toticketno, long ticketno, string reauctiondate, string oldtransactiondate, string createddate, string subscriberjvno, string dividendtransactionnos, string bidjvtransactionnos)
        {
            try
            {
                string result = _easychittools.UpdateReauctionTransactionDates(branchschema, chitgroupid, toticketno, ticketno, reauctiondate, oldtransactiondate, createddate, subscriberjvno, dividendtransactionnos, bidjvtransactionnos, Con);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("GetnamechangeDetails")]

        public IActionResult GetnamechangeDetails(
        string branchschema,
        string groupcode,
        int ticketno, string branch_code)
        {
            try
            {
                var result = _easychittools.GetnamechangeDetails(
                    Con,
                    branchschema, GlobalSchema, groupcode, ticketno, branch_code);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("UpdateSubscriberGuarantorName")]
        public IActionResult UpdateSubscriberGuarantorName(string branchschema, long chitgroupid, long ticketno, long contactid, string contactname, string contactmailingname, string contactsurname)
        {
            try
            {
                string result = _easychittools.UpdateSubscriberGuarantorName(branchschema, GlobalSchema, chitgroupid, ticketno, contactid, contactname, contactmailingname, Con, contactsurname);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("GetGuarantormvoDetails")]
        public IActionResult GetGuarantormvoDetails(string BranchSchema, string BranchCode, string GroupCode, long TicketNo)
        {
            try
            {

                var result = _easychittools.GetGuarantormvoDetails(BranchSchema, GlobalSchema, BranchCode, GroupCode, TicketNo, Con);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetMVOReferenceIds")]
        public IActionResult GetMVOReferenceIds(long GuarantorIds, string branchSchema)
        {
            try
            {

                var result = _easychittools.GetMVOReferenceIds(
                                branchSchema,
                                GuarantorIds,
                                Con);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("DeleteGuarantorDetails")]
        public IActionResult DeleteGuarantorDetails(string branchSchema, long chitGroupId, long ticketNo, string contactIds, string guarantorMVOIds)
        {
            try
            {

                string result = _easychittools.DeleteGuarantorDetails(branchSchema, chitGroupId, ticketNo, contactIds, guarantorMVOIds, Con);

                // return Ok(result);

                return Ok(new
                {
                    message = "Deleted Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("GetGuarantorbpoDetails")]
        public IActionResult GetGuarantorbpoDetails(string BranchSchema, string BranchCode, string GroupCode, long TicketNo)
        {
            try
            {

                var result = _easychittools.GetGuarantorbpoDetails(BranchSchema, GlobalSchema, BranchCode, GroupCode, TicketNo, Con);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("DeletepdoGuarantorDetails")]
        public IActionResult DeletepdoGuarantorDetails(string branchSchema, long chitGroupId, long ticketNo, string contactIds, string guarantorMVOIds)
        {
            try
            {

                string result = _easychittools.DeletepdoGuarantorDetails(branchSchema, chitGroupId, ticketNo, contactIds, guarantorMVOIds, Con);

                return Ok(new
                {
                    message = "Deleted Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Getfinalsetlement")]
        public IActionResult Getfinalsetlement(string BranchSchema, string GroupCode, long TicketNo)
        {
            try
            {

                var result = _easychittools.Getfinalsetlement(BranchSchema, GlobalSchema, GroupCode, TicketNo, Con);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpGet]
        [Route("Getfinalsetlementdetails")]
        public List<finalsettlement> Getfinalsetlementdetails(string surety_name, string groupCode, long ticketNo)
        {
            try
            {
                var result = _easychittools.Getfinalsetlementdetails(surety_name, GlobalSchema, groupCode, ticketNo, Con);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving final settlement details: {ex.Message}");
            }
        }


        [HttpPost]
        [Route("DeleteFinalMemoDetails")]
        public IActionResult DeleteFinalMemoDetails(string branchSchema, long chitGroupId, long ticketNo, string groupCode)
        {
            try
            {
                string result = _easychittools.DeleteFinalMemoDetails(
                    branchSchema,
                    GlobalSchema,
                    chitGroupId,
                    ticketNo,
                    groupCode,
                    Con);

                // return Ok(result);
                return Ok(new
                {
                    message = "Deleted Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Getchitpledgecancel")]
        public IActionResult Getchitpledgecancel(string BranchSchema, string GroupCode, long TicketNo)
        {
            try
            {

                var result = _easychittools.Getchitpledgecancel(BranchSchema, GlobalSchema, GroupCode, TicketNo, Con);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("DeleteChitPledgeAndSecurityLienDetails")]
        public IActionResult DeleteChitPledgeAndSecurityLienDetails(string branchSchema, string psGroupCode, long psTicketNo, string npsGroupCode)
        {
            try
            {
                string result = _easychittools.DeleteChitPledgeAndSecurityLienDetails(
                    branchSchema,
                    GlobalSchema,
                    psGroupCode,
                    psTicketNo,
                    npsGroupCode,
                    Con
                );

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Getsuretyname")]
        public IActionResult Getsuretyname(string BranchSchema, string BranchCode, string GroupCode, long TicketNo)
        {
            try
            {

                var result = _easychittools.Getsuretyname(BranchSchema, GlobalSchema, BranchCode, GroupCode, TicketNo, Con);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Getsuretynamedetails")]
        public IActionResult Getsuretynamedetails(string GroupCode, long TicketNo)
        {
            try
            {

                var result = _easychittools.Getsuretynamedetails(GlobalSchema, GroupCode, TicketNo, Con);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


         [HttpPost]
        [Route("DeleteMvoSvoSuretyDetails")]
        public IActionResult DeleteMvoSvoSuretyDetails(string branchSchema, string groupCode, long ticketNo, long mvoSuretyId, long chitGroupId)
        {
            try
            {
                string result = _easychittools.DeleteMvoSvoSuretyDetails(branchSchema, GlobalSchema, groupCode, ticketNo, mvoSuretyId, chitGroupId, Con);

                if (result == "Success")
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


         [HttpGet]
        [Route("GetRECEIVEDDOCUMENT")]
        public IActionResult GetRECEIVEDDOCUMENT(string BranchSchema, string GroupCode, long TicketNo)
        {
            try
            {

                var result = _easychittools.GetRECEIVEDDOCUMENT(BranchSchema, GlobalSchema, Con, GroupCode, TicketNo);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("DeleteKgmsMvoSvoDetails")]
        public IActionResult DeleteKgmsMvoSvoDetails(string branchschema, string groupcode, long ticketno, bool filedownloadstatus)
        {
            try
            {
                string result = _easychittools.DeleteKgmsMvoSvoDetails(
                    branchschema,
                    GlobalSchema,
                    groupcode,
                    ticketno,
                    filedownloadstatus,
                    Con
                );

                if (result == "Success")
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

         [HttpGet]
        [Route("Getcentralofficechitsdetails")]
        public IActionResult Getcentralofficechitsdetails(string BranchSchema, string BranchCode, string GroupCode, long TicketNo)
        {
            try
            {

                var result = _easychittools.Getcentralofficechitsdetails(Con, BranchSchema, GlobalSchema, GroupCode, TicketNo, BranchCode);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("GetcoBranchNames")]
        public IActionResult GetcoBranchNames()
        {
            List<BranchNamesDTO> branchList = new List<BranchNamesDTO>();
            try
            {


                var result = _easychittools.GetcoBranchNames(GlobalSchema, Con);



                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdateAdvanceInterestPaymentBank")]
        public IActionResult UpdateAdvanceInterestPaymentBank(UpdateAdvanceInterestBankDTO obj,string BranchSchema)
        {
            try
            {

                string result = _easychittools.UpdateAdvanceInterestPaymentBank(BranchSchema, obj, Con);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}