using BookingService.Application.Dtos;
using BookingService.Application.Services;
using BookingService.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookingRequest request)
        {
            var result = await _bookingService.CreateAsync(request);

            return result.Status switch
            {
                CreateBookingStatus.Created => CreatedAtAction(nameof(GetById), new { id = result.Booking!.Id }, ToResponse(result.Booking!)),
                CreateBookingStatus.InvalidDates => BadRequest("Дата выезда должна быть позже даты заезда"),
                CreateBookingStatus.RoomNotFound => NotFound(),
                CreateBookingStatus.DatesUnavailable => Conflict("Комната занята на эти даты"),
                _ => StatusCode(StatusCodes.Status500InternalServerError)
            };
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var booking = await _bookingService.GetByIdAsync(id);

            if (booking is null)
            {
                return NotFound();
            }

            return Ok(ToResponse(booking));
        }
        private static BookingResponse ToResponse(Booking booking)
        {
            return new BookingResponse
            {
                Id = booking.Id,
                RoomId = booking.RoomId,
                UserId = booking.UserId,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate
            };
        }
    }
}