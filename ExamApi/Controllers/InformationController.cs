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
        _personalInfoService = personalInfoService;
        _addressService = addressService;
        _userService = userService;
    }

    [Authorize]
    [HttpPost]
    public ActionResult<PersonalInfoDto> AddPersonalInfo([FromForm] PersonalInfoUploadRequest uploadRequest)
    {
        var userId = _userService.GetUser(this.User.Identity.Name).Id;
        if (!_personalInfoService.AddInfo(uploadRequest, userId, out PersonalInfo createdEntry))
        {
            return BadRequest(); // details would be nice here
        }
        var result = ObjectMapper.MapPersonalInfoDto(createdEntry);
        return Created(new Uri(Request.GetEncodedUrl() + "/" + createdEntry.Id), result);        
    }

    [HttpGet("{userId}")]
    public ActionResult<PersonalInfoDto> GetInfo(Guid userId)
    {
        if (!_personalInfoService.GetInfo(userId, out var result))
            return NotFound();
        return Ok(result);
    }

    [Authorize]
    [HttpPatch("firstName")]
    public ActionResult UpdateFirstName([FromBody] string name)
    {
        var userId = _userService.GetUser(this.User.Identity.Name).Id;
        if (String.IsNullOrWhiteSpace(name))
            return BadRequest();
        _personalInfoService.UpdateProperty<string>(userId, "FirstName", name);
        return NoContent();
    }

    [Authorize]
    [HttpPatch("personalNumber")]
    public ActionResult UpdateLastName([FromBody] ulong personalNo)
    {
        var userId = _userService.GetUser(this.User.Identity.Name).Id;
        if (personalNo.ToString().Length != 11)
            return BadRequest();
        _personalInfoService.UpdateProperty<ulong>(userId, "PersonalNumber", personalNo);
        return NoContent();
    }
}