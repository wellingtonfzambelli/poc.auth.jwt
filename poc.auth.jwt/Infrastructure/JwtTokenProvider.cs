using Microsoft.IdentityModel.Tokens;
using poc.auth.jwt.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace poc.auth.jwt.Infrastructure;

public sealed class JwtTokenProvider : IJwtTokenProvider
{
    private readonly IConfiguration _configuration;

    public JwtTokenProvider(IConfiguration configuration) =>
        _configuration = configuration;

    public string GetToken(User user)
    {
        var expirationDateAccessToken = DateTime.Now.AddMinutes(
            int.Parse(_configuration["Jwt:AccessTokenExpiresInMinutes"]));

        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(user),
            Expires = expirationDateAccessToken,
            NotBefore = DateTime.Now,
            SigningCredentials = credentials,
        };

        var token = handler.CreateToken(tokenDescriptor); // signing the JWT
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(User user)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
        claims.AddClaim(new Claim(ClaimTypes.Email, user.Email));

        return claims;
    }
}