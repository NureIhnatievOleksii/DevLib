using MediatR;
using Microsoft.AspNetCore.Mvc;
using DevLib.Application.CQRS.Commands.Customers.CreateCustomer;
using DevLib.Application.CQRS.Commands.Customers.DeleteCustomer;
using DevLib.Application.CQRS.Commands.Customers.UpdateCustomer;
using DevLib.Application.CQRS.Queries.Customers.GetAllCustomers;
using DevLib.Application.CQRS.Queries.Customers.GetCustomerById;
using System.ComponentModel.DataAnnotations;

namespace DevLib.Api.Controllers;

[Route("api/[controller]/[action]")]
public class CustomerController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get_All_Customers(CancellationToken cancellationToken)
    {
        var customers = await mediator.Send(new GetAllCustomersQuery(), cancellationToken);

        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get_Customer_By_Id(Guid id, CancellationToken cancellationToken)
    {
        var customer = await mediator.Send(new GetCustomerByIdQuery(id), cancellationToken);

        if (customer == null)
        {
            return NotFound();
        }

        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Create_Customer([FromBody, Required] CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update_Customer([FromBody, Required] UpdateCustomerCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete_Customer(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteCustomerCommand(id), cancellationToken);

        return Ok();
    }
}
