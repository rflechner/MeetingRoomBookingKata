using System;

namespace MeetingRoomBookingKata.Domain.Temporality
{
    public abstract class Period : IPeriod
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        protected Period(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public bool Contains(IPeriod period)
        {
            return Start <= period.Start && period.End <= End;
        }

        public bool Contains(DateTime dateTime)
        {
            return Start <= dateTime && dateTime <= End;
        }

        public bool IntersectWith(IPeriod period)
        {
            return period.Start <= End && Start <= period.End;
        }

        public override string ToString()
        {
            return $"[{Start};{End}]";
        }
    }
}