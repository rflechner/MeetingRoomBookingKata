using System.Collections.Generic;
using System.Linq;
using MeetingRoomBookingKata.Domain;
using MeetingRoomBookingKata.Tests.Integration.Helpers;
using MeetingRoomBookingKata.WebApi;
using Microsoft.Owin.Testing;
using NUnit.Framework;

namespace MeetingRoomBookingKata.Tests.Integration
{
    [TestFixture]
    public class RoomsIntegrationTests
    {
        private TestServer server;

        [OneTimeSetUp]
        public void FixtureInit()
        {
            server = TestServer.Create<Startup>();
        }

        [OneTimeTearDown]
        public void FixtureDispose()
        {
            server.Dispose();
        }

        [Test]
        public void GettingAllRooms_ShouldReturn10Rooms()
        {
            var response = server.HttpClient.GetAsync("/rooms").Result;
            var result = response.Content.ReadJsonAsAsync<List<Room>>().Result;

            Assert.AreEqual(10, result.Count);
            Assert.AreEqual("room0", result.First().Name);
            Assert.AreEqual("room9", result.Last().Name);
            Assert.AreEqual(200, (int)response.StatusCode);
        }
        
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        public void GettingSpecifiedRoom_ShouldReturnRoomWithStatusCodeOk(int number)
        {
            var response = server.HttpClient.GetAsync($"/rooms/{number}").Result;
            var room = response.Content.ReadJsonAsAsync<Room>().Result;

            Assert.AreEqual($"room{number}", room.Name);
            Assert.AreEqual(200, (int)response.StatusCode);
        }
        
        [Test]
        public void GettingNotExistingRoom_ShouldReturn404()
        {
            var response = server.HttpClient.GetAsync("/rooms/1000").Result;
            
            Assert.AreEqual(404, (int)response.StatusCode);
        }
        
        
    }
}