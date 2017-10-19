using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingRoomBookingKata.Domain.Temporality;
using NUnit.Framework;

namespace MeetingRoomBookingKata.Tests
{
    [TestFixture]
    public class TemporalityTests
    {

        [Test]
        public void WhenBuildingDayFromDateTime_StartAndEndShouldBeFirstAndLastSecondOfThis()
        {
            var day = Day.From(new DateTime(2017, 10, 20, 12, 05, 23));

            Assert.AreEqual(new DateTime(2017, 10, 20), day.Start);
            Assert.AreEqual(new DateTime(2017, 10, 20, 23, 59, 59, 999), day.End);
        }

        [Test]
        public void WhenComputingSlotsOf30MinutesInDat_ShouldHaveCountOf48()
        {
            var day = Day.From(new DateTime(2017, 10, 20, 12, 05, 23));

            var slots = day.ComputeSlots(TimeSpan.FromMinutes(30));

            Assert.AreEqual(48, slots.Count());
        }

        [Test]
        public void WhenComputingSlotsOf1HourInDat_ShouldHaveCountOf24()
        {
            var day = Day.From(new DateTime(2017, 10, 20, 12, 05, 23));

            var slots = day.ComputeSlots(TimeSpan.FromHours(1)).ToList();

            Assert.AreEqual(24, slots.Count);

            var slot0 = slots[0];
            var slot1 = slots[1];

            Assert.AreEqual(new DateTime(2017, 10, 20, 00, 00, 00), slot0.Start);
            Assert.AreEqual(new DateTime(2017, 10, 20, 00, 59, 59, 999), slot0.End);

            Assert.AreEqual(new DateTime(2017, 10, 20, 01, 00, 00), slot1.Start);
            Assert.AreEqual(new DateTime(2017, 10, 20, 01, 59, 59, 999), slot1.End);
            

        }

        [Test]
        public void DifferentsTimeSlots_ShouldIntersect()
        {
            var period1 = new TimeSlot(new DateTime(2017, 02, 05, 15, 30, 10), new DateTime(2017, 03, 04, 14, 15, 12));
            var period2 = new TimeSlot(new DateTime(2017, 02, 20, 14, 32, 24), new DateTime(2017, 04, 04, 14, 15, 12));

            Assert.IsTrue(period1.IntersectWith(period2));
            Assert.IsTrue(period2.IntersectWith(period1));
        }

        [Test]
        public void ContiguousTimeSlots_ShouldNotIntersect()
        {
            var period1 = new TimeSlot(new DateTime(2017, 10, 20), new DateTime(2017, 10, 20, 23, 59, 59, 999));
            var period2 = new TimeSlot(new DateTime(2017, 10, 21), new DateTime(2017, 10, 21, 23, 59, 59, 999));

            Assert.IsFalse(period1.IntersectWith(period2));
            Assert.IsFalse(period2.IntersectWith(period1));
        }

    }
}
