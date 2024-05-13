using HikingTracks.Application.Interfaces;
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
}
