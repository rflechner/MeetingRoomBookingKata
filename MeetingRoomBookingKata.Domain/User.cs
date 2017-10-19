using System;

namespace MeetingRoomBookingKata.Domain
{
    public class User
    {
        public User(Guid id, string lastName, string firstName)
        {
            Id = id;
            LastName = lastName;
            FirstName = firstName;
        }

        public Guid Id { get; }
        public string LastName { get; }
        public string FirstName { get; }
    }
}
