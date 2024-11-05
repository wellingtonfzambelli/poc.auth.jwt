// using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using poc.auth.jwt.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace poc.auth.jwt.Infrastructure;

public sealed class TokenProvider : ITokenProvider
{
    // Uncomment this property to user the IdentityUser
    // private readonly UserManager<IdentityUser> _userManager;

    private readonly IConfiguration _configuration;
    public TokenProvider(IConfiguration configuration) =>
       _configuration = configuration;

    public async Task<Tuple<string, string>> GenerateCredentialsAsync(User user)
    {
        // The line bellow is getting the User object by email from Identity
        // var user = await _userManager.FindByEmailAsync(email);

        var accessTokenClaims = await GetClaimsAsync(user);
        var refreshTokenClaims = await GetClaimsAsync(user, true);

        var expirationDateAccessToken = DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:AccessTokenExpiresInMinutes"]));
        var expirationDateRefreshToken = DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:RefreshTokenExpiresInMinutes"]));

        var accessToken = GenerateToken(accessTokenClaims, expirationDateAccessToken);
        var refreshToken = GenerateToken(refreshTokenClaims, expirationDateRefreshToken);

        return new Tuple<string, string>(accessToken, refreshToken);
    }

    private string GenerateToken(IEnumerable<Claim> claims, DateTime expirationDate)
    {
        var jwt = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            notBefore: DateTime.Now,
            expires: expirationDate,
            signingCredentials: new SigningCredentials
            (
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"])),
                SecurityAlgorithms.HmacSha512
            ));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    private async Task<IList<Claim>> GetClaimsAsync(User user, bool isRefreshToken = false)
    {
        var claims = new List<Claim>();
        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));

        if (isRefreshToken == false)
        {
            // Uncomment the line bellow to user Microsoft Identity
            // var userClaims = await _userManager.GetClaimsAsync(user);

            // Mocking the method GetClaimsAsync(user)
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            claims.AddRange(userClaims);

            await SetRoleToClaimsAsync(claims, user);
        }

        return claims;
    }

    private async Task SetRoleToClaimsAsync(IList<Claim> claims, User user)
    {
        // Uncomment the line bellow to use Microsoft Identity
        // var roles = await _userManager.GetRolesAsync(user);

        // Mocking the method GetRolesAsync(user);
        var roles = new List<string> { "Admin", "User" };

        foreach (var role in roles)
            claims.Add(new Claim("Role", role));
    }
}