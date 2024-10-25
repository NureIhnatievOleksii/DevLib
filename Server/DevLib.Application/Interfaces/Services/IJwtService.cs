using DevLib.Domain.UserAggregate;

namespace DevLib.Application.Interfaces.Services;

public interface IJwtService
{
    Task<string> GenerateJwtTokenAsync(User user);
}
