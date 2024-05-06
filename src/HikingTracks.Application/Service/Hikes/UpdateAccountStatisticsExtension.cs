using HikingTracks.Domain.Entities;

namespace HikingTracks.Application.Service.Hikes;

public static class UpdateAccountStatisticsExtension
{
    public static void UpdateAccountStatistics(this Account account, Hike hike)
    {
        account.TotalHikes++;
        account.TotalDistance += hike.Distance;
        account.TotalMovingTime += hike.MovingTime;
    }
}
