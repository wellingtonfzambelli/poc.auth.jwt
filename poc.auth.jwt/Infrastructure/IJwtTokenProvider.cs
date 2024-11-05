using poc.auth.jwt.Domain;

namespace poc.auth.jwt.Infrastructure;

public interface IJwtTokenProvider
{
    string GetToken(User user);
}
