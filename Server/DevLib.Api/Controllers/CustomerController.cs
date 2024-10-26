using MediatR;
using Microsoft.AspNetCore.Mvc;
using DevLib.Application.CQRS.Commands.Customers.CreateCustomer;
using DevLib.Application.CQRS.Commands.Customers.DeleteCustomer;
using DevLib.Application.CQRS.Commands.Customers.UpdateCustomer;
using DevLib.Application.CQRS.Queries.Customers.GetAllCustomers;
using DevLib.Application.CQRS.Queries.Customers.GetCustomerById;
using System.ComponentModel.DataAnnotations;
//using Microsoft.AspNetCore.Authorization;

namespace DevLib.Api.Controllers;

[Route("api/[controller]/[action]")]
public class CustomerController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    //[Authorize(Roles = "Client")]
    public async Task<IActionResult> GetAllCustomers(CancellationToken cancellationToken)
    {
        var customers = await mediator.Send(new GetAllCustomersQuery(), cancellationToken);

        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(Guid id, CancellationToken cancellationToken)
    {
        var customer = await mediator.Send(new GetCustomerByIdQuery(id), cancellationToken);

        if (customer == null)
        {
            return NotFound();
        }

        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody, Required] CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCustomer([FromBody, Required] UpdateCustomerCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteCustomerCommand(id), cancellationToken);

        return Ok();
    }
}
