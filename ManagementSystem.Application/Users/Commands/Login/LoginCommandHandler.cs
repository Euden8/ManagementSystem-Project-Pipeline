using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens; 
using Microsoft.IdentityModel.Tokens;
using ManagementSystem.Domain; 

namespace ManagementSystem.Application.Users.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public LoginCommandHandler(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new Exception("Invalid email or password.");
        }

        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new Dictionary<string, object>
        {
            { ClaimTypes.NameIdentifier, user.Id.ToString() },
            { ClaimTypes.Email, user.Email! },
            { JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString() }
        };


        if (userRoles.Count == 1)
        {
            authClaims.Add(ClaimTypes.Role, userRoles[0]);
        }
        else if (userRoles.Count > 1)
        {
            authClaims.Add(ClaimTypes.Role, userRoles.ToArray());
        }

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
            Claims = authClaims
        };
        
        var tokenHandler = new JsonWebTokenHandler();
        return tokenHandler.CreateToken(tokenDescriptor); // Returns string cleanly
    }
}