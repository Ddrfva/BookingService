using Microsoft.EntityFrameworkCore;
using BookingService.Domain;

namespace BookingService.Domain;

public class Booking
{
    public int Id { get; set; }

    public int RoomId { get; set; }
    public Room Room { get; set; } = null!;

    public int UserId { get; set; }
    public DateOnly StartDate { get; set; } 
    public DateOnly EndDate { get; set; }
}