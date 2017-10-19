using System;
using System.Collections.Generic;
using System.Linq;
using MeetingRoomBookingKata.Domain.Persistence;
using MeetingRoomBookingKata.Domain.Temporality;

namespace MeetingRoomBookingKata.Domain.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }

        public BookingResult BookRoom(Room room, User user, TimeSlot slot)
        {
            var day = Day.From(slot.Start.Date);

            var dailyReservations = reservationRepository.GetReservations(day, room.Number);
            var existingReservations = dailyReservations.Where(r => r.Slot.IntersectWith(slot)).ToList();
            var canBook = !existingReservations.Any();

            if (canBook)
            {
                var reservation = new Reservation(Guid.NewGuid(), user, room, slot);
                reservationRepository.Save(reservation);

                return new BookingResult(BookingStatus.Accepted, reservation.Id);
            }

            return BookingResult.ConflictFailure;
        }

        public IEnumerable<TimeSlot> GetBookableTimeSlots(Room room, TimeSlot slot)
        {
            var day = Day.From(slot.Start.Date);
            var bookedSlots = reservationRepository.GetReservations(day, room.Number).Select(r => r.Slot).ToList();
            var slots = day.ComputeSlots(Constraints.SlotDuration);

            foreach (var timeSlot in slots)
            {
                if (!bookedSlots.Any(s => s.IntersectWith(timeSlot)))
                    yield return timeSlot;
            }
        }

        public bool CancelReservation(Guid id)
        {
            var reservation = reservationRepository.Get(id);
            if (reservation == null)
                return false;
            reservationRepository.Delete(reservation);

            return true;
        }
    }
}
