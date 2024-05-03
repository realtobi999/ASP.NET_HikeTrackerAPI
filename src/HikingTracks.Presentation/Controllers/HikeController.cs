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

    [HttpGet("{hikeID:guid}")]
    public async Task<IActionResult> GetHike(Guid hikeID)
    {
        var hike = await _service.HikeService.GetHike(hikeID);

        return Ok(hike.ToDTO());
    }
}
