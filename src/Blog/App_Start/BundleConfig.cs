using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Blog
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Bootstrap").Include("~/Scripts/bootstrap.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Bootstrap-rtl").Include("~/Scripts/bootstrap-rtl.js"));
            bundles.Add(new ScriptBundle("~/Scripts/JQuery").Include("~/Scripts/jquery-{version}.js"));
            bundles.Add(new StyleBundle("~/Styles/Bootstrap").Include("~/Content/bootstrap.css"));
            bundles.Add(new StyleBundle("~/Styles/Bootstrap-rtl").Include("~/Content/css/bootstrap-rtl.css"));
        }
    }
}