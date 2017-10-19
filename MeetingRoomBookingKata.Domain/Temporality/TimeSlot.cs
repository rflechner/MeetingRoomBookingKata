using System;

namespace MeetingRoomBookingKata.Domain.Temporality
{
    public class TimeSlot : Period
    {
        public TimeSlot(DateTime start, DateTime end) : base(start, end)
        {
        }
    }
}