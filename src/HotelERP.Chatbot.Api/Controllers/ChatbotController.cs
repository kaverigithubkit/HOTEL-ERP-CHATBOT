using HotelERP.Chatbot.Application.Services;
using HotelERP.Chatbot.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HotelERP.Chatbot.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatbotController : ControllerBase
    {
        private readonly ChatbotService _service;

        public ChatbotController(ChatbotService service)
        {
            _service = service;
        }

        [HttpPost("ask")]
        public async Task<IActionResult> Ask([FromBody] ChatRequest request)
        {
            var response = await _service.GetResponseAsync(request);
            return Ok(response);

        }
    }
}
