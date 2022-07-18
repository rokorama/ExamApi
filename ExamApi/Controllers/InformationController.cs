using ExamApi.BusinessLogic;
using ExamApi.Models;
using ExamApi.UserAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExamApi.Controllers;

[ApiController]
[Route("[controller]")]
public class InformationController : ControllerBase
{
    // private readonly ILoggerFactory _loggerFactory;
    // private readonly ILogger _logger;
    private readonly IPersonalInfoService _personalInfoService;
    private readonly IAddressService _addressService;
    
    private readonly IUserService _userService;

    public InformationController(
                                // ILoggerFactory loggerFactory,
                                // ILogger logger,
                                 IPersonalInfoService personalInfoService,
                                 IAddressService addressService,
                                 // do i need this???
                                 IUserService userService)
    {
        // _loggerFactory = loggerFactory;
        // _logger = logger;
        _personalInfoService = personalInfoService;
        _addressService = addressService;
        _userService = userService;
    }

    // [Authorize]
    [HttpPost]
    public ActionResult<PersonalInfo> AddPersonalInfo([FromForm] PersonalInfoDto personalInfoDto)
    {
        // var userId = _userService.GetUser(this.User.Identity.Name).Id;
        var userId = Guid.NewGuid();
        var newPersonalInfo = _personalInfoService.AddInfo(userId, personalInfoDto);
        // ditch the null
        // if (newPersonalInfo == null)
        //     return BadRequest();
        return Created(new Uri(Request.GetEncodedUrl() + "/" + newPersonalInfo.Id), newPersonalInfo);        
    }

    [HttpGet("{id}")]
    public ActionResult<PersonalInfo> GetInfo(Guid id)
    {
        var data = _personalInfoService.GetInfo(id); //simulation for the data base access

        return Ok(data);
    }
}