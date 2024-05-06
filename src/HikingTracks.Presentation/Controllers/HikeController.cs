﻿using System.Collections;
using HikingTracks.Application.Interfaces;
using HikingTracks.Domain.DTO;
using HikingTracks.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HikingTracks.Presentation.Controllers;

[ApiController]
public class HikeController : ControllerBase
{
    private readonly IServiceManager _service;

    public HikeController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet("api/hike")]
    public async Task<IActionResult> GetHikes(Guid accountId, int limit = 0, int offset = 0)
    {
        var hikes = await _service.HikeService.GetAllHikes();

        if (accountId != Guid.Empty)
            hikes = hikes.Where(hike => hike.accountId == accountId);

        if (offset > 0)
            hikes = hikes.Skip(offset);

        if (limit > 0)
            hikes = hikes.Take(limit);
        
        var hikesDto = hikes.Select(hike => hike.ToDTO()).ToList();
        return Ok(hikesDto);
    }

    [HttpGet("api/hike/{hikeId:guid}")]
    public async Task<IActionResult> GetHike(Guid hikeId)
    {
        var hike = await _service.HikeService.GetHike(hikeId);

        return Ok(hike.ToDTO());
    }
    
    [Authorize]
    [HttpPost("api/account/{accountId:guid}/hike")]
    public async Task<IActionResult> CreateHike(Guid accountId, [FromBody] CreateHikeDto createHikeDto)
    {
        if (createHikeDto is null)
        {
            return BadRequest("Body is not provided");
        }

        var hike = await _service.HikeService.CreateHike(accountId, createHikeDto);

        return Created(string.Format("/api/hike/{0}", hike.ID), null);
    }

    [HttpDelete("api/hike/{hikeId:guid}")]
    public async Task<IActionResult> DeleteHike(Guid hikeId)
    {
        await _service.HikeService.DeleteHike(hikeId);

        return Ok();
    }
}
