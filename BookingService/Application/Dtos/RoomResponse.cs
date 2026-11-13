using BookingService.Domain;

namespace BookingService.Application.Dtos
{
    public class RoomResponse
    {
        public int Id { get; set; }
        public RoomClass Class { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}

  