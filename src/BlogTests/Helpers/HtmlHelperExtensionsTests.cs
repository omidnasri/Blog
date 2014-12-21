using Moq;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Xunit;
using Blog.Helpers;
using Xunit.Extensions;

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
            Assert.Equal(expectedClass, GetHtmlHelperMock(currentController, currentAction).GetClassForNavbarItem(controller, action));
        }

        private HtmlHelper GetHtmlHelperMock(string controller, string action)
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
