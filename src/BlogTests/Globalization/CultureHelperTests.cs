using Blog.Globalization;
using System.Globalization;
using System.Threading;
using Xunit;
using Xunit.Extensions;
namespace BlogTests.Globalization
{
    public class CultureHelperTests
    {
        [Fact]
        public void IsCurrentCultureRightToLeft_CultureIsLeftToRight_ReturnsFalse()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
            Assert.Equal(false, CultureHelper.IsCurrentCultureRightToLeft());
        }

        [Fact]
        public void IsCurrentCultureRightToLeft_CultureIsRightToLeft_ReturnsTrue()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fa");
            Assert.Equal(true, CultureHelper.IsCurrentCultureRightToLeft());
        }

        [Theory]
        [InlineData("~/Scripts/bootstrap", "~/Scripts/bootstrap-rtl", true)]
        [InlineData("~/Scripts/bootstrap", "~/Scripts/bootstrap", false)]
        public void GetCultureSensitiveBundleName_IsRtlIsPassed(string bundleName, string expectedBundleName, bool isRtl)
        {
            Assert.Equal(expectedBundleName, CultureHelper.GetCultureSensitiveBundleName(bundleName, isRtl));
        }

        [Theory]
        [InlineData("~/Scripts/bootstrap", "~/Scripts/bootstrap-rtl", "fa")]
        [InlineData("~/Scripts/bootstrap", "~/Scripts/bootstrap", "en")]
        public void GetCultureSensitiveBundleName_UsingCurrentCulture(string bundleName, string expectedBundleName, string cultureName)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            Assert.Equal(expectedBundleName, CultureHelper.GetCultureSensitiveBundleName(bundleName));
        }
    }
}
