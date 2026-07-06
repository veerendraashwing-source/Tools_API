// using Microsoft.AspNetCore.Mvc;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Easychit_Infrastructure.Dar.Dar_Masters;
// using Easychit_Repository.DataAccess.Dar.Dar_Masters;
// using Easychit_Repository.Interfaces.Dar.Dar_Masters;
// using Easychit_Repository.Interfaces.Dar;
// using Microsoft.AspNetCore.Cors;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Http;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Logging;

// namespace Easychit_Api.Controllers.Dar.Dar_Masters
// {
//     public class DarMastersController : Controller
//     {

//         string Con = string.Empty;
//         string GlobalSchema = string.Empty;
//         string db = string.Empty;
//         string Url = string.Empty;
//         static IConfiguration _iconfiguration;
//         private readonly IWebHostEnvironment _hostingEnvironment;
//         private readonly ILogger<DarMastersController> _logger;
//         IDarMasters _darmaster = new DarMastersDAL();

//         private readonly IHttpContextAccessor _httpContextAccessor;
//         public DarMastersController(IConfiguration iconfiguration, IWebHostEnvironment hostingEnvironment, ILogger<DarMastersController> logger, IHttpContextAccessor httpContextAccessor)
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
//         [Route("api/Dar/DarMasters/ProductCreation/GetUOM")]
//         public async Task<IActionResult> GetUOM()
//         {
//             List<UOMDTO> uOMDTOlist = new List<UOMDTO>();
//             try
//             {
//                 uOMDTOlist = await _darmaster.GetUOM(GlobalSchema, Con);

//                 return uOMDTOlist != null ? Ok(uOMDTOlist) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }


//         [HttpGet]
//         [Route("api/Dar/DarMasters/ProductCreation/GetGST")]
//         public async Task<IActionResult> GetGST()
//         {
//             List<GSTDTO> gstDTOlist = new List<GSTDTO>();
//             try
//             {
//                 gstDTOlist = await _darmaster.GetGST(GlobalSchema, Con);

//                 return gstDTOlist != null ? Ok(gstDTOlist) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }


//         [HttpGet]
//         [Route("api/Dar/DarMasters/ProductCreation/GetVendertype")]
//         public async Task<IActionResult> GetVendertype()
//         {
//             List<ProductTypeDTO> productTypeDTOlist = new List<ProductTypeDTO>();
//             try
//             {
//                 productTypeDTOlist = await _darmaster.GetVendertype(GlobalSchema, Con);

//                 return productTypeDTOlist != null ? Ok(productTypeDTOlist) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }


//         [HttpGet]
//         [Route("api/Dar/DarMasters/ProductCreation/GetCategoryType")]
//         public async Task<IActionResult> GetCategoryType()
//         {
//             List<CategoryTypeDTO> categoryTypeDTOlist = new List<CategoryTypeDTO>();
//             try
//             {
//                 categoryTypeDTOlist = await _darmaster.GetCategoryType(GlobalSchema, Con);

//                 return categoryTypeDTOlist != null ? Ok(categoryTypeDTOlist) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }



//         [HttpGet]
//         [Route("api/Dar/DarMasters/ProductCreation/GetProductCompanyName")]
//         public async Task<IActionResult> GetProductCompanyName()
//         {
//             List<CompanyNameDTO> companyNameDTOlist = new List<CompanyNameDTO>();
//             try
//             {
//                 companyNameDTOlist = await _darmaster.GetProductCompanyName(GlobalSchema, Con);

//                 return companyNameDTOlist != null ? Ok(companyNameDTOlist) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

        
//         [HttpPost]
//         [Route("api/Dar/DarMasters/ProductCreation/AddProductCompanyName")]
//         public IActionResult AddProductCompanyName([FromBody] CompanyNameDTO companyNameDTO)
//         {
//             try
//             {
//                 bool IsValid = false;
//                 if (_darmaster.AddProductCompanyName(companyNameDTO, GlobalSchema, Con))
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

//         [HttpPost]
//         [Route("api/Dar/DarMasters/ProductCreation/SaveProductCreation")]
//         public IActionResult SaveProductCreation([FromBody] ProductMasterDTO masterDTO)
//         {
//             try
//             {
//                 bool IsValid = false;
//                 if (_darmaster.SaveProductCreation(masterDTO, GlobalSchema, Con))
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
//         [Route("api/Dar/DarMasters/ProductCreation/GetProductDetails")]
//         public async Task<IActionResult> GetProductDetails()
//         {
//             List<ProductMasterDTO> productMasterDTOList = new List<ProductMasterDTO>();
//             try
//             {
//                 productMasterDTOList = await _darmaster.GetProductDetails(GlobalSchema, Con);

//                 return productMasterDTOList != null ? Ok(productMasterDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }


//         [HttpGet]
//         [Route("api/Dar/DarMasters/VendorCreation/GetStateNames")]
//         public async Task<IActionResult> GetStateNames()
//         {
//             List<StateNamesDTO> stateNamesDTOList = new List<StateNamesDTO>();
//             try
//             {
//                 stateNamesDTOList = await _darmaster.GetStateNames(GlobalSchema, Con);

//                 return stateNamesDTOList != null ? Ok(stateNamesDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }


//         [HttpPost]
//         [Route("api/Dar/DarMasters/VendorCreation/SaveVendordDetails")]
//         public IActionResult SaveVendordDetails([FromBody] VendorDTO vendorDTO)
//         {
//             try
//             {
//                 bool IsValid = false;
//                 if (_darmaster.SaveVendordDetails(vendorDTO, GlobalSchema, Con))
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
//         [Route("api/Dar/DarMasters/VendorCreation/GetVendorDetails")]
//         public async Task<IActionResult> GetVendorDetails()
//         {
//             List<VendorDTO> vendorsDTOList = new List<VendorDTO>();
//             try
//             {
//                 vendorsDTOList = await _darmaster.GetVendorDetails(GlobalSchema, Con);

//                 return vendorsDTOList != null ? Ok(vendorsDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpPost]
//         [Route("api/Dar/DarMasters/ValveCreation/SaveTankDetails")]
//         public IActionResult SaveTankDetails([FromBody] TanksDTO tankDTO)
//         {
//             try
//             {
//                 bool IsValid = false;
//                 if (_darmaster.SaveTankDetails(tankDTO, GlobalSchema, Con))
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
//         [Route("api/Dar/DarMasters/ValveCreation/GetTanksDetails")]
//         public async Task<IActionResult> GetTanksDetails()
//         {
//             List<TanksDTO> tanksDTOList = new List<TanksDTO>();
//             try
//             {
//                 tanksDTOList = await _darmaster.GetTanksDetails(GlobalSchema, Con);

//                 return tanksDTOList != null ? Ok(tanksDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }


//         [HttpPost]
//         [Route("api/Dar/DarMasters/ValveCreation/SaveMainValveDetails")]
//         public IActionResult SaveMainValveDetails([FromBody] MainValveDTO mainValveDTO)
//         {
//             try
//             {
//                 bool IsValid = false;
//                 if (_darmaster.SaveMainValveDetails(mainValveDTO, GlobalSchema, Con))
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
//         [Route("api/Dar/DarMasters/ValveCreation/GetMainValveDetails")]
//         public async Task<IActionResult> GetMainValveDetails()
//         {
//             List<MainValveDTO> mainValvesDTOList = new List<MainValveDTO>();
//             try
//             {
//                 mainValvesDTOList = await _darmaster.GetMainValveDetails(GlobalSchema, Con);

//                 return mainValvesDTOList != null ? Ok(mainValvesDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpGet]
//         [Route("api/Dar/DarMasters/ValveCreation/GetMainValvesByTankId")]
//         public async Task<IActionResult> GetMainValvesByTankId(string tankId)
//         {
//             List<MainValveDTO> mainvalvesDTOList = new List<MainValveDTO>();
//             try
//             {
//                 mainvalvesDTOList = await _darmaster.GetMainValvesByTankId(tankId,GlobalSchema, Con);

//                 return mainvalvesDTOList != null ? Ok(mainvalvesDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }


//         [HttpPost]
//         [Route("api/Dar/DarMasters/ValveCreation/SaveSubValveDetails")]
//         public IActionResult SaveSubValveDetails([FromBody] SubValveDTO subValveDTO)
//         {
//             try
//             {
//                 bool IsValid = false;
//                 if (_darmaster.SaveSubValveDetails(subValveDTO, GlobalSchema, Con))
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
//         [Route("api/Dar/DarMasters/ValveCreation/GetSubValveDetails")]
//         public async Task<IActionResult> GetSubValveDetails()
//         {
//             List<SubValveDTO> subValvesDTOList = new List<SubValveDTO>();
//             try
//             {
//                 subValvesDTOList = await _darmaster.GetSubValveDetails(GlobalSchema, Con);

//                 return subValvesDTOList != null ? Ok(subValvesDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }


//         [HttpGet]
//         [Route("api/Dar/DarMasters/CustomerCreation/GetRouteNames")]
//         public async Task<IActionResult> GetRouteNames()
//         {
//             List<RouteNamesDTO> routeNamessDTOList = new List<RouteNamesDTO>();
//             try
//             {
//                 routeNamessDTOList = await _darmaster.GetRouteNames(GlobalSchema, Con);

//                 return routeNamessDTOList != null ? Ok(routeNamessDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }


//         [HttpPost]
//         [Route("api/Dar/DarMasters/CustomerCreation/SaveCustomerDetails")]
//         public IActionResult SaveCustomerDetails([FromBody] CustomerDTO customerDTO)
//         {
//             try
//             {
//                 bool IsValid = false;
//                 if (_darmaster.SaveCustomerDetails(customerDTO, GlobalSchema, Con))
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
//         [Route("api/Dar/DarMasters/CustomerCreation/GetCustomerDetails")]
//         public async Task<IActionResult> GetCustomerDetails()
//         {
//             List<CustomerDTO> customersDTOList = new List<CustomerDTO>();
//             try
//             {
//                 customersDTOList = await _darmaster.GetCustomerDetails(GlobalSchema, Con);

//                 return customersDTOList != null ? Ok(customersDTOList) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpPost]
//         [Route("api/Dar/DarMasters/WeatherEntry/SaveWeatherDetails")]
//         public IActionResult SaveWeatherDetails([FromBody] WeatherEntryDTO weatherentryDTO)
//         {
//             try
//             {
//                 bool IsValid = false;
//                 if (_darmaster.SaveWeatherDetails(weatherentryDTO, GlobalSchema, Con))
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
//         [Route("api/Dar/DarMasters/ElectricityBill/GetServiceNos")]
//         public async Task<IActionResult> GetServiceNos()
//         {
//             List<ServiceNosDTO> serviceNosDTOlist = new List<ServiceNosDTO>();
//             try
//             {
//                 serviceNosDTOlist = await _darmaster.GetServiceNos(GlobalSchema, Con);

//                 return serviceNosDTOlist != null ? Ok(serviceNosDTOlist) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpGet]
//         [Route("api/Dar/DarMasters/ElectricityBill/GetUscNoValues")]
//         public async Task<IActionResult> GetUscNoValues(string serviceno)
//         {
//             ServiceNosDTO serviceNosDTOlist = new ServiceNosDTO();
//             try
//             {
//                 serviceNosDTOlist = await _darmaster.GetUscNoValues(serviceno,GlobalSchema, Con);

//                 return serviceNosDTOlist != null ? Ok(serviceNosDTOlist) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpPost]
//         [Route("api/Dar/DarMasters/ElectricityBill/SaveElectricityBillDetails")]
//         public IActionResult SaveElectricityBillDetails([FromBody] ElectricityBillDTO electricityBillDTO)
//         {
//             try
//             {
//                 bool IsValid = false;
//                 if (_darmaster.SaveElectricityBillDetails(electricityBillDTO, GlobalSchema, Con))
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
