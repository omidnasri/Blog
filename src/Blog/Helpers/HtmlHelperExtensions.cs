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
    }
}