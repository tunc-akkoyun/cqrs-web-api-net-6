using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Store.Application.Auth.Commands.Register;
using Store.Application.Auth.Queries.Login;
using Store.Domain.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace Store.Presentation.Controllers;

/// <summary>
/// Represents the authentication controller.
/// </summary>
[Route("api/v1/auth")]
[AllowAnonymous]
public sealed class AuthController : ApiController
{
    private readonly JwtSettings _jwtSettings;

    public AuthController(IOptions<JwtSettings> jwtSettings)
        => _jwtSettings = jwtSettings.Value;


    [HttpPost("login")]
    [SwaggerResponse(200)]
    [SwaggerOperation(Summary = "Login", Description = "Login user with email and password", OperationId = "loginUser")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new LoginQuery(request.Email, request.Password), cancellationToken);

        result.Token = GenerateToken(result.Hash, result.Name, result.Email);

        return Ok(result);
    }

    [HttpPost]
    [SwaggerResponse(200)]
    [SwaggerOperation(Summary = "Register", Description = "Creates a new user with email and password", OperationId = "registerUser")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request.Adapt<RegisterCommand>(), cancellationToken);

        result.Token = GenerateToken(result.Hash, result.Name, result.Email);

        return Ok(result);
    }

    private string GenerateToken(Guid hash, string name, string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Hash, hash.ToString()),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Email, email)
            }),
            Expires = DateTime.UtcNow.AddDays(_jwtSettings.ExpiryDays),
            Audience = _jwtSettings.Audience,
            Issuer = _jwtSettings.Issuer,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}