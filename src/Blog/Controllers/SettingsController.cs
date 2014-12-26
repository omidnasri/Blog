using Blog.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class SettingsController : Controller
    {
        public RedirectResult ChangeLanguage(string language, string returnUrl)
        {
            CultureHelper.ChangeCulture(language, Response);
            return new RedirectResult(returnUrl);
        }
    }
}
