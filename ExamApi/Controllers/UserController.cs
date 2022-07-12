using ExamApi.Models;
using ExamApi.UserAccess;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ExamApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpPost]
    public ActionResult<User> Signup(string username, string password)
    {
        var newUser = _userService.CreateUser(username, password);
        if (newUser == null)
            return BadRequest();
        return Created(new Uri(Request.GetEncodedUrl() + "/" + newUser.Id), newUser);
    }
}
