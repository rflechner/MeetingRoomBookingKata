using System;

namespace MeetingRoomBookingKata.WebApi.Models.Requests
{
    public class CreateReservation
    {
        public int RoomNumber { get; set; }
        public DateTime Hour { get; set; }
        public UserName UserName{ get; set; }
    }
}
