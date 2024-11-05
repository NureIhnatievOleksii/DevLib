using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevLib.Application.CQRS.Commands.Tags.DeleteTagsFromBook
{
    public record DeleteTagFromBookCommand(Guid bookId, Guid tagId) : IRequest;
}
