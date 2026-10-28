using BookingService.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Domain;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Room> Rooms { get; set; }
    public DbSet<Booking> Bookings { get; set; }
}