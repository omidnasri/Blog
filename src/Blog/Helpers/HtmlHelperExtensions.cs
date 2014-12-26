using Blog.Globalization;
using System.Web.Mvc;
namespace Blog.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static string GetClassForNavbarItem(this HtmlHelper helper, string controller, string action)
        {
            var classToReturn = string.Empty;

            if (controller == helper.ViewContext.RouteData.Values["controller"].ToString()
                && action == helper.ViewContext.RouteData.Values["action"].ToString())
            {
                classToReturn = "active";
            }

            return classToReturn;
        }

        /// <summary>
        /// Gets the appropriate dir attribute for current culture to use in html elements.
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static MvcHtmlString DirectionAttribute(this HtmlHelper helper)
        {
            return new MvcHtmlString(CultureHelper.IsCurrentCultureRightToLeft() ? "dir='rtl'" : "dir='ltr'");
        }

        /// <summary>
        /// Gets the reverse dir attribute for current culture to use in html elements.
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static MvcHtmlString DirectionAttributeReverse(this HtmlHelper helper)
        {
            return new MvcHtmlString(CultureHelper.IsCurrentCultureRightToLeft() ? "dir='ltr'" : "dir='rtl'");
        }
    }
}