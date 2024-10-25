using AutoMapper;
using DevLib.Application.CQRS.Commands.Customers.CreateCustomer;
using DevLib.Application.CQRS.Commands.Customers.UpdateCustomer;
using DevLib.Application.CQRS.Commands.Directories.CreateDirectories;
using DevLib.Application.CQRS.Commands.Directories.UpdateDirectories;
using DevLib.Application.CQRS.Dtos.Queries;
using DevLib.Domain.CustomerAggregate;
using DevLib.Domain.DirectoryAggregate;

namespace DevLib.Infrastructure;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateCustomerCommand, Customer>();
        CreateMap<UpdateCustomerCommand, Customer>();

        CreateMap<Customer, GetCustomerByIdQueryDto>();
        CreateMap<Customer, GetAllCustomersQueryDto>();

        CreateMap<CreateDirectoryCommand, DLDirectory>()
            .ForMember(dest => dest.DirectoryId, opt => opt.MapFrom(src => Guid.NewGuid()));

        CreateMap<UpdateDirectoryCommand, DLDirectory>()
            .ForMember(dest => dest.DirectoryId, opt => opt.MapFrom(src => src.DirectoryId));
    }
}
