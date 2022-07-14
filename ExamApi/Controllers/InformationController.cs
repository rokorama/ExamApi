using ExamApi.BusinessLogic;
using ExamApi.Models;
using ExamApi.UserAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ExamApi.Controllers;

[ApiController]
[Route("[controller]")]
public class InformationController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IPersonalInfoService _personalInfoService;
    private readonly IResidenceInfoService _residenceInfoService;
    
    private readonly IUserService _userService;

    public InformationController(ILogger<InformationController> logger,
                                 IPersonalInfoService personalInfoService,
                                 IResidenceInfoService residenceInfoService,
                                 // do i need this???
                                 IUserService userService)
    {
        _logger = logger;
        _personalInfoService = personalInfoService;
        _residenceInfoService = residenceInfoService;
        _userService = userService;
    }

    // [Authorize]
    [HttpPost]
    public ActionResult<PersonalInfo> AddPersonalInfo([FromForm] PersonalInfoDto personalInfoDto, [FromForm] ResidenceInfoDto residenceInfoDto)
    {
        var userId = _userService.GetUser(this.User.Identity.Name).Id;
        var newPersonalInfo = _personalInfoService.AddInfo(userId, personalInfoDto);
        var newResidenceInfo = _residenceInfoService.AddInfo(residenceInfoDto);
        // ditch the null
        if (newPersonalInfo == null)
            return BadRequest();
        return Created(new Uri(Request.GetEncodedUrl() + "/" + newPersonalInfo.Id), newPersonalInfo);        
    }
}