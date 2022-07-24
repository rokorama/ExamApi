using ExamApi.Models;
using ExamApi.Models.DTOs;
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

    [Authorize(Roles = "Admin")]
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
        // move this check somewhere?
        if (_userService.GetUser(userDto.Username!) != null)
            return BadRequest($"This username is taken, please try another.");
        var newUser = _loginService.CreateUser(userDto.Username!, userDto.Password!);
        if (newUser == null)
            return BadRequest();
        _logger.LogInformation($"New user {newUser.Id} created at {DateTime.Now}");
        return Created(new Uri(Request.GetEncodedUrl() + "/" + newUser.Id), newUser);
    }

    [HttpPost("login")]
    public ActionResult<string> Login([FromForm] UserDto userDto)
    {
        try
        {
            var token = _loginService.Login(userDto.Username!, userDto.Password!);
            _logger.LogInformation($"User {userDto.Username} logged in at {DateTime.Now}");
            return token;
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Failed login for {userDto.Username} at {DateTime.Now}: {ex.Message}");
            return BadRequest("Invalid credentials");
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{userId}")]
    public ActionResult<bool> DeleteUser([FromRoute] Guid userId)
    {
        if (!_userService.DeleteUser(userId))
            return BadRequest();
        _logger.LogInformation($"User {userId} was deleted by admin at {DateTime.Now}");
        return NoContent();
    }
}
