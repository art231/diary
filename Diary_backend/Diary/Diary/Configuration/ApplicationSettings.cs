using System;

namespace Diary.Configuration
{
    public class ApplicationSettings
    {
        public string[] PhoneNumbersWhitelist { get; set; } = Array.Empty<string>();

        public string Host { get; set; }
    }
}
