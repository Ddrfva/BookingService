using BookingService.Application.Dtos;
using BookingService.Data;
using BookingService.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Application.Services;

public class RoomService : IRoomService
{
    private readonly AppDbContext _db;

    public RoomService(AppDbContext db) => _db = db;

    public async Task<Room> CreateAsync(CreateRoomRequest request)
    {
        var room = new Room
        {
            Class = request.Class,
            Price = request.Price,
            Description = request.Description
        };

        _db.Rooms.Add(room);
        await _db.SaveChangesAsync();

        return room;
    }

    public async Task<Room?> GetByIdAsync(int id)
    {
        return await _db.Rooms
            .FirstOrDefaultAsync(room => room.Id == id);
    }

    public async Task<List<Room>> GetAllAsync()
    {
        return await _db.Rooms.ToListAsync();
    }

    public async Task<DeleteRoomResult> DeleteAsync(int id)
    {
        var room = await _db.Rooms
            .FirstOrDefaultAsync(room => room.Id == id);

        if (room is null)
        {
            return DeleteRoomResult.NotFound;
        }

        var hasBookings = await _db.Bookings
            .AnyAsync(b => b.RoomId == id);

        if (hasBookings)
        {
            return DeleteRoomResult.HasBookings;
        }

        _db.Rooms.Remove(room);
        await _db.SaveChangesAsync();

        return DeleteRoomResult.Deleted;
    }
}