namespace DevLib.Domain.CustomerAggregate;
public class Customer
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string LinkedInUrl { get; set; }
    public string FacebookUrl { get; set; }
    public string InstagramUrl { get; set; }
    public string TwitterUrl { get; set; }
    public string WebsiteUrl { get; set; }
}
