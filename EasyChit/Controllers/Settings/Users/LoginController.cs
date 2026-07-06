// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Easychit_Infrastructure.Settings.Users;
// using Easychit_Repository.DataAccess;
// using Easychit_Repository.Interfaces.Security;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Cors;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Logging;

// // For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

// namespace Easychit_Api.Controllers.Settings.Users
// {
//     [Authorize]
//     [EnableCors("CorsPolicy")]
//     [ApiController]
//     public class LoginController : ControllerBase
//     {
//         string Con = string.Empty;
//         string Con1 = string.Empty;
//         string GlobalSchema = string.Empty;
//         static IConfiguration _iconfiguration;
//         private IUserService _userService;

//         private readonly IWebHostEnvironment _hostingEnvironment;
//         private readonly ILogger<LoginController> _logger;
//         private readonly IHttpContextAccessor _httpContextAccessor;

//         public LoginController(IUserService userService, IConfiguration iconfiguration, IWebHostEnvironment hostingEnvironment, ILogger<LoginController> logger, IHttpContextAccessor httpContextAccessor)
//         {
//             _logger = logger;
//             _userService = userService;

//             _httpContextAccessor = httpContextAccessor;
//             _iconfiguration = iconfiguration;
//             _hostingEnvironment = hostingEnvironment;
//             Con = _iconfiguration.GetSection("ConnectionStrings").GetSection("Connection").Value;
//             Con1 = _iconfiguration.GetSection("ConnectionStrings").GetSection("Connection1").Value;
//             GlobalSchema = _iconfiguration.GetSection("ConnectionStrings").GetSection("GlobalSchemeName").Value;

//         }



//         [AllowAnonymous]
//         [Route("/api/RegionName")]
//         [HttpGet]
//         public async Task<IActionResult> RegionName()
        
//         {
//             try
//             {
//                 List<UserWiseBranches> _lstUserWiseBranches = await _userService.RegionName(GlobalSchema, Con1);
//                 return Ok(_lstUserWiseBranches);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "GetALLRegionName");
//                 throw ex;
//             }
//         }



//         [AllowAnonymous]
//         [Route("/api/GetALLBranchesBasedOnRegion")]
//         [HttpGet]
//         public async Task<IActionResult> GetALLBranchesBasedOnRegion(string region_Name)
//         {
//             try
//             {
//                 List<UserWiseBranches> _lstUserWiseBranches = await _userService.GetALLBranchesBasedOnRegion(GlobalSchema, region_Name, Con1);
//                 return Ok(_lstUserWiseBranches);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "GetALLBranches");
//                 throw ex;
//             }
//         }



//         [AllowAnonymous]
//         [Route("/api/AGROlogin")]
//         [HttpPost]
//         public IActionResult AGROAuthenticate([FromBody] UserAccessDTO userParam)
//         {
//             string baseCon = _iconfiguration.GetConnectionString("Connection");

//             Con = baseCon.Replace("DBNAME", userParam.pBranchname);

//             var user = _userService.AGROAuthenticate(userParam.pUserName, userParam.pPassword, GlobalSchema, Con);

//             var remoteIpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

//             if (user != null)
//                 user.ipaddress = remoteIpAddress;

//             if (user == null)
//                 return Unauthorized();

//             return Ok(user);
//         }



//     }
// }
