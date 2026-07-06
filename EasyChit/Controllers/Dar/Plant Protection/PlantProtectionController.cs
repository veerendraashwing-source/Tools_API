// using Microsoft.AspNetCore.Mvc;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Easychit_Repository.Interfaces.Dar.Plant_Protection;
// using Easychit_Repository.DataAccess.Dar.Plant_Protection;
// using Easychit_Infrastructure.Dar.Plant_Protection;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Http;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Logging;

// namespace Easychit_Api.Controllers.Dar.Plant_Protection
// {
//     public class PlantProtectionController : Controller
//     {
//         string Con = string.Empty;
//         string GlobalSchema = string.Empty;
//         string db = string.Empty;
//         string Url = string.Empty;
//         static IConfiguration _iconfiguration;
//         private readonly IWebHostEnvironment _hostingEnvironment;
//         private readonly ILogger<PlantProtectionController> _logger;
//         IPlantProtection _darplantProtection = new PlantProtectionDAL();

//         private readonly IHttpContextAccessor _httpContextAccessor;
//         public PlantProtectionController(IConfiguration iconfiguration, IWebHostEnvironment hostingEnvironment, ILogger<PlantProtectionController> logger, IHttpContextAccessor httpContextAccessor)
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
//         [Route("api/Dar/PlantProtection/DailyActivity/GetPlationationDivisionNames")]
//         public async Task<IActionResult> GetPlationationDivisionNames()
//         {
//             List<DevisionNamesDTO> subValvesDTOList = new List<DevisionNamesDTO>();
//             try
//             {
//                 subValvesDTOList = await _darplantProtection.GetPlationationDivisionNames(GlobalSchema, Con);

//                 return subValvesDTOList != null ? Ok(subValvesDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpGet]
//         [Route("api/Dar/PlantProtection/DailyActivity/GetRecomendationValve")]
//         public async Task<IActionResult> GetRecomendationValve(string division)
//         {
//             List<RecomendationValveDTO> recomendationDTOList = new List<RecomendationValveDTO>();
//             try
//             {
//                 recomendationDTOList = await _darplantProtection.GetRecomendationValve(division,GlobalSchema, Con);

//                 return recomendationDTOList != null ? Ok(recomendationDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpGet]
//         [Route("api/Dar/PlantProtection/DailyActivity/GetRecomendationCrop")]
//         public async Task<IActionResult> GetRecomendationCrop(string division,string valve)
//         {
//             List<RecomendationValveDTO> recomendationDTOList = new List<RecomendationValveDTO>();
//             try
//             {
//                 recomendationDTOList = await _darplantProtection.GetRecomendationCrop(division, valve, GlobalSchema, Con);

//                 return recomendationDTOList != null ? Ok(recomendationDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpGet]
//         [Route("api/Dar/PlantProtection/DailyActivity/GetDivisionNames")]
//         public async Task<IActionResult> GetDivisionNames()
//         {
//             List<DevisionNamesDTO> devisionNamesDTOList = new List<DevisionNamesDTO>();
//             try
//             {
//                 devisionNamesDTOList = await _darplantProtection.GetDivisionNames(GlobalSchema, Con);

//                 return devisionNamesDTOList != null ? Ok(devisionNamesDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpGet]
//         [Route("api/Dar/PlantProtection/DailyActivity/GetCatergoryNames")]
//         public async Task<IActionResult> GetCatergoryNames(string division)
//         {
//             List<CategoryTypeDTO> categoryNamesDTOList = new List<CategoryTypeDTO>();
//             try
//             {
//                 categoryNamesDTOList = await _darplantProtection.GetCatergoryNames(division,GlobalSchema, Con);

//                 return categoryNamesDTOList != null ? Ok(categoryNamesDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }
        
//         [HttpGet]
//         [Route("api/Dar/PlantProtection/DailyActivity/GetStockCompanynames")]
//         public async Task<IActionResult> GetStockCompanynames(string division,string category)
//         {
//             List<StockCompanyNamesDTO> stockCompanyNamesDTOList = new List<StockCompanyNamesDTO>();
//             try
//             {
//                 stockCompanyNamesDTOList = await _darplantProtection.GetStockCompanynames(division, category, GlobalSchema, Con);

//                 return stockCompanyNamesDTOList != null ? Ok(stockCompanyNamesDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpGet]
//         [Route("api/Dar/PlantProtection/DailyActivity/GetProductByCategoryAndCompnay_Missue")]
//         public async Task<IActionResult> GetProductByCategoryAndCompnay_Missue(string division, string category,string prodCompName)
//         {
//             List<StockCompanyNamesDTO> stockCompanyNamesDTOList = new List<StockCompanyNamesDTO>();
//             try
//             {
//                 stockCompanyNamesDTOList = await _darplantProtection.GetProductByCategoryAndCompnay_Missue(division, category,prodCompName, GlobalSchema, Con);

//                 return stockCompanyNamesDTOList != null ? Ok(stockCompanyNamesDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpGet]
//         [Route("api/Dar/PlantProtection/DailyActivity/GetUOMName")]
//         public async Task<IActionResult> GetUOMName(string ferti)
//         {
//             try
//             {
//                 var uom = await _darplantProtection.GetUOMName(ferti, GlobalSchema, Con);

//                 if (string.IsNullOrEmpty(uom))
//                 {
//                     return Ok(new { uom = "" });
//                 }

//                 return Ok(new { uom = uom });
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpGet]
//         [Route("api/Dar/PlantProtection/DailyActivity/GetRATE")]
//         public async Task<IActionResult> GetRATE(string ferti,string fromdate)
//         {
//             try
//             {
//                 var rate = await _darplantProtection.GetRATE(ferti,fromdate, GlobalSchema, Con);

//                 if (string.IsNullOrEmpty(rate))
//                 {
//                     return Ok(new {rate ="" });
//                 }

//                 return Ok(new { rate = rate });
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpGet]
//         [Route("api/Dar/PlantProtection/DailyActivity/GetEmployeeNames")]
//         public async Task<IActionResult> GetEmployeeNames(string gender)
//         {
//             List<EmployeeNamesDTO> employeeNamesDTOList = new List<EmployeeNamesDTO>();
//             try
//             {
//                 employeeNamesDTOList = await _darplantProtection.GetEmployeeNames(gender,GlobalSchema, Con);

//                 return employeeNamesDTOList != null ? Ok(employeeNamesDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpGet]
//         [Route("api/Dar/PlantProtection/DailyActivity/GetGeneralItems")]
//         public async Task<IActionResult> GetGeneralItems()
//         {
//             List<GeneralItemsDTO> generalItemsDTOList = new List<GeneralItemsDTO>();
//             try
//             {
//                 generalItemsDTOList = await _darplantProtection.GetGeneralItems(GlobalSchema, Con);

//                 return generalItemsDTOList != null ? Ok(generalItemsDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpGet]
//         [Route("api/Dar/PlantProtection/DailyActivity/GetvchUOM")]
//         public async Task<IActionResult> GetvchUOM(string general)
//         {
//             try
//             {
//                 var uom = await _darplantProtection.GetvchUOM(general, GlobalSchema, Con);
//                 if (string.IsNullOrEmpty(uom))
//                 {
//                     return Ok(new { Uom=""});
//                 }

//                 return Ok(new { UOM = uom });
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }


//         [HttpPost]
//         [Route("api/Dar/PlantProtection/DailyActivity/SavePlantDailyActivity")]
//         public IActionResult SavePlantDailyActivity([FromBody] DailyActivityDTO plantation)
//         {
//             try
//             {
//                 bool IsValid = false;
//                 if (_darplantProtection.SavePlantDailyActivity(plantation, GlobalSchema, Con))
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


//         [HttpGet]
//         [Route("api/Dar/PlantProtection/RoboEntry/GetMachineNames")]
//         public async Task<IActionResult> GetMachineNames()
//         {
//             List<MachineNamesDTO> machineNamesDTOList = new List<MachineNamesDTO>();
//             try
//             {
//                 machineNamesDTOList = await _darplantProtection.GetMachineNames(GlobalSchema, Con);

//                 return machineNamesDTOList != null ? Ok(machineNamesDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpGet]
//         [Route("api/Dar/PlantProtection/RoboEntry/GetDivisions")]
//         public async Task<IActionResult> GetDivisions()
//         {
//             List<DivisionNamesDTO> divisionNamesDTOList = new List<DivisionNamesDTO>();
//             try
//             {
//                 divisionNamesDTOList = await _darplantProtection.GetDivisions(GlobalSchema, Con);

//                 return divisionNamesDTOList != null ? Ok(divisionNamesDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpGet]
//         [Route("api/Dar/PlantProtection/RoboEntry/GetRoboValves")]
//         public async Task<IActionResult> GetRoboValves()
//         {
//             List<ValveNamesDTO> valveNamesDTOList = new List<ValveNamesDTO>();
//             try
//             {
//                 valveNamesDTOList = await _darplantProtection.GetRoboValves(GlobalSchema, Con);

//                 return valveNamesDTOList != null ? Ok(valveNamesDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpGet]
//         [Route("api/Dar/PlantProtection/RoboEntry/GetRoboCrops")]
//         public async Task<IActionResult> GetRoboCrops(string valve)
//         {
//             List<CropNamesDTO> cropNamesDTOList = new List<CropNamesDTO>();
//             try
//             {
//                 cropNamesDTOList = await _darplantProtection.GetRoboCrops(valve,GlobalSchema, Con);

//                 return cropNamesDTOList != null ? Ok(cropNamesDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpPost]
//         [Route("api/Dar/PlantProtection/RoboEntry/SaveRoboEntry")]
//         public IActionResult SaveRoboEntry([FromBody] List<RoboEntryDTO> roboEntryDTOList)
//         {
//             try
//             {
//                 bool IsValid = false;
//                 if (_darplantProtection.SaveRoboEntry(roboEntryDTOList, GlobalSchema, Con))
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
