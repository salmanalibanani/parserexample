using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using static Parser.LogParser;

namespace Parser.Test
{
    [TestClass]
    public class LogParserTests
    {

        [TestMethod]
        public void TestReturnsRightNumberOfCollections1()
        {
            var parser = BuildLogParser(MockGetLines.MockWithLines);
            var lists = parser.GetLists();

            Assert.AreEqual(2, lists.Count);
        }

        [TestMethod]
        public void TestReturnsRightNumberOfCollections2()
        {
            var parser = BuildLogParserWithoutRegex(MockGetLines.MockWithLines);
            var lists = parser.GetLists();

            Assert.AreEqual(0, lists.Count);
        }

        [TestMethod]
        public void TestReturnsRightNumberOfCollections3()
        {
            var parser = BuildLogParser(MockGetLines.MockEmptyFile);
            var lists = parser.GetLists();

            Assert.AreEqual(2, lists.Count);
        }

        [TestMethod]
        public void TestUniqueIPAddresses()
        {
            var parser = BuildLogParserForIPOnly(MockGetLines.MockWithLines);
            var lists = parser.GetLists();

            Assert.AreEqual(3, lists[0].Count);
            Assert.AreEqual(5, lists[0].TotalUniqueAttempted);
            Assert.AreEqual("177.71.128.24", lists[0].GetAt(1).Value);
        }

        private LogParser BuildLogParserWithoutRegex(GetLines getLinesDelegate)
        {
            IList<RegexHelperBase> regexList = new List<RegexHelperBase>();
            return new LogParser("some_file_name", regexList, 3, getLinesDelegate);
        }

        private LogParser BuildLogParser(GetLines getLinesDelegate)
        {
            IList<RegexHelperBase> regexList = new List<RegexHelperBase>();
            regexList.Add(new IPRegexHelper());
            regexList.Add(new URLRegexHelper());

            return new LogParser("some_file_name", regexList, 3, getLinesDelegate);
        }

        private LogParser BuildLogParserForIPOnly(GetLines getLinesDelegate)
        {
            IList<RegexHelperBase> regexList = new List<RegexHelperBase>();
            regexList.Add(new IPRegexHelper());

            return new LogParser("some_file_name", regexList, 3, getLinesDelegate);
        }
    }

    public class MockGetLines
    {
        public static IEnumerable<string> MockWithLines(string fileName)
        {
            string[] items =
            {
                "177.71.128.21 \"GET /intranet-analytics/ HTTP/1.1\"",
                "177.71.128.21 \"GET /intranet-analytics/ HTTP/1.1\"",
                "177.71.128.21 \"GET /intranet-analytics/ HTTP/1.1\"",
                "177.71.128.22 \"GET /intranet-analytics/ HTTP/1.1\"",
                "177.71.128.23 \"GET /intranet-analytics/ HTTP/1.1\"",
                "177.71.128.24 \"GET /intranet-analytics/ HTTP/1.1\"",
                "177.71.128.24 \"GET /intranet-analytics/ HTTP/1.1\"",
                "177.71.128.25 \"GET /intranet-analytics/ HTTP/1.1\""
            };

            return items;
        }

        public static IEnumerable<string> MockEmptyFile(string fileName) => new string[] { };
    }
}
