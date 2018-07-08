using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Autofac;
using MeetingRoomBookingKata.Domain;
using MeetingRoomBookingKata.Domain.Persistence;
using MeetingRoomBookingKata.Domain.Temporality;
using MeetingRoomBookingKata.Tests.Integration.Helpers;
using MeetingRoomBookingKata.WebApi;
using MeetingRoomBookingKata.WebApi.Models.Requests;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using UserName = MeetingRoomBookingKata.WebApi.Models.UserName;

namespace MeetingRoomBookingKata.Tests.Integration
{
    [TestFixture]
    public class ReservationIntegrationTests
    {
        private TestServer server;

        [OneTimeSetUp]
        public void TestsInit()
        {
            server = TestServer.Create<Startup>();
        }

        [OneTimeTearDown]
        public void TestsDispose()
        {
            server.Dispose();
        }

        [SetUp]
        public void FixtureInit()
        {
            CurrentReservationRepository.Clear();
        }

        private IReservationRepository CurrentReservationRepository => AutofacConfig.Container.Resolve<IReservationRepository>();

        [Test]
        public void MakingReservationOnEmptyDay_ShouldReturnReservationId()
        {
            var response = MakeReservation(new DateTime(2018, 01, 02, 12, 00, 00), 1);

            var result = response.Content.ReadJsonAsAsync<Guid>().Result;

            Assert.AreEqual(202, (int) response.StatusCode);
            Assert.AreNotEqual(Guid.Empty, result);
        }

        [Test]
        public void MakingReservationOnExistingSlot_ShouldReturnConflict()
        {
            FakeReservation(1, new DateTime(2018, 01, 02, 10, 00, 00));
            FakeReservation(1, new DateTime(2018, 01, 02, 12, 00, 00));
            FakeReservation(1, new DateTime(2018, 01, 02, 14, 00, 00));
            FakeReservation(1, new DateTime(2018, 01, 02, 15, 00, 00));
            FakeReservation(1, new DateTime(2018, 01, 02, 18, 00, 00));

            var response = MakeReservation(new DateTime(2018, 01, 02, 12, 00, 00), 1);

            var slotsStarts = response.Content.ReadJsonAsAsync<List<DateTime>>().Result;

            Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);
            Assert.AreEqual(19, slotsStarts.Count);
        }

        [Test]
        public void DeletingExistingReservation_ShouldReturnOk()
        {
            var id = Guid.NewGuid();
            FakeReservation(1, new DateTime(2018, 01, 02, 12, 00, 00), id);

            var response = server.HttpClient.DeleteAsync($"/reservations/{id}").Result;
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void DeletingNotExistingReservation_ShouldReturnNotFound()
        {
            var response = server.HttpClient.DeleteAsync($"/reservations/{Guid.NewGuid()}").Result;
            
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        private void FakeReservation(int number, DateTime start, Guid? id = null)
        {
            CurrentReservationRepository.Save(
                new Reservation(id ?? Guid.NewGuid(), 
                new User(Guid.NewGuid(), "John", "Doe"),
                new Room(number),
                new TimeSlot(start, start.AddHours(1).AddMilliseconds(-1))));
        }

        private HttpResponseMessage MakeReservation(DateTime hour, int roomNumber)
        {
            var message = new CreateReservation
            {
                UserName = new UserName
                {
                    LastName = "John",
                    FirstName = "Doe"
                },
                Hour = hour,
                RoomNumber = roomNumber
            };

            return server.HttpClient.PostAsync("/reservations", message.CreateHttpJsonMessage()).Result;
        }
    }
}