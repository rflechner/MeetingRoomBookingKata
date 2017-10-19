using System;
using System.Collections.Generic;
using System.Linq;
using MeetingRoomBookingKata.Domain.Temporality;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace MeetingRoomBookingKata.Tests.Specs
{
    [Binding]
    public class TimeSlotComputationSteps
    {
        private Day currentDay;
        private TimeSpan duration;
        private List<TimeSlot> slots;

        [Given(@"A period of one day")]
        public void GivenAPeriodOfOneDay()
        {
            currentDay = Day.From(new DateTime(2017, 12, 03));
        }
        
        [Given(@"Slots has (.*) hour of duration")]
        public void GivenSlotsHasHourOfDuration(int hoursCount)
        {
            duration = TimeSpan.FromHours(hoursCount);
        }
        
        [When(@"I compute all available slots")]
        public void WhenIComputeAllAvailableSlots()
        {
            slots = currentDay.ComputeSlots(duration).ToList();
        }
        
        [Then(@"the result should be (.*) slots")]
        public void ThenTheResultShouldBeSlots(int count)
        {
            Assert.AreEqual(count, slots.Count);
        }
    }
}
