using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevLib.Application.CQRS.Commands.Directories.DeleteDirectory;

public record DeleteDirectoryCommand(Guid Id) : IRequest<IdentityResult>;
