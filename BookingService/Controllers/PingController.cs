using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookingService.Services;

namespace BookingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        private readonly ITimeService _timeService;

        public PingController(ITimeService timeService)
        {
            _timeService = timeService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_timeService.GetCreatedAt());
        }

    }
}

