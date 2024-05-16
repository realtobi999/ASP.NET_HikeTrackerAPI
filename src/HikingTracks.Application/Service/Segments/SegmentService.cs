using HikingTracks.Application.Interfaces;
using HikingTracks.Domain;
using HikingTracks.Domain.DTO;
using HikingTracks.Domain.Entities;
using HikingTracks.Domain.Exceptions;
using HikingTracks.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace HikingTracks.Application;

public class SegmentService : ISegmentService 
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;

    public SegmentService(IRepositoryManager repository, ILoggerManager logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Segment> CreateSegment(CreateSegmentDto createSegmentDto)
    {
        var segment = new Segment{
            ID = createSegmentDto.ID ?? Guid.NewGuid(),
            Name = createSegmentDto.Name,
            Distance = createSegmentDto.Distance,
            ElevationGain = createSegmentDto.ElevationGain,
            ElevationLoss = createSegmentDto.ElevationLoss,
            CreatedAt = DateTimeOffset.UtcNow,
            Coordinates = createSegmentDto.Coordinates,
        };

        _repository.Segment.CreateSegment(segment);
        await _repository.SaveAsync();

        return segment;
    }

    public async Task DeleteSegment(Guid id)
    {
        var segment = await _repository.Segment.GetSegment(id) ?? throw new SegmentNotFoundException(id);

        _repository.Segment.DeleteSegment(segment);
        await _repository.SaveAsync();
    }

    public async Task<IEnumerable<Segment>> GetAllSegments()
    {
        var segments = await _repository.Segment.GetAllSegments();

        return segments;
    }

    public async Task<Segment> GetSegment(Guid id)
    {
        var segment = await _repository.Segment.GetSegment(id) ?? throw new SegmentNotFoundException(id);

        return segment;
    }

    public async Task<int> UpdateSegment(Guid id, UpdateSegmentDto updateSegmentDto) 
    {
        var segment = await _repository.Segment.GetSegment(id) ?? throw new SegmentNotFoundException(id);

        segment.Name = updateSegmentDto.Name;
        segment.Distance = updateSegmentDto.Distance;
        segment.ElevationGain = updateSegmentDto.ElevationGain;
        segment.ElevationGain = updateSegmentDto.ElevationLoss;

        // Updating the coordinates is not required in the Dto
        if (!updateSegmentDto.Coordinates.IsNullOrEmpty())
        {
            segment.Coordinates = updateSegmentDto.Coordinates;
        }

        return await _repository.SaveAsync();
    }
}
