using BookingService.Application.Dtos;
using BookingService.Application.Services;
using Microsoft.AspNetCore.Mvc;
using BookingService.Domain;

namespace BookingService.Controllers;

[ApiController]
[Route("api/rooms")]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var room = await _roomService.GetByIdAsync(id);

        if (room is null)
        {
            return NotFound();
        }

        return Ok(ToResponse(room));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var rooms = await _roomService.GetAllAsync();

        return Ok(rooms.Select(ToResponse));
    }

    private static RoomResponse ToResponse(Room room)
    {
        return new RoomResponse
        {
            Id = room.Id,
            Class = room.Class,
            Price = room.Price,
            Description = room.Description
        };
    }
}
