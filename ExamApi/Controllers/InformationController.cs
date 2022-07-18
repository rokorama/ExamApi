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
    private readonly IPersonalInfoService _personalInfoService;
    private readonly IAddressService _addressService;
    
    private readonly IUserService _userService;

    public InformationController(
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

    [Authorize]
    [HttpPost]
    public ActionResult<PersonalInfoDto> AddPersonalInfo([FromForm] PersonalInfoUploadRequest uploadRequest)
    {
        var userId = _userService.GetUser(this.User.Identity.Name).Id;
        var createdEntry = _personalInfoService.AddInfo(uploadRequest, userId);
        var result = ObjectMapper.MapPersonalInfoDto(createdEntry);
        return Created(new Uri(Request.GetEncodedUrl() + "/" + createdEntry.Id), result);        
    }

    [HttpGet("{id}")]
    public ActionResult<PersonalInfoDto> GetInfo(Guid id)
    {
        if (!_personalInfoService.GetInfo(id, out var result))
            return NotFound();
        return Ok(result);
    }
}