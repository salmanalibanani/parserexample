using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Parser.Test
{
    [TestClass]
    public class RegexHelperTests
    {
        [TestMethod]
        public void TestIPAddressRegex1()
        {
            var ipRegexHelper = new IPRegexHelper();
            string output;

            bool returnValue = ipRegexHelper.Find("this is a line without ip address", out output);
            Assert.AreEqual(false, returnValue);
            Assert.AreEqual(string.Empty, output);
        }

        [TestMethod]
        public void TestIPAddressRegex2()
        {
            var ipRegexHelper = new IPRegexHelper();
            string output;

            bool returnValue = ipRegexHelper.Find("this is a line with ip address 192.1.1.1 lets see if it finds it", out output);
            Assert.AreEqual(true, returnValue);
            Assert.AreEqual("192.1.1.1", output);
        }

        [TestMethod]
        public void TestURLAddressRegex1()
        {
            var urlRegexHelper = new URLRegexHelper();
            string output;

            bool returnValue = urlRegexHelper.Find("this is a line without URL", out output);
            Assert.AreEqual(false, returnValue);
            Assert.AreEqual(string.Empty, output);
        }

        [TestMethod]
        public void TestURLAddressRegex2()
        {
            var urlRegexHelper = new URLRegexHelper();
            string output;

            bool returnValue = urlRegexHelper.Find(@"\""GET /intranet-analytics/ HTTP", out output);
            Assert.AreEqual(true, returnValue);
            Assert.AreEqual(@" /intranet-analytics/ ", output);
        }
    }
}
