using CookieBooking.Infrastructure.Contracts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CookieBooking.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ILogger _logger;

        public NotificationService(ILogger logger)
        {
            _logger = logger;

        }
        public async  Task<bool> NotifyAsync(string to, string title, string body)
        {
            var serverKey = "";
            var senderId = "";

            var data = new
            {
                to, // Recipient device token
                notification = new { title, body }
            };
            var jsonBody = JsonConvert.SerializeObject(data);

            using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send"))
            {
                httpRequest.Headers.TryAddWithoutValidation("Authorization", serverKey);
                httpRequest.Headers.TryAddWithoutValidation("Sender", senderId);
                httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    var result = await httpClient.SendAsync(httpRequest);

                    if (result.IsSuccessStatusCode)
                    {
                        return  true;
                    }
                    else
                    {
                        
                        // Use result.StatusCode to handle failure
                        // Your custom error handler here
                        _logger.LogError($"Error sending notification. Status Code: {result.StatusCode}");
                        return false;
                    }
                }
            }
        }
    }
}
