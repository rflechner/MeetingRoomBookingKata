namespace MeetingRoomBookingKata.Domain.Services
{
    public interface IUserProvider
    {
        User GetFromUserName(UserName userName);
    }
}