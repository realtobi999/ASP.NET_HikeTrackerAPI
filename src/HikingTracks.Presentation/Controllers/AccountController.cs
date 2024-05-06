using HikingTracks.Application;
using HikingTracks.Application.Interfaces;
using HikingTracks.Domain;
using HikingTracks.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace HikingTracks.Presentation.Controllers;

[ApiController]
/*

GET /api/account
GET /api/account/{account_id}
POST /api/account
PUT /api/account/{account_id}
DELETE /api/account/{account_id}

*/
public class AccountController : ControllerBase
{
    private readonly IServiceManager _service;
    private readonly IConfiguration _config;
    private readonly ITokenService _token;

    public AccountController(IServiceManager service, IConfiguration config, ITokenService token)
    {
        _service = service;
        _config = config;
        _token = token;
    }

    [HttpGet("api/account")]
    public async Task<IActionResult> GetAccounts(int limit = 0, int offset = 0)
    {
        var accounts = await _service.AccountService.GetAllAccounts();

        if (offset > 0) 
            accounts = accounts.Skip(offset);

        if (limit > 0) 
            accounts = accounts.Take(limit);

        var accountsDto = accounts.Select(account => account.ToDTO()).ToList();
        return Ok(accountsDto);
    }

    [HttpGet("api/account/{accountID:guid}")]
    public async Task<IActionResult> GetAccount(Guid accountID) 
    {
        var account = await _service.AccountService.GetAccount(accountID);

        return Ok(account.ToDTO());
    }

    [HttpPost("api/account")]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDto createAccountDto)
    {
        if (createAccountDto is null) 
            return BadRequest("Body is not provided");

        var account = await _service.AccountService.CreateAccount(createAccountDto);

        return Created(string.Format("/api/account/{0}", account.ID), null);
    }

    [HttpPut("api/account/{accountID:guid}")]
    public async Task<IActionResult> UpdateAccount(Guid accountID, [FromBody] UpdateAccountDto updateAccountDto)
    {
        if (updateAccountDto is null)
            return BadRequest("Body is not provided");

        _ = await _service.AccountService.UpdateAccount(accountID, updateAccountDto);

        return Ok();
    }

    [Authorize, AccountAuth]
    [HttpDelete("api/account/{accountID:guid}")]
    public async Task<IActionResult> DeleteAccount(Guid accountID)
    {
        await _service.AccountService.DeleteAccount(accountID);

        return Ok();
    }

    [HttpPost("api/account/token")]
    public async Task<IActionResult> LoginAccount([FromBody] LoginAccountDto loginAccountDto)
    {
        var account = await _service.AccountService.LoginAccount(loginAccountDto);

        var token = _token.CreateToken(account.ID.ToString());

        return Ok(token);
    }
}
