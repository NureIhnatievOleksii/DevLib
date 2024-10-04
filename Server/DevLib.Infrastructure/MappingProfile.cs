using AutoMapper;
using DevLib.Application.CQRS.Commands.Customers.CreateCustomer;
using DevLib.Application.CQRS.Commands.Customers.UpdateCustomer;
using DevLib.Application.CQRS.Dtos.Queries;
using DevLib.Domain.CustomerAggregate;

namespace DevLib.Infrastructure;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<CreateCustomerCommand, Customer>();
        CreateMap<UpdateCustomerCommand, Customer>();

        CreateMap<Customer, GetCustomerByIdQueryDto>();
        CreateMap<Customer, GetAllCustomersQueryDto>();
    }
}
