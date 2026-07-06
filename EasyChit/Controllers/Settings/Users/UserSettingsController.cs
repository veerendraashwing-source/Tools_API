// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Net;
// using System.Net.Mail;
// using System.Threading.Tasks;
// using Easychit_Infrastructure.Settings.Users;
// using Easychit_Repository.DataAccess.Settings.Users;
// using Easychit_Repository.Interfaces.Settings;
// using Microsoft.AspNetCore.Cors;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Logging;
// using Microsoft.AspNetCore.Authorization;
// using Easychit_Repository.Interfaces.Security;

// namespace Easychit_Api.Controllers.Settings.Users
// {
//     //[Authorize]
//     [EnableCors("CorsPolicy")]
//     [ApiController]
//     public class UserSettingsController : ControllerBase
//     {

//         string Con = string.Empty;
//         string GlobalSchema = string.Empty;
//         string db = string.Empty;
//         string Url = string.Empty;
//         static IConfiguration _iconfiguration;
//         private readonly IWebHostEnvironment _hostingEnvironment;
//         private readonly ILogger<UserSettingsController> _logger;
//         IUserSettings _UserSettings = new UserSettingsDAL();
//         private readonly IHttpContextAccessor _httpContextAccessor;


//         public UserSettingsController(IConfiguration iconfiguration, IWebHostEnvironment hostingEnvironment, ILogger<UserSettingsController> logger, IHttpContextAccessor httpContextAccessor)
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
//         [Route("api/Settings/Users/UserRights/GetUserRightsAGROBasedonRoleAnduserId")]
//         public async Task<IActionResult> GetUserRightsAGROBasedonRoleAnduserId(string userId)
//         {
//             List<UserRightsAgroParentModuleDTO> ParentModuleDTOlist = new List<UserRightsAgroParentModuleDTO>();
//             try
//             {
//                 ParentModuleDTOlist = await _UserSettings.GetUserRightsAGROBasedonRoleAnduserId(userId, GlobalSchema, Con);

//                 return ParentModuleDTOlist != null ? Ok(ParentModuleDTOlist) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }


//         [HttpPost]
//         [Route("api/Settings/Users/AGROChangeUserPassword")]
//         public IActionResult AGROChangeUserPassword([FromBody] UserRegistrationDTO _UserRegistrationDTO)
//         {
//             try
//             {

//                 //_UserRegistrationDTO.pConfirmPassWord = _passwordHasher.HashPassword(_UserRegistrationDTO.pConfirmPassWord);
//                 if (_UserSettings.AGROChangeUserPassword(_UserRegistrationDTO, GlobalSchema, Con))
//                 {
//                     return Ok(true);
//                 }
//                 else
//                 {
//                     return StatusCode(StatusCodes.Status304NotModified);
//                 }

//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }









//         [HttpGet]
//         [Route("api/Settings/Users/UserRights/GetParentModuleAGRO_By_UserId")]
//         public async Task<IActionResult> GetParentModuleAGRO_By_UserId(string userId)
//         {
//             List<UserRightsAgroParentModuleDTO> ParentModuleDTOlist = new List<UserRightsAgroParentModuleDTO>();
//             try
//             {
//                 ParentModuleDTOlist = await _UserSettings.GetParentModuleAGRO_By_UserId(userId, GlobalSchema, Con);

//                 return ParentModuleDTOlist != null ? Ok(ParentModuleDTOlist) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }


//         [HttpGet]
//         [Route("api/Settings/Users/UserRights/GetSubModuleAGRO")]
//         public async Task<IActionResult> GetSubModuleAGRO(string userId, long parentmoduleid)
//         {
//             List<UserRightsAgroSubModuleDTO> subModuleDTOlist = new List<UserRightsAgroSubModuleDTO>();
//             try
//             {
//                 subModuleDTOlist = await _UserSettings.GetSubModuleAGRO(userId, parentmoduleid, GlobalSchema, Con);

//                 return subModuleDTOlist != null ? Ok(subModuleDTOlist) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }

//         [HttpPost]
//         [Route("api/Settings/Users/RolesCreation/SaveAddFormToMenuAGRO")]
//         public IActionResult SaveAddFormToMenuAGRO([FromBody] FormsAGRODTO _FormsDTO)
//         {
//             try
//             {
//                 bool IsValid = false;
//                 IsValid = _UserSettings.SaveAddFormToMenuAGRO(_FormsDTO, GlobalSchema, Con);

//                 return Ok(IsValid);
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }
//         }



//         [HttpGet]
//         [Route("api/Settings/Users/UserRights/GetUserNamesAGRO")]

//         public async Task<IActionResult> GetUserNamesAGRO()
//         {
//             List<UsersDTO> UserDTOlist = new List<UsersDTO>();
//             try
//             {
//                 UserDTOlist = await _UserSettings.GetUserNamesAGRO(GlobalSchema, Con);

//                 return UserDTOlist != null ? Ok(UserDTOlist) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }

//         }


//         [HttpGet]
//         [Route("api/Settings/Users/UserRights/GetUserRightsFromsGridAGRO")]

//         public async Task<IActionResult> GetUserRightsFromsGridAGRO(long user_id, string submoduleid)
//         {
//             List<formaccesscheckAGRODto> Formslist = new List<formaccesscheckAGRODto>();
//             try
//             {
//                 Formslist = await _UserSettings.GetUserRightsFromsGridAGRO(GlobalSchema, Con, user_id, submoduleid);

//                 return Formslist != null ? Ok(Formslist) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
//             }
//             catch (Exception)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//                 throw;
//             }

//         }



//         [HttpPost]
//         [Route("api/Settings/Users/UserRights/SaveUserRightsAGRO")]
//         public IActionResult SaveUserRightsAGRO([FromBody] UserRightsAGRODTO _FormsDTO)
//         {
//             try
//             {
//                 bool IsValid = false;
//                 List<formaccesscheckAGRODto> _ListSubForms = new List<formaccesscheckAGRODto>();
//                 _ListSubForms = _FormsDTO._formaccesscheckDto;
//                 if (_UserSettings.SaveUserRightsAGRO(_FormsDTO, GlobalSchema, Con, _ListSubForms))
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
//         [Route("api/Settings/Users/UserRights/SaveUserCreationAGRO")]
//         public IActionResult SaveUserCreationAGRO([FromBody] UsersAGRODTO _FormsDTO)
//         {
//             try
//             {
//                 bool IsValid = false;

//                 if (_UserSettings.SaveUserCreationAGRO(_FormsDTO, GlobalSchema, Con))
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