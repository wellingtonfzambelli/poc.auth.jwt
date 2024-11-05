namespace poc.auth.jwt.Dto;

public sealed class LoginRequestDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}