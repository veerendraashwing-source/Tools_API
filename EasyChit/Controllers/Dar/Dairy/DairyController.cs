// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Cors;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Logging;
// using Easychit_Repository.Interfaces.Dar.Dairy;
// using Easychit_Repository.DataAccess.Dar.Dairy;
// using Easychit_Infrastructure.Dar.Dairy;



// namespace Easychit_Api.Controllers.Dar.Dairy
// {
//     public class DairyController : ControllerBase
//     {
//         string Con = string.Empty;
//         string GlobalSchema = string.Empty;
//         string db = string.Empty;
//         string Url = string.Empty;
//         static IConfiguration _iconfiguration;
//         private readonly IWebHostEnvironment _hostingEnvironment;
//         private readonly ILogger<DairyController> _logger;
//         IDairy _dairy = new DairyDAL();

//         private readonly IHttpContextAccessor _httpContextAccessor;

//         public DairyController(IConfiguration iconfiguration, IWebHostEnvironment hostingEnvironment, ILogger<DairyController> logger, IHttpContextAccessor httpContextAccessor)
//         {
//             _logger = logger;
//             _iconfiguration = iconfiguration;
//             _hostingEnvironment = hostingEnvironment;
//             _httpContextAccessor = httpContextAccessor;
//             db = _httpContextAccessor.HttpContext.Request.Headers["database"].ToString();
//             Con = _iconfiguration.GetConnectionString("Connection");
//             GlobalSchema = _iconfiguration.GetSection("ConnectionStrings").GetSection("GlobalSchemeName").Value;
//             Url = _iconfiguration.GetSection("ApplicationUrl").GetSection("URL").Value;
//             //string db= HttpContext.Request.Headers.ContainsKey("database");
//             if (db != "")
//             {
//                 Con = Con.Replace("DBNAME", db);
//             }
//         }
//         [HttpGet]
//         [Route("api/Dar/Dairy/Production/GetTagNo")]
//         public async Task<IActionResult> GetTagNo()
//         {
//             List<ProductionDTO> productDTOlist = new List<ProductionDTO>();

//             try
//             {
//                 productDTOlist = await _dairy.GetTagNo(GlobalSchema, Con);


//                 return productDTOlist != null ? Ok(productDTOlist) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpGet]
//         [Route("api/Dar/Dairy/Production/GetMilkType")]
//         public async Task<IActionResult> GetMilkType(string tag)
//         {
//             List<ProductionDTO> productDTOlist = new List<ProductionDTO>();
//             try
//             {
//                 productDTOlist = await _dairy.GetMilkType(GlobalSchema, Con, tag);


//                 return productDTOlist != null ? Ok(productDTOlist) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpGet]
//         [Route("api/Dar/Dairy/Production/GetDamageReason")]
//         public async Task<IActionResult> GetDamageReason()
//         {
//             List<DamageReasonDTO> reasonDTOlist = new List<DamageReasonDTO>();
//             try
//             {
//                 reasonDTOlist = await _dairy.GetDamageReason(GlobalSchema, Con);
//                 return reasonDTOlist != null ? Ok(reasonDTOlist) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }



//         [HttpPost]
//         [Route("api/Dar/Dairy/Production/SaveProductionDetails")]
//         public IActionResult SaveProductionDetails([FromBody] List<SaveProductionDTO> SaveProductionDTO)
//         {
//             List<SaveProductionDTO> productionDTOlist = new List<SaveProductionDTO>();

//             try
//             {
//                 bool IsValid = false;
//                 if (_dairy.SaveProductionDetails(SaveProductionDTO, GlobalSchema, Con))
//                 {
//                     IsValid = true;
//                 }

//                 return Ok(IsValid);
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }
//     }

// }