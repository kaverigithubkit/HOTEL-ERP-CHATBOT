using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using HotelERP.Chatbot.Core.DTOs;

namespace HotelERP.Chatbot.Application.Services
{
    public class ChatbotService
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, List<string>> _dbSchema = new();

        public ChatbotService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("HotelERPChatbotDb")
                                ?? throw new InvalidOperationException("Missing DB connection string");
            LoadDatabaseSchema();
        }

        // 🚀 Step 1: Load table/column names automatically
        private void LoadDatabaseSchema()
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            DataTable schema = conn.GetSchema("Columns");

            foreach (DataRow row in schema.Rows)
            {
                string table = row["TABLE_NAME"].ToString()!;
                string column = row["COLUMN_NAME"].ToString()!;

                if (!_dbSchema.ContainsKey(table))
                    _dbSchema[table] = new List<string>();

                _dbSchema[table].Add(column);
            }

            Console.WriteLine($"[Chatbot] Schema loaded with {_dbSchema.Count} tables.");
        }

        // 🌐 Step 2: Main chatbot entry point
        public async Task<ChatResponse> GetResponseAsync(ChatRequest request)
        {
            string question = request.Question.ToLower().Trim();

            // Generate an SQL query automatically
            string? sql = GenerateSqlFromQuestion(question);

            if (string.IsNullOrEmpty(sql))
                return new ChatResponse
                {
                    Answer = "Sorry, I couldn’t understand your question or generate a valid query."
                };

            try
            {
                string answer = await ExecuteSqlAndFormatAsync(sql);
                return new ChatResponse { Answer = answer };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DB ERROR] {ex.Message}");
                return new ChatResponse { Answer = "There was an error while executing your request." };
            }
        }

        // 🧠 Step 3: Generate smart SQL query based on schema & question
        private string? GenerateSqlFromQuestion(string question)
        {
            // Find relevant table by keyword
            var table = _dbSchema.Keys.FirstOrDefault(t => question.Contains(t.ToLower()));

            if (table == null)
            {
                // Guess based on common hotel entities
                if (question.Contains("room")) table = "Rooms";
                else if (question.Contains("guest")) table = "Guests";
                else if (question.Contains("bill") || question.Contains("invoice")) table = "Bills";
                else return null;
            }

            var cols = _dbSchema[table];

            // COUNT queries
            if (Regex.IsMatch(question, @"(how many|count|number of)"))
            {
                string filter = "";

                if (question.Contains("free") || question.Contains("available"))
                    filter = "WHERE Status = 'Free'";
                else if (question.Contains("occupied") || question.Contains("booked"))
                    filter = "WHERE Status = 'Occupied'";
                else if (question.Contains("not checked out") || question.Contains("stay"))
                    filter = "WHERE CheckOutDate IS NULL";

                return $"SELECT COUNT(*) AS Result FROM {table} {filter}";
            }

            // SUM queries (like revenue)
            if (Regex.IsMatch(question, @"(total|sum|revenue|income|sales)"))
            {
                string amountCol = cols.FirstOrDefault(c => c.ToLower().Contains("amount") || c.ToLower().Contains("total")) ?? "*";
                string dateCol = cols.FirstOrDefault(c => c.ToLower().Contains("date"));
                string filter = "";

                if (question.Contains("today") && dateCol != null)
                    filter = $"WHERE CAST({dateCol} AS DATE) = CAST(GETDATE() AS DATE)";
                else if (question.Contains("month") && dateCol != null)
                    filter = $"WHERE MONTH({dateCol}) = MONTH(GETDATE()) AND YEAR({dateCol}) = YEAR(GETDATE())";

                return $"SELECT ISNULL(SUM({amountCol}),0) AS Result FROM {table} {filter}";
            }

            // LIST queries
            if (question.Contains("show") || question.Contains("list"))
            {
                string topCols = string.Join(", ", cols.Take(3));
                return $"SELECT TOP 10 {topCols} FROM {table}";
            }

            // A fallback simple select
            return $"SELECT TOP 5 * FROM {table}";
        }

        // ⚙️ Step 4: Execute SQL & return readable text
        private async Task<string> ExecuteSqlAndFormatAsync(string sql)
        {
            if (!sql.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
                return "Only SELECT queries are allowed for safety.";

            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new SqlCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            if (!reader.HasRows)
                return "No results found.";

            var sb = new StringBuilder();

            int rowCount = 0;
            while (await reader.ReadAsync() && rowCount < 10)
            {
                var values = new List<string>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add($"{reader.GetName(i)}: {reader.GetValue(i)}");
                }
                sb.AppendLine(string.Join(", ", values));
                rowCount++;
            }

            return sb.ToString();
        }
    }
}
