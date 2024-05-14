using HikingTracks.Application.Interfaces;
using HikingTracks.Domain.DTO;
using HikingTracks.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HikingTracks.Presentation;

[ApiController]
public class SegmentController : ControllerBase
{
    private readonly IServiceManager _service;

    public SegmentController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet("api/segment")]
    public async Task<IActionResult> GetSegments(int limit = 0, int offset = 0)
    {
        var segments = await _service.SegmentService.GetAllSegments();

        if (offset > 0)
            segments = segments.Skip(offset);
        if (limit > 0)
            segments = segments.Take(limit);

        var segmentsDto = segments.Select(segment => segment.ToDTO()).ToList();
        return Ok(segmentsDto);
    }

    [HttpPost("api/segment")]
    public async Task<IActionResult> CreateSegment([FromBody] CreateSegmentDto createSegmentDto)
    {
        if (createSegmentDto is null)
            return BadRequest(new ErrorDetails
            {
                StatusCode = (int)System.Net.HttpStatusCode.BadRequest,
                Message = "Body is not provided."
            });
        if (createSegmentDto.Coordinates.Count == 0)
            return BadRequest(new ErrorDetails
            {
                StatusCode = (int)System.Net.HttpStatusCode.BadRequest,
                Message = string.Format("Coordinates must be set, provide the following fields: '{0}', for each coordinate", Coordinate.ValidCoordinateFormat) 
            });

        var segment = await _service.SegmentService.CreateSegment(createSegmentDto);

        return Created(string.Format("/api/segment/{0}", segment.ID), null);
    }
}
