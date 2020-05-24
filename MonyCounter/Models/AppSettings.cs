using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonyCounter.Models
{
    public static class AppSettings
    {
        public static string Url { get; set; } = "https://monycounter.azurewebsites.net:443/{0}";

        public static string Name { get; set; } = "BotMoneyCosts_bot";

        public static string Key { get; set; } = "700703898:AAEMWiqHcyNs4g7tg6M0dj9avSKvulByC1E";
    }
}