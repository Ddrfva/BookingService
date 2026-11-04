using BookingService.Application.Dtos;
using BookingService.Domain;

namespace BookingService.Application.Services;

public interface IRoomService
{
    Task<Room> CreateAsync(CreateRoomRequest request);
    Task<Room?> GetByIdAsync(int id);
    Task<List<Room>> GetAllAsync();
    Task<DeleteRoomResult> DeleteAsync(int id);
}