using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using poc.auth.jwt.Infrastructure;
using System.Text;

namespace poc.auth.jwt.Configuration;

public static class AuthenticationConfig
{
    public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]));

        // Token requirements
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidIssuer = configuration["Jwt:Issuer"],

            ValidateAudience = false,
            ValidAudience = configuration["Jwt:Audience"],

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = securityKey,

            RequireExpirationTime = true,
            ValidateLifetime = true,

            ClockSkew = TimeSpan.Zero
        };

        // .Net enables bearer token authentication
        services.AddAuthentication(p =>
        {
            p.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
            p.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        // .Net uses what we especified above
        .AddJwtBearer(p =>
        {
            p.RequireHttpsMetadata = false; // should be 'true' in production
            p.SaveToken = true;
            p.TokenValidationParameters = tokenValidationParameters;
        });

        services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();
        services.AddAuthorization();
    }

    public static void AppUseJwtConfiguration(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
