using System;
using MeetingRoomBookingKata.Domain.Temporality;

namespace MeetingRoomBookingKata.Domain
{
    public class Reservation
    {
        public Guid Id { get; }
        public User User { get; }
        public Room Room { get; }
        public TimeSlot Slot { get; }

        public Reservation(Guid id, User user, Room room, TimeSlot slot)
        {
            Id = id;
            User = user;
            Room = room;
            Slot = slot;
        }
    }
}
