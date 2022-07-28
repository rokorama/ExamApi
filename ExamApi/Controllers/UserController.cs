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

        return (result is null) ? NotFound() : Ok(result);
    }

    [HttpPost("signup")]
    public ActionResult<User> Signup([FromForm] UserDto userDto)
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}";

        var result = _loginService.CreateUser(userDto.Username!, userDto.Password!);
        if (result.Success is false)
            return BadRequest(result.Message);

        var newUser = _userService.GetUser(userDto.Username!);
        _logger.LogInformation($"New user {newUser!.Username} created at {DateTime.Now}");
        return Created(new Uri(baseUrl + "/User/" + newUser.Id), newUser);
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
        return (!_userService.DeleteUser(userId))? BadRequest() : NoContent();
    }
}
