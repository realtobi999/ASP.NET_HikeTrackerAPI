using HikingTracks.Application.Interfaces;
using HikingTracks.Domain.DTO;
using HikingTracks.Domain.Entities;
using HikingTracks.Domain.Exceptions;
using HikingTracks.Domain.Interfaces;

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
            ElevationLoss = createSegmentDto.ElevationLoss,
            CreatedAt = DateTimeOffset.UtcNow,
            Coordinates = createSegmentDto.Coordinates,
        };

        _repository.Segment.CreateSegment(segment);
        await _repository.SaveAsync();

        return segment;
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
}
