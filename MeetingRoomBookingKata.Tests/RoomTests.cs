using System.Linq;
using MeetingRoomBookingKata.Domain;
using MeetingRoomBookingKata.Domain.Services;
using NUnit.Framework;

namespace MeetingRoomBookingKata.Tests
{
    [TestFixture]
    public class RoomTests
    {
        [Test]
        public void WhenBuildingRoomFromNumber_ShouldHaveNameContainingIt()
        {
            var room = new Room(54);
            Assert.AreEqual("room54", room.Name);
        }

        [Test]
        public void WhenProvidingRoomList_ShouldHave10rooms()
        {
            var roomProvider = new RoomProvider();
            var rooms = roomProvider.GetAllRooms().ToList();

            Assert.AreEqual(10, rooms.Count);

            Assert.AreEqual("room0", rooms[0].Name);
            Assert.AreEqual("room1", rooms[1].Name);
            Assert.AreEqual("room9", rooms.Last().Name);
        }
    }
}