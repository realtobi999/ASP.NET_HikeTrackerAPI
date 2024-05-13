using HikingTracks.Domain.Entities;

namespace HikingTracks.Application.Interfaces;

public interface ISegmentService
{
   Task<IEnumerable<Segment>> GetAllSegments(); 
}
