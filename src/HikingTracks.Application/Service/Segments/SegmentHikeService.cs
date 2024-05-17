using HikingTracks.Application.Interfaces;
using HikingTracks.Domain.Entities;
using HikingTracks.Domain.Interfaces;

namespace HikingTracks.Application.Service.Segments;

public class SegmentHikeService : ISegmentHikeService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;

    public SegmentHikeService(IRepositoryManager repository, ILoggerManager logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<SegmentHike> CreateSegmentHike(Segment segment, Hike hike)
    {
        var segmentHike = new SegmentHike(){
            SegmentId = segment.ID,
            Segment = segment,
            HikeId = hike.ID,
            Hike = hike,
        };

        _repository.SegmentHike.CreateSegmentHike(segmentHike);
        await _repository.SaveAsync();

        return segmentHike;
    }
}
