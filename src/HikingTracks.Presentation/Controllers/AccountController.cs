using HikingTracks.Application.Interfaces;
using HikingTracks.Domain;
using HikingTracks.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HikingTracks.Presentation.Controllers;

[Route("api/account")]
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

    public AccountController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
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

    [HttpGet("{accountID:guid}")]
    public async Task<IActionResult> GetAccount(Guid accountID) 
    {
        var account = await _service.AccountService.GetAccount(accountID);

        return Ok(account.ToDTO());
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDto createAccountDto)
    {
        if (createAccountDto is null) 
        {
            return BadRequest("Body is not provided");
        }

        var account = await _service.AccountService.CreateAccount(createAccountDto);

        return Created(string.Format("/api/account/{0}", account.ID), new { 
            Token = account.Token
        });
    }

    [HttpPut("{accountID:guid}")]
    public async Task<IActionResult> UpdateAccount(Guid accountID, [FromBody] UpdateAccountDto updateAccountDto)
    {
        if (updateAccountDto is null)
        {
            return BadRequest("Body is not provided");
        }

        _ = await _service.AccountService.UpdateAccount(accountID, updateAccountDto);

        return Ok();
    }

    [HttpDelete("{accountID:guid}")]
    public async Task<IActionResult> DeleteAccount(Guid accountID)
    {
        await _service.AccountService.DeleteAccount(accountID);

        return Ok();
    }

    [HttpPost("{accountID:guid}/hike")]
    public async Task<IActionResult> CreateHike(Guid accountID, [FromBody] CreateHikeDto createHikeDto)
    {
        if (createHikeDto is null)
        {
            return BadRequest("Body is not provided");
        }

        var hike = await _service.HikeService.CreateHike(accountID, createHikeDto);

        return Created(string.Format("/api/hike/{0}", hike.ID), null);
    }
}
