namespace MeetingRoomBookingKata.Domain
{
    public class Room
    {
        public int Number { get; }
        public string Name => $"room{Number}";

        public Room(int number)
        {
            Number = number;
        }
    }
}
