using System.Threading.Tasks;
using CompanyWebsite.Models.TokenAuth;
using CompanyWebsite.Web.Controllers;
using Shouldly;
using Xunit;

namespace CompanyWebsite.Web.Tests.Controllers
{
    public class HomeController_Tests: CompanyWebsiteWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}