using System;

namespace MeetingRoomBookingKata.Domain.Services
{
    public class UserProvider : IUserProvider
    {
        public User GetFromUserName(UserName userName)
        {
            return new User(Guid.NewGuid(), userName.LastName, userName.FirstName);
        }
    }
}
