using ExamApi.Models;
using ExamApi.UserAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ExamApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;
    private readonly ILoginService _loginService;

    public UserController(IUserService userService,
                          ILoginService loginService,
                          ILogger<UserController> logger)
    {
        _userService = userService;
        _loginService = loginService;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUser([FromRoute] Guid id)
    {
        _logger.LogInformation($"Getting user info for user {id}");
        var result = _userService.GetUser(id);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost("signup")]
    public ActionResult<User> Signup([FromForm] UserDto userDto)
    {
        var newUser = _loginService.CreateUser(userDto.Username, userDto.Password);
        if (newUser == null)
            return BadRequest();
        _logger.LogInformation($"New user {newUser.Id} created at {DateTime.Now.ToString()}");
        return Created(new Uri(Request.GetEncodedUrl() + "/" + newUser.Id), newUser);
    }

    [HttpPost("login")]
    public ActionResult<string> Login([FromForm] UserDto userDto)
    {
        try
        {
            return _loginService.Login(userDto.Username, userDto.Password);
        }
        catch (Exception)
        {
            return BadRequest("Invalid credentials");
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{userId}")]
    public ActionResult<bool> DeleteUser([FromRoute] Guid userId)
    {
        if (!_userService.DeleteUser(userId))
            return BadRequest();
        _logger.LogInformation($"User {userId} was deleted by admin at {DateTime.Now.ToString()}");
        return NoContent();
    }
}
