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

    [HttpGet("{id}")]
    public ActionResult<User> GetUser([FromRoute] Guid id)
    {
        var result = _userService.GetUser(id);
        if (result == null)
            return BadRequest();
        return Ok(result);
    }

    [HttpPost("signup")]
    public ActionResult<User> Signup(string username, string password)
    {
        var newUser = _userService.CreateUser(username, password);
        if (newUser == null)
            return BadRequest();
        return Created(new Uri(Request.GetEncodedUrl() + "/" + newUser.Id), newUser);
    }

    [HttpGet("login")]
    public ActionResult<string> Login(string username, string password)
    {
        try
        {
            return _userService.Login(username, password);
        }
        catch (Exception)
        {
            return BadRequest("Invalid credentials");
        }
    }
}
