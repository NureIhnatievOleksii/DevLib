using DevLib.Domain.PostAggregate;
using Microsoft.AspNetCore.Identity;

namespace DevLib.Domain.UserAggregate;

public class User : IdentityUser<Guid>
{
    public string ?Photo { get; set; }

    public ICollection<Post> Posts { get; set; } = new List<Post>();
}