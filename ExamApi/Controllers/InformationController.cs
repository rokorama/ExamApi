using ExamApi.BusinessLogic;
using ExamApi.DataAccess;
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
        _personalInfoService.UpdatePersonalInfo<string>(userId, "FirstName", name);
        return NoContent();
    }

    [Authorize]
    [HttpPatch("personalNumber")]
    public ActionResult UpdateLastName([FromBody] ulong personalNo)
    {
        var user = _userService.GetUser(this.User.Identity.Name).Id;
        if (personalNo.ToString().Length != 11)
            return BadRequest();
        _personalInfoService.UpdatePersonalInfo<ulong>(user, "PersonalNumber", personalNo);
        return NoContent();
    }

    [Authorize]
    [HttpPatch("email")]
    public ActionResult UpdateEmail([FromBody] string email)
    {
        var user = _userService.GetUser(this.User.Identity.Name).Id;
        if (email.ToString().Length != 11)
            return BadRequest();
        _personalInfoService.UpdatePersonalInfo<string>(user, "Email", email);
        return NoContent();
    }

    [Authorize]
    [HttpPatch("photo")]
    public ActionResult UpdatePhoto([FromForm] ImageUploadRequest image)
    {
        var user = _userService.GetUser(this.User.Identity.Name).Id;
        var imageBytes = ImageConverter.ConvertImage(image);
        _personalInfoService.UpdatePersonalInfo<byte[]>(user, "Photo", imageBytes);
        return NoContent();
    }

    [Authorize]
    [HttpPatch("city")]
    public ActionResult UpdateCity([FromBody] string city)
    {
        var user = _userService.GetUser(this.User.Identity.Name).Id;
        if (String.IsNullOrWhiteSpace(city))
            return BadRequest();
        _personalInfoService.UpdateAddress<string>(user, "City", city);
        return NoContent();
    }

    [Authorize]
    [HttpPatch("street")]
    public ActionResult UpdateStreet([FromBody] string street)
    {
        var user = _userService.GetUser(this.User.Identity.Name).Id;
        if (String.IsNullOrWhiteSpace(street))
            return BadRequest();
        _personalInfoService.UpdateAddress<string>(user, "Street", street);
        return NoContent();
    }

    [Authorize]
    [HttpPatch("house")]
    public ActionResult UpdateHouse([FromBody] string house)
    {
        var user = _userService.GetUser(this.User.Identity.Name).Id;
        if (String.IsNullOrWhiteSpace(house))
            return BadRequest();
        _personalInfoService.UpdateAddress<string>(user, "House", house);
        return NoContent();
    }

    [Authorize]
    [HttpPatch("flat")]
    public ActionResult UpdateFlat([FromBody] int flat)
    {
        var user = _userService.GetUser(this.User.Identity.Name).Id;
        if (String.IsNullOrWhiteSpace(flat.ToString()))
            return BadRequest();
        _personalInfoService.UpdateAddress<int>(user, "Flat", flat);
        return NoContent();
    }
}