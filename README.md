Hotel ERP Chatbot (ASP.NET + SQL Server)

A chatbot system built for Hotel ERP operations using ASP.NET Web API, C#, and SQL Server.
It allows hotel staff to quickly get information such as bookings, invoices, KOT orders, menu items, and payments using simple chat-based queries.

Features:
- Chat-based hotel assistant
- Fetch bookings, invoices, KOT, payments, menu, tables
- ASP.NET Web API backend
- SQL Server database integration
- Lightweight HTML/JavaScript Chat UI
- Clean and modular code structure

Tech Stack:
Backend: ASP.NET Web API, C#, SQL Server
Frontend: HTML, CSS, JavaScript
Database: SQL Server (ERP Schema)

How to Run:
1. Clone repository
2. Open solution in Visual Studio
3. Update SQL Server connection string in appsettings.json or Web.config
4. Restore NuGet packages
5. Run the API
6. Open ChatbotUI/index.html in browser
7. Start asking questions

API Endpoints:
Ask chatbot (POST /api/chat/ask)
Body: { "question": "Show today's KOT" }

Get booking (GET /api/bookings?id=101)

Get menu (GET /api/menu)

Project Structure:
Controllers
Models
Services
ChatbotUI
wwwroot

Future Enhancements:
- Add NLP/AI for better responses
- Add authentication
- Cloud ERP integration
- Admin dashboard


