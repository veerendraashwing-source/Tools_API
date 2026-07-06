// using Microsoft.AspNetCore.Mvc;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Easychit_Infrastructure.Dar.Dar_Masters;
// using Easychit_Repository.Interfaces.Dar.Horticulture;
// using Easychit_Repository.DataAccess.Dar.Horticulture;
// using Easychit_Infrastructure.Dar.Horticulture;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Http;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Logging;


// namespace Easychit_Api.Controllers.Dar.Horticulture
// {
//     public class HorticultureController : Controller
//     {
//         string Con = string.Empty;
//         string GlobalSchema = string.Empty;
//         string db = string.Empty;
//         string Url = string.Empty;
//         static IConfiguration _iconfiguration;
//         private readonly IWebHostEnvironment _hostingEnvironment;
//         private readonly ILogger<HorticultureController> _logger;
//         IHorticulture _darmaster = new HorticultureDAL();

//         private readonly IHttpContextAccessor _httpContextAccessor;
//         public HorticultureController(IConfiguration iconfiguration, IWebHostEnvironment hostingEnvironment, ILogger<HorticultureController> logger, IHttpContextAccessor httpContextAccessor)
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
//         [Route("api/Dar/Horticulture/Plantation/GetSubValvesByTankAndMainValve")]
//         public async Task<IActionResult> GetSubValvesByTankAndMainValve(string tankId, string mainValve)
//         {
//             List<SubValveDTO> subValvesDTOList = new List<SubValveDTO>();
//             try
//             {
//                 subValvesDTOList = await _darmaster.GetSubValvesByTankAndMainValve(tankId, mainValve, GlobalSchema, Con);

//                 return subValvesDTOList != null ? Ok(subValvesDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpGet]
//         [Route("api/Dar/Horticulture/Plantation/GetFruits")]
//         public async Task<IActionResult> GetFruits()
//         {
//             List<FruitsDTO> fruitsDTOList = new List<FruitsDTO>();
//             try
//             {
//                 fruitsDTOList = await _darmaster.GetFruits(GlobalSchema, Con);

//                 return fruitsDTOList != null ? Ok(fruitsDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpPost]
//         [Route("api/Dar/Horticulture/Plantation/SavePlantationDetails")]
//         public IActionResult SavePlantationDetails([FromBody] PlantationDTO plantationDTO)
//         {
//             try
//             {
//                 bool IsValid = false;
//                 if (_darmaster.SavePlantationDetails(plantationDTO, GlobalSchema, Con))
//                 {
//                     IsValid = true;
//                 }

//                 return Ok(IsValid);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpGet]
//         [Route("api/Dar/Horticulture/Production/GetUOMByFruit")]
//         public async Task<IActionResult> GetUOMByFruit(string productType,string ModuleName)
//         {
//             List<FruitsDTO> fruitsDTOList = new List<FruitsDTO>();
//             try
//             {
//                 fruitsDTOList = await _darmaster.GetUOMByFruit(productType, ModuleName,GlobalSchema, Con);

//                 return fruitsDTOList != null ? Ok(fruitsDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpGet]
//         [Route("api/Dar/Horticulture/Production/GetAreaValves")]
//         public async Task<IActionResult> GetAreaValves(string productType)
//         {
//             List<AreaValvesDTO> fruitsDTOList = new List<AreaValvesDTO>();
//             try
//             {
//                 fruitsDTOList = await _darmaster.GetAreaValves(productType, GlobalSchema, Con);

//                 return fruitsDTOList != null ? Ok(fruitsDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpGet]
//         [Route("api/Dar/Horticulture/Production/GetRows")]
//         public async Task<IActionResult> GetRows(string productType,string valve)
//         {
//             List<AreaValvesDTO> fruitsDTOList = new List<AreaValvesDTO>();
//             try
//             {
//                 fruitsDTOList = await _darmaster.GetRows(productType, valve, GlobalSchema, Con);

//                 return fruitsDTOList != null ? Ok(fruitsDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }


//         [HttpPost]
//         [Route("api/Dar/Horticulture/Production/SaveProductionDetails")]
//         public IActionResult SaveProductionDetails([FromBody] ProductionDTO productionDTO)
//         {
//             try
//             {
//                 bool IsValid = false;
//                 if (_darmaster.SaveProductionDetails(productionDTO, GlobalSchema, Con))
//                 {
//                     IsValid = true;
//                 }

//                 return Ok(IsValid);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//     }
// }
