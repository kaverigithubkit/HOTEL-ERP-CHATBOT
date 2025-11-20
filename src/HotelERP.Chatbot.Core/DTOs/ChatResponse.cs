using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAI.Chat;
using HotelERP.Chatbot.Core.DTOs;





namespace HotelERP.Chatbot.Core.DTOs
{
    using OpenAIChatResponse = OpenAI.Chat.ChatResponse;


    public class ChatResponse
    {
        public string Answer { get; set; } = "";
    }



   



}



