using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ExamApi.UserAccess;

public class JwtService : IJwtService
{
    private readonly IConfiguration _iConfig;

    public JwtService(IConfiguration iConfig)
    {
        _iConfig = iConfig;
    }

    public string GetJwtToken(string username, string role)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };

        var secretToken = _iConfig.GetSection("Jwt:Secret").Value;
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretToken));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            issuer: _iConfig.GetSection("Jwt:Issuer").Value,
            audience: _iConfig.GetSection("Jwt:Audience").Value,
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: cred
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}