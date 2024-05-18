using HikingTracks.Application;
using HikingTracks.Application.Interfaces;
using HikingTracks.Domain;
using HikingTracks.Domain.DTO;
using HikingTracks.Domain.Entities;
using HikingTracks.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HikingTracks.Presentation.Controllers;

[ApiController]
/*

GET     /api/hike - params: limit, offset, accountId
GET     /api/hike/{hike_id}
POST    /api/hike
DELETE  /api/hike/{hike_id}
POST    /api/hike/{hike_id}/photos/upload
POST    /api/hike/{hike_id}/segment/upload

*/
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
            hikes = hikes.Where(hike => hike.AccountId == accountId);

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

    [Authorize, AccountAuth]
    [HttpPost("api/hike")]
    public async Task<IActionResult> CreateHike([FromBody] CreateHikeDto createHikeDto)
    {
        if (createHikeDto.Coordinates.Count == 0)
            throw new BadRequestException(string.Format("Coordinates must be set, provide the following fields: '{0}', for each coordinate", Coordinate.ValidCoordinateFormat));

        var hike = await _service.HikeService.CreateHike(createHikeDto);

        return Created(string.Format("/api/hike/{0}", hike.ID), null);
    }

    [Authorize, HikeAuth]
    [HttpDelete("api/hike/{hikeId:guid}")]
    public async Task<IActionResult> DeleteHike(Guid hikeId)
    {
        await _service.HikeService.DeleteHike(hikeId);

        return Ok();
    }

    [Authorize, HikeAuth]
    [HttpPost("api/hike/{hikeId:guid}/photo/upload")]
    public async Task<IActionResult> UploadHikePhotos(Guid hikeId, [FromForm] List<IFormFile> files)
    {
        var photos = new List<Photo>();

        foreach (var file in files)
        {
            var photo = await _service.PhotoService.CreatePhoto(new CreatePhotoDto(){
                HikeID = hikeId,
                FileName = file.FileName,
                Length = file.Length,
                Content = await _service.FormFileService.IntoByteArray(file)
            });

            photos.Add(photo);
        }

        await _service.HikeService.UpdateHikePictures(hikeId, photos);

        return Ok();
    }

    [Authorize, HikeAuth]
    [HttpPost("api/hike/{hikeId:guid}/segment/upload")]
    public async Task<IActionResult> UploadHikeSegments(Guid hikeId)
    {
        var hike = await _service.HikeService.GetHike(hikeId);
        var segments = await _service.SegmentService.GetHikeSegments(hike); 

        foreach (var segment in segments)
        {
            await _service.SegmentHikeService.CreateSegmentHike(segment, hike);
        }

        return Ok();
    }
}
