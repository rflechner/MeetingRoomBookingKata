using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using MeetingRoomBookingKata.Domain;
using MeetingRoomBookingKata.Domain.Persistence;
using MeetingRoomBookingKata.Domain.Temporality;

namespace MeetingRoomBookingKata.Persistence.InMemory
{
    public class ReservationRepository : IReservationRepository
    {
        private ConcurrentDictionary<Guid, Reservation> reservations;

        public ReservationRepository()
        {
            reservations = new ConcurrentDictionary<Guid, Reservation>();
        }

        public IEnumerable<Reservation> GetReservations(Day day, int roomNumber)
        {
            return reservations.Values.Where(r => r.Room.Number == roomNumber && day.Contains(r.Slot)).ToList();
        }

        public void Save(Reservation reservation)
        {
            reservations.AddOrUpdate(reservation.Id, reservation, (id, r) => reservation);
        }

        public void Clear()
        {
            reservations.Clear();
        }

        public Reservation Get(Guid id)
        {
            if (reservations.TryGetValue(id, out Reservation reservation))
                return reservation;

            return null;
        }

        public void Delete(Reservation reservation)
        {
            Reservation ignored;
            reservations.TryRemove(reservation.Id, out ignored);
        }
    }
}