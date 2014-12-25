using Blog.Globalization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class BaseController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            CultureHelper.SetCultureUsingCookie(Request);
            return base.BeginExecuteCore(callback, state);
        }

    }
}
