using BookingService.Application.Dtos;
using BookingService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRoomRequest request)
    {
        var room = await _roomService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = room.Id },
            room);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var room = await _roomService.GetByIdAsync(id);

        if (room is null)
        {
            return NotFound();
        }

        return Ok(room);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var rooms = await _roomService.GetAllAsync();

        return Ok(rooms);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _roomService.DeleteAsync(id);

        return result switch
        {
            DeleteRoomResult.Deleted => NoContent(),
            DeleteRoomResult.NotFound => NotFound(),
            DeleteRoomResult.HasBookings => Conflict("У комнаты есть бронирования"),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }
}
