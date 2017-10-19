using System.Collections.Generic;
using System.Linq;

namespace MeetingRoomBookingKata.Domain.Services
{
    public class RoomProvider : IRoomProvider
    {
        public IEnumerable<Room> GetAllRooms() 
            => Enumerable.Range(0, 10).Select(number => new Room(number));

        public Room GetRoom(int number) 
            => GetAllRooms().SingleOrDefault(r => r.Number == number);
    }
}
