using Blog.Globalization;
using Moq;
using System.Globalization;
using System.Threading;
using System.Web;
using Xunit;
using Xunit.Extensions;
namespace BlogTests.Globalization
{
    public class CultureHelperTests
    {
        [Fact]
        public void IsCurrentCultureRightToLeft_CultureIsLeftToRight_ReturnsFalse()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
            Assert.Equal(false, CultureHelper.IsCurrentCultureRightToLeft());
        }

        [Fact]
        public void IsCurrentCultureRightToLeft_CultureIsRightToLeft_ReturnsTrue()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fa");
            Assert.Equal(true, CultureHelper.IsCurrentCultureRightToLeft());
        }

        [Theory]
        [InlineData("~/Scripts/bootstrap", "~/Scripts/bootstrap-rtl", true)]
        [InlineData("~/Scripts/bootstrap", "~/Scripts/bootstrap", false)]
        public void GetCultureSensitiveBundleName_IsRtlIsPassed(string bundleName, string expectedBundleName, bool isRtl)
        {
            Assert.Equal(expectedBundleName, CultureHelper.GetCultureSensitiveBundleName(bundleName, isRtl));
        }

        [Theory]
        [InlineData("~/Scripts/bootstrap", "~/Scripts/bootstrap-rtl", "fa")]
        [InlineData("~/Scripts/bootstrap", "~/Scripts/bootstrap", "en")]
        public void GetCultureSensitiveBundleName_UsingCurrentCulture(string bundleName, string expectedBundleName, string cultureName)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            Assert.Equal(expectedBundleName, CultureHelper.GetCultureSensitiveBundleName(bundleName));
        }

        [Theory]
        [InlineData("fa-IR")]
        [InlineData("en-US")]
        public void ChangeCulture_PassedLanguageIsValid_CultureIsChanged(string cultureName)
        {
            var response = new Mock<HttpResponseBase>();

            CultureHelper.ChangeCulture(cultureName, response.Object);

            response.Verify(resp => resp.SetCookie(It.Is<HttpCookie>(cookie => cookie.Name == "_culture" && cookie.Value == cultureName)), Times.Once());
            Assert.Equal(cultureName, Thread.CurrentThread.CurrentCulture.Name);
            Assert.Equal(cultureName, Thread.CurrentThread.CurrentUICulture.Name);
        }

        [Theory]
        [InlineData("fr-FR")]
        [InlineData("en-GB")]
        public void ChangeCulture_PassedLanguageIsNotValid_CultureIsNotChanged(string cultureName)
        {
            var response = new Mock<HttpResponseBase>();

            CultureHelper.ChangeCulture(cultureName, response.Object);

            response.Verify(resp => resp.SetCookie(It.Is<HttpCookie>(cookie => cookie.Name == "_culture" && cookie.Value == cultureName)), Times.Never());
        }

        [Theory]
        [InlineData("en-US")]
        [InlineData("fa-IR")]
        public void SetCultureUsingCookie_CookieIsPresentAndValueIsValid_CultureIsSet(string cultureName)
        {
            var request = new Mock<HttpRequestBase>();
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("_culture", cultureName));
            request.Setup(x => x.Cookies).Returns(cookies);

            CultureHelper.SetCultureUsingCookie(request.Object);

            Assert.Equal(cultureName, Thread.CurrentThread.CurrentCulture.Name);
            Assert.Equal(cultureName, Thread.CurrentThread.CurrentUICulture.Name);
        }

        [Fact]
        public void SetCultureUsingCookie_CookieIsNotPresent_CultureIsSetToEnUs()
        {
            var request = new Mock<HttpRequestBase>();
            request.Setup(x => x.Cookies).Returns(new HttpCookieCollection());

            CultureHelper.SetCultureUsingCookie(request.Object);

            Assert.Equal("en-US", Thread.CurrentThread.CurrentCulture.Name);
            Assert.Equal("en-US", Thread.CurrentThread.CurrentUICulture.Name);
        }

        [Fact]
        public void SetCultureUsingCookie_CookieIsInvalid_CultureIsSetToEnUs()
        {
            var request = new Mock<HttpRequestBase>();
            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("_culture", "fr-FR"));
            request.Setup(x => x.Cookies).Returns(cookies);

            CultureHelper.SetCultureUsingCookie(request.Object);

            Assert.Equal("en-US", Thread.CurrentThread.CurrentCulture.Name);
            Assert.Equal("en-US", Thread.CurrentThread.CurrentUICulture.Name);
        }
    }
}
