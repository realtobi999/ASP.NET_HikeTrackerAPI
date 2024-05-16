using System.Linq.Expressions;
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
        var segment = new Segment
        {
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

    public async Task<IEnumerable<Segment>> GetHikeSegments(Hike hike)
    {
        var segments = new List<Segment>();
        var allSegments = await _repository.Segment.GetAllSegments();
        var coordinates = hike.Coordinates;

        // We loop through each hike coordinate
        for (int i = 0; i < coordinates.Count; i++)
        {
            var coordinate = coordinates.ElementAt(i);
            for (int j = 0; j < allSegments.Count(); j++)
            {
                var segment = allSegments.ElementAt(j);

                // Foreach segment we check if the current hike coordinate matches the starting point coordinate of the segment
                if (coordinate.IsWithinRange(segment.Coordinates.First(), 1000))
                {
                    // If it does match we loop over the segment and match it with the rest of the hike coordinates starting at i (the hike coordinate we found the starting point match)
                    bool matches = true;
                    for (int k = 0; k < segment.Coordinates.Count; k++)
                    {
                        // We check if we are in range and then if the hike coordinate matches with the next segment coordinate
                        if (i + k >= coordinates.Count || !coordinates.ElementAt(i + k).IsWithinRange(segment.Coordinates.ElementAt(k), 1000))
                        {
                            matches = false;
                            break;
                        }
                    }

                    if (matches)
                    {
                        segments.Add(segment);
                        break;
                    }
                }
            }
        }

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
