using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;
using Xunit;
using Xunit.Extensions;
using Blog.Helpers;
using Moq;

namespace BlogTests.Helpers
{
    public class HtmlHelperExtensionsTests
    {
        [Theory]
        [InlineData("Home", "Index", "Home", "Index", "active")]
        [InlineData("Home", "About", "Home", "Index", "")]
        [InlineData("Account", "Index", "Home", "Index", "")]
        [InlineData("Account", "Index", "Account", "Index", "active")]
        public void GetClassForNavbarItem(string controller, string action, string currentController, string currentAction, string expectedClass)
        {
            Assert.Equal(expectedClass, GetHtmlHelper(currentController, currentAction).GetClassForNavbarItem(controller, action));
        }

        [Theory]
        [InlineData("fa-IR", "dir='rtl'")]
        [InlineData("en-US", "dir='ltr'")]
        public void DirectionAttribute_ReturnsCorrectDirAttribute(string culture, string expected)
        {
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            Assert.Equal(expected, GetHtmlHelper("", "").DirectionAttribute().ToString());
        }

        [Theory]
        [InlineData("fa-IR", "dir='ltr'")]
        [InlineData("en-US", "dir='rtl'")]
        public void DirectionAttributeReverse_ReturnsCorrectDirAttribute(string culture, string expected)
        {
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            Assert.Equal(expected, GetHtmlHelper("", "").DirectionAttributeReverse().ToString());
        }

        private HtmlHelper GetHtmlHelper(string controller, string action)
        {
            var routeData = new RouteData();
            routeData.Values.Add("controller", controller);
            routeData.Values.Add("action", action);
            var viewContextMock = new Mock<ViewContext>();
            viewContextMock.Setup(x => x.RouteData).Returns(routeData);
            return new HtmlHelper(viewContextMock.Object, new Mock<IViewDataContainer>().Object);
        }
    }
}
