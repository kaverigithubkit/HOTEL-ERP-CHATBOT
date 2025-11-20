using HotelERP.Chatbot.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelERP.Chatbot.Infrastructure.Data
{
    public class HotelContext : DbContext
    {
        public HotelContext(DbContextOptions<HotelContext> options)
            : base(options)
        {
        }

        public DbSet<Room> Rooms => Set<Room>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed sample data
            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Number = "101", IsAvailable = true },
                new Room { Id = 2, Number = "102", IsAvailable = false },
                new Room { Id = 3, Number = "103", IsAvailable = true }
            );
        }
    }
}
