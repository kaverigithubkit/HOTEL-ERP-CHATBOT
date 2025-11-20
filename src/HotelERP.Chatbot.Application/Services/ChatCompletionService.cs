//using OpenAI;
//using OpenAI.Completions;
//using HotelERP.Chatbot.Core.DTOs;
//using System.Text;

//namespace HotelERP.Chatbot.Application.Services
//{
//    public class ChatCompletionService
//    {
//        private readonly OpenAIClient _client;

//        public ChatCompletionService(string apiKey)
//        {
//            if (string.IsNullOrWhiteSpace(apiKey))
//                throw new ArgumentException("API key cannot be empty", nameof(apiKey));

//            _client = new OpenAIClient(apiKey);
//        }

//        /// <summary>
//        /// Sends system + user messages as a single prompt to OpenAI and returns the assistant's reply.
//        /// </summary>
//        public async Task<string> CreateChatCompletionAsync(List<ChatMessage> messages, string model = "gpt-4o-mini", int maxTokens = 200)
//        {
//            if (messages == null || messages.Count == 0)
//                throw new ArgumentException("Messages cannot be null or empty.", nameof(messages));

//            // Convert messages into a single prompt string
//            var promptBuilder = new StringBuilder();
//            foreach (var msg in messages)
//            {
//                promptBuilder.AppendLine($"{msg.Role}: {msg.Content}");
//            }

//            string prompt = promptBuilder.ToString();

//            // Call the Completions API (8.8.2)
//            var completion = await _client.Completions.CreateCompletionAsync(
//                model: model,
//                prompt: prompt,
//                max_tokens: maxTokens
//            );

//            return completion.Choices[0].Text ?? "Sorry, I couldn't generate a response.";
//        }
//    }

//    /// <summary>
//    /// Simple ChatMessage DTO for system/user messages.
//    /// </summary>
//    public class ChatMessage
//    {
//        public string Role { get; set; } = "User"; // or "System"
//        public string Content { get; set; } = "";

//        public ChatMessage() { }

//        public ChatMessage(string role, string content)
//        {
//            Role = role;
//            Content = content;
//        }
//    }
//}
