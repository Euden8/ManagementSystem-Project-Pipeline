using ManagementSystem.Application.Common.Interfaces;

namespace ManagementSystem.Infrastructure.Common;

public class CurrentUserService:ICurrentUserService
{
    public string? UserId { get; }
}