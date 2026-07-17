using MediatR;

namespace ManagementSystem.Application.Users.Commands.Login;
public record LoginCommand(string Email, string Password) : IRequest<string>;