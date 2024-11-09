using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevLib.Application.CQRS.Commands.Posts.DeletePosts;

public record DeletePostCommand(Guid postId) : IRequest;
