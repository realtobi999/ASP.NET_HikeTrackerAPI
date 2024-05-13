using HikingTracks.Application.Interfaces;
using HikingTracks.Domain.Entities;
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

    public async Task<IEnumerable<Segment>> GetAllSegments()
    {
        var segments = await _repository.Segment.GetAllSegments();

        return segments;
    }
}
