using System.ComponentModel.DataAnnotations;

namespace CompanyWebsite.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}