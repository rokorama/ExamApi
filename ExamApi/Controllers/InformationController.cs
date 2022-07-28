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
        var result = _personalInfoService.AddInfo(uploadRequest, userId!.Value);
        var baseUrl = $"{Request.Scheme}://{Request.Host}";

        return (result.Success is false) ? BadRequest(result.Message)
                                         : Created(new Uri(baseUrl + "/Information/" + userId + "/personalInfo"),
                                                   _personalInfoService.GetInfo(userId!.Value));
    }

    [HttpGet("{userId}/personalInfo")]
    public ActionResult<PersonalInfoDto> GetPersonalInfo([FromRoute] Guid userId)
    {
        var result = _personalInfoService.GetInfo(userId);

        return (result is null) ? NotFound() : Ok(result);
    }

    [Authorize]
    [HttpPatch("firstName")]
    public ActionResult UpdateFirstName(string name)
    {
        var userId = _userService.GetUserId(this.User!.Identity!.Name!);
        var result = _personalInfoService.UpdateInfo<string>(userId!.Value, "FirstName", name);

        return (result.Success is false) ? BadRequest(result.Message) : NoContent();
    }

    [Authorize]
    [HttpPatch("lastName")]
    public ActionResult UpdateLastName(string name)
    {
        var userId = _userService.GetUserId(this.User!.Identity!.Name!);
        var result = _personalInfoService.UpdateInfo<string>(userId!.Value, "LastName", name);

        return (result.Success is false) ? BadRequest(result.Message) : NoContent();
    }

    [Authorize]
    [HttpPatch("personalNumber")]
    public ActionResult UpdatePersonalNo(ulong personalNo)
    {
        var userId = _userService.GetUserId(this.User!.Identity!.Name!);
        var result = _personalInfoService.UpdateInfo<ulong>(userId!.Value, "PersonalNumber", personalNo);

        return (result.Success is false) ? BadRequest(result.Message) : NoContent();
    }

    [Authorize]
    [HttpPatch("email")]
    public ActionResult UpdateEmail(string email)
    {
        var userId = _userService.GetUserId(this.User!.Identity!.Name!);
        var result = _personalInfoService.UpdateInfo<string>(userId!.Value, "Email", email);

        return (result.Success is false) ? BadRequest(result.Message) : NoContent();
    }

    [Authorize]
    [HttpPatch("photo")]
    public ActionResult UpdatePhoto(ImageUploadRequest image)
    {
        var userId = _userService.GetUserId(this.User!.Identity!.Name!);
        var newImage = _imageConverter.ConvertImage(image);
        if (newImage is null)
            return BadRequest("Invalid photo provided. Please try a different file");
        var result = _personalInfoService.UpdateInfo<byte[]>(userId!.Value, "Photo", newImage);

        return (result.Success is false) ? BadRequest(result.Message) : NoContent();
    }

    [Authorize]
    [HttpPatch("city")]
    public ActionResult UpdateCity(string city)
    {
        var userId = _userService.GetUserId(this.User!.Identity!.Name!);
        var result = _addressService.UpdateAddress<string>(userId!.Value, "City", city);

        return (result.Success is false) ? BadRequest(result.Message) : NoContent();
    }

    [Authorize]
    [HttpPatch("street")]
    public ActionResult UpdateStreet(string street)
    {
        var userId = _userService.GetUserId(this!.User!.Identity!.Name!);
        var result = _addressService.UpdateAddress<string>(userId!.Value, "Street", street);

        return (result.Success is false) ? BadRequest(result.Message) : NoContent();
    }

    [Authorize]
    [HttpPatch("house")]
    public ActionResult UpdateHouse(string house)
    {
        var userId = _userService.GetUserId(this.User!.Identity!.Name!);
        var result = _addressService.UpdateAddress<string>(userId!.Value, "House", house);

        return (result.Success is false) ? BadRequest(result.Message) : NoContent();
    }

    [Authorize]
    [HttpPatch("flat")]
    public ActionResult UpdateFlat(int? flat = null)
    {
        var userId = _userService.GetUserId(this.User!.Identity!.Name!);
        var result = _addressService.UpdateAddress<int?>(userId!.Value, "Flat", flat);

        return (result.Success is false) ? BadRequest(result.Message) : NoContent();
    }
}