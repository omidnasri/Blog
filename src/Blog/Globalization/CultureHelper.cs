using System;
using System.Collections.Generic;
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