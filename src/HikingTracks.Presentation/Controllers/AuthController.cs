using System.Security.Claims;
using HikingTracks.Application.Interfaces;
using HikingTracks.Domain;
using HikingTracks.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace HikingTracks.Presentation;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly IServiceManager _service;
    private readonly ITokenService _token;

    public AuthController(IServiceManager service, ITokenService token)
    {
        _service = service;
        _token = token;
    }
    
    [HttpPost("api/login")]
    public async Task<IActionResult> LoginAccount([FromBody] LoginAccountDto loginAccountDto)
    {
        var account = await _service.AccountService.LoginAccount(loginAccountDto);

        var claims = new List<Claim>(){
            new("accountId", account.ID.ToString())
        };
        var token = _token.WithPayload(claims).CreateToken();

        return Ok(new TokenDto{Token = token});
    }
}
