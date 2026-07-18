using MediatR;

namespace ManagementSystem.Application.Users.Commands.RegisterUser;

public record RegisterUserCommand(string Email, string Password, string FirstName, string LastName) : IRequest<string>;

