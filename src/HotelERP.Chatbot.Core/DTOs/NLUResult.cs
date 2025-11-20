using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelERP.Chatbot.Core.DTOs
{
    public class NLUResult
    {
        public string Intent { get; set; }
        public string RoomType { get; set; }
        public string Dates { get; set; }
        public int Quantity { get; set; }
    }
}
