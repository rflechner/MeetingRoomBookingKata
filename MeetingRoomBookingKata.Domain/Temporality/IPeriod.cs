using System;

namespace MeetingRoomBookingKata.Domain.Temporality
{
    public interface IPeriod
    {
        DateTime Start { get; }
        DateTime End { get; }

        bool Contains(IPeriod period);
        bool IntersectWith(IPeriod period);
    }
}
