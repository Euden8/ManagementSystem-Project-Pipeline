using MediatR;

namespace ManagementSystem.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(
    string Id,
    string Username,
    string Email
) : IRequest<bool>;