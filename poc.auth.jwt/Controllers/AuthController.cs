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
    private readonly ITokenProvider _tokenProvider;

    public AuthController(ITokenProvider tokenProvider)
    {
        _tokenProvider = tokenProvider;

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

        var token = await _tokenProvider.GenerateCredentialsAsync(user);

        return Ok(new
        {
            accessTokenProp = token.Item1,
            refreshToken = token.Item2
        });
    }
}