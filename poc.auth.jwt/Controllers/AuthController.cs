using Microsoft.AspNetCore.Mvc;
using poc.auth.jwt.Domain;
using poc.auth.jwt.Dto;
using poc.auth.jwt.Infrastructure;

namespace poc.auth.jwt.Controllers;

[ApiController]
[Route("api/v1/auth")]
public sealed class AuthController : Controller
{
    private readonly IList<User> _users = new List<User>();
    private readonly IJwtTokenProvider _jwtTokenProvider;

    public AuthController(IJwtTokenProvider jwtTokenProvider)
    {
        _jwtTokenProvider = jwtTokenProvider;

        // Mocking User
        _users.Add(new User
        {
            Id = 1,
            Name = "Wellington Zambelli",
            Email = "wellington.zambelli@test.com",
            UserName = "wellington.zambelli",
            Password = "123456"
        });
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginAsync
    (
        [FromBody] LoginRequestDto request,
        CancellationToken ct
    )
    {
        if (_users.SingleOrDefault(s => s.Email.Equals(request.Email) && s.Password.Equals(request.Password))
            is var user && user is null)
            return BadRequest("E-mail or Password invalid.");

        var token = _jwtTokenProvider.GetToken(user);

        return Ok(new { accessToken = token });
    }
}