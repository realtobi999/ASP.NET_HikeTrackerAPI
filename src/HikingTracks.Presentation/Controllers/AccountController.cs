using HikingTracks.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HikingTracks.Presentation;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IServiceManager _service;

    public AccountController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAccounts()
    {
        var accounts = _service.AccountService.GetAllAccounts();

        return Ok(accounts);
    }

    [HttpGet("/{accountID:guid}")]
    public IActionResult GetAccount(Guid accountID) 
    {
        var account = _service.AccountService.GetAccount(accountID);

        if (account is null)
        {
            return NotFound();
        }

        return Ok(account);
    }
}
