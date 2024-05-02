﻿using HikingTracks.Application.Interfaces;
using HikingTracks.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HikingTracks.Presentation.Controllers;

[Route("api/hike")]
[ApiController]
public class HikeController : ControllerBase
{
    private readonly IServiceManager _service;

    public HikeController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetHikes()
    {
        var hikes = await _service.HikeService.GetAllHikes();
        var hikesDto = new List<HikeDto>();

        foreach (var hike in hikes)
        {
            hikesDto.Add(hike.ToDTO());
        }

        return Ok(hikesDto);
    }

    [HttpGet("{accountID:guid}")]
    public async Task<IActionResult> GetHike(Guid accountID)
    {
        var hike = await _service.HikeService.GetHike(accountID);

        return Ok(hike.ToDTO());
    }

    [HttpPost("{accountID:guid}")]
    public async Task<IActionResult> CreateHike(Guid accountID, [FromBody] CreateHikeDto createHikeDto)
    {
        if (createHikeDto is null)
        {
            return BadRequest("Body is not provided");
        }

        var hike = await _service.HikeService.CreateHike(accountID, createHikeDto);

        return Created(string.Format("/api/account/{0}", hike.ID), null);
    }
}
