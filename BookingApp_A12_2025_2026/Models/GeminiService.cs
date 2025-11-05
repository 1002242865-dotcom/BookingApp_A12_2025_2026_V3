namespace BookingApp_A12_2025_2026.Models
{
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class GeminiService
    {
        // ضع الـ API Key هنا مباشرة لأغراض تجريبية فقط
        private const string ApiKey = "AIzaSyASj8_R41ksL-WrS-x7nqZVarQ5m5c85tU";

        public async Task<string> AskAsync(string prompt)
        {
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={ApiKey}";

            var requestData = new
            {
                contents = new[]
                {
                new
                {
                    parts = new[]
                    {
                        new { text = prompt }
                    }
                }
            }
            };

            var json = JsonSerializer.Serialize(requestData);
            using var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return "خطأ أثناء الاتصال بـ Gemini API";

            using var doc = JsonDocument.Parse(responseContent);
            var root = doc.RootElement;

            return root
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString() ?? "لا يوجد رد.";
        }
    }


}
