using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MeetingRoomBookingKata.Domain;
using MeetingRoomBookingKata.Domain.Persistence;
using MeetingRoomBookingKata.Domain.Services;
using MeetingRoomBookingKata.Domain.Temporality;
using NSubstitute;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace MeetingRoomBookingKata.Tests.Specs
{
    [Binding]
    public class RoomReservationSteps
    {
        private List<Room> rooms;
        private TimeSpan slotDuration;
        private List<Reservation> reservations;
        private ReservationService reservationService;
        
        [Given(@"(.*) rooms")]
        public void GivenRooms(int roomCount)
        {
            rooms = Enumerable.Range(0, roomCount).Select(number => new Room(number)).ToList();

            reservations = new List<Reservation>();

            var reservationRepository = Substitute.For<IReservationRepository>();
            reservationRepository.GetReservations(Arg.Any<Day>(), Arg.Any<int>())
                .Returns(x =>
                {
                    var day = (Day)x[0];
                    var roomNumber = (int)x[1];
                    return reservations.Where(r => r.Room.Number == roomNumber && day.Contains(r.Slot)).ToList();
                });

            reservationRepository
                .When(r => r.Save(Arg.Any<Reservation>()))
                .Do(x =>
                {
                    var reservation = (Reservation)x[0];
                    reservations.Add(reservation);
                });

            reservationService = new ReservationService(reservationRepository);
        }

        [Given(@"time slots have (.*) hour of duration")]
        public void GivenTimeSlotsHaveHourOfDuration(int hour)
        {
            slotDuration = TimeSpan.FromHours(hour);
        }

        [When(@"no slots are reserved")]
        public void WhenNoSlotsAreReserved()
        {
            reservations.Clear();
        }

        [When(@"following slots are reserved")]
        public void WhenFollowingSlotsAreReserved(Table table)
        {
            foreach (var row in table.Rows)
            {
                var roomNumber = int.Parse(row["RoomNumber"]);
                var day = ParseDay(row["Day"]);
                var slotNumber = int.Parse(row["SlotNumber"]);

                var room = rooms[roomNumber];
                var user = new User(Guid.NewGuid(), "John", "Doe");
                var slots = day.ComputeSlots(slotDuration).ToArray();
                reservations.Add(new Reservation(Guid.NewGuid(), user, room, slots[slotNumber]));
            }
        }

        [Then(@"user can book slot number (.*) of day '(.*)' for room (.*)")]
        public void ThenUserCanBookSlotNumberOfDayForRoom(int slotNumber, string date, int roomNumber)
        {
            var reservationsCount = reservations.Count;
            var booked = TestBookRoom(slotNumber, date, roomNumber);

            Assert.IsTrue(booked);

            Assert.AreNotEqual(reservationsCount, reservations.Count);
        }

        [Then(@"user can not book slot number (.*) of day '(.*)' for room (.*)")]
        public void ThenUserCanNotBookSlotNumberOfDayForRoom(int slotNumber, string date, int roomNumber)
        {
            var reservationsCount = reservations.Count;
            var booked = TestBookRoom(slotNumber, date, roomNumber);

            Assert.IsFalse(booked);

            Assert.AreEqual(reservationsCount, reservations.Count);
        }

        private bool TestBookRoom(int slotNumber, string date, int roomNumber)
        {
            var day = ParseDay(date);
            var user = new User(Guid.NewGuid(), "John", "Doe");
            var slots = day.ComputeSlots(slotDuration).ToArray();
            var room = rooms[roomNumber];
            ;
            return reservationService.BookRoom(room, user, slots[slotNumber]).Status == BookingStatus.Accepted;
        }

        private static Day ParseDay(string text)
            => Day.From(DateTime.ParseExact(text, "yyyy-MM-dd", CultureInfo.InvariantCulture));

    }
}
