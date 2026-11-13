using System.ComponentModel.DataAnnotations;

namespace BookingService.Application.Dtos;

public class CreateBookingRequest
{
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный идентификатор комнаты")]
    public int RoomId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Некорректный идентификатор пользователя")]
    public int UserId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}