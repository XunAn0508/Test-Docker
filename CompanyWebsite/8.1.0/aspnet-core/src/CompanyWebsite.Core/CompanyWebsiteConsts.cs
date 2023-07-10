using CompanyWebsite.Debugging;

namespace CompanyWebsite
{
    public class CompanyWebsiteConsts
    {
        public const string LocalizationSourceName = "CompanyWebsite";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "3b0f19e948d54d6087d7add25dea59dd";
    }
}
