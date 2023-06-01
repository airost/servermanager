using HotelManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelManager.Data;

public class ApplicationDbContext : IdentityDbContext {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) {
    }

    public DbSet<HotelModel> HotelModel { get; set; } = default!;
    public DbSet<ReservationModel> ReservationModel { get; set; } = default!;
    public DbSet<RoomModel> RoomModel { get; set; } = default!;
}