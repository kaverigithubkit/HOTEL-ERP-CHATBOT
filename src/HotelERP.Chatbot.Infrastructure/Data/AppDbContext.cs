using HotelERP.Chatbot.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelERP.Chatbot.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<ChatQA> ChatQAs { get; set; }
    }
}
