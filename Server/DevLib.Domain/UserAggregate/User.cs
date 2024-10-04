using Microsoft.AspNetCore.Identity;

namespace DevLib.Domain.UserAggregate;

public class User : IdentityUser<Guid>
{
    public int Age { get; set; }
}