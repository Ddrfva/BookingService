namespace BookingService.Services;

public class TimeService : ITimeService
{
    private readonly DateTime _createdAt;

    public TimeService()
    {
        _createdAt = DateTime.Now;
    }

    public DateTime GetCreatedAt()
    {
        return _createdAt;
    }
}
