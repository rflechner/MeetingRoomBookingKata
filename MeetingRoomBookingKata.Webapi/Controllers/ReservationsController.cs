using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using MeetingRoomBookingKata.Domain;
using MeetingRoomBookingKata.Domain.Services;
using MeetingRoomBookingKata.Domain.Temporality;
using MeetingRoomBookingKata.WebApi.Models.Requests;
using Swashbuckle.Swagger.Annotations;

namespace MeetingRoomBookingKata.WebApi.Controllers
{
    /// <summary>
    /// Manage reservations
    /// </summary>
    [RoutePrefix("reservations")]
    public class ReservationsController : ApiController
    {

        private readonly IReservationService reservationService;
        private readonly IRoomProvider roomProvider;
        private readonly IUserProvider userProvider;
        

        public ReservationsController(IReservationService reservationService, IRoomProvider roomProvider, IUserProvider userProvider)
        {
            this.reservationService = reservationService;
            this.roomProvider = roomProvider;
            this.userProvider = userProvider;
        }

        /// <summary>
        /// Create a reservation
        /// </summary>
        /// <param name="message"></param>
        /// <returns>
        /// Id of reservation in success or a conflict status code with available time slots
        /// </returns>
        [HttpPost, Route("")]
        //[ResponseType(typeof(Guid))]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(Guid))]
        [SwaggerResponse(HttpStatusCode.Conflict, Type = typeof(IEnumerable<DateTime>))]
        public IHttpActionResult CreateBooking([FromBody]CreateReservation message)
        {
            var room = roomProvider.GetRoom(message.RoomNumber);
            var user = userProvider.GetFromUserName(new UserName(message.UserName.LastName, message.UserName.FirstName));
            var day = Day.From(message.Hour.Date);
            var timeSlot = day.ComputeSlots(Constraints.SlotDuration).First(s => s.Contains(message.Hour));
            var bookingResult = reservationService.BookRoom(room, user, timeSlot);

            if (bookingResult.Status == BookingStatus.Accepted)
                return Content(HttpStatusCode.Accepted, bookingResult.ReservationId);

            var availableSlotStarts = reservationService.GetBookableTimeSlots(room, timeSlot).Select(s => s.Start).ToList();

            return Content(HttpStatusCode.Conflict, availableSlotStarts);
        }

        [HttpDelete, Route("{id}")]
        public IHttpActionResult CancelBooking(Guid id)
        {
            if (reservationService.CancelReservation(id))
                return Ok();
            
            return NotFound();
        }
    }
}