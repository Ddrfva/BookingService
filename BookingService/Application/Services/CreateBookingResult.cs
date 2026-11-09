using BookingService.Domain;

namespace BookingService.Application.Services;

public enum CreateBookingStatus
{
    Created,
    RoomNotFound,
    InvalidDates,
    DatesUnavailable
}

public class CreateBookingResult
{
    public CreateBookingStatus Status { get; set; }
    public Booking? Booking { get; set; }
}