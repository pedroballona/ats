using System.Diagnostics.CodeAnalysis;
using HR.ATS.CrossCutting.Localization;
using Tnf.Localization;
using Tnf.Localization.Dictionaries;

// ReSharper disable once CheckNamespace
namespace Tnf.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class TnfConfigurationExtensions
    {
        public static void ConfigureLocalization(this ITnfConfiguration configuration)
        {
            configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    LocalizationConstants.LocalizationSourceName,
                    new JsonEmbeddedFileLocalizationDictionaryProvider(
                        typeof(LocalizationConstants).Assembly,
                        "HR.ATS.CrossCutting"
                    )
                )
            );

            foreach (var language in LocalizationConstants.SupportedLanguages)
                configuration.Localization.Languages.Add(
                    new LanguageInfo(language.Acronym, language.Name, isDefault: language.IsDefault)
                );
        }
    }
}