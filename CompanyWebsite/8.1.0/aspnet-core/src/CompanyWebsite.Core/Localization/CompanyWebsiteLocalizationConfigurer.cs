using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace CompanyWebsite.Localization
{
    public static class CompanyWebsiteLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(CompanyWebsiteConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(CompanyWebsiteLocalizationConfigurer).GetAssembly(),
                        "CompanyWebsite.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
