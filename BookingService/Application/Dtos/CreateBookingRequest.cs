
namespace BookingService.Application.Dtos;

public class CreateBookingRequest
{

    public int RoomId { get; set; }
    public int UserId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}