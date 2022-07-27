using ExamApi.BusinessLogic;
using ExamApi.BusinessLogic.Helpers;
using ExamApi.Models;
using ExamApi.Models.DTOs;
using ExamApi.Models.UploadRequests;
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
    private readonly ILogger<InformationController> _logger;
    private readonly IObjectMapper _mapper;
    private readonly string _baseUrl;

    public InformationController(
                                 IPersonalInfoService personalInfoService,
                                 IAddressService addressService,
                                 IUserService userService,
                                 ILogger<InformationController> logger,
                                 IObjectMapper mapper)
    {
        _personalInfoService = personalInfoService;
        _addressService = addressService;
        _userService = userService;
        _logger = logger;
        _mapper = mapper;
        _baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
    }

    [Authorize]
    [HttpPost]
    public ActionResult<PersonalInfoDto> AddPersonalInfo([FromForm] PersonalInfoUploadRequest uploadRequest)
    {
        var userId = _userService.GetUser(this.User!.Identity!.Name!).Id;

        var result = _personalInfoService.AddInfo(uploadRequest, userId);
        // if (result.Success is not true)
        //     return BadRequest(result.Message);



        // return Created(new Uri(_baseUrl + "/Information/" + _personalInfoService.GetInfoId(userId) + "/personalInfo"),
        //                _personalInfoService.GetInfo(userId));

        return (result.Success is false) ? BadRequest(result.Message)
                                         : Created(new Uri(_baseUrl + "/Information/" + _personalInfoService.GetInfoId(userId) + "/personalInfo"),
                                                   _personalInfoService.GetInfo(userId));
    }

    [HttpGet("{userId}/personalInfo")]
    public ActionResult<PersonalInfoDto> GetPersonalInfo([FromRoute] Guid userId)
    {
        var result = _personalInfoService.GetInfo(userId);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("{userId}/address")]
    public ActionResult<PersonalInfoDto> GetAddress([FromRoute] Guid userId)
    {
        var result = _addressService.GetAddress(userId);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [Authorize]
    [HttpPatch("firstName")]
    public ActionResult UpdateFirstName([FromBody] string name)
    {
        var userId = _userService.GetUser(this.User!.Identity!.Name!).Id;
        if (String.IsNullOrWhiteSpace(name))
            return BadRequest($"New value cannot be empty.");
        _personalInfoService.UpdateInfo<string>(userId, "FirstName", name);
        // throw up bad request if ResponseDto is negative
        return NoContent();
    }

    [Authorize]
    [HttpPatch("personalNumber")]
    public ActionResult UpdateLastName([FromBody] ulong personalNo)
    {
        var user = _userService.GetUser(this.User!.Identity!.Name!).Id;
        if (personalNo.ToString().Length != 11)
            return BadRequest($"Invalid personal number format");
        _personalInfoService.UpdateInfo<ulong>(user, "PersonalNumber", personalNo);
        return NoContent();
    }

    [Authorize]
    [HttpPatch("email")]
    public ActionResult UpdateEmail([FromBody] string email)
    {
        var user = _userService.GetUser(this.User!.Identity!.Name!).Id;
        if (String.IsNullOrWhiteSpace(email))
            return BadRequest($"New value cannot be empty.");
        _personalInfoService.UpdateInfo<string>(user, "Email", email);
        return NoContent();
    }

    [Authorize]
    [HttpPatch("photo")]
    public ActionResult UpdatePhoto([FromForm] ImageUploadRequest image)
    {
        var user = _userService.GetUser(this.User!.Identity!.Name!).Id;
        // var imageBytes = ImageConverter.ConvertImage(image);

        //placeholder value
        _personalInfoService.UpdateInfo<byte[]>(user, "Photo", new byte[] {});
        return NoContent();
    }

    [Authorize]
    [HttpPatch("city")]
    public ActionResult UpdateCity([FromBody] string city)
    {
        var user = _userService.GetUser(this.User!.Identity!.Name!).Id;
        if (String.IsNullOrWhiteSpace(city))
            return BadRequest($"New value cannot be empty.");
        _addressService.UpdateAddress<string>(user, "City", city);
        return NoContent();
    }

    [Authorize]
    [HttpPatch("street")]
    public ActionResult UpdateStreet([FromBody] string street)
    {
        var user = _userService.GetUser(this!.User!.Identity!.Name!).Id;
        if (String.IsNullOrWhiteSpace(street))
            return BadRequest($"New value cannot be empty.");
        _addressService.UpdateAddress<string>(user, "Street", street);
        return NoContent();
    }

    [Authorize]
    [HttpPatch("house")]
    public ActionResult UpdateHouse([FromBody] string house)
    {
        var user = _userService.GetUser(this.User!.Identity!.Name!).Id;
        if (String.IsNullOrWhiteSpace(house))
            return BadRequest($"New value cannot be empty.");
        _addressService.UpdateAddress<string>(user, "House", house);
        return NoContent();
    }

    [Authorize]
    [HttpPatch("flat")]
    public ActionResult UpdateFlat(int? flat = null)
    {
        var user = _userService.GetUser(this.User!.Identity!.Name!).Id;
        var updateSuccessful = _addressService.UpdateAddress<int?>(user, "Flat", flat).Success;
        if (updateSuccessful is false)
            return BadRequest();
        return NoContent();
    }
}