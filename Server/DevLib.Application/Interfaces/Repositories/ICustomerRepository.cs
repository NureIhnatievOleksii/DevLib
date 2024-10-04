using DevLib.Domain.CustomerAggregate;

namespace DevLib.Application.Interfaces.Repositories;

public interface ICustomerRepository
{
    Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(Customer customer, CancellationToken cancellationToken);
    Task UpdateAsync(Customer customer, CancellationToken cancellationToken);
    Task DeleteAsync(Customer customer, CancellationToken cancellationToken);
    Task<Customer> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
