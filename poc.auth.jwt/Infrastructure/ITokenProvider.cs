using poc.auth.jwt.Domain;

namespace poc.auth.jwt.Infrastructure;

public interface ITokenProvider
{
    Task<Tuple<string, string>> GenerateCredentialsAsync(User user);
}