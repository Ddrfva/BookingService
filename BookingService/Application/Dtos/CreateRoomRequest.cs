using BookingService.Domain;

namespace BookingService.Application.Dtos;

public class CreateRoomRequest
{
    public RoomClass Class { get; set; }

    public decimal Price { get; set; }

    public string Description { get; set; } = string.Empty;
}