using System;
using System.Collections.Generic;
using MeetingRoomBookingKata.Domain.Temporality;

namespace MeetingRoomBookingKata.Domain.Services
{
    public interface IReservationService
    {
        BookingResult BookRoom(Room room, User user, TimeSlot slot);
        bool CancelReservation(Guid id);
        IEnumerable<TimeSlot> GetBookableTimeSlots(Room room, TimeSlot slot);
    }
}
