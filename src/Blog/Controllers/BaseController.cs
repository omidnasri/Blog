using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class BaseController : Controller
    {
        private Dictionary<string, Func<CultureInfo>> _cultureDic;

        public BaseController()
        {
            _cultureDic = new Dictionary<string, Func<CultureInfo>>
            {
                { "fa", () => new CultureInfo("fa") },
            };
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            SetCulture();
            return base.BeginExecuteCore(callback, state);
        }

        /// <summary>
        /// Sets the thread culture using request cookies, defaults to en-us
        /// </summary>
        public void SetCulture()
        {
            var cultureCookie = Request.Cookies["_culture"];
            var culture = new CultureInfo("en-us");

            if (cultureCookie != null && _cultureDic.ContainsKey(cultureCookie.Value))
            {
                culture = _cultureDic[cultureCookie.Value]();
            }

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}
