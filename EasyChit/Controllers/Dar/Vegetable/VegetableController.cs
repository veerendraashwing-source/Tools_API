// using Microsoft.AspNetCore.Mvc;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Easychit_Infrastructure.Dar.Vegetable;
// using Easychit_Repository.Interfaces.Dar.Vegetable;
// using Easychit_Repository.DataAccess.Dar.Vegetable;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Http;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Logging;

// namespace Easychit_Api.Controllers.Dar.Vegetable
// {
//     public class VegetableController : ControllerBase
//     {
//         string Con = string.Empty;
//         string GlobalSchema = string.Empty;
//         string db = string.Empty;
//         string Url = string.Empty;
//         static IConfiguration _iconfiguration;
//         private readonly IWebHostEnvironment _hostingEnvironment;
//         private readonly ILogger<VegetableController> _logger;
//         IVegetable _darVegetable = new VegetableDAL();

//         private readonly IHttpContextAccessor _httpContextAccessor;
//         public VegetableController(IConfiguration iconfiguration, IWebHostEnvironment hostingEnvironment, ILogger<VegetableController> logger, IHttpContextAccessor httpContextAccessor)
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
//         [Route("api/Dar/Vegetable/Production/GetCollectionInchargeNames")]
//         public async Task<IActionResult> GetCollectionInchargeNames()
//         {
//             List<VegetableProductionDTO> CollectionInchargeDTOList = new List<VegetableProductionDTO>();
//             try
//             {
//                 CollectionInchargeDTOList = await _darVegetable.GetCollectionInchargeNames(GlobalSchema, Con);

//                 return CollectionInchargeDTOList != null ? Ok(CollectionInchargeDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpGet]
//         [Route("api/Dar/Vegetable/Production/GettypeOfVegetable")]
//         public async Task<IActionResult> GettypeOfVegetable()
//         {
//             List<VegetableProductionDTO> typeOfVegetableList = new List<VegetableProductionDTO>();
//             try
//             {
//                 typeOfVegetableList = await _darVegetable.GettypeOfVegetable(GlobalSchema, Con);

//                 return typeOfVegetableList != null ? Ok(typeOfVegetableList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }


//         [HttpGet]
//         [Route("api/Dar/Vegetable/Production/GetBookNobasedonCollectionIncharge")]

//         public async Task<IActionResult> GetBookNobasedonCollectionIncharge(string CollectionInchargeName)
//         {
//             List<VegetableProductionDTO> BookNoDTOList = new List<VegetableProductionDTO>();
//             try
//             {
//                 BookNoDTOList = await _darVegetable.GetBookNobasedonCollectionIncharge(CollectionInchargeName, GlobalSchema, Con);

//                 return BookNoDTOList != null ? Ok(BookNoDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }



//         [HttpGet]
//         [Route("api/Dar/Vegetable/Production/GetUomByVegName")]

//         public async Task<IActionResult> GetUomByVegName(string VegName, string ModuleName)
//         {
//             List<VegetableProductionDTO> UomList = new List<VegetableProductionDTO>();
//             try
//             {
//                 UomList = await _darVegetable.GetUomByVegName(VegName, ModuleName, GlobalSchema, Con);

//                 return UomList != null ? Ok(UomList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         //BlockName
//         [HttpGet]
//         [Route("api/Dar/Vegetable/Production/GetBlockName")]

//         public async Task<IActionResult> GetBlockName(string VegName)
//         {
//             List<VegetableProductionDTO> BlockNameList = new List<VegetableProductionDTO>();
//             try
//             {
//                 BlockNameList = await _darVegetable.GetBlockName(VegName, GlobalSchema, Con);

//                 return BlockNameList != null ? Ok(BlockNameList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }


//         //Bed No
//         [HttpGet]
//         [Route("api/Dar/Vegetable/Production/GetBedNo")]
//         public async Task<IActionResult> GetBedNo(string VegName, string BlockName)
//         {
//             List<VegetableProductionDTO> BedNoList = new List<VegetableProductionDTO>();
//             try
//             {
//                 BedNoList = await _darVegetable.GetBedNo(VegName, BlockName, GlobalSchema, Con);

//                 return BedNoList != null ? Ok(BedNoList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpPost]
//         [Route("api/Dar/Vegetable/Production/SaveProductionDetails")]
//         public IActionResult SaveProductionDetails([FromBody] List<VegetableProductionDTO> productionDTO)
//         {
//             try
//             {
//                 bool IsValid = false;
//                 if (_darVegetable.SaveProductionDetails(productionDTO, GlobalSchema, Con))
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
