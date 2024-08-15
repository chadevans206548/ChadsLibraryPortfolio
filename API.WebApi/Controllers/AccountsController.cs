using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using ChadsLibraryPortfolio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Authentication;
using ViewModels.Registration;
using ViewModels.User;
using static ChadsLibraryPortfolio.Helpers.Constants;

namespace API.WebApi.Controllers;

[Route("api/accounts")]
[ApiController]
public class AccountsController(UserManager<User> userManager, IMapper mapper, Services.AuthenticationService authenticationService) : ControllerBase
{
    [HttpPost("Registration")]
    public async Task<IActionResult> RegisterUser([FromBody] AddUserViewModel addUserVM)
    {
        if (addUserVM == null || !this.ModelState.IsValid)
        {
            return this.BadRequest();
        }

        var user = mapper.Map<User>(addUserVM);

        var result = await userManager.CreateAsync(user, addUserVM.Password ?? string.Empty);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);

            return this.BadRequest(new RegistrationResponseViewModel { Errors = errors });
        }

        var userRole = addUserVM.Role ?? string.Empty;
        if (userRole.Equals(AuthPolicy.LibrarianUser, StringComparison.CurrentCultureIgnoreCase))
        {
            await userManager.AddToRoleAsync(user, AuthPolicy.LibrarianUser);
        }
        if (userRole.Equals(AuthPolicy.CustomerUser, StringComparison.CurrentCultureIgnoreCase))
        {
            await userManager.AddToRoleAsync(user, AuthPolicy.CustomerUser);
        }

        return this.StatusCode(201);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] AuthenticateUserViewModel authenticatedUserVM)
    {
        var user = await userManager.FindByNameAsync(authenticatedUserVM.Email ?? string.Empty);

        if (user == null || !await userManager.CheckPasswordAsync(user, authenticatedUserVM.Password ?? string.Empty))
        {
            return this.Unauthorized(new AuthenticationResponseViewModel { ErrorMessage = "Invalid Authentication" });
        }

        var signingCredentials = authenticationService.GetSigningCredentials();
        var claims = await authenticationService.GetClaims(user);
        var tokenOptions = authenticationService.GenerateTokenOptions(signingCredentials, claims);
        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return this.Ok(new AuthenticationResponseViewModel { IsAuthSuccessful = true, Token = token });
    }
}
