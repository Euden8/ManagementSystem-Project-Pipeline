using MediatR;
using Microsoft.AspNetCore.Identity;
using ManagementSystem.Domain;
using Microsoft.VisualBasic;

namespace ManagementSystem.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UpdateUserCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id);

        if (user == null)
        {
            throw new Exception("User not found.");
        }

        user.UserName = request.Username;
        user.Email = request.Email;

        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }
}