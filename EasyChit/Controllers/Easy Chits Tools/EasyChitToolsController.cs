////using Microsoft.AspNetCore.Mvc;
////using System;
////using System.Collections.Generic;
////using Microsoft.AspNetCore.Hosting;
////using Microsoft.AspNetCore.Http;
////using Microsoft.Extensions.Configuration;
////using Microsoft.Extensions.Logging;
////using Easychit_Repository.Interfaces.Easy_Chit_Tools;
////using Easychit_Infrastructure.Easy_Chit_Tools;
////using Easychit_Repository.DataAccess.Easy_Chit_Tools;

////namespace Easychit_Api.Controllers.Easy_Chits_Tools
////{
////    [ApiController]
////    public class EasyChitToolsController : ControllerBase
////    {
////        string Con = string.Empty;
////        string GlobalSchema = string.Empty;
////        string Schema = string.Empty;
////        string db = string.Empty;
////        string Url = string.Empty;
////        static IConfiguration _iconfiguration;
////        private readonly IWebHostEnvironment _hostingEnvironment;
////        private readonly ILogger<EasyChitToolsController> _logger;
////        IEasyChitTools _easychittools = new EasyChitToolsDAL();
////        private readonly IHttpContextAccessor _httpContextAccessor;

////        public EasyChitToolsController(
////            IConfiguration iconfiguration,
////            IWebHostEnvironment hostingEnvironment,
////            ILogger<EasyChitToolsController> logger,
////            IHttpContextAccessor httpContextAccessor)
////        {
////            _logger = logger;
////            _iconfiguration = iconfiguration;
////            _hostingEnvironment = hostingEnvironment;
////            _httpContextAccessor = httpContextAccessor;


////            db = _httpContextAccessor.HttpContext.Request.Headers["database"].ToString();
////            Con = _iconfiguration.GetConnectionString("Connection1");
////            GlobalSchema = _iconfiguration.GetSection("ConnectionStrings").GetSection("GlobalSchemeName").Value;
////            Schema = _iconfiguration.GetSection("ConnectionStrings").GetSection("SchemeName").Value;
////            Url = _iconfiguration.GetSection("ApplicationUrl").GetSection("URL").Value;
////        }

////        #region Branch & Group APIs

////        [HttpGet]
////        [Route("api/EasyChitTools/GetBranchNames")]
////        public IActionResult GetBranchNames()
////        {
////            List<BranchNamesDTO> branchList = new List<BranchNamesDTO>();
////            try
////            {
////                branchList = _easychittools.GetBranchNames(GlobalSchema, Con);
////                return Ok(branchList);
////            }
////            catch (Exception)
////            {
////                return StatusCode(StatusCodes.Status500InternalServerError);
////            }
////        }

////        [HttpGet]
////        [Route("api/EasyChitTools/GetGroupcodes")]
////        public IActionResult GetGroupcodes(string branchschema)
////        {
////            List<GroupCodeDTO> groupList = new List<GroupCodeDTO>();
////            try
////            {
////                groupList = _easychittools.GetGroupcodes(branchschema, Con);
////                return Ok(groupList);
////            }
////            catch (Exception)
////            {
////                return StatusCode(StatusCodes.Status500InternalServerError);
////            }
////        }

////        [HttpGet]
////        [Route("api/EasyChitTools/GetTickets")]
////        public IActionResult GetTickets(string schema, string groupcode)
////        {
////            List<TicketDTO> ticketList = new List<TicketDTO>();
////            try
////            {
////                ticketList = _easychittools.GetTickets(schema, Con, groupcode);
////                return Ok(ticketList);
////            }
////            catch (Exception)
////            {
////                return StatusCode(StatusCodes.Status500InternalServerError);
////            }
////        }

////        #endregion
////        [HttpPost]
////        [Route("api/EasyChitTools/UpdateMoblieNoByContact")]
////        public IActionResult UpdateMoblieNoByContact(string contactid, Easychit_Repository.Interfaces.Easy_Chit_Tools.NameChangeDto dto)
////        {
////            try
////            {
////                _easychittools.UpdateMoblieNoByContact(GlobalSchema, Con, contactid, dto);
////                return Ok(new { Message = "Name updated successfully" });
////            }
////            catch (Exception)
////            {
////                return StatusCode(StatusCodes.Status500InternalServerError);
////            }
////        }
////        [HttpPost]
////        [Route("api/EasyChitTools/UpdateAddressByContact")]
////        public IActionResult UpdateAddressByContact(string contactid, Easychit_Infrastructure.ChangeDetails.ChangeDTO.NameChangeDto dto)
////        {
////            try
////            {
////                _easychittools.UpdateAddressByContact(GlobalSchema, Con, contactid, dto);
////                return Ok(new { Message = "Name updated successfully" });
////            }
////            catch (Exception)
////            {
////                return StatusCode(StatusCodes.Status500InternalServerError);
////            }
////        }
////        [HttpGet]
////        [Route("api/EasyChitTools/GetReferralDetails")]
////        public IActionResult GetReferralDetails(string referralCode)
////        {
////            List<ReferralDetailsDto> lstref = new List<ReferralDetailsDto>();
////            try
////            {
////                lstref = _easychittools.GetReferralDetails(GlobalSchema, Con, referralCode);
////                return lstref != null ? Ok(lstref) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
////            }
////            catch (Exception)
////            {
////                return StatusCode(StatusCodes.Status500InternalServerError);
////                throw;
////            }
////        }
////        [HttpGet]
////        [Route("api/EasyChitTools/Agentcode")]
////        public IActionResult Agentcode()
////        {
////            List<ReferralDetailsDto> lstref = new List<ReferralDetailsDto>();
////            try
////            {
////                lstref = _easychittools.Agentcode(GlobalSchema, Con);
////                return lstref != null ? Ok(lstref) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
////            }
////            catch (Exception)
////            {
////                return StatusCode(StatusCodes.Status500InternalServerError);
////                throw;
////            }
////        }

////        #region Name Change APIs

////        [HttpGet]
////        [Route("GetOldNameByTicketNo")]
////        public IActionResult GetOldNameByTicketNo(string Schema, int ticketNo, int chitgroupid)
////        {
////            try
////            {
////                var result = _easychittools.GetOldNameByTicketNo(GlobalSchema, Schema, Con, ticketNo, chitgroupid);

////                if (result == null || string.IsNullOrEmpty(result.OldName))
////                    return StatusCode(StatusCodes.Status204NoContent);

////                return Ok(result);
////            }
////            catch (Exception)
////            {
////                return StatusCode(StatusCodes.Status500InternalServerError);
////            }
////        }

////        [HttpPost]
////        [Route("api/EasyChitTools/UpdateNameByTicketNo1")]
////        public IActionResult UpdateNameByTicketNo1(Easychit_Repository.Interfaces.Easy_Chit_Tools.NameChangeDto dtonames)
////        {
////            try
////            {
////                _easychittools.UpdateNameByTicketNo1(GlobalSchema, Con, dtonames);
////                return Ok(new { Message = "Name updated successfully" });
////            }
////            catch (Exception)
////            {
////                return StatusCode(StatusCodes.Status500InternalServerError);
////            }
////        }

////        [HttpPost]
////        [Route("api/EasyChitTools/UpdateNameByTicketNo")]

////        public IActionResult UpdateNameByTicketNo([FromBody]  List<Easychit_Repository.Interfaces.Easy_Chit_Tools.NameChangeDto> dtoNames,string branchschema)
////        {
////            try
////            {
////                _easychittools.UpdateNameByTicketNo(GlobalSchema, Con, dtoNames,branchschema);
////                return Ok(new { Message = "Name updated successfully" });
////            }
////            catch (Exception)
////            {
////                return StatusCode(StatusCodes.Status500InternalServerError);
////            }
////        }

////        [HttpPost]
////        [Route("api/EasyChitTools/InsertGridDetails")]
////        public IActionResult InsertGridDetails(List<GridDTO> lstgriddto)
////        {
////            try
////            {
////                foreach(var grid in lstgriddto)
////                {
////                    _easychittools.InsertGridDetails(Con, GlobalSchema, grid);
////                }

////                return Ok(new { Message = "Record inserted successfully" });
////            }
////            catch (Exception)
////            {
////                return StatusCode(StatusCodes.Status500InternalServerError);
////            }
////        }

////        #endregion
////        [HttpGet]
////        [Route("api/EasyChitTools/GetUpdateReports")]
////        public IActionResult GetUpdateReports(DateTime fromdate, DateTime todate, string changetype)
////        {
////            List<ReportsofUpdatesDTO> lstupdatereports = new List<ReportsofUpdatesDTO>();
////            try
////            {
////                lstupdatereports = _easychittools.GetUpdateReports(GlobalSchema, Con, fromdate, todate, changetype);

////                return lstupdatereports != null ? Ok(lstupdatereports) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
////            }
////            catch (Exception)
////            {
////                return StatusCode(StatusCodes.Status500InternalServerError);
////                throw;
////            }
////        }

////        [HttpPost]
////        [Route("SaveNameChange")]
////        public IActionResult SaveNameChange([FromBody] Easychit_Infrastructure.Easy_Chit_Tools.NameChangeSaveDTO dto)
////        {
////            if (dto == null)
////                return BadRequest("Request body is empty");

////            if (dto.Contact_Id <= 0)
////                return BadRequest("Invalid Contact_Id");

////            try
////            {

////                bool result = _easychittools.SaveNameChange(GlobalSchema, Con, dto);


////                if (!result)
////                    return NoContent(); // nothing updated

////                return Ok(new { message = "Name changed successfully" });
////            }
////            catch (Exception ex)
////            {
////                return StatusCode(StatusCodes.Status500InternalServerError,
////                                  new { error = ex.Message });
////            }
////        }

////        [HttpGet]
////        [Route("api/ReportsofUpdates/GetChangeTypes")]
////        public IActionResult GetChangeTypes()
////        {
////            List<ChangeTypes> lstct = new List<ChangeTypes>();
////            try
////            {
////                lstct = _easychittools.GetChangeTypes(GlobalSchema, Con);
////                return lstct != null ? Ok(lstct) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
////            }
////            catch (Exception)
////            {
////                return StatusCode(StatusCodes.Status500InternalServerError);
////                throw;
////            }
////        }
////        [HttpGet]
////        [Route("api/IssuedCheque/GetBranchNames")]
////        public IActionResult GetBranchName()
////        {
////            List<BranchNameDTO> branchlist = new List<BranchNameDTO>();
////            try
////            {
////                branchlist = _easychittools.GetBranchName(GlobalSchema, Con);

////                return branchlist != null ? Ok(branchlist) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
////            }
////            catch (Exception)
////            {
////                return StatusCode(StatusCodes.Status500InternalServerError);
////                throw;
////            }
////        }
////        [HttpGet]
////        [Route("api/IssuedCheque/GetChequeDetails")]
////        public IActionResult GetChequeDetails(string branchschema, string checkreferno)
////        {
////            List<ChequeDetailsDTO> checkdetails = new List<ChequeDetailsDTO>();
////            try
////            {
////                checkdetails = _easychittools.GetChequeDetails(branchschema, Con, checkreferno);

////                return checkdetails != null ? Ok(checkdetails) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
////            }
////            catch (Exception)
////            {
////                return StatusCode(StatusCodes.Status500InternalServerError);
////                throw;
////            }
////        }
////        [HttpPut]
////        [Route("api/IssuedCheque/ClearDateandStatus")]
////        public IActionResult ClearDateandStatus(string branchschema, string checkreferno, string paymentno)
////        {

////            try
////            {
////                _easychittools.ClearDateandStatus(branchschema, Con, checkreferno, paymentno);
////                return Ok(new { Message = "Name updated successfully" });
////            }
////            catch (Exception)
////            {
////                return StatusCode(StatusCodes.Status500InternalServerError);
////            }
////        }
////        [HttpPost]
////        [Route("api/IssuedCheque/SaveData")]
////        public IActionResult SaveData(List<ChangeAuditDto> audits)
////        {
////            try
////            {
////                foreach (var audit in audits)
////                {
////                    _easychittools.SaveData(GlobalSchema, Con, audit);
////                }

////                return Ok(new { Message = "Name updated successfully" });
////            }
////            catch (Exception)
////            {
////                return StatusCode(StatusCodes.Status500InternalServerError);
////            }
////        }

////    }

////}

//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Easychit_Repository.DataAccess.Easy_Chit_Tools;
//using Easychit_Repository.Interfaces.Easy_Chit_Tools;
//using Easychit_Infrastructure.Easy_Chit_Tools;

//namespace Easychit_Api.Controllers.Easy_Chits_Tools
//{
//    [ApiController]
//    public class EasyChitToolsController : ControllerBase
//    {
//        string Con = string.Empty;
//        string GlobalSchema = string.Empty;
//        string Schema = string.Empty;
//        string db = string.Empty;
//        string Url = string.Empty;
//        static IConfiguration _iconfiguration;
//        private readonly IWebHostEnvironment _hostingEnvironment;
//        private readonly ILogger<EasyChitToolsController> _logger;
//        IEasyChitTools _easychittools = new EasyChitToolsDAL();
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public EasyChitToolsController(
//            IConfiguration iconfiguration,
//            IWebHostEnvironment hostingEnvironment,
//            ILogger<EasyChitToolsController> logger,
//            IHttpContextAccessor httpContextAccessor)
//        {
//            _logger = logger;
//            _iconfiguration = iconfiguration;
//            _hostingEnvironment = hostingEnvironment;
//            _httpContextAccessor = httpContextAccessor;


//            db = _httpContextAccessor.HttpContext.Request.Headers["database"].ToString();
//            Con = _iconfiguration.GetConnectionString("Connection1");
//            GlobalSchema = _iconfiguration.GetSection("ConnectionStrings").GetSection("GlobalSchemeName").Value;
//            Schema = _iconfiguration.GetSection("ConnectionStrings").GetSection("SchemeName").Value;
//            Url = _iconfiguration.GetSection("ApplicationUrl").GetSection("URL").Value;
//        }

//        #region Branch & Group APIs

//        [HttpGet]
//        [Route("api/EasyChitTools/GetBranchNames")]
//        public IActionResult GetBranchNames()
//        {
//            List<BranchNamesDTO> branchList = new List<BranchNamesDTO>();
//            try
//            {
//                branchList = _easychittools.GetBranchNames(GlobalSchema, Con);
//                return Ok(branchList);
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }

//        [HttpGet]
//        [Route("api/EasyChitTools/GetGroupcodes")]
//        public IActionResult GetGroupcodes(string branchschema)
//        {
//            List<GroupCodeDTO> groupList = new List<GroupCodeDTO>();
//            try
//            {
//                groupList = _easychittools.GetGroupcodes(branchschema, Con);
//                return Ok(groupList);
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }

//        [HttpGet]
//        [Route("api/EasyChitTools/GetTickets")]
//        public IActionResult GetTickets(string schema, string groupcode)
//        {
//            List<TicketDTO> ticketList = new List<TicketDTO>();
//            try
//            {
//                ticketList = _easychittools.GetTickets(schema, Con, groupcode);
//                return Ok(ticketList);
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }

//        #endregion
        
//        [HttpGet]
//        [Route("api/EasyChitTools/GetReferralDetails")]
//        public IActionResult GetReferralDetails(string referralCode)
//        {
//            List<ReferralDetailsDto> lstref = new List<ReferralDetailsDto>();
//            try
//            {
//                lstref = _easychittools.GetReferralDetails(GlobalSchema, Con, referralCode);
//                return lstref != null ? Ok(lstref) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//                throw;
//            }
//        }
//        [HttpGet]
//        [Route("api/EasyChitTools/Agentcode")]
//        public IActionResult Agentcode()
//        {
//            List<ReferralDetailsDto> lstref = new List<ReferralDetailsDto>();
//            try
//            {
//                lstref = _easychittools.Agentcode(GlobalSchema, Con);
//                return lstref != null ? Ok(lstref) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//                throw;
//            }
//        }

//        #region Name Change APIs

//        [HttpGet]
//        [Route("api/EasyChitTools/GetOldNameByTicketNo")]
//        public IActionResult GetOldNameByTicketNo(string Schema, int ticketNo, int chitgroupid)
//        {
//            try
//            {
//                var result = _easychittools.GetOldNameByTicketNo(GlobalSchema, Schema, Con, ticketNo, chitgroupid);

//                if (result == null || string.IsNullOrEmpty(result.OldName))
//                    return StatusCode(StatusCodes.Status204NoContent);

//                return Ok(result);
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }

//        //[HttpPost]
//        //[Route("api/EasyChitTools/UpdateNameByTicketNo1")]
//        //public IActionResult UpdateNameByTicketNo1(NameChangeDto dtonames)
//        //{
//        //    try
//        //    {
//        //        _easychittools.UpdateNameByTicketNo1(GlobalSchema, Con, dtonames);
//        //        return Ok(new { Message = "Name updated successfully" });
//        //    }
//        //    catch (Exception)
//        //    {
//        //        return StatusCode(StatusCodes.Status500InternalServerError);
//        //    }
//        //}

//        //[HttpPost]
//        //[Route("api/EasyChitTools/UpdateNameByTicketNo")]
//        //public IActionResult UpdateNameByTicketNo([FromBody] List<NameChangeDto> dtoNames, string branchschema)
//        //{
//        //    try
//        //    {
//        //        _easychittools.UpdateNameByTicketNo(GlobalSchema, Con, dtoNames, branchschema);
//        //        return Ok(new { Message = "Name updated successfully" });
//        //    }
//        //    catch (Exception)
//        //    {
//        //        return StatusCode(StatusCodes.Status500InternalServerError);
//        //    }
//        //}

//        [HttpPost]
//        [Route("api/EasyChitTools/InsertGridDetails")]
//        public IActionResult InsertGridDetails(List<GridDTO> lstgriddto)
//        {
//            try
//            {
//                foreach (var grid in lstgriddto)
//                {
//                    _easychittools.InsertGridDetails(Con, GlobalSchema, grid);
//                }

//                return Ok(new { Message = "Record inserted successfully" });
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }

//        #endregion
//        [HttpGet]
//        [Route("api/EasyChitTools/GetUpdateReports")]
//        public IActionResult GetUpdateReports(DateTime fromdate, DateTime todate, string changetype)
//        {
//            List<ReportsofUpdatesDTO> lstupdatereports = new List<ReportsofUpdatesDTO>();
//            try
//            {
//                lstupdatereports = _easychittools.GetUpdateReports(GlobalSchema, Con, fromdate, todate, changetype);

//                return lstupdatereports != null ? Ok(lstupdatereports) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//                throw;
//            }
//        }
//        [HttpGet]
//        [Route("api/ReportsofUpdates/GetChangeTypes")]
//        public IActionResult GetChangeTypes()
//        {
//            List<ChangeTypes> lstct = new List<ChangeTypes>();
//            try
//            {
//                lstct = _easychittools.GetChangeTypes(GlobalSchema, Con);
//                return lstct != null ? Ok(lstct) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//                throw;
//            }
//        }
//        [HttpGet]
//        [Route("api/IssuedCheque/GetBranchNames")]
//        public IActionResult GetBranchName()
//        {
//            List<BranchNameDTO> branchlist = new List<BranchNameDTO>();
//            try
//            {
//                branchlist = _easychittools.GetBranchName(GlobalSchema, Con);

//                return branchlist != null ? Ok(branchlist) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//                throw;
//            }
//        }
//        [HttpGet]
//        [Route("api/IssuedCheque/GetChequeDetails")]
//        public IActionResult GetChequeDetails(string branchschema, string checkreferno)
//        {
//            List<ChequeDetailsDTO> checkdetails = new List<ChequeDetailsDTO>();
//            try
//            {
//                checkdetails = _easychittools.GetChequeDetails(branchschema, Con, checkreferno);

//                return checkdetails != null ? Ok(checkdetails) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//                throw;
//            }
//        }
//        [HttpPut]
//        [Route("api/IssuedCheque/ClearDateandStatus")]
//        public IActionResult ClearDateandStatus(string branchschema, string checkreferno, string paymentno)
//        {

//            try
//            {
//                _easychittools.ClearDateandStatus(branchschema, Con, checkreferno, paymentno);
//                return Ok(new { Message = "Name updated successfully" });
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }
//        [HttpPost]
//        [Route("api/IssuedCheque/SaveData")]
//        public IActionResult SaveData(List<ChangeAuditDto> audits)
//        {
//            try
//            {
//                foreach (var audit in audits)
//                {
//                    _easychittools.SaveData(GlobalSchema, Con, audit);
//                }

//                return Ok(new { Message = "Name updated successfully" });
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }

//    }

//}



