using MediatR;
using Microsoft.AspNetCore.Identity;
using ManagementSystem.Domain;
using Microsoft.VisualBasic;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;


namespace ManagementSystem.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
{
    private readonly UserManager<ApplicationUser> _userManager;

    private readonly RoleManager<IdentityRole> _roleManager;

    public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
        {
            return string.Empty;
        }

        var newUser = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,

        };
        var result = await _userManager.CreateAsync( newUser , request.Password);

        if (!result.Succeeded)
        {
            return string.Empty;
        }

        return newUser.Id;
    }
}    