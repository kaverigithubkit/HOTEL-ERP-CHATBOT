using HotelERP.Chatbot.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelERP.Chatbot.Core.Interfaces
{
    public interface IRoomRepository
    {
        Task<int> GetAvailableRoomsCountAsync();
        Task<List<Room>> GetAllRoomsAsync();
        Task<List<Room>> GetBookedRoomsAsync();
        Task<Room?> GetRoomByNumberAsync(string number);
        Task<bool> BookRoomAsync(string number, string guest);
        Task<bool> CancelBookingAsync(string number);
    }
}
