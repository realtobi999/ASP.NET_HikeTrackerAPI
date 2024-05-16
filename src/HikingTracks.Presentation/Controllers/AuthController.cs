using System.Security.Claims;
using HikingTracks.Application.Interfaces;
using HikingTracks.Domain;
using HikingTracks.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace HikingTracks.Presentation;

[ApiController]
/*

POST    /api/login

*/
public class AuthController : ControllerBase
{
    private readonly IServiceManager _service;

    public AuthController(IServiceManager service)
    {
        _service = service;
    }
    
    [HttpPost("api/login")]
    public async Task<IActionResult> LoginAccount([FromBody] LoginAccountDto loginAccountDto)
    {
        var account = await _service.AccountService.LoginAccount(loginAccountDto);

        var claims = new List<Claim>(){
            new("accountId", account.ID.ToString())
        };
        var token = _service.TokenService.WithPayload(claims).CreateToken();

        return Ok(new TokenDto{Token = token});
    }
}
