using MediatR;

namespace ManagementSystem.Application.Users.Queries.GetAllUsers;

public record UserDto(string Id, string Username, string Email);

public record GetAllUsersQuery : IRequest<IEnumerable<UserDto>>;