using HikingTracks.Application.Interfaces;
using HikingTracks.Domain;
using HikingTracks.Domain.DTO;
using HikingTracks.Domain.Entities;
using HikingTracks.Domain.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HikingTracks.Presentation;

[ApiController]
/*

GET     /api/segment - params: limit, offset
GET     /api/segment/{segment_id}
POST    /api/segment
PUT     /api/segment/{segment_id}

*/
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

    [HttpGet("api/segment/{segmentId:guid}")]
    public async Task<IActionResult> GetSegment(Guid segmentId)
    {
        var segment = await _service.SegmentService.GetSegment(segmentId);

        return Ok(segment.ToDTO());
    }

    [HttpPost("api/segment")]
    public async Task<IActionResult> CreateSegment([FromBody] CreateSegmentDto createSegmentDto)
    {
        if (createSegmentDto.Coordinates.Count == 0)
            throw new BadRequestException(string.Format("Coordinates must be set, provide the following fields: '{0}', for each coordinate", Coordinate.ValidCoordinateFormat));

        var segment = await _service.SegmentService.CreateSegment(createSegmentDto);

        return Created(string.Format("/api/segment/{0}", segment.ID), null);
    }

    [HttpPut("api/segment/{segmentId:guid}")]
    public async Task<IActionResult> UpdateSegment(Guid segmentId, [FromBody] UpdateSegmentDto updateSegmentDto)
    {
        if (updateSegmentDto.Coordinates.Count == 0)
           throw new BadRequestException(string.Format("Coordinates must be set, provide the following fields: '{0}', for each coordinate", Coordinate.ValidCoordinateFormat)); 

        var affected = await _service.SegmentService.UpdateSegment(segmentId, updateSegmentDto);

        if (affected == 0)
            throw new InternalServerErrorException("No rows affected.");
            
        return Ok();
    } 
}
