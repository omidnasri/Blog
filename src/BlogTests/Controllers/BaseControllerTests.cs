using Blog.Controllers;
using Moq;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Xunit;
using Xunit.Extensions;

namespace BlogTests.Controllers
{
    public class BaseControllerTests
    {
        private BaseController GetBaseController(HttpCookieCollection cookies)
        {
            var baseController = new BaseController();
            var requestMock = new Mock<HttpRequestBase>();
            requestMock.Setup(x => x.Cookies).Returns(cookies);
            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.Setup(x => x.Request).Returns(requestMock.Object);
            baseController.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), baseController);

            return baseController;
        }

        [Theory]
        [InlineData("fa")]
        public void SetCulture_CultureCookieIsPresent_ChoosesCorrectCulture(string langName)
        {
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("_culture", langName));

            GetBaseController(cookies).SetCulture();

            Assert.Equal(langName, Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
            Assert.Equal(langName, Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName);
        }

        [Fact]
        public void SetCulture_CultureCookieIsNotPresent_ChoosesEnUs()
        {
            GetBaseController(new HttpCookieCollection()).SetCulture();

            Assert.Equal(new CultureInfo("en-us"), Thread.CurrentThread.CurrentCulture);
            Assert.Equal(new CultureInfo("en-us"), Thread.CurrentThread.CurrentUICulture);
        }
    }
}
