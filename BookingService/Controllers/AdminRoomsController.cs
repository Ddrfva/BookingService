using BookingService.Application.Dtos;
using BookingService.Application.Services;
using BookingService.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Controllers;

[ApiController]
[Route("api/admin/rooms")]
public class AdminRoomsController : ControllerBase
{
    private readonly IRoomService _roomService;

    public AdminRoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRoomRequest request)
    {
        var room = await _roomService.CreateAsync(request);

        return CreatedAtAction(
            actionName: nameof(RoomsController.GetById),
            controllerName: "Rooms",
            routeValues: new { id = room.Id },
            value: ToResponse(room));
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