using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Settings
{
    public class WhatsAppSettings
    {
        public string PhoneNumberId { get; set; } = null!;
        public string AccessToken { get; set; } = null!;
        public string TemplateName { get; set; } = null!;
        public string LanguageCode { get; set; } = null!;
    }
}
