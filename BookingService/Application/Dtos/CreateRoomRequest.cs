using System.ComponentModel.DataAnnotations;
using BookingService.Domain;

namespace BookingService.Application.Dtos;

public class CreateRoomRequest
{
    [EnumDataType(typeof(RoomClass), ErrorMessage = "Недопустимый класс комнаты")]
    public RoomClass Class { get; set; }

    [Range(0.01, 1000000, ErrorMessage = "Цена должна быть больше 0")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Описание обязательно")]
    [MinLength(1, ErrorMessage = "Описание не должно быть пустым")]
    public string Description { get; set; } = string.Empty;
}