using BookingService.Application.Dtos;
using BookingService.Domain;

namespace BookingService.Application.Services;

public interface IBookingService
{
    Task<CreateBookingResult> CreateAsync(CreateBookingRequest request);
    Task<Booking?> GetByIdAsync(int id);
}