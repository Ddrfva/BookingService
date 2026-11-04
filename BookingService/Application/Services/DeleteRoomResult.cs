namespace BookingService.Application.Services;

public enum DeleteRoomResult
{
    Deleted,
    NotFound,
    HasBookings
}