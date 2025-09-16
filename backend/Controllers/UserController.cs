using Microsoft.AspNetCore.Mvc;
using backend.Interfaces;
using backend.JWT;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using backend.Services;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUser _UserService;
    private readonly WebsiteService _WebSiteService;

    public UserController(IUser user, WebsiteService WebSiteService)
    {
        _UserService = user;
        _WebSiteService = WebSiteService;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterDTO user)
    {

        if (string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Password) || string.IsNullOrWhiteSpace(user.Email))
        {
            return BadRequest("Имя и пароль обязательны");
        }

        var existingUser = _UserService.GetByName(user.Name);
        if (existingUser != null)
        {
            return Conflict("Имя занято");
        }

        _UserService.Register(user);
        var token = JWTHelper.GenerateToken(new UserDTO { Name = user.Name });
        return Ok(new { token });
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDTO user)
    {
        if (string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Password))
        {
            return BadRequest("Имя и пароль обязательны");
        }
        var existingUser = _UserService.GetByName(user.Name);
        if (existingUser == null)
        {
            return Unauthorized("Пользователь не зарегистрирован");
        }
        if (!BCrypt.Net.BCrypt.Verify(user.Password, existingUser.PasswordHash))
        {
            return Unauthorized("Имя или парол неправильны");
        }
        var token = JWTHelper.GenerateToken(existingUser);
        return Ok(new { token });
    }

    [HttpGet("me")]
    [Authorize]
    public IActionResult GetCurrentUser()
    {
        // Получаем имя пользователя из токена
        var userName = User.Identity.Name;
        var user = _UserService.GetByName(userName);

        if (user == null)
        {
            return NotFound("Пользователь не найден");
        }
        Console.WriteLine(user.Name);
        return Ok(user);
    }

    [HttpPost("me")]
    [Authorize]
    public ActionResult<WebSiteDTO> Add([FromBody] WebSiteDTO site)
    {
        var userName = User.Identity.Name;
        var data = _WebSiteService.Add(site.URL, userName);
        return Ok(data);
    }
}