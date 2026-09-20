using Application.Interfaces;
using Application.ResultPattern;
using Infrastructure.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.ExternalServices
{
    public class WhatsAppService : IWhatsAppService
    {
        private readonly HttpClient _httpClient;
        private readonly WhatsAppSettings _settings;
        private readonly ILogger<WhatsAppService> _logger;
        public WhatsAppService(
           HttpClient httpClient,
           IOptions<WhatsAppSettings> settings,
           ILogger<WhatsAppService> logger)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            _logger = logger;
        }
        public async Task<Result> SendAppointmentConfirmationAsync(
            string patientPhone,
            string patientName,
            CancellationToken cancellationToken)
        {
            var formattedPhone = FormatPhoneNumber(patientPhone);

            var url = $"https://graph.facebook.com/v21.0/{_settings.PhoneNumberId}/messages";

            var payload = new
            {
                messaging_product = "whatsapp",
                to = formattedPhone,
                type = "template",
                template = new
                {
                    name = _settings.TemplateName,
                    language = new { code = _settings.LanguageCode },
                    components = new object[]
                    {
                        new
                        {
                            type = "body",
                            parameters = new object[]
                            {
                                new { type = "text", text = patientName },
                                new { type = "text", text = "TEST-001" },
                                new { type = "text", text = "100" }
                            }
                        }
                    }
                }
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content
            };
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", _settings.AccessToken);

            try
            {
                var response = await _httpClient.SendAsync(request, cancellationToken);
                var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning(
                        "WhatsApp message failed for {Phone}. Status: {Status}. Body: {Body}",
                        formattedPhone,
                        response.StatusCode,
                        responseBody);

                    return Result.Failure(
                        $"WhatsApp Error: {responseBody}");
                }

                return Result.Success(
                    "WhatsApp confirmation message sent successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending WhatsApp message to {Phone}", formattedPhone);

                return Result.Failure(
                    "An error occurred while sending WhatsApp confirmation message.");
            }
        }

        private static string FormatPhoneNumber(string phone)
        {
            phone = phone.Trim().Replace(" ", "").Replace("-", "");

            if (phone.StartsWith("0"))
            {
                phone = "2" + phone;
            }

            if (phone.StartsWith("+"))
            {
                phone = phone.Substring(1);
            }

            return phone;
        }
    }
}
