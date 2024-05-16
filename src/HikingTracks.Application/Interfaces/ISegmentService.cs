using HikingTracks.Domain;
using HikingTracks.Domain.DTO;
using HikingTracks.Domain.Entities;

namespace HikingTracks.Application.Interfaces;

public interface ISegmentService
{
   Task<IEnumerable<Segment>> GetAllSegments(); 
   Task<Segment> GetSegment(Guid id);
   Task<Segment> CreateSegment(CreateSegmentDto createSegmentDto);
   Task<int> UpdateSegment(Guid id, UpdateSegmentDto updateSegmentDto   );
   Task DeleteSegment(Guid id);
   Task<IEnumerable<Segment>> GetHikeSegments(Hike hike);
}
