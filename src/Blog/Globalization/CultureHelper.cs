using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace Blog.Globalization
{
    /// <summary>
    /// Helper class for working with cultures
    /// </summary>
    public static class CultureHelper
    {
        private static Dictionary<string, Func<CultureInfo>> _cultureDic = new Dictionary<string, Func<CultureInfo>>
        {
            { "fa-IR", () => new CultureInfo("fa-IR") },
            { "en-US", () => new CultureInfo("en-US")}
        };

        /// <summary> 
        /// Sets the thread's current culture and UI culture
        /// </summary>
        /// <param name="culture"></param>
        private static void SetCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        /// <summary>
        /// Sets current culture to specified language, if specified language is not supported culture won't be changed
        /// </summary>
        /// <param name="lang">Language to set</param>
        /// <param name="resposne"></param>
        public static void ChangeCulture(string language, HttpResponseBase response)
        {
            if (!string.IsNullOrEmpty(language) && _cultureDic.ContainsKey(language))
            {
                response.SetCookie(new HttpCookie("_culture", language));
                SetCulture(_cultureDic[language]());
            }
        }

        /// <summary>
        /// Sets current culture to specified language in cookie, if no language is specified en is chosen
        /// </summary>
        /// <param name="request"></param>
        public static void SetCultureUsingCookie(HttpRequestBase request)
        {
            var culture = new CultureInfo("en-US");
            var cultureCookie = request.Cookies["_culture"];

            if (cultureCookie != null && _cultureDic.ContainsKey(cultureCookie.Value))
            {
                culture = _cultureDic[cultureCookie.Value]();
            }

            SetCulture(culture);
        }

        /// <summary>
        /// Gets a boolean value indicating if the current thread's culture's language is right to left 
        /// </summary>
        /// <returns></returns>
        public static bool IsCurrentCultureRightToLeft()
        {
            return Thread.CurrentThread.CurrentCulture.TextInfo.IsRightToLeft;
        }

        /// <summary>
        /// Gets the correct bundles name using passed isRtl parameter
        /// </summary>
        /// <param name="bundleName"></param>
        /// <param name="isRtl"></param>
        /// <returns></returns>
        public static string GetCultureSensitiveBundleName(string bundleName, bool isRtl)
        {
            return isRtl ? string.Format("{0}-rtl", bundleName) : bundleName;
        }

        /// <summary>
        /// Gets the correct bundles name using current culture's language direction
        /// </summary>
        /// <param name="bundleName"></param>
        /// <param name="isRtl"></param>
        /// <returns></returns>
        public static string GetCultureSensitiveBundleName(string bundleName)
        {
            return GetCultureSensitiveBundleName(bundleName, IsCurrentCultureRightToLeft());
        }
    }
}