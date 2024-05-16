using HikingTracks.Application;
using HikingTracks.Application.Interfaces;
using HikingTracks.Domain;
using HikingTracks.Domain.DTO;
using HikingTracks.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace HikingTracks.Presentation.Controllers;

[ApiController]
/*

GET     /api/account - params: limit, offset
GET     /api/account/{account_id}
POST    /api/account
PUT     /api/account/{account_id}
DELETE  /api/account/{account_id}

*/
public class AccountController : ControllerBase
{
    private readonly IServiceManager _service;

    public AccountController(IServiceManager service)
    {
        _service = service;
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

    [HttpGet("api/account/{accountId:guid}")]
    public async Task<IActionResult> GetAccount(Guid accountId)
    {
        var account = await _service.AccountService.GetAccount(accountId);

        return Ok(account.ToDTO());
    }

    [HttpPost("api/account")]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDto createAccountDto)
    {
        var account = await _service.AccountService.CreateAccount(createAccountDto);

        return Created(string.Format("/api/account/{0}", account.ID), null);
    }

    [Authorize, AccountAuth]
    [HttpPut("api/account/{accountId:guid}")]
    public async Task<IActionResult> UpdateAccount(Guid accountId, [FromBody] UpdateAccountDto updateAccountDto)
    {
        _ = await _service.AccountService.UpdateAccount(accountId, updateAccountDto);

        return Ok();
    }

    [Authorize, AccountAuth]
    [HttpDelete("api/account/{accountId:guid}")]
    public async Task<IActionResult> DeleteAccount(Guid accountId)
    {
        await _service.AccountService.DeleteAccount(accountId);

        return Ok();
    }
}
