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
                          ILoginService loginService)
    {
        _userService = userService;
        _loginService = loginService;
    }

    // placeholder, for now
    [HttpGet("{id}")]
    public ActionResult<User> GetUser([FromRoute] Guid id)
    {
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
        return NoContent();
    }
}
