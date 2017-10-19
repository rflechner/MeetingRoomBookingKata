using System;
using System.Collections.Generic;

namespace MeetingRoomBookingKata.Domain.Temporality
{
    public class Day : Period
    {
        public Day(DateTime start, DateTime end) : base(start, end)
        {
        }

        public static Day From(DateTime date)
        {
            var start = date.Date;
            var end = start.AddDays(1).AddMilliseconds(-1);

            return new Day(start, end);
        }

        public IEnumerable<TimeSlot> ComputeSlots(TimeSpan duration)
        {
            var start = Start;

            while (start <= End)
            {
                var next = start.Add(duration);
                yield return new TimeSlot(start, next.AddMilliseconds(-1));
                start = next;
            }
        }
    }
}