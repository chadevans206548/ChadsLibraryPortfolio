using AutoMapper;
using ChadsLibraryPortfolio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Registration;
using ViewModels.User;

namespace API.WebApi.Controllers;

[Route("api/accounts")]
[ApiController]
public class AccountsController(UserManager<User> userManager, IMapper mapper) : ControllerBase
{
    [HttpPost("Registration")]
    public async Task<IActionResult> RegisterUser([FromBody] AddUserViewModel addUserVM)
    {
        if (addUserVM == null || !this.ModelState.IsValid)
        {
            return this.BadRequest();
        }

        var user = mapper.Map<User>(addUserVM);

        try
        {
            var result = await userManager.CreateAsync(user, addUserVM.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                return this.BadRequest(new RegistrationResponseViewModel { Errors = errors });
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }



        return this.StatusCode(201);
    }
}
