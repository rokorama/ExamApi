using ExamApi.BusinessLogic;
using ExamApi.BusinessLogic.Helpers;
using ExamApi.Models.DTOs;
using ExamApi.Models.UploadRequests;
using ExamApi.UserAccess;
using Microsoft.AspNetCore.Authorization;
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
    private readonly IImageConverter _imageConverter;

    public InformationController(IPersonalInfoService personalInfoService,
                                 IAddressService addressService,
                                 IUserService userService,
                                 ILogger<InformationController> logger,
                                 IImageConverter imageConverter)
    {
        _personalInfoService = personalInfoService;
        _addressService = addressService;
        _userService = userService;
        _logger = logger;
        _imageConverter = imageConverter;
    }

    [Authorize]
    [HttpPost]
    public ActionResult<PersonalInfoDto> AddPersonalInfo([FromForm] PersonalInfoUploadRequest uploadRequest)
    {
        var userId = _userService.GetUserId(this.User!.Identity!.Name!);
        // var userId = Guid.NewGuid();
        var result = _personalInfoService.AddInfo(uploadRequest, userId);
        var baseUrl = $"{Request.Scheme}://{Request.Host}";

        return (result.Success is false) ? BadRequest(result.Message)
                                         : Created(new Uri(baseUrl + "/Information/" + userId + "/personalInfo"),
                                                   _personalInfoService.GetInfo(userId));
    }

    [HttpGet("{userId}/personalInfo")]
    public ActionResult<PersonalInfoDto> GetPersonalInfo([FromRoute] Guid userId)
    {
        var result = _personalInfoService.GetInfo(userId);

        return (result is null) ? NotFound() : Ok(result);
    }

    [Authorize]
    [HttpPatch("firstName")]
    public ActionResult UpdateFirstName([FromBody] string name)
    {
        var userId = _userService.GetUserId(this.User!.Identity!.Name!);
        var result = _personalInfoService.UpdateInfo<string>(userId, "FirstName", name);

        return (result.Success is false) ? BadRequest(result.Message) : NoContent();
    }

    [Authorize]
    [HttpPatch("lastName")]
    public ActionResult UpdateLastName([FromBody] string name)
    {
        var userId = _userService.GetUserId(this.User!.Identity!.Name!);
        var result = _personalInfoService.UpdateInfo<string>(userId, "LastName", name);

        return (result.Success is false) ? BadRequest(result.Message) : NoContent();
    }

    [Authorize]
    [HttpPatch("personalNumber")]
    public ActionResult UpdatePersonalNo([FromBody] ulong personalNo)
    {
        var user = _userService.GetUserId(this.User!.Identity!.Name!);
        var result = _personalInfoService.UpdateInfo<ulong>(user, "PersonalNumber", personalNo);

        return (result.Success is false) ? BadRequest(result.Message) : NoContent();
    }

    [Authorize]
    [HttpPatch("email")]
    public ActionResult UpdateEmail([FromBody] string email)
    {
        var user = _userService.GetUserId(this.User!.Identity!.Name!);
        var result = _personalInfoService.UpdateInfo<string>(user, "Email", email);

        return (result.Success is false) ? BadRequest(result.Message) : NoContent();
    }

    [Authorize]
    [HttpPatch("photo")]
    public ActionResult UpdatePhoto([FromForm] ImageUploadRequest image)
    {
        var user = _userService.GetUserId(this.User!.Identity!.Name!);
        var newImage = _imageConverter.ConvertImage(image);
        if (newImage is null)
            return BadRequest("Invalid photo provided. Please try a different file");
        var result = _personalInfoService.UpdateInfo<byte[]>(user, "Photo", newImage);

        return (result.Success is false) ? BadRequest(result.Message) : NoContent();
    }

    [Authorize]
    [HttpPatch("city")]
    public ActionResult UpdateCity([FromBody] string city)
    {
        var user = _userService.GetUserId(this.User!.Identity!.Name!);
        var result = _addressService.UpdateAddress<string>(user, "City", city);

        return (result.Success is false) ? BadRequest(result.Message) : NoContent();
    }

    [Authorize]
    [HttpPatch("street")]
    public ActionResult UpdateStreet([FromBody] string street)
    {
        var user = _userService.GetUserId(this!.User!.Identity!.Name!);
        var result = _addressService.UpdateAddress<string>(user, "Street", street);

        return (result.Success is false) ? BadRequest(result.Message) : NoContent();
    }

    [Authorize]
    [HttpPatch("house")]
    public ActionResult UpdateHouse([FromBody] string house)
    {
        var user = _userService.GetUserId(this.User!.Identity!.Name!);
        var result = _addressService.UpdateAddress<string>(user, "House", house);

        return (result.Success is false) ? BadRequest(result.Message) : NoContent();
    }

    [Authorize]
    [HttpPatch("flat")]
    public ActionResult UpdateFlat(int? flat = null)
    {
        var user = _userService.GetUserId(this.User!.Identity!.Name!);
        var result = _addressService.UpdateAddress<int?>(user, "Flat", flat);

        return (result.Success is false) ? BadRequest(result.Message) : NoContent();
    }
}