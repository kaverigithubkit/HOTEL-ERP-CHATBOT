using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelERP.Chatbot.Core.Entities
{
   
    
        public class Room
        {
            public int Id { get; set; }
            public string Number { get; set; } = string.Empty;
            public bool IsAvailable { get; set; }
            public string? GuestName { get; set; } // Optional for booked rooms
        }
    }


