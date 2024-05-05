using HikingTracks.Application.Interfaces;
using HikingTracks.Domain.DTO;
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
    public async Task<IActionResult> GetHikes(int limit = 0, int offset = 0)
    {
        var hikes = await _service.HikeService.GetAllHikes(limit, offset);
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

    [HttpDelete("{hikeID:guid}")]
    public async Task<IActionResult> DeleteHike(Guid hikeID)
    {
        await _service.HikeService.DeleteHike(hikeID);
        
        return Ok();
    }
}
