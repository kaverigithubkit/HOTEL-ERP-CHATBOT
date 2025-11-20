using HotelERP.Chatbot.Core.Entities;
using HotelERP.Chatbot.Core.Interfaces;
using HotelERP.Chatbot.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelERP.Chatbot.Infrastructure.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelContext _context;

        public RoomRepository(HotelContext context)
        {
            _context = context;
        }

        public async Task<int> GetAvailableRoomsCountAsync()
        {
            return await _context.Rooms.CountAsync(r => r.IsAvailable);
        }

        public async Task<List<Room>> GetAllRoomsAsync()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<List<Room>> GetBookedRoomsAsync()
        {
            return await _context.Rooms.Where(r => !r.IsAvailable).ToListAsync();
        }

        public async Task<Room?> GetRoomByNumberAsync(string number)
        {
            return await _context.Rooms.FirstOrDefaultAsync(r => r.Number == number);
        }

        public async Task<bool> BookRoomAsync(string number, string guest)
        {
            var room = await GetRoomByNumberAsync(number);
            if (room == null || !room.IsAvailable) return false;

            room.IsAvailable = false;
            room.GuestName = guest;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CancelBookingAsync(string number)
        {
            var room = await GetRoomByNumberAsync(number);
            if (room == null || room.IsAvailable) return false;

            room.IsAvailable = true;
            room.GuestName = null;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
