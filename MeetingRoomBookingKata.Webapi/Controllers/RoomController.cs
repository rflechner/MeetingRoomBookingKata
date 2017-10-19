using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using MeetingRoomBookingKata.Domain;
using MeetingRoomBookingKata.Domain.Services;

namespace MeetingRoomBookingKata.WebApi.Controllers
{
    /// <summary>
    /// Manipulate rooms
    /// </summary>
    [RoutePrefix("rooms")]
    public class RoomController : ApiController
    {
        private readonly IRoomProvider roomProvider;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="roomProvider">Service providing all available rooms</param>
        public RoomController(IRoomProvider roomProvider)
        {
            this.roomProvider = roomProvider;
        }

        /// <summary>
        /// Get all rooms
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("")]
        public List<Room> GetRooms()
        {
            return roomProvider.GetAllRooms().ToList();
        }

        /// <summary>
        /// Get specified room number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [HttpGet, Route("{number}")]
        [ResponseType(typeof(Room))]
        public IHttpActionResult GetRoom(int number)
        {
            var room = roomProvider.GetRoom(number);
            if (room != null)
                return Ok(room);
            return NotFound();
        }
    }
    
}