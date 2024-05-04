using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DatapacLibrary.ApplicationCore.Commands;
using DatapacLibrary.Domain;
using DatapacLibrary.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatapacLibrary.ApplicationCore.Handlers;

public class AuthenticateUserHandler : IRequestHandler<AuthenticateAdminCommand, string>
{
    private readonly IAdminRepository _adminRepository;
    private readonly IConfiguration _config;

    public AuthenticateUserHandler(IAdminRepository adminRepository, IConfiguration config)
    {
        _adminRepository = adminRepository;
        _config = config;
    }

    public async Task<string> Handle(AuthenticateAdminCommand request, CancellationToken cancellationToken)
    {
        var admin = await _adminRepository.GetAdminAsync(request.Name);
        if (admin == null)
            throw new ArgumentException("User name or password is incorrect!");
        if (!AuthenticationHelper.VerifyPassword(request.Password ,admin.Password, admin.Salt))
            throw new ArgumentException("User name or password is incorrect!");

        return BuildToken();
    }

    private string BuildToken()
    {
        var issuer = _config["JwtSettings:Issuer"];
        var audience = _config.GetSection("JwtSettings:Audience").Get<string[]>();
        var keyConfig = _config["JwtSettings:Key"];
        if (keyConfig is null || audience is null)
            throw new Exception("key is missing");
        var key = Encoding.ASCII.GetBytes(keyConfig);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            ]),
            Expires = DateTime.UtcNow.AddMinutes(30),
            Issuer = issuer,
            Claims = new Dictionary<string, object> {{JwtRegisteredClaimNames.Aud, audience}},
            SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}