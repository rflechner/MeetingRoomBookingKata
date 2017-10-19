using System.Collections.Generic;

namespace MeetingRoomBookingKata.Domain.Services
{
    public interface IRoomProvider
    {
        IEnumerable<Room> GetAllRooms();
        Room GetRoom(int number);
    }
}
