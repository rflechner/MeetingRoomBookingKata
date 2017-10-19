using System;

namespace MeetingRoomBookingKata.Domain
{
    public static class Constraints
    {
        public static readonly TimeSpan SlotDuration = TimeSpan.FromHours(1); // could be in a configuration or settings system

    }
}
