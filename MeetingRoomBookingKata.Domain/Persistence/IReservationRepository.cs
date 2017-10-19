using System;
using System.Collections.Generic;
using MeetingRoomBookingKata.Domain.Temporality;

namespace MeetingRoomBookingKata.Domain.Persistence
{
    public interface IReservationRepository
    {
        IEnumerable<Reservation> GetReservations(Day day, int roomNumber);
        void Save(Reservation reservation);
        void Clear();
        Reservation Get(Guid id);
        void Delete(Reservation reservation);
    }
}
