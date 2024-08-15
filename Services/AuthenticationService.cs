using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ChadsLibraryPortfolio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Services;

public class AuthenticationService
{
    private readonly IConfiguration _configuration;
    private readonly IConfigurationSection _jwtSettings;
    private readonly UserManager<User> _userManager;
    public AuthenticationService(IConfiguration configuration, UserManager<User> userManager)
    {
        this._configuration = configuration;
        this._jwtSettings = this._configuration.GetSection("JwtSettings");
        this._userManager = userManager;
    }

    public SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(this._jwtSettings.GetSection("securityKey").Value ?? string.Empty);
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    public async Task<List<Claim>> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Email ?? string.Empty)
        };

        var roles = await this._userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }

    public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var tokenOptions = new JwtSecurityToken(
            issuer: this._jwtSettings["validIssuer"],
            audience: this._jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(this._jwtSettings["expiryInMinutes"])),
            signingCredentials: signingCredentials);

        return tokenOptions;
    }
}
