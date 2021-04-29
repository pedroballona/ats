using System.Collections.Generic;

namespace HR.ATS.CrossCutting.Localization
{
    public static class LocalizationConstants
    {
        public const string LocalizationSourceName = "I18n";

        public static readonly IList<Language> SupportedLanguages = new List<Language>
        {
            new()
            {
                Acronym = "pt-BR",
                Name = "Portuguese"
            },
            new()
            {
                Acronym = "en-US",
                Name = "English",
                IsDefault = true
            },
            new()
            {
                Acronym = "es-ES",
                Name = "Spanish"
            }
        }.AsReadOnly();

        public class Language
        {
            public string Acronym { get; set; }

            public string Name { get; set; }

            public bool IsDefault { get; set; }
        }
    }
}