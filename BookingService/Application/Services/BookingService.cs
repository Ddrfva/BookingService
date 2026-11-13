using BookingService.Application.Dtos;
using BookingService.Domain;
using BookingService.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Application.Services;

public class BookingService : IBookingService
{
    private readonly AppDbContext _db;

    public BookingService(AppDbContext db) => _db = db;

    public async Task<CreateBookingResult> CreateAsync(CreateBookingRequest request)
    {
        if (request.EndDate <= request.StartDate)
        {
            return new CreateBookingResult { Status = CreateBookingStatus.InvalidDates };
        }
        var room = await _db.Rooms.FirstOrDefaultAsync(r => r.Id  == request.RoomId);
       
        if(room is null)
        {
            return new CreateBookingResult { Status = CreateBookingStatus.RoomNotFound };
        }

        var hasOverlap = await _db.Bookings.AnyAsync(b =>
        b.RoomId == request.RoomId &&
        request.StartDate < b.EndDate &&
        request.EndDate > b.StartDate);

        if (hasOverlap)
        {
            return new CreateBookingResult { Status = CreateBookingStatus.DatesUnavailable };
        }

        var booking = new Booking{
            RoomId = request.RoomId,
            UserId = request.UserId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
        };

        _db.Bookings.Add(booking);
        await _db.SaveChangesAsync();

        return new CreateBookingResult { Status = CreateBookingStatus.Created, Booking = booking};
    }

    public async Task <Booking?> GetByIdAsync(int id)
    {
        return await _db.Bookings
        .FirstOrDefaultAsync(booking => booking.Id == id);
    }
}
