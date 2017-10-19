namespace MeetingRoomBookingKata.Domain
{
    public class UserName
    {
        public UserName(string lastName, string firstName)
        {
            LastName = lastName;
            FirstName = firstName;
        }

        public string LastName { get; }
        public string FirstName { get; }
    }
}