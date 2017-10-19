using System;

namespace MeetingRoomBookingKata.Domain.Services
{
    public class BookingResult
    {
        public BookingStatus Status { get; }
        public Guid ReservationId { get; }

        public BookingResult(BookingStatus status, Guid reservationId)
        {
            Status = status;
            ReservationId = reservationId;
        }
        
        public static BookingResult ConflictFailure => new BookingResult(BookingStatus.Conflict, Guid.Empty);
    }
}